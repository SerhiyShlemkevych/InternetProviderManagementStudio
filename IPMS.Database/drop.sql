USE InternetProviderManagementStudio;
GO

ALTER TABLE tblCustomer
	DROP CONSTRAINT FK_tblCustomer_HouseId_tblConnectedHouse_Id;
GO

ALTER TABLE tblCustomer
	DROP CONSTRAINT FK_tblCustomer_TariffId_tblTariff_Id;
GO

ALTER TABLE tblCustomer
	DROP CONSTRAINT FK_tblCustomer_LastChangeInitiatorId_tblAdministrator_Id;
GO

ALTER TABLE tblConnectedHouse
	DROP CONSTRAINT FK_tblConnectedHouse_LastChangeInitiatorId_tblAdministrator_Id;
GO

ALTER TABLE tblTariff
	DROP CONSTRAINT FK_tblTariff_LastChangeInitiatorId_tblAdministrator_Id;
GO

ALTER TABLE tblActionLog
	DROP CONSTRAINT FK_tblActionLog_AdministratorId_tblAdministrator_Id;
GO

ALTER TABLE tblBalanceLog
	DROP CONSTRAINT FK_tblBalanceLog_CustomerId_tblCustomer_Id;
GO

DROP TABLE tblCustomer;
GO

DROP TABLE tblTariff;
GO

DROP TABLE tblConnectedHouse;
GO

DROP TABLE tblBalanceLog;
GO

DROP TABLE tblActionLog;
GO

DROP TABLE tblAdministrator;
GO

DROP PROC spGetCharge;
GO

DROP PROC spArchiveTariff;
GO

DROP PROC spAddFunds;
GO