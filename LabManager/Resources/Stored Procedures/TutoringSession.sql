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

--

--DROP PROC IF EXISTS TutoringSession_Get
--GO
--CREATE PROCEDURE TutoringSession_Get
--	@code VARCHAR(20),
--	@startTime DATETIME,
--	@endTime DATETIME
--AS
--BEGIN
--	SELECT * FROM TutoringSession 
--	WHERE code = @code AND startTime = @startTime AND endtime = @endTime;
--END
--GO

--

--DROP PROC IF EXISTS TutoringSession_GetAll
--GO
--CREATE PROCEDURE TutoringSession_GetAll
--AS
--BEGIN
--SELECT * FROM TutoringSession
--END
--GO

----

--DROP PROC IF EXISTS TutoringSession_GetAll_code_startTime_endTime
--GO
--CREATE PROCEDURE TutoringSession_GetAll_code_startTime_endTime
--	@code VARCHAR (20),
--	@startTime DATETIME,
--	@endTime DATETIME
--AS
--BEGIN
--SELECT * FROM TutoringSession
--WHERE code = @code AND startTime = @startTime AND endtime = @endtime;
--END
--GO

----

--DROP PROC IF EXISTS TutoringSession_GetAll_code
--GO
--CREATE PROCEDURE TutoringSession_GetAll_code
--	@code VARCHAR (20)
--AS
--BEGIN
--SELECT * FROM TutoringSession
--WHERE code = @code;
--END
--GO

