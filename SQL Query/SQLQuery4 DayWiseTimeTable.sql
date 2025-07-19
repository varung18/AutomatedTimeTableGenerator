USE [AutoTimeTableDb]
GO
-- Create StoredProcedure [sp_PrintAllDaysTimeTablesOneByOne]
CREATE PROC [dbo].[sp_PrintAllDaysTimeTablesOneByOne]
AS
BEGIN
DECLARE @AllDaysTimeTable Table( [TimeTableID] int,
[DayTitle] nvarchar(300),
[LabRoomTitle] nvarchar(300),
[Slot1] nvarchar(300),
[Slot2] nvarchar(300),
[Slot3] nvarchar(300),
[Slot4] nvarchar(300),
[Slot5] nvarchar(300),
[Slot6] nvarchar(300),
[Slot7] nvarchar(300),
[Slot8] nvarchar(300),
[Slot9] nvarchar(300)
)

	--Getting All Rooms Slots    
	DECLARE @AllRoomsSlots Table(
	TimeTableID INT, 
	ProgramSemesterSubjectID INT,	
	SubjectTitle nvarchar(500), 
	RoomID INT,
	RoomNo nvarchar(400), 
	DayTimeSlotID INT,
	SlotTitle nvarchar(400), 
	DayTitle nvarchar(200),
	LectureID int, 
	DayID int )

	INSERT INTO @AllRoomsSlots(
	TimeTableID, 
	ProgramSemesterSubjectID,	
	SubjectTitle, 
	RoomID,
	RoomNo, 
	DayTimeSlotID,
	SlotTitle, 
	DayTitle,
	LectureID, 
	DayID) 
	SELECT * FROM (SELECT 
	TD.TimeTableID, 
	TD.ProgramSemesterSubjectID,	
	TD.SubjectTitle, 
	TD.RoomID,
	RT.RoomNo, 
	TD.DayTimeSlotID,
	ATS.SlotTitle, 
	ATS.Name,
	TD.LectureID, 
	TD.DayID 
	 FROM TimeTableDetailsTable TD
	INNER JOIN v_AllActiveTimeSlots ATS
	ON TD.DayTimeSlotID = ATS.DayTimeSlotID
	LEFT JOIN RoomTable RT
	ON TD.RoomID = RT.RoomID
	WHERE TD.DayID = 1 AND TD.RoomID > 0) ALLSlots

	--Getting All Labs Slots
    DECLARE @AllLabsSlots Table(
	TimeTableID INT, 
	ProgramSemesterSubjectID INT,	
	SubjectTitle nvarchar(500), 
	LabID INT,
	LabNo nvarchar(400), 
	DayTimeSlotID INT,
	SlotTitle nvarchar(400), 
	DayTitle nvarchar(200),
	LectureID int, 
	DayID int )

	INSERT INTO @AllLabsSlots(
	TimeTableID, 
	ProgramSemesterSubjectID,	
	SubjectTitle, 
	LabID,
	LabNo,  
	DayTimeSlotID,
	SlotTitle, 
	DayTitle,
	LectureID, 
	DayID) 
    SELECT * FROM (SELECT 
	TD.TimeTableID, 
	TD.ProgramSemesterSubjectID,	
	TD.SubjectTitle, 
	TD.LabID, 
	LT.LabNo,
	TD.DayTimeSlotID,
	ATS.SlotTitle, 
	ATS.Name,
	TD.LectureID, 
	TD.DayID 
	 FROM TimeTableDetailsTable TD
	INNER JOIN v_AllActiveTimeSlots ATS
	ON TD.DayTimeSlotID = ATS.DayTimeSlotID
	INNER JOIN LabTable LT
	ON TD.LabID = LT.LabID
	WHERE TD.DayID = 1 AND TD.LabID > 0) ALLSlots
    
	DECLARE @CountTotalDays int  = (SELECT COUNT(*) FROM DayTable Where IsActive = 1);
	DECLARE @AllDays Table (RowNo int, DayID int, DayTitle nvarchar(150))
	INSERT INTO @AllDays(RowNo, DayID, DayTitle) Select ROW_NUMBER() over (Order By(Select 1)), DayID, Name From DayTable where IsActive = 1
	
	DECLARE @GETTimeTableOneByOne int = 1;
	WHILE @GETTimeTableOneByOne <= @CountTotalDays
	BEGIN
		DECLARE @DayTimeTableTitle NVARCHAR(200) = (SELECT TOP 1 DayTitle FROM @AllDays WHERE RowNo = @GETTimeTableOneByOne)
		DECLARE @DayTimeTable Table( 
		[TimeTableID] int,
		[DayTitle] nvarchar(300),
		[LabRoomTitle] nvarchar(300),
		[Slot1] nvarchar(300),
		[Slot2] nvarchar(300),
		[Slot3] nvarchar(300),
		[Slot4] nvarchar(300),
		[Slot5] nvarchar(300),
		[Slot6] nvarchar(300),
	[Slot7] nvarchar(300),
	[Slot8] nvarchar(300),
	[Slot9] nvarchar(300)
		)
    
		DECLARE @DayID int = (Select Top 1 DayID FROM @AllDays WHERE RowNo = @GETTimeTableOneByOne)
		DECLARE @DayTitle nvarchar(100) = (Select Top 1 DayTitle FROM @AllDays WHERE RowNo = @GETTimeTableOneByOne)
		
		--Clear Table
		DELETE FROM @DayTimeTable;

		--Getting Room Time Slot
		DECLARE @RoomTimeSlotTimeTable Table(RowNo int, SlotTitle nvarchar(200))
		--Clear Table
		DELETE FROM @RoomTimeSlotTimeTable;

		INSERT INTO @RoomTimeSlotTimeTable(RowNo,SlotTitle)  SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)),RoomNo FROM (Select RoomID, RoomNo from @AllRoomsSlots Group By RoomID, RoomNo) AR
		
		--Getting Lab Time Slot
		DECLARE @LabTimeSlotTimeTable Table(RowNo int, SlotTitle nvarchar(200))
		--Clear Table
		DELETE FROM @LabTimeSlotTimeTable;

		INSERT INTO @LabTimeSlotTimeTable(RowNo,SlotTitle)  SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)),LabNo FROM (Select LabNo from @AllLabsSlots Group By  LabNo) LR
				
		DECLARE @CountTotalRooms INT = (SELECT COUNT(*) FROM @RoomTimeSlotTimeTable);

		DECLARE @CreateRoomSlotOneByOne INT = 1;
		WHILE @CreateRoomSlotOneByOne <= @CountTotalRooms
		BEGIN
			DECLARE @TIMETITLE NVARCHAR(200) = (SELECT TOP 1 SlotTitle FROM @RoomTimeSlotTimeTable WHERE RowNo = @CreateRoomSlotOneByOne);
			INSERT INTO @DayTimeTable([TimeTableID],[DayTitle],[LabRoomTitle],[Slot1],[Slot2],[Slot3],[Slot4],[Slot5],[Slot6],[Slot7],[Slot8], [Slot9])
			VALUES(0,@DayTitle,@TIMETITLE,'Break','Break','Break','Break','Break','Break','Break','Break','Break')
			SET @CreateRoomSlotOneByOne = @CreateRoomSlotOneByOne + 1;
		END

		DECLARE @CountTotalLabs INT = (SELECT COUNT(*) FROM @LabTimeSlotTimeTable);
		DECLARE @CreateLabSlotOneByOne INT = 1;
		WHILE @CreateLabSlotOneByOne <= @CountTotalLabs
		BEGIN
			DECLARE @TIMETITLETITLE NVARCHAR(200) = (SELECT TOP 1 SlotTitle FROM @LabTimeSlotTimeTable WHERE RowNo = @CreateLabSlotOneByOne);
			INSERT INTO @DayTimeTable([TimeTableID],[DayTitle],[LabRoomTitle],[Slot1],[Slot2],[Slot3],[Slot4],[Slot5],[Slot6],[Slot7],[Slot8], [Slot9])
			VALUES(0,@DayTitle,@TIMETITLETITLE,'Break','Break','Break','Break','Break','Break','Break','Break','Break')
			SET @CreateLabSlotOneByOne = @CreateLabSlotOneByOne + 1;
		END
		
		-- Print Time Table Slots
		-- SELECT * FROM @DayTimeTable	
		 DECLARE @DayTimeTableDetails Table(
				RowNo int,
				TimeTableID int,
				ProgramSemesterSubjectID int, 
				SubjectTitle nvarchar(400), 
				RoomID int, 
				LabID int, 
				DayTimeSlotID int, 
				SlotTitle nvarchar(200), 
				DayTitle nvarchar(80), 
				LectureID int, 
				DayID int, 
				IsActive bit);

         --Clear Table
		 DELETE FROM @DayTimeTableDetails;
		 -- Getting Semester Wise Subject
		 INSERT INTO @DayTimeTableDetails(
		 RowNo, 
		 TimeTableID,
		 ProgramSemesterSubjectID, 
		 SubjectTitle, 
		 RoomID, 
		 LabID, 
		 DayTimeSlotID, 
		 SlotTitle,
		 DayTitle,
		 LectureID, 
		 DayID, 
		 IsActive)
		    SELECT 
		    ROW_NUMBER() over(order by(Select 1)),
		    TTD.TimeTableID,
		    TTD.ProgramSemesterSubjectID, 
		    TTD.SubjectTitle, 
		    TTD.RoomID, 
		    TTD.LabID, 
		    TTD.DayTimeSlotID,
		    TTD.SlotTitle, 
		    TTD.Name,
		    TTD.LectureID, 
		    TTD.DayID, 
		    TTD.IsActive
		    FROM 
		    (SELECT 
		    TD.TimeTableID,
		    TD.ProgramSemesterSubjectID, 
		    TD.SubjectTitle, 
		    TD.RoomID, 
		    TD.LabID, 
		    TD.DayTimeSlotID,
		    ATS.SlotTitle, 
		    ATS.Name,
		    TD.LectureID, 
		    TD.DayID, 
		    TD.IsActive
		     FROM TimeTableDetailsTable TD
		    INNER JOIN v_AllActiveTimeSlots ATS
		    ON TD.DayTimeSlotID = ATS.DayTimeSlotID) TTD 
		    WHERE TTD.DayID = @DayID Order By DayTimeSlotID
		-- Print SELECT Semester Class 
		DECLARE @TimeTableID int = @GETTimeTableOneByOne;
		UPDATE @DayTimeTable SET TimeTableID = @TimeTableID;

		DECLARE @LocationTitleTimeTable NVARCHAR(200);
		DECLARE @SemsterTitleTimeTable NVARCHAR(200);
		DECLARE @SubjectTitleTimeTable NVARCHAR(200);
		DECLARE @CountTimeSlotTimeTable int = (SELECT Count(*) FROM @DayTimeTableDetails);
		DECLARE @AddOnebyOne int  = 1;
		WHILE @AddOnebyOne <= @CountTimeSlotTimeTable
		BEGIN
		    DECLARE @GETProgramSemesterSubjectID int  = (SELECT Top 1 ProgramSemesterSubjectID FROM @DayTimeTableDetails WHERE RowNo = @AddOnebyOne AND IsActive = 1);
		    IF @GETProgramSemesterSubjectID > 0
			BEGIN

			SET @SubjectTitleTimeTable = (SELECT TOP 1 SubjectTitle FROM @DayTimeTableDetails WHERE  RowNo = @AddOnebyOne AND IsActive = 1);
		    DECLARE @GETRoomID int  = (SELECT Top 1 RoomID FROM @DayTimeTableDetails WHERE RowNo = @AddOnebyOne AND IsActive = 1);
		    DECLARE @GETLabID int  = (SELECT Top 1 LabID FROM @DayTimeTableDetails WHERE RowNo = @AddOnebyOne AND IsActive = 1);
		    
			DECLARE @GETRoomNo Nvarchar(100)  = (SELECT Top 1 RoomNo FROM RoomTable WHERE RoomID = @GETRoomID);
		    DECLARE @GETLabNo Nvarchar(100)  = (SELECT Top 1 LabNo FROM LabTable WHERE LabID = @GETLabID);
		    DECLARE @LRTitle NVARCHAR(300);
				IF @GETRoomID > 0
				BEGIN
					SET @LRTitle = @GETRoomNo;
				END
				ELSE
					SET @LRTitle = @GETLabNo;
				END
		    DECLARE @GETDayTimeSlotID int  = (SELECT Top 1 DayTimeSlotID FROM @DayTimeTableDetails WHERE RowNo = @AddOnebyOne AND IsActive = 1);
		    
			DECLARE @GETLectureID int  = (SELECT Top 1 LectureID FROM @DayTimeTableDetails WHERE RowNo = @AddOnebyOne AND IsActive = 1);
		    DECLARE @GETTimeSlotName nvarchar(200)  = (SELECT Top 1 SlotTitle FROM @DayTimeTableDetails WHERE RowNo = @AddOnebyOne AND IsActive = 1);
		    DECLARE @GETDayTitle nvarchar(100)  = (SELECT Top 1 DayTitle FROM @DayTimeTableDetails WHERE RowNo = @AddOnebyOne AND IsActive = 1);
			IF @GETRoomID > 0
			BEGIN
				SET @LocationTitleTimeTable = (Select TOP 1 RoomNo From RoomTable WHERE RoomID = @GETRoomID)
			END
			IF @GETLabID > 0
			BEGIN
				SET @LocationTitleTimeTable = (Select TOP 1 LabNo From LabTable WHERE LabID = @GETLabID)
			END
			
			SET @SubjectTitleTimeTable = @SubjectTitleTimeTable + '(' + @LocationTitleTimeTable + ')';

			DECLARE @Time_SlotTitle Nvarchar(100) = (SELECT TOP 1 SlotTitle FROM DayTimeSlotTable WHERE DayTimeSlotID = @GETDayTimeSlotID)
			SET @Time_SlotTitle = REPLACE(@Time_SlotTitle,' ','_')
			SET @Time_SlotTitle = REPLACE(@Time_SlotTitle,':','_')
			IF @Time_SlotTitle = '09_30-10_16'
			BEGIN
				UPDATE @DayTimeTable SET Slot1 =  @SubjectTitleTimeTable WHERE [LabRoomTitle]  = @LRTitle
			END
			ELSE IF @Time_SlotTitle = '10_16-11_02'
			BEGIN
				UPDATE @DayTimeTable SET Slot2 =  @SubjectTitleTimeTable WHERE [LabRoomTitle]  = @LRTitle
			END
			ELSE IF @Time_SlotTitle = '11_02-11_48'
			BEGIN
				UPDATE @DayTimeTable SET Slot3 =  @SubjectTitleTimeTable WHERE [LabRoomTitle]  = @LRTitle
			END
			ELSE IF @Time_SlotTitle = '11_48-12_34'
			BEGIN
				UPDATE @DayTimeTable SET Slot4 =  @SubjectTitleTimeTable WHERE [LabRoomTitle]  = @LRTitle
			END
			ELSE IF @Time_SlotTitle = '12_34-13_20'
			BEGIN
				UPDATE @DayTimeTable SET Slot5 =  @SubjectTitleTimeTable WHERE [LabRoomTitle]  = @LRTitle
			END
			ELSE IF @Time_SlotTitle = '13_20-14_06'
			BEGIN
				UPDATE @DayTimeTable SET Slot6 =  @SubjectTitleTimeTable WHERE [LabRoomTitle]  = @LRTitle
			END
			ELSE IF @Time_SlotTitle = '14_06-14_52'
			BEGIN
				UPDATE @DayTimeTable SET Slot7 =  @SubjectTitleTimeTable WHERE [LabRoomTitle]  = @LRTitle
			END
			ELSE IF @Time_SlotTitle = '14_52-15_38'
			BEGIN
				UPDATE @DayTimeTable SET Slot8 =  @SubjectTitleTimeTable WHERE [LabRoomTitle]  = @LRTitle
			END
			ELSE IF @Time_SlotTitle = '15_38-16_24'
			BEGIN
				UPDATE @DayTimeTable SET Slot9 =  @SubjectTitleTimeTable WHERE [LabRoomTitle]  = @LRTitle
			END

			
			 SET @AddOnebyOne = @AddOnebyOne + 1;
			END	
		SET @GETTimeTableOneByOne = @GETTimeTableOneByOne + 1;
	       
		INSERT INTO @AllDaysTimeTable([TimeTableID],[DayTitle],[LabRoomTitle],[Slot1],[Slot2],[Slot3],[Slot4],[Slot5],[Slot6],[Slot7],[Slot8], [Slot9])
		SELECT [TimeTableID],[DayTitle],[LabRoomTitle],[Slot1],[Slot2],[Slot3],[Slot4],[Slot5],[Slot6],[Slot7],[Slot8], [Slot9] FROM @DayTimeTable
	 END
	  SELECT * FROM @AllDaysTimeTable	
END
