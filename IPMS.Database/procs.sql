USE InternetProviderManagementStudio;
GO

CREATE PROCEDURE GetCharge
AS
BEGIN
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
			 DATEPART(month, customer.LastChargedDate) != DATEPART(month, GETDATE())
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
			 DATEPART(month, customer.LastChargedDate) != DATEPART(month, GETDATE())
			)
		;	
	COMMIT TRAN
END
GO


CREATE PROCEDURE ArchiveTariff
	@targetTariffId INT,
	@substituteTariffId INT
AS
BEGIN
	DECLARE @isArchive BIT = (SELECT IsArchive FROM tblTariff WHERE Id = @substituteTariffId);
	IF(@isArchive IS NULL OR @isArchive = 1)
		THROW 1, 'message', 1;
	SET TRAN ISOLATION LEVEL READ COMMITTED
	BEGIN TRAN
		UPDATE tblCustomer SET TariffId = @substituteTariffId WHERE TariffId = @targetTariffId;
		UPDATE tblTariff SET IsArchive = 1 WHERE Id = @targetTariffId;
	COMMIT TRAN
END
GO