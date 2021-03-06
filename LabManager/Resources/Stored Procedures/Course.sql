DROP PROC IF EXISTS Course_Add
GO
CREATE PROCEDURE Course_Add
	@code VARCHAR(20),
	@name VARCHAR(60),
	@credits DECIMAL(5,2),
	@numberOfStudents INT
AS
BEGIN
	INSERT INTO Course 
	VALUES (@code, @name, @credits, @numberOfStudents);
END
GO

--

DROP PROC IF EXISTS Course_Update
GO
CREATE PROCEDURE Course_Update
	@code VARCHAR(20),
	@name VARCHAR(60),
	@credits DECIMAL(5,2),
	@numberOfStudents INT
AS
BEGIN
	UPDATE Course 
	SET code = @code, name = @name, credits = @credits, numberOfStudents = @numberOfStudents
	WHERE code = @code;
END
GO

--

DROP PROC IF EXISTS Course_Delete
GO
CREATE PROCEDURE Course_Delete
	@code VARCHAR(20)
AS
BEGIN
	DELETE FROM Course 
	WHERE code = @code;
END
GO





