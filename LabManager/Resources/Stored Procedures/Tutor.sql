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
	VALUES (@ssn, @firstName, @lastName, @email, @password);
END
GO

--

DROP PROC IF EXISTS Tutor_Update
GO
CREATE PROCEDURE Tutor_Update
	@ssn varchar(20),
	@firstName varchar(20),
	@lastName varchar(40),
	@email varchar(40),
	@password varchar(256)
AS
BEGIN
	UPDATE Tutor 
	SET ssn = @ssn, firstName = @firstName, lastName = @lastName, email = @email, password = @password
	WHERE ssn = @ssn
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
	WHERE ssn = @ssn;
END
GO

