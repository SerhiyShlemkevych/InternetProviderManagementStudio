﻿USE InternetProviderManagementStudio;
GO

CREATE TRIGGER TR_tblCustomer_INSERT ON tblCustomer AFTER INSERT
AS
BEGIN
	DECLARE @affectedRowIds NVARCHAR(MAX) = (SELECT LEFT(CAST(Id as VARCHAR), LEN(CAST(Id as VARCHAR)) - 1) FROM (SELECT CAST(Id as VARCHAR) + ', ' FROM inserted FOR XML PATH ('')) c (Id));
	DECLARE @administratorId INT = (SELECT TOP 1 LastChangeInitiatorId FROM inserted);

	INSERT INTO tblActionLog (AdministratorId, [Date], [Target], [Action], AffectedRowIds) VALUES (@administratorId, GETDATE(), 'Customer', 'Create', @affectedRowIds);
END;
GO

CREATE TRIGGER TR_tblCustomer_UPDATE ON tblCustomer AFTER UPDATE
AS
BEGIN
	DECLARE @affectedRowIds NVARCHAR(MAX) = (SELECT LEFT(CAST(Id as VARCHAR), LEN(CAST(Id as VARCHAR)) - 1) FROM (SELECT CAST(Id as VARCHAR) + ', ' FROM inserted FOR XML PATH ('')) c (Id));
	DECLARE @administratorId INT = (SELECT TOP 1 LastChangeInitiatorId FROM inserted);

	INSERT INTO tblActionLog (AdministratorId, [Date], [Target], [Action], AffectedRowIds) VALUES (@administratorId, GETDATE(), 'Customer', 'Change', @affectedRowIds);
END;
GO

CREATE TRIGGER TR_tblTariff_UPDATE ON tblTariff AFTER UPDATE
AS
BEGIN
	DECLARE @affectedRowIds NVARCHAR(MAX) = (SELECT LEFT(CAST(Id as VARCHAR), LEN(CAST(Id as VARCHAR)) - 1) FROM (SELECT CAST(Id as VARCHAR) + ', ' FROM inserted FOR XML PATH ('')) c (Id));
	DECLARE @administratorId INT = (SELECT TOP 1 LastChangeInitiatorId FROM inserted);

	INSERT INTO tblActionLog (AdministratorId, [Date], [Target], [Action], AffectedRowIds) VALUES (@administratorId, GETDATE(), 'Tariff', 'Change', @affectedRowIds);
END;
GO

CREATE TRIGGER TR_tblTariff_INSERT ON tblTariff AFTER INSERT
AS
BEGIN
	DECLARE @affectedRowIds NVARCHAR(MAX) = (SELECT LEFT(CAST(Id as VARCHAR), LEN(CAST(Id as VARCHAR)) - 1) FROM (SELECT CAST(Id as VARCHAR) + ', ' FROM inserted FOR XML PATH ('')) c (Id));
	DECLARE @administratorId INT = (SELECT TOP 1 LastChangeInitiatorId FROM inserted);

	INSERT INTO tblActionLog (AdministratorId, [Date], [Target], [Action], AffectedRowIds) VALUES (@administratorId, GETDATE(), 'Tariff', 'Create', @affectedRowIds);
END;
GO

CREATE TRIGGER TR_tblConnectedHouse_INSERT ON tblConnectedHouse AFTER INSERT
AS
BEGIN
	DECLARE @affectedRowIds NVARCHAR(MAX) = (SELECT LEFT(CAST(Id as VARCHAR), LEN(CAST(Id as VARCHAR)) - 1) FROM (SELECT CAST(Id as VARCHAR) + ', ' FROM inserted FOR XML PATH ('')) c (Id));
	DECLARE @administratorId INT = (SELECT TOP 1 LastChangeInitiatorId FROM inserted);

	INSERT INTO tblActionLog (AdministratorId, [Date], [Target], [Action], AffectedRowIds) VALUES (@administratorId, GETDATE(), 'House', 'Create', @affectedRowIds);
END;
GO

CREATE TRIGGER TR_tblConnectedHouse_UPDATE ON tblConnectedHouse AFTER UPDATE
AS
BEGIN
	DECLARE @affectedRowIds NVARCHAR(MAX) = (SELECT LEFT(CAST(Id as VARCHAR), LEN(CAST(Id as VARCHAR)) - 1) FROM (SELECT CAST(Id as VARCHAR) + ', ' FROM inserted FOR XML PATH ('')) c (Id));
	DECLARE @administratorId INT = (SELECT TOP 1 LastChangeInitiatorId FROM inserted);

	INSERT INTO tblActionLog (AdministratorId, [Date], [Target], [Action], AffectedRowIds) VALUES (@administratorId, GETDATE(), 'House', 'Change', @affectedRowIds);
END;
GO


CREATE PROCEDURE spGetCharge
AS
BEGIN
	DECLARE @currentMonth DATETIME  = MONTH(GETDATE());
	DECLARE @currentDate DATETIME = GETDATE();

	SET TRAN ISOLATION LEVEL SERIALIZABLE;
	BEGIN TRAN;
		DISABLE TRIGGER TR_tblCustomer_UPDATE ON tblCustomer;	

		INSERT 
		INTO 
		tblAcconutLog (CustomerId, Amount, Balance, [Date], [Description])
		(	SELECT 
				customer.Id as CustomerId, 
				(tariff.Price * -1) as Amount, 
				(customer.Balance - tarrif.Price) as Balance,
				@currentDate as [Date], 
				'Charge' as [Description]
			FROM tblCustomer customer
			JOIN tblTariff tariff 
			ON customer.TariffId = tariff.Id 
			WHERE customer.Balance > tariff.Price
			AND 
			DATEPART(month, customer.LastChargedDate) != @currentMonth
			AND
			[State] = 1
		);

		UPDATE tblCustomer 
		SET 
		Balance = (Balance - (SELECT Price FROM tblTariff WHERE Id = TariffId)),
		LastChargedDate = GETDATE()
		WHERE Id IN 
			(SELECT customer.Id 
			 FROM tblCustomer customer
			 JOIN tblTariff tariff 
			 ON customer.TariffId = tariff.Id 
			 WHERE customer.Balance > tariff.Price
			 AND 
			 DATEPART(month, customer.LastChargedDate) != @currentMonth
			 AND
			 [State] = 1
			)
		;

		UPDATE tblCustomer
		SET
		[State] = 2
		WHERE Id IN
		   ( SELECT customer.Id 
			 FROM tblCustomer customer
			 JOIN tblTariff tariff 
			 ON customer.TariffId = tariff.Id 
			 WHERE customer.Balance < tariff.Price
			 AND 
			 DATEPART(month, customer.LastChargedDate) != @currentMonth
			)
		;

		ENABLE TRIGGER TR_tblCustomer_UPDATE ON tblCustomer;	
	COMMIT TRAN
END;
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
END;
GO

