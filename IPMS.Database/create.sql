CREATE DATABASE InternetProviderManagementStudio;
GO

USE InternetProviderManagementStudio;
GO

CREATE TABLE tblConnectedHouse(
	Id INT NOT NULL IDENTITY(1, 1),
	City NVARCHAR(128) NOT NULL,
	Street NVARCHAR(256) NOT NULL,
	House NVARCHAR (16),

	CONSTRAINT UN_tblConnectedHouse_Id UNIQUE(Id),
	CONSTRAINT PK_tblConnectedHouse_Id PRIMARY KEY(Id),
	);
GO

CREATE TABLE tblTariff(
	Id INT NOT NULL IDENTITY(1, 1),
	Name NVARCHAR(128) NOT NULL,
	Price NUMERIC(18, 4) NOT NULL,
	UploadSpeed INT NOT NULL,
	DownloadSpeed INT NOT NULL,
	IsArchive BIT NOT NULL,

	CONSTRAINT UN_tblTariff_Id UNIQUE(Id),
	CONSTRAINT PK_tblTariff_Id PRIMARY KEY(Id),
	CONSTRAINT [CK_tblTariff_Price>0] CHECK(Price > 0),
	CONSTRAINT [CK_tblTariff_UploadSpeed>0] CHECK(UploadSpeed > 0),
	CONSTRAINT [CK_tblTarif_DownloadSpeed>0] CHECK(DownloadSpeed > 0)
	);
GO
CREATE TABLE tblCustomer(
	Id INT NOT NULL IDENTITY(1, 1),
	Forename NVARCHAR(256) NOT NULL,
	Surname NVARCHAR(256) NOT NULL,
	HouseId INT NOT NULL,
	Flat NVARCHAR(16) NOT NULL,
	TariffId INT NOT NULL,
	Balance NUMERIC(18, 4) NOT NULL,
	[State] int NOT NULL,
	IpAddress NVARCHAR(15) NULL,
	MacAddress NVARCHAR(17) NULL,
	LastChargedDate DateTime NOT NULL,

	CONSTRAINT UN_tblCustomer_Id UNIQUE(Id),
	CONSTRAINT PK_tblCustomer_Id PRIMARY KEY(Id),
	CONSTRAINT FK_tblCustomer_TariffId_tblTariff_Id FOREIGN KEY(TariffId) REFERENCES tblTariff(Id),
	CONSTRAINT FK_tblCustomer_HouseId_tblConnectedHouse_Id FOREIGN KEY(HouseId) REFERENCES tblConnectedHouse(Id),
	CONSTRAINT [CK_tblCustomer_Balance>=0] CHECK(Balance >= 0),
	CONSTRAINT [CK_tblCustomer_Status_=_1_OR_2] CHECK([State] IN (1, 2))
	);
GO
