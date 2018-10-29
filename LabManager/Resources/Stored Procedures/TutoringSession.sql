DROP PROC IF EXISTS TutoringSession_Add
GO
CREATE PROCEDURE TutoringSession_Add
	@code VARCHAR(20),
	@startTime DATETIME,
	@endTime DATETIME,
	@numberOfParticipants INT
AS
BEGIN
	INSERT INTO TutoringSession 
	VALUES (@code, @startTime, @endTime, @numberOfParticipants);
END
GO

--

DROP PROC IF EXISTS TutoringSession_Update
GO
CREATE PROCEDURE TutoringSession_Update
	@code VARCHAR(20),
	@startTime DATETIME,
	@endTime DATETIME,
	@numberOfParticipants INT
AS
BEGIN
	UPDATE TutoringSession 
	SET code = @code, startTime = @startTime, endTime = @endTime, numberOfParticipants = @numberOfParticipants
	WHERE code = @code AND startTime = @startTime AND endTime = @endTime;
END
GO

--

DROP PROC IF EXISTS TutoringSession_Delete
GO
CREATE PROCEDURE TutoringSession_Delete
	@code VARCHAR(20), 
	@startTime DATETIME,
	@endTime DATETIME
AS
BEGIN
	DELETE FROM TutoringSession 
	WHERE code = @code AND startTime = @startTime AND endTime = endTime;
END
GO



