-- Function to calculate the number of tutors for a tutor session
CREATE OR ALTER FUNCTION TutorTutoringSession_GetNumberOfTutors(@code VARCHAR(20), @startTime DATETIME, @endTime DATETIME)
RETURNS INT
AS
BEGIN
	RETURN (SELECT COUNT(*) AS numberOfTutors FROM TutorTutoringSession WHERE code = @code AND startTime = @startTime AND endTime = @endTime);
END;
GO

-- Function to calculate the ratio of students per tutor for a tutor session
CREATE OR ALTER FUNCTION TutoringSession_GetStudentsPerTutorRatio(@code VARCHAR(20), @startTime DATETIME, @endTime DATETIME)
RETURNS DECIMAL(5, 2)
AS
BEGIN
	RETURN (SELECT CONVERT(DECIMAL(5, 2), (SELECT numberOfParticipants FROM TutoringSession WHERE code = @code AND startTime = @startTime AND endTime = @endTime) / 
			CONVERT(DECIMAL(5, 2), (SELECT dbo.TutorTutoringSession_GetNumberOfTutors(@code, @startTime, @endTime)))) AS ratio);
END;
GO

-- Function to calculate the number of tutored hours
CREATE OR ALTER FUNCTION Tutor_GetTutoredHours(@ssn VARCHAR(20))
RETURNS DECIMAL(5, 2)
AS
BEGIN
	RETURN (SELECT ISNULL(SUM(DATEDIFF(SECOND, startTime, endTime)), 0) / 3600.0 AS hours FROM TutorTutoringSession WHERE ssn = @ssn AND endTime < GETDATE());
END;
GO

-- Function to calculate the number of planned hours
CREATE OR ALTER FUNCTION Tutor_GetPlannedHours(@ssn VARCHAR(20))
RETURNS DECIMAL(5, 2)
AS
BEGIN
	RETURN (SELECT ISNULL(SUM(DATEDIFF(SECOND, startTime, endTime)), 0) / 3600.0 AS hours FROM TutorTutoringSession WHERE ssn = @ssn AND startTime > GETDATE());
END;
GO