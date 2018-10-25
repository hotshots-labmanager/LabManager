
DROP PROC IF EXISTS Course_Add
GO
CREATE PROCEDURE Course_Add
@code varchar(20),
@name varchar(60),
@credits decimal(5,2),
@numberOfStudents int
AS
BEGIN
INSERT INTO Course 
VALUES (
@code,
@name,
@credits,
@numberOfStudents)
END
GO

--

DROP PROC IF EXISTS Course_Update
GO
CREATE PROCEDURE Course_Update
@code varchar(20),
@name varchar(60),
@credits decimal(5,2),
@numberOfStudents int
AS
BEGIN
UPDATE Course SET code = @code, name = @name, credits = @credits, numberOfStudents = @numberOfStudents
END
GO

--

DROP PROC IF EXISTS Course_Delete
GO
CREATE PROCEDURE Course_Delete
@code varchar(20)
AS
BEGIN
DELETE FROM Course
WHERE 
code = @code
END
GO

--

DROP PROC IF EXISTS Course_Get
GO
CREATE PROCEDURE Course_Get
@code varchar(20)
AS
BEGIN
SELECT * FROM Course
WHERE (code = @code)
END
GO

--

DROP PROC IF EXISTS Course_GetALL
GO
CREATE PROCEDURE Course_GetAll
AS
BEGIN
SELECT * FROM Course
END
GO



