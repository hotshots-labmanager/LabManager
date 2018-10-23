DROP PROC IF EXISTS Tutor_AddTutor
GO
CREATE PROCEDURE Tutor_AddTutor
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
END;

--

DROP PROC IF EXISTS Tutor_DeleteTutor
GO
CREATE PROCEDURE Tutor_DeleteTutor
@ssn varchar(20)
AS
BEGIN
DELETE FROM Tutor
WHERE 
ssn = @ssn;
END

--

DROP PROC IF EXISTS Tutor_GetTutor
GO
CREATE PROCEDURE Tutor_GetTutor
@ssn varchar(20)
AS
BEGIN
SELECT * FROM Tutor
WHERE (ssn = @ssn);
END;

DROP PROCEDURE IF EXISTS Tutor_GetAllTutors
GO

CREATE PROCEDURE Tutor_GetAllTutors
AS
BEGIN
	SELECT * FROM Tutor;
END;