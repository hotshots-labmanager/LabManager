DROP PROC IF EXISTS PlanToTutor_Add
GO
CREATE PROC PlanToTutor_Add
	@ssn VARCHAR(20),
	@code VARCHAR(20),
	@startTime DATETIME,
	@endTime DATETIME
AS
BEGIN
	INSERT INTO PlanToTutor 
	VALUES (@ssn, @code, @startTime, @endTime);
END
GO

--

DROP PROC IF EXISTS PlanToTutor_Update
GO
CREATE PROC PlanToTutor_Update
	@ssn VARCHAR(20),
	@code VARCHAR(20),
	@startTime DATETIME,
	@endTime DATETIME
AS
BEGIN
	UPDATE PlanToTutor 
	SET ssn = @ssn, code = @code, startTime = @startTime, endTime = @endTime
	WHERE ssn = @ssn AND code = @code AND startTime = @startTime AND endTime = @endTime;
END
GO

--

DROP PROC IF EXISTS PlanToTutor_Delete
GO
CREATE PROC PlanToTutor_Delete
	@ssn VARCHAR(20),
	@code VARCHAR(20),
	@startTime DATETIME,
	@endTime DATETIME
AS
BEGIN
	DELETE FROM PlanToTutor 
	WHERE ssn = @ssn AND code = @code AND startTime = @startTime AND endTime = @endTime;
END
GO