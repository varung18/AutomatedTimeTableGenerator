USE [AutoTimeTableDb]

GO

-- Create StoredProcedure [sp_PrintTeacherWiseTimeTable]    

CREATE PROC [dbo].[sp_PrintTeacherWiseTimeTable]
AS
BEGIN
DECLARE @AllTeacherTimeTable Table(
[TEACHERID] INT,
[TEACHERNAME] nvarchar(300),
[TIME] nvarchar(300),
[MONDAY] nvarchar(300),
[TUESDAY] nvarchar(300),
[WEDNESDAY] nvarchar(300),
[THURSDAY] nvarchar(300),
[FRIDAY] nvarchar(300),
[SATURDAY] nvarchar(300),
[SUNDAY] nvarchar(300))

DECLARE @AllTeachers Table(RowNo INT, LectureID INT,LectureName NVARCHAR(200));

	DELETE FROM @AllTeachers;

	INSERT INTO @AllTeachers(RowNo,LectureID,LectureName)

	 Select ROW_NUMBER() OVER (ORDER BY(SELECT 1)) RowNo, TTD.LectureID, TTD.FullName
			  FROM
			 (SELECT TD.LectureID,FullName FROM											TimeTableDetailsTable TD INNER JOIN LectureTable LEC ON TD.LectureID = LEC.LectureID) TTD					WHERE TTD.LectureID > 0  GROUP BY TTD.LectureID, TTD.FullName

     DECLARE @CountTotalTeacher int  = (SELECT COUNT(*) FROM @AllTeachers);

		DECLARE @GETTimeTableOneByOne int = 1;
		WHILE @GETTimeTableOneByOne <= @CountTotalTeacher
		BEGIN
			DECLARE @TeacherTimeTableTitle NVARCHAR(200) = (SELECT TOP 1 LectureName FROM				@AllTeachers WHERE RowNo = @GETTimeTableOneByOne)
			DECLARE @TeacherTimeTable Table([TEACHERID] INT,
										
					[TEACHERNAME] nvarchar(300),
					[TIME] nvarchar(300),
					[MONDAY] nvarchar(300),
					[TUESDAY] nvarchar(300),
					[WEDNESDAY] nvarchar(300),
					[THURSDAY] nvarchar(300),
					[FRIDAY] nvarchar(300),
					[SATURDAY] nvarchar(300),
					[SUNDAY] nvarchar(300))

        --Clear Table
		DELETE FROM @TeacherTimeTable;
		DECLARE @TimeSlotTimeTable Table(RowNo int, SlotTitle nvarchar(200))

		--Clear Table
		DELETE FROM @TimeSlotTimeTable;

		INSERT INTO @TimeSlotTimeTable(RowNo,SlotTitle)  SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)),SlotTitle FROM (Select SlotTitle, StartTime ,EndTime from DayTimeSlotTable WHERE ISActive  = 1 Group By SlotTitle, StartTime ,EndTime) DTST ORDER BY StartTime

		DECLARE @COUNTTIMEROWSTIMETABLE INT = (SELECT COUNT(*) FROM @TimeSlotTimeTable);
		DECLARE @CREATESLOTSVARIABLE INT = 1;
		WHILE @CREATESLOTSVARIABLE <= @COUNTTIMEROWSTIMETABLE

		BEGIN
			DECLARE @TIMETITLE NVARCHAR(200) = (SELECT TOP 1 SlotTitle FROM @TimeSlotTimeTable			WHERE RowNo = @CREATESLOTSVARIABLE);
			INSERT INTO @TeacherTimeTable([TEACHERID],[TEACHERNAME],[TIME],[MONDAY],[TUESDAY],			[WEDNESDAY],[THURSDAY],[FRIDAY],[SATURDAY],[SUNDAY])
			VALUES(0,NULL,@TIMETITLE,'Break','Break','Break','Break','Break','Break','Break')
			SET @CREATESLOTSVARIABLE = @CREATESLOTSVARIABLE + 1;
		END

		-- Print Time Table Slots
		--SELECT * FROM @SemesterTimeTable
		DECLARE @TeacherTimeTableDetails Table(
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
		 LectureName NVARCHAR(200),
		 DayID int,
		 IsActive bit);

         --Clear Table
		 DELETE FROM @TeacherTimeTableDetails;

		 -- Getting Semester Wise Subject
		 INSERT INTO @TeacherTimeTableDetails (RowNo,
			TimeTableID,
			ProgramSemesterSubjectID, 
			SubjectTitle,
			RoomID,
			LabID,
			DayTimeSlotID,
			SlotTitle,
			DayTitle,
			LectureID,
			LectureName,
			DayID,
			IsActive)

			SELECT
			ROW_NUMBER() over(order by(Select 1)),
			TTD.TimeTableID, TTD.ProgramSemesterSubjectID, TTD.SubjectTitle,
			TTD.RoomID, TTD.LabID, TTD.DayTimeSlotID,
			TTD.SlotTitle,
			TTD.Name,
			TTD.LectureID,
			TTD.FullName,
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
			LEC.FullName, 
			TD.DayID, 
			TD.IsActive
			FROM TimeTableDetailsTable TD
			INNER JOIN v_AllActiveTimeSlots ATS

ON TD.DayTimeSlotID = ATS.DayTimeSlotID
INNER JOIN LectureTable LEC 

