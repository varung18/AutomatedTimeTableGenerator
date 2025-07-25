USE [AutoTimeTableDb]
GO
-- Create StoredProcedure [sp_PrintSemesterwiseTimeTables]

CREATE PROC [dbo].[sp_PrintSemesterwiseTimeTables]
AS
BEGIN
	DECLARE @AllSemesterTimeTable Table( [TimeTableID] int,
[SEMESTER] nvarchar(300),
[TIME] nvarchar(300),
[MONDAY] nvarchar(300),
[TUESDAY] nvarchar(300),
[WEDNESDAY] nvarchar(300),
[THURSDAY] nvarchar(300),
[FRIDAY] nvarchar(300),
[SATURDAY] nvarchar(300),
[SUNDAY] nvarchar(300))


    DECLARE @CountTotalSemester int  = (SELECT COUNT(*) FROM TimeTblTable);
	DECLARE @GETTimeTableOneByOne int = 1;
	WHILE @GETTimeTableOneByOne <= @CountTotalSemester
	BEGIN
		DECLARE @SemesterTimeTableTitle NVARCHAR(200) = (SELECT TOP 1 TimeTableTitle FROM TimeTblTable WHERE TimeTableID = @GETTimeTableOneByOne)
		DECLARE @SemesterTimeTable Table([TimeTableID] int,
[SEMESTER] nvarchar(300),
[TIME] nvarchar(300),
[MONDAY] nvarchar(300),
[TUESDAY] nvarchar(300),
[WEDNESDAY] nvarchar(300),
[THURSDAY] nvarchar(300),
[FRIDAY] nvarchar(300),
[SATURDAY] nvarchar(300),
[SUNDAY] nvarchar(300))
        --Clear Table
		DELETE FROM @SemesterTimeTable;

		DECLARE @TimeSlotTimeTable Table(RowNo int, SlotTitle nvarchar(200))
		--Clear Table
		DELETE FROM @TimeSlotTimeTable;

		INSERT INTO @TimeSlotTimeTable(RowNo,SlotTitle)  SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)),SlotTitle FROM (Select SlotTitle, StartTime ,EndTime from DayTimeSlotTable WHERE ISActive  = 1 Group By SlotTitle, StartTime ,EndTime) DTST ORDER BY StartTime
		DECLARE @COUNTTIMEROWSTIMETABLE INT = (SELECT COUNT(*) FROM @TimeSlotTimeTable);
		DECLARE @CREATESLOTSVARIABLE INT = 1;
		WHILE @CREATESLOTSVARIABLE <= @COUNTTIMEROWSTIMETABLE
		BEGIN
			DECLARE @TIMETITLE NVARCHAR(200) = (SELECT TOP 1 SlotTitle FROM @TimeSlotTimeTable WHERE RowNo = @CREATESLOTSVARIABLE);
			INSERT INTO @SemesterTimeTable([TimeTableID],[TIME],[MONDAY],[TUESDAY],[WEDNESDAY],[THURSDAY],[FRIDAY],[SATURDAY],[SUNDAY])
			VALUES(0,@TIMETITLE,'Break','Break','Break','Break','Break','Break','Break')
			SET @CREATESLOTSVARIABLE = @CREATESLOTSVARIABLE + 1;
		END

		-- Print Time Table Slots
		--SELECT * FROM @SemesterTimeTable

		DECLARE @SemesterTimeTableDetails Table(
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
		 DELETE FROM @SemesterTimeTableDetails;
		 -- Getting Semester Wise Subject
		 INSERT INTO @SemesterTimeTableDetails (
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
WHERE TTD.TimeTableID = @GETTimeTableOneByOne Order By DayTimeSlotID
			-- Print SELECT Semester Class 
			--SELECT * FROM @SemesterTimeTableDetails
			DECLARE @TimeTableID int = (SELECT TOP 1 TimeTableID FROM @SemesterTimeTableDetails);
			UPDATE @SemesterTimeTable SET TimeTableID = @TimeTableID;
			DECLARE @LocationTitleTimeTable NVARCHAR(200);
			DECLARE @SemsterTitleTimeTable NVARCHAR(200);
			DECLARE @SubjectTitleTimeTable NVARCHAR(200);
			DECLARE @CountTimeSlotTimeTable int = (SELECT Count(*) FROM @SemesterTimeTableDetails);
			DECLARE @AddOnebyOne int  = 1;
			WHILE @AddOnebyOne <= @CountTimeSlotTimeTable
			BEGIN
			    DECLARE @GETProgramSemesterSubjectID int  = (SELECT Top 1 ProgramSemesterSubjectID FROM @SemesterTimeTableDetails WHERE RowNo = @AddOnebyOne AND IsActive = 1);
			    IF @GETProgramSemesterSubjectID > 0
				BEGIN
				SET @SubjectTitleTimeTable = (SELECT TOP 1 SemesterSubjectTitle FROM ProgramSemesterSubjectTable WHERE ProgramSemesterSubjectID = @GETProgramSemesterSubjectID);
			    DECLARE @GETRoomID int  = (SELECT Top 1 RoomID FROM @SemesterTimeTableDetails WHERE RowNo = @AddOnebyOne AND IsActive = 1);
			    DECLARE @GETLabID int  = (SELECT Top 1 LabID FROM @SemesterTimeTableDetails WHERE RowNo = @AddOnebyOne AND IsActive = 1);
			    DECLARE @GETDayTimeSlotID int  = (SELECT Top 1 DayTimeSlotID FROM @SemesterTimeTableDetails WHERE RowNo = @AddOnebyOne AND IsActive = 1);
			    DECLARE @GETLectureID int  = (SELECT Top 1 LectureID FROM @SemesterTimeTableDetails WHERE RowNo = @AddOnebyOne AND IsActive = 1);
			    DECLARE @GETTimeSlotName nvarchar(200)  = (SELECT Top 1 SlotTitle FROM @SemesterTimeTableDetails WHERE RowNo = @AddOnebyOne AND IsActive = 1);
			    DECLARE @GETDayTitle nvarchar(100)  = (SELECT Top 1 DayTitle FROM @SemesterTimeTableDetails WHERE RowNo = @AddOnebyOne AND IsActive = 1);
					IF @GETRoomID > 0
					BEGIN
						SET @LocationTitleTimeTable = (Select TOP 1 RoomNo From RoomTable WHERE RoomID = @GETRoomID)
					END
					IF @GETLabID > 0
					BEGIN
						SET @LocationTitleTimeTable = (Select TOP 1 LabNo From LabTable WHERE LabID = @GETLabID)
					END

					SET @SubjectTitleTimeTable = @SubjectTitleTimeTable + ' \ ' + @LocationTitleTimeTable;

					IF @GETDayTitle = 'MONDAY'
					BEGIN
						UPDATE @SemesterTimeTable SET MONDAY =  @SubjectTitleTimeTable,SEMESTER = @SemesterTimeTableTitle WHERE [TIME]  = @GETTimeSlotName
					END
					ELSE IF @GETDayTitle = 'TUESDAY'
					BEGIN
						UPDATE @SemesterTimeTable SET TUESDAY =  @SubjectTitleTimeTable,SEMESTER = @SemesterTimeTableTitle WHERE [TIME]  = @GETTimeSlotName
					END
					ELSE IF @GETDayTitle = 'WEDNESDAY'
					BEGIN
						UPDATE @SemesterTimeTable SET WEDNESDAY =  @SubjectTitleTimeTable,SEMESTER = @SemesterTimeTableTitle WHERE [TIME]  = @GETTimeSlotName
					END
					ELSE IF @GETDayTitle = 'THURSDAY'
					BEGIN
						UPDATE @SemesterTimeTable SET THURSDAY =  @SubjectTitleTimeTable,SEMESTER = @SemesterTimeTableTitle WHERE [TIME]  = @GETTimeSlotName
					END
					ELSE IF @GETDayTitle = 'FRIDAY'
					BEGIN
						UPDATE @SemesterTimeTable SET FRIDAY =  @SubjectTitleTimeTable,SEMESTER = @SemesterTimeTableTitle WHERE [TIME]  = @GETTimeSlotName
					END
					ELSE IF @GETDayTitle = 'SATURDAY'
					BEGIN
						UPDATE @SemesterTimeTable SET SATURDAY =  @SubjectTitleTimeTable,SEMESTER = @SemesterTimeTableTitle WHERE [TIME]  = @GETTimeSlotName
					END
					ELSE IF @GETDayTitle = 'SUNDAY'
					BEGIN
						UPDATE @SemesterTimeTable SET SUNDAY =  @SubjectTitleTimeTable,SEMESTER = @SemesterTimeTableTitle WHERE [TIME]  = @GETTimeSlotName
					END
				END
			SET @AddOnebyOne = @AddOnebyOne + 1;
			END	
			SET @GETTimeTableOneByOne = @GETTimeTableOneByOne + 1;
	       INSERT INTO @AllSemesterTimeTable([TimeTableID],[SEMESTER],[TIME],[MONDAY],[TUESDAY],[WEDNESDAY],[THURSDAY],[FRIDAY],[SATURDAY],[SUNDAY])
								  SELECT [TimeTableID],[SEMESTER],[TIME],[MONDAY],[TUESDAY],[WEDNESDAY],[THURSDAY],[FRIDAY],[SATURDAY],[SUNDAY] FROM @SemesterTimeTable
	 END
	  SELECT * FROM @AllSemesterTimeTable
END
