-- Trigger to check that the hours a tutor has tutored a tutoring session does not exceed the tutoring sessions duration
DROP TRIGGER IF EXISTS HaveTutored_InsteadOfTrigger
GO

CREATE TRIGGER HaveTutored_InsteadOfTrigger
ON HaveTutored
INSTEAD OF INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @timeDifference DECIMAL(5, 2), @hours DECIMAL(5, 2);
	DECLARE @errorMessageHaveTutored VARCHAR(200);

    SELECT @timeDifference = DATEDIFF(SECOND, i.startTime, i.endTime) / 3600.0, @hours = i.hours FROM inserted i;
    IF @hours > @timeDifference
    BEGIN
        SET @errorMessageHaveTutored = 'The number of hours (' + CONVERT(VARCHAR(20), @hours) + ') is larger than the time difference (' + CONVERT(VARCHAR(20), @timeDifference) + ')';
        THROW 64000, @errorMessageHaveTutored, 1;
    END
    ELSE
        INSERT INTO HaveTutored (ssn, code, startTime, endTime, hours) SELECT ssn, code, startTime, endTime, hours FROM inserted;
END;
GO

DROP TRIGGER IF EXISTS TutoringSession_InsteadOfTrigger
GO

CREATE TRIGGER TutoringSession_InsteadOfTrigger
ON TutoringSession
INSTEAD OF INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

	DECLARE @code VARCHAR(20), @startTime DATETIME, @endTime DATETIME;
	DECLARE @errorMessageTutoringSession VARCHAR(200);

	SELECT @code = code, @startTime = startTime, @endTime = endTime FROM inserted i;

	-- Check that the start time for a tutoring session occurs before the end time
    IF @startTime >= @endTime
    BEGIN
        SET @errorMessageTutoringSession = 'The start time (' + CONVERT(VARCHAR(30), @startTime) + ') must occur before the end time (' + CONVERT(VARCHAR(30), @endTime) + ')';
        THROW 61000, @errorMessageTutoringSession, 1;
    END

	-- Check that a tutor session does not begin while another is in progress
	DECLARE @concurrentSessions INT = (SELECT COUNT(*) FROM TutoringSession WHERE code = @code AND ((startTime <= @startTime AND endTime > @startTime) 
																									  OR (startTime < @endTime AND endTime >= @endTime)))
	IF @concurrentSessions > 0
	BEGIN
		SET @errorMessageTutoringSession = 'There is already a tutoring session in progress overlapping (' + CONVERT(VARCHAR(30), @startTime) + ') and (' + CONVERT(VARCHAR(30), @endTime) + ')';
		THROW 61001, @errorMessageTutoringSession, 1;
	END

    INSERT INTO TutoringSession (code, startTime, endTime, numberOfParticipants) SELECT code, startTime, endTime, numberOfParticipants FROM inserted;
END;
GO

DROP TRIGGER IF EXISTS PlanToTutor_InsteadOfTrigger
GO

CREATE TRIGGER PlanToTutor_InsteadOfTrigger
ON PlanToTutor
INSTEAD OF INSERT, UPDATE
AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @ssn VARCHAR(20), @code VARCHAR(20), @startTime DATETIME, @endTime DATETIME;
	DECLARE @errorMessagePlanToTutor VARCHAR(200);

	SELECT @ssn = ssn, @code = code, @startTime = startTime, @endTime = endTime FROM inserted i;

	-- Check that a tutor does not plan to tutor two sessions at the same time
	DECLARE @concurrentSessions INT = (SELECT COUNT(*) FROM PlanToTutor WHERE ssn = @ssn AND ((startTime <= @startTime AND endTime > @startTime) 
																									OR (startTime < @endTime AND endTime >= @endTime)))

	IF @concurrentSessions > 0
	BEGIN
		SET @errorMessagePlanToTutor = 'The tutor with social security number (' + @ssn + ') is already tutoring a session overlapping (' + CONVERT(VARCHAR(30), @startTime) + ') and (' + CONVERT(VARCHAR(30), @endTime) + ')';
		THROW 63000, @errorMessagePlanToTutor, 1;
	END

	INSERT INTO PlanToTutor (ssn, code, startTime, endTime) SELECT ssn, code, startTime, endTime FROM inserted;
END;
GO
