DROP PROC IF EXISTS TutorTutoringSession_Add
GO
CREATE PROC TutorTutoringSession_Add
	@ssn VARCHAR(20),
	@code VARCHAR(20),
	@startTime DATETIME,
	@endTime DATETIME
AS
BEGIN
	INSERT INTO TutorTutoringSession 
	VALUES (@ssn, @code, @startTime, @endTime);
END
GO

--

DROP PROC IF EXISTS TutorTutoringSession_Update
GO
CREATE PROC TutorTutoringSession_Update
	@ssn VARCHAR(20),
	@code VARCHAR(20),
	@startTime DATETIME,
	@endTime DATETIME
AS
BEGIN
	UPDATE TutorTutoringSession 
	SET ssn = @ssn, code = @code, startTime = @startTime, endTime = @endTime
	WHERE ssn = @ssn AND code = @code AND startTime = @startTime AND endTime = @endTime;
END
GO

--

DROP PROC IF EXISTS TutorTutoringSession_Delete
GO
CREATE PROC TutorTutoringSession_Delete
	@ssn VARCHAR(20),
	@code VARCHAR(20),
	@startTime DATETIME,
	@endTime DATETIME
AS
BEGIN
	DELETE FROM TutorTutoringSession 
	WHERE ssn = @ssn AND code = @code AND startTime = @startTime AND endTime = @endTime;
END
GO