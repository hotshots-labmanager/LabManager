-- Trigger to check that a tutor session does not begin while another is in progress
DROP TRIGGER IF EXISTS TutoringSession_NoOthersInProgress
GO

CREATE TRIGGER TutoringSession_NoOthersInProgress
ON TutoringSession
INSTEAD OF INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @startTime DATETIME, @endTime DATETIME;

    SELECT @startTime = startTime, @endTime = endTime FROM inserted i;
    IF @startTime >= @endTime
    BEGIN
        DECLARE @errorMessage VARCHAR(100) = 'The start time (' + CONVERT(VARCHAR(30), @startTime) + ') must occur before the end time (' + CONVERT(VARCHAR(30), @endTime) + ')';
        THROW 61000, @errorMessage, 1;
    END
    ELSE
        INSERT INTO TutoringSession (code, startTime, endTime, numberOfParticipants) SELECT code, startTime, endTime, numberOfParticipants FROM inserted;
END;

-- Trigger to check that the hours a tutor has tutored a tutoring session does not exceed the tutoring sessions duration
DROP TRIGGER IF EXISTS HaveTutored_HoursIsContainedInSessionDuration
GO

CREATE TRIGGER HaveTutored_HoursIsContainedInSessionDuration
ON HaveTutored
INSTEAD OF INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @timeDifference DECIMAL(5, 2), @hours DECIMAL(5, 2);

    SELECT @timeDifference = DATEDIFF(SECOND, i.startTime, i.endTime) / 3600.0, @hours = i.hours FROM inserted i;
    IF @hours > @timeDifference
    BEGIN
        DECLARE @errorMessage VARCHAR(100) = 'The number of hours (' + CONVERT(VARCHAR(20), @hours) + ') is larger than the time difference (' + CONVERT(VARCHAR(20), @timeDifference) + ')';
        THROW 64000, @errorMessage, 1;
    END
    ELSE
        INSERT INTO HaveTutored (ssn, code, startTime, endTime, hours) SELECT ssn, code, startTime, endTime, hours FROM inserted;
END;

-- Trigger to check that the start time for a tutoring session occurs before the end time
DROP TRIGGER IF EXISTS TutoringSession_StartTimeOccursBeforeEndTime
GO

CREATE TRIGGER TutoringSession_StartTimeOccursBeforeEndTime
ON TutoringSession
INSTEAD OF INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @startTime DATETIME, @endTime DATETIME;

    SELECT @startTime = startTime, @endTime = endTime FROM inserted i;
    IF @startTime >= @endTime
    BEGIN
        DECLARE @errorMessage VARCHAR(100) = 'The start time (' + CONVERT(VARCHAR(30), @startTime) + ') must occur before the end time (' + CONVERT(VARCHAR(30), @endTime) + ')';
        THROW 61000, @errorMessage, 1;
    END
    ELSE
        INSERT INTO TutoringSession (code, startTime, endTime, numberOfParticipants) SELECT code, startTime, endTime, numberOfParticipants FROM inserted;
END;