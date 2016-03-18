USE InternetProviderManagementStudio;
GO

CREATE PROCEDURE spGetCharge
AS
BEGIN
	DECLARE @currentMonth DATETIME  = MONTH(GETDATE());
	SET TRAN ISOLATION LEVEL SERIALIZABLE;
	BEGIN TRAN
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
	COMMIT TRAN
END
GO


CREATE PROCEDURE spArchiveTariff
	@targetTariffId INT,
	@substituteTariffId INT
AS
BEGIN
	DECLARE @isArchive BIT = (SELECT IsArchive FROM tblTariff WHERE Id = @substituteTariffId);
	IF(@isArchive IS NULL OR @isArchive = 1)
		THROW 60001, 'Susbstitute tariff does not exists or is archive', 100;
	SET TRAN ISOLATION LEVEL READ COMMITTED
	BEGIN TRAN
		UPDATE tblCustomer SET TariffId = @substituteTariffId WHERE TariffId = @targetTariffId;
		UPDATE tblTariff SET IsArchive = 1 WHERE Id = @targetTariffId;
	COMMIT TRAN
END
GO