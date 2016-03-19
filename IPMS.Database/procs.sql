USE InternetProviderManagementStudio;
GO

CREATE TRIGGER TR_tblCustomer_INSERT ON tblCustomer AFTER INSERT
AS
BEGIN
	DECLARE @fullQuery NVARCHAR(MAX) = CONVERT(varchar(max), EVENTDATA().query('data(/EVENT_INSTANCE/TSQLCommand/CommandText)'));
	DECLARE @administratorId INT = (SELECT TOP 1 LastChangeInitiatorId FROM inserted);

	INSERT INTO tblActionLog (AdministratorId, [Date], [Target], [Action], FullQuery) VALUES (@administratorId, GETDATE(), 'Customer', 'Create', @fullQuery);
END
GO

CREATE TRIGGER TR_tblCustomer_UPDATE ON tblCustomer AFTER UPDATE
AS
BEGIN
	DECLARE @fullQuery NVARCHAR(MAX) = CONVERT(varchar(max), EVENTDATA().query('data(/EVENT_INSTANCE/TSQLCommand/CommandText)'));
	DECLARE @administratorId INT = (SELECT TOP 1 LastChangeInitiatorId FROM inserted);

	INSERT INTO tblActionLog (AdministratorId, [Date], [Target], [Action], FullQuery) VALUES (@administratorId, GETDATE(), 'Customer', 'Change', @fullQuery);
END
GO

ALTER TRIGGER TR_tblTariff_UPDATE ON tblTariff AFTER UPDATE
AS
BEGIN
	DECLARE @fullQuery NVARCHAR(MAX) = CONVERT(varchar(max), EVENTDATA());
	DECLARE @administratorId INT = (SELECT TOP 1 LastChangeInitiatorId FROM inserted);

	INSERT INTO tblActionLog (AdministratorId, [Date], [Target], [Action], FullQuery) VALUES (@administratorId, GETDATE(), 'Tariff', 'Change', @fullQuery);
END
GO

CREATE TRIGGER TR_tblTariff_INSERT ON tblTariff AFTER INSERT
AS
BEGIN
	DECLARE @fullQuery NVARCHAR(MAX) = CONVERT(varchar(max), EVENTDATA().query('data(/EVENT_INSTANCE/TSQLCommand/CommandText)'));
	DECLARE @administratorId INT = (SELECT TOP 1 LastChangeInitiatorId FROM inserted);

	INSERT INTO tblActionLog (AdministratorId, [Date], [Target], [Action], FullQuery) VALUES (@administratorId, GETDATE(), 'Tariff', 'Create', @fullQuery);
END
GO

CREATE TRIGGER TR_tblConnectedHouse_INSERT ON tblConnectedHouse AFTER INSERT
AS
BEGIN
	DECLARE @fullQuery NVARCHAR(MAX) = CONVERT(varchar(max), EVENTDATA().query('data(/EVENT_INSTANCE/TSQLCommand/CommandText)'));
	DECLARE @administratorId INT = (SELECT TOP 1 LastChangeInitiatorId FROM inserted);

	INSERT INTO tblActionLog (AdministratorId, [Date], [Target], [Action], FullQuery) VALUES (@administratorId, GETDATE(), 'House', 'Create', @fullQuery);
END
GO

CREATE TRIGGER TR_tblConnectedHouse_UPDATE ON tblConnectedHouse AFTER UPDATE
AS
BEGIN
	DECLARE @fullQuery NVARCHAR(MAX) = CONVERT(varchar(max), EVENTDATA().query('data(/EVENT_INSTANCE/TSQLCommand/CommandText)'));
	DECLARE @administratorId INT = (SELECT TOP 1 LastChangeInitiatorId FROM inserted);

	INSERT INTO tblActionLog (AdministratorId, [Date], [Target], [Action], FullQuery) VALUES (@administratorId, GETDATE(), 'House', 'Change', @fullQuery);
END
GO


CREATE PROCEDURE spGetCharge
AS
BEGIN
	DECLARE @currentMonth DATETIME  = MONTH(GETDATE());
	SET TRAN ISOLATION LEVEL SERIALIZABLE;
	BEGIN TRAN
		DISABLE TRIGGER TR_tblCustomer_UPDATE ON tblCustomer;
		
		UPDATE tblCustomer 
		SET 
		Balance = (Balance - (SELECT Price FROM tblTariff WHERE Id = TariffId)),
		LastChargedDate = GETDATE(),
		[State] = 1
		WHERE Id IN 
			(SELECT customer.Id 
			from tblCustomer customer
			 JOIN tblTariff tariff 
			 ON customer.TariffId = tariff.Id 
			 WHERE customer.Balance > tariff.Price
			 AND 
			 DATEPART(month, customer.LastChargedDate) != @currentMonth
			)
		;
		UPDATE tblCustomer
		SET
		[State] = 2
		WHERE Id IN
		   (SELECT customer.Id 
			from tblCustomer customer
			 JOIN tblTariff tariff 
			 ON customer.TariffId = tariff.Id 
			 WHERE customer.Balance < tariff.Price
			 AND 
			 DATEPART(month, customer.LastChargedDate) != @currentMonth
			)
		;
		ENABLE TRIGGER TR_tblCustomer_UPDATE ON tblCustomer;	
	COMMIT TRAN
END
GO


CREATE PROCEDURE spArchiveTariff
	@targetTariffId INT,
	@substituteTariffId INT
AS
BEGIN
	DECLARE @isArchive BIT = (SELECT Archive FROM tblTariff WHERE Id = @substituteTariffId);
	IF(@isArchive IS NULL OR @isArchive = 1)
		THROW 60001, 'Susbstitute tariff does not exists or is archive', 100;
	SET TRAN ISOLATION LEVEL READ COMMITTED
	BEGIN TRAN
		UPDATE tblCustomer SET TariffId = @substituteTariffId WHERE TariffId = @targetTariffId;
		UPDATE tblTariff SET Archive = 1 WHERE Id = @targetTariffId;
	COMMIT TRAN
END
GO

