DROP PROC IF EXISTS TutoringSession_AddTutoringSession
GO
CREATE PROCEDURE TutoringSession_AddTutoringSession
@code varchar(20),
@startTime datetime,
@endTime datetime,
@numberOfParticipants int
AS
BEGIN
INSERT INTO TutoringSession 
VALUES (
	@code,
	@startTime,
	@endTime,
	@numberOfParticipants)
END

--

DROP PROC IF EXISTS TutoringSession_DeleteTutoringSession
GO
CREATE PROCEDURE TutoringSession_DeleteTutoringSession
	@code varchar(20), 
	@startTime datetime,
	@endTime datetime
AS
BEGIN
DELETE FROM TutoringSession
WHERE 
code = @code AND startTime = @startTime AND endTime = endTime;
END

--

DROP PROC IF EXISTS TutoringSession_GetTutoringSession
GO
CREATE PROCEDURE TutoringSession_GetTutoringSession
	@code varchar(20),
	@startTime datetime,
	@endTime datetime
AS
BEGIN
SELECT * FROM TuringSession
WHERE (code = @code AND startTime = @startTime AND endtime = @endTime)
END


--

DROP PROC IF EXISTS TutoringSession_GetAllTutoringSessions
GO
CREATE PROCEDURE TutoringSession_GetAllTutoringSessions
AS
BEGIN
SELECT * FROM TutoringSession
END

--

DROP PROC IF EXISTS TutoringSession_GetAll_ssn
GO
CREATE PROCEDURE TutoringSession_GetAll_ssn
	@ssn VARCHAR (20)
AS
BEGIN
SELECT * FROM TutoringSession
WHERE ssn = @ssn;
END

--

DROP PROC IF EXISTS TutoringSession_GetAll_code_startTime_endTime
GO
CREATE PROCEDURE TutoringSession_GetAll_code_startTime_endTime
	@code VARCHAR (20),
	@startTime datetime,
	@endTime datetime
AS
BEGIN
SELECT * FROM TutoringSession
WHERE code = @code AND startTime = @startTime AND endtime = @endtime;
END;

--

DROP PROC IF EXISTS TutoringSession_GetAll_code
GO
CREATE PROCEDURE TutoringSession_GetAll_code
	@code VARCHAR (20)
AS
BEGIN
SELECT * FROM TutoringSession
WHERE code = @code;
END;

