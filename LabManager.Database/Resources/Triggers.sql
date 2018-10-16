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
        THROW 60001, @errorMessage, 1;
    END
    ELSE
        INSERT INTO HaveTutored (ssn, code, startTime, endTime, hours) SELECT ssn, code, startTime, endTime, hours FROM inserted;
END;