ON TD.LectureID = LEC.LectureID) TTD 
WHERE TTD.LectureID = @GETTimeTableOneByOne Order By DayTimeSlotID

			-- Print SELECT Semester Class 

			--SELECT * FROM @SemesterTimeTableDetails

			DECLARE @TeacherID INT = (SELECT TOP 1 LectureID FROM @TeacherTimeTableDetails);
			DECLARE @TeacherName NVARCHAR(200) = (SELECT TOP 1 LectureName FROM @TeacherTimeTableDetails);

			SET @TeacherName = @TeacherName + '- Time Table';
			UPDATE @TeacherTimeTable SET TEACHERID = @TeacherID, TEACHERNAME = @TeacherName;
			DECLARE @LocationTitleTimeTable NVARCHAR(200);
			DECLARE @SemsterTitleTimeTable NVARCHAR(200);			
			DECLARE @SubjectTitleTimeTable NVARCHAR(200);			
			DECLARE @CountTimeSlotTimeTable int = (SELECT Count(*) FROM @TeacherTimeTableDetails);			
			DECLARE @AddOnebyOne int  = 1;			
			WHILE @AddOnebyOne <= @CountTimeSlotTimeTable			
				BEGIN
			    DECLARE @GETProgramSemesterSubjectID int  = (SELECT Top 1 ProgramSemesterSubjectID FROM @TeacherTimeTableDetails WHERE RowNo = @AddOnebyOne AND IsActive = 1);			    
				IF @GETProgramSemesterSubjectID > 0				
					BEGIN				
					DECLARE @LectureSUBJECTID INT = (SELECT TOP 1 LectureSubjectID FROM ProgramSemesterSubjectTable 
					WHERE ProgramSemesterSubjectID = @GETProgramSemesterSubjectID);
					SET @SubjectTitleTimeTable = (SELECT TOP 1 SubjectTitle FROM @TeacherTimeTableDetails WHERE RowNo = @AddOnebyOne AND IsActive = 1);
					DECLARE @GETRoomID int  = (SELECT Top 1 RoomID FROM @TeacherTimeTableDetails WHERE RowNo = @AddOnebyOne AND IsActive = 1);
					DECLARE @GETLabID int  = (SELECT Top 1 LabID FROM @TeacherTimeTableDetails WHERE RowNo = @AddOnebyOne AND IsActive = 1);

					DECLARE @GETDayTimeSlotID int  = (SELECT Top 1 DayTimeSlotID FROM @TeacherTimeTableDetails WHERE RowNo = @AddOnebyOne AND IsActive = 1);
					DECLARE @GETLectureID int  = (SELECT Top 1 LectureID FROM @TeacherTimeTableDetails WHERE RowNo = @AddOnebyOne AND IsActive = 1);
					DECLARE @GETTimeSlotName nvarchar(200)  = (SELECT Top 1 SlotTitle FROM @TeacherTimeTableDetails WHERE RowNo = @AddOnebyOne AND IsActive = 1);
					DECLARE @GETDayTitle nvarchar(100)  = (SELECT Top 1 DayTitle FROM @TeacherTimeTableDetails WHERE RowNo = @AddOnebyOne AND IsActive = 1);

					IF @GETRoomID > 0					BEGIN						SET @LocationTitleTimeTable = (Select TOP 1 RoomNo From RoomTable WHERE RoomID = @GETRoomID)
					END					IF @GETLabID > 0					BEGIN						SET @LocationTitleTimeTable = (Select TOP 1 LabNo From LabTable WHERE LabID = @GETLabID)
					END					SET @SubjectTitleTimeTable = @SubjectTitleTimeTable + ' \ ' + @LocationTitleTimeTable;

					IF @GETDayTitle = 'MONDAY'					BEGIN						UPDATE @TeacherTimeTable SET MONDAY =  @SubjectTitleTimeTable WHERE [TIME]  = @GETTimeSlotName
					END					ELSE IF @GETDayTitle = 'TUESDAY'
					BEGIN						UPDATE @TeacherTimeTable SET TUESDAY =  @SubjectTitleTimeTable WHERE [TIME]  = @GETTimeSlotName
					END					ELSE IF @GETDayTitle = 'WEDNESDAY'
					BEGIN						UPDATE @TeacherTimeTable SET WEDNESDAY =  @SubjectTitleTimeTable WHERE [TIME]  = @GETTimeSlotName
					END					ELSE IF @GETDayTitle = 'THURSDAY'
					BEGIN						UPDATE @TeacherTimeTable SET THURSDAY =  @SubjectTitleTimeTable WHERE [TIME]  = @GETTimeSlotName
					END					ELSE IF @GETDayTitle = 'FRIDAY'
					BEGIN						UPDATE @TeacherTimeTable SET FRIDAY =  @SubjectTitleTimeTable WHERE [TIME]  = @GETTimeSlotName
					END					ELSE IF @GETDayTitle = 'SATURDAY'
					BEGIN						UPDATE @TeacherTimeTable SET SATURDAY =  @SubjectTitleTimeTable WHERE [TIME]  = @GETTimeSlotName
					END					ELSE IF @GETDayTitle = 'SUNDAY'
					BEGIN						UPDATE @TeacherTimeTable SET SUNDAY =  @SubjectTitleTimeTable WHERE [TIME]  = @GETTimeSlotName
					END				END
			SET @AddOnebyOne = @AddOnebyOne + 1;			END	
			SET @GETTimeTableOneByOne = @GETTimeTableOneByOne + 1;

	       INSERT INTO @AllTeacherTimeTable([TEACHERID],[TEACHERNAME],[TIME],[MONDAY],[TUESDAY],[WEDNESDAY],[THURSDAY],[FRIDAY],[SATURDAY],[SUNDAY])

								  SELECT [TEACHERID],[TEACHERNAME],[TIME],[MONDAY],[TUESDAY],[WEDNESDAY],[THURSDAY],[FRIDAY],[SATURDAY],[SUNDAY] FROM @TeacherTimeTable
	 END
	  SELECT * FROM @AllTeacherTimeTable WHERE TEACHERNAME IS NOT NULL
END