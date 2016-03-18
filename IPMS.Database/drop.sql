USE InternetProviderManagementStudio;
GO

ALTER TABLE tblCustomer
DROP CONSTRAINT FK_tblCustomer_HouseId_tblConnectedHouse_Id;
GO

ALTER TABLE tblCustomer
DROP CONSTRAINT FK_tblCustomer_TariffId_tblTariff_Id;
GO

DROP TABLE tblCustomer;
GO

DROP TABLE tblTariff;
GO

DROP TABLE tblConnectedHouse;
GO

DROP PROC GetCharge;
GO

DROP PROC ArchiveTariff;
GO

--USE master;
--GO

--DROP DATABASE InternetProviderManagementStudio;
--GO