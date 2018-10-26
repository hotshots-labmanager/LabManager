-- Function to calculate the number of tutors for a tutor session
CREATE OR ALTER FUNCTION TutorTutoringSession_GetNumberOfTutors(@code VARCHAR(20), @startTime DATETIME, @endTime DATETIME)
RETURNS INT
AS
BEGIN
	RETURN (SELECT COUNT(*) AS numberOfTutors FROM TutorTutoringSession WHERE code = @code AND startTime = @startTime AND endTime = @endTime);
END;
GO

-- Function to calculate the number of tutors for a tutor session (HaveTutored)
--CREATE OR ALTER FUNCTION HaveTutored_GetNumberOfTutors(@code VARCHAR(20), @startTime DATETIME, @endTime DATETIME)
--RETURNS INT
--AS
--BEGIN
--	RETURN (SELECT COUNT(*) AS numberOfTutors FROM HaveTutored WHERE code = @code AND startTime = @startTime AND endTime = @endTime);
--END;
--GO

-- Function to calculate the ratio of students per tutor for a tutor session
CREATE OR ALTER FUNCTION TutoringSession_GetStudentsPerTutorRatio(@code VARCHAR(20), @startTime DATETIME, @endTime DATETIME)
RETURNS DECIMAL(5, 2)
AS
BEGIN
	RETURN (SELECT CONVERT(DECIMAL(5, 2), (SELECT numberOfParticipants FROM TutoringSession WHERE code = @code AND startTime = @startTime AND endTime = @endTime) / 
			CONVERT(DECIMAL(5, 2), (SELECT dbo.TutorTutoringSession_GetNumberOfTutors(@code, @startTime, @endTime)))) AS ratio);
END;
GO

-- Function to calculate the number of plan to tutor hours
CREATE OR ALTER FUNCTION Tutor_GetTutorTutoringSessionHours(@ssn VARCHAR(20))
RETURNS DECIMAL(5, 2)
AS
BEGIN
	RETURN (SELECT SUM(DATEDIFF(SECOND, startTime, endTime)) / 3600.0 AS hours FROM PlanToTutor WHERE ssn = @ssn);
END;
GO

-- Function to calculate the number of have tutored hours
--CREATE OR ALTER FUNCTION Tutor_GetHaveTutoredHours(@ssn VARCHAR(20))
--RETURNS DECIMAL(5, 2)
--AS
--BEGIN
--	RETURN (SELECT SUM(hours) AS hours FROM HaveTutored WHERE ssn = @ssn);
--END;
--GO

-- Function to calculate the number of hours a tutor have missed
--CREATE OR ALTER FUNCTION Tutor_GetMissedHours(@ssn VARCHAR(20))
--RETURNS DECIMAL(5, 2)
--AS
--BEGIN
--	RETURN (SELECT SUM(DATEDIFF(SECOND, startTime, endTime)) / 3600.0 AS hours FROM HaveTutored WHERE ssn = @ssn) - (SELECT dbo.Tutor_GetHaveTutoredHours(@ssn));
--END;
--GO