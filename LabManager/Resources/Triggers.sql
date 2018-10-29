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
        SET @errorMessageTutoringSession = 'The start time ' + CONVERT(VARCHAR(30), @startTime) + ' must occur before the end time ' + CONVERT(VARCHAR(30), @endTime) + '';
        THROW 61000, @errorMessageTutoringSession, 1;
    END

	-- Check that a tutor session does not begin while another is in progress
	DECLARE @concurrentSessions INT = (SELECT COUNT(*) FROM TutoringSession WHERE code = @code AND ((startTime <= @startTime AND endTime > @startTime) 
																						       OR (startTime < @endTime AND endTime >= @endTime)))
	IF @concurrentSessions > 0
	BEGIN
		SET @errorMessageTutoringSession = 'There is already a tutoring session in progress overlapping ' + CONVERT(VARCHAR(30), @startTime) + ' and ' + CONVERT(VARCHAR(30), @endTime) + '';
		THROW 61001, @errorMessageTutoringSession, 1;
	END

    INSERT INTO TutoringSession (code, startTime, endTime, numberOfParticipants) SELECT code, startTime, endTime, numberOfParticipants FROM inserted;
END;
GO

DROP TRIGGER IF EXISTS TutorTutoringSession_InsteadOfTrigger
GO

CREATE TRIGGER TutorTutoringSession_InsteadOfTrigger
ON TutorTutoringSession
INSTEAD OF INSERT, UPDATE
AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @ssn VARCHAR(20), @code VARCHAR(20), @startTime DATETIME, @endTime DATETIME;
	DECLARE @errorMessagePlanToTutor VARCHAR(200);

	SELECT @ssn = ssn, @code = code, @startTime = startTime, @endTime = endTime FROM inserted i;

	-- Check that a tutor does not plan to tutor two sessions at the same time
	DECLARE @concurrentSessions INT = (SELECT COUNT(*) FROM TutorTutoringSession WHERE ssn = @ssn AND ((startTime <= @startTime AND endTime > @startTime) 
																						       OR (startTime < @endTime AND endTime >= @endTime)))

	IF @concurrentSessions > 0
	BEGIN
		SET @errorMessagePlanToTutor = 'The tutor with social security number ' + @ssn + ' is already tutoring a session overlapping ' + CONVERT(VARCHAR(30), @startTime) + ' and ' + CONVERT(VARCHAR(30), @endTime);
		THROW 63000, @errorMessagePlanToTutor, 1;
	END

	INSERT INTO TutorTutoringSession (ssn, code, startTime, endTime) SELECT ssn, code, startTime, endTime FROM inserted;
END;
GO