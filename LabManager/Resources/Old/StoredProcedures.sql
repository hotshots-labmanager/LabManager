---- Stored procedure to move a planned tutor session (PlanToTutor) to completed tutoring (HaveTutored)
--DROP PROCEDURE IF EXISTS PlanToTutor_CompleteTutoringSession
--GO

--CREATE PROCEDURE PlanToTutor_CompleteTutoringSession
--	@ssn VARCHAR(20),
--	@code VARCHAR(20),
--	@startTime DATETIME,
--	@endTime DATETIME,
--	@hours DECIMAL(5, 2)
--AS
--BEGIN
--	BEGIN TRY
--		BEGIN TRANSACTION;

--		SET NOCOUNT ON;
		
--		DELETE FROM PlanToTutor WHERE ssn = @ssn AND code = @code AND startTime = @startTime AND endTime = @endTime;

--		INSERT INTO HaveTutored VALUES (@ssn, @code, @startTime, @endTime, @hours);

--		COMMIT TRANSACTION;
--	END TRY
--	BEGIN CATCH
--		ROLLBACK TRANSACTION;
--		THROW
--	END CATCH
--END;