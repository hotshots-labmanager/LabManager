DROP PROC IF EXISTS Tutor_Add
GO
CREATE PROCEDURE Tutor_Add
@ssn varchar(20),
@firstName varchar(20),
@lastName varchar(40),
@email varchar(40),
@password varchar(256)
AS
BEGIN
INSERT INTO Tutor 
VALUES (
@ssn,
@firstName,
@lastName,
@email,
@password)
END
GO

--

DROP PROC IF EXISTS Tutor_Delete
GO
CREATE PROCEDURE Tutor_Delete
@ssn varchar(20)
AS
BEGIN
DELETE FROM Tutor
WHERE 
ssn = @ssn;
END
GO

--

DROP PROC IF EXISTS Tutor_Get
GO
CREATE PROCEDURE Tutor_GetTutor
@ssn varchar(20)
AS
BEGIN
SELECT * FROM Tutor
WHERE (ssn = @ssn);
END
GO

DROP PROCEDURE IF EXISTS Tutor_GetAll
GO

CREATE PROCEDURE Tutor_GetAll
AS
BEGIN
	SELECT * FROM Tutor;
END
GO