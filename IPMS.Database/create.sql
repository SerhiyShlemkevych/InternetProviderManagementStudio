USE InternetProviderManagementStudio;
GO

CREATE TABLE tblAdministrator (
	Id INT NOT NULL IDENTITY(1, 1),
	[Login] VARCHAR(50) NOT NULL,
	[Password] VARCHAR(50) NOT NULL,
	Forename NVARCHAR(128) NOT NULL,
	Surname NVARCHAR(128) NOT NULL,
	[Disabled] BIT NOT NULL,

	CONSTRAINT UQ_tblAdministrator_Id UNIQUE(Id),
	CONSTRAINT PK_tblAdministratot_Id PRIMARY KEY(Id),
	CONSTRAINT UQ_tblAdministrator_Login UNIQUE([Login])
	);
GO

CREATE TABLE tblConnectedHouse(
	Id INT NOT NULL IDENTITY(1, 1),
	City NVARCHAR(128) NOT NULL,
	Street NVARCHAR(128) NOT NULL,
	House NVARCHAR (16),
	LastChangeInitiatorId INT NOT NULL,

	CONSTRAINT UQ_tblConnectedHouse_Id UNIQUE(Id),
	CONSTRAINT PK_tblConnectedHouse_Id PRIMARY KEY(Id),
	CONSTRAINT FK_tblConnectedHouse_LastChangeInitiatorId_tblAdministrator_Id FOREIGN KEY(LastChangeInitiatorId) REFERENCES tblAdministrator(Id)
	);
GO

CREATE TABLE tblTariff(
	Id INT NOT NULL IDENTITY(1, 1),
	Name NVARCHAR(128) NOT NULL,
	Price NUMERIC(18, 4) NOT NULL,
	UploadSpeed INT NOT NULL,
	DownloadSpeed INT NOT NULL,
	Archive BIT NOT NULL,
	LastChangeInitiatorId INT NOT NULL,

	CONSTRAINT UQ_tblTariff_Id UNIQUE(Id),
	CONSTRAINT PK_tblTariff_Id PRIMARY KEY(Id),
	CONSTRAINT FK_tblTariff_LastChangeInitiatorId_tblAdministrator_Id FOREIGN KEY(LastChangeInitiatorId) REFERENCES tblAdministrator(Id),
	CONSTRAINT [CK_tblTariff_Price>0] CHECK(Price > 0),
	CONSTRAINT [CK_tblTariff_UploadSpeed>0] CHECK(UploadSpeed > 0),
	CONSTRAINT [CK_tblTarif_DownloadSpeed>0] CHECK(DownloadSpeed > 0)
	);
GO

CREATE TABLE tblCustomer(
	Id INT NOT NULL IDENTITY(1, 1),
	Forename NVARCHAR(128) NOT NULL,
	Surname NVARCHAR(128) NOT NULL,
	HouseId INT NOT NULL,
	Flat NVARCHAR(16) NOT NULL,
	TariffId INT NOT NULL,
	Balance NUMERIC(18, 4) NOT NULL,
	[State] int NOT NULL,
	IpAddress NVARCHAR(15) NOT NULL,
	MacAddress NVARCHAR(17) NOT NULL,
	LastChargedDate DateTime NOT NULL,
	LastChangeInitiatorId INT NOT NULL,

	CONSTRAINT UQ_tblCustomer_Id UNIQUE(Id),
	CONSTRAINT PK_tblCustomer_Id PRIMARY KEY(Id),
	CONSTRAINT FK_tblCustomer_TariffId_tblTariff_Id FOREIGN KEY(TariffId) REFERENCES tblTariff(Id),
	CONSTRAINT FK_tblCustomer_HouseId_tblConnectedHouse_Id FOREIGN KEY(HouseId) REFERENCES tblConnectedHouse(Id),
	CONSTRAINT FK_tblCustomer_LastChangeInitiatorId_tblAdministrator_Id FOREIGN KEY(LastChangeInitiatorId) REFERENCES tblAdministrator(Id),
	CONSTRAINT UQ_tblCustomer_IpAddress UNIQUE(IpAddress),
	CONSTRAINT UQ_tvlCustomer_MacAddress UNIQUE(MacAddress),
	CONSTRAINT [CK_tblCustomer_Balance>=0] CHECK(Balance >= 0),
	CONSTRAINT [CK_tblCustomer_Status_=_1_OR_2] CHECK([State] IN (1, 2))
	);
GO

CREATE TABLE tblBalanceLog (
	Id INT NOT NULL IDENTITY(1, 1),
	CustomerId INT NOT NULL,
	Amount NUMERIC(18, 4) NOT NULL,
	Balance NUMERIC(18, 4) NOT NULL,
	[Date] DATETIME NOT NULL,
	[Description] NVARCHAR(128) NOT NULL,

	CONSTRAINT UQ_tblBalanceLog_Id UNIQUE(Id),
	CONSTRAINT PK_tblBalanceLog_Id PRIMARY KEY(Id),
	CONSTRAINT FK_tblBalanceLog_CustomerId_tblCustomer_Id FOREIGN KEY(CustomerId) REFERENCES tblCustomer(Id),
	CONSTRAINT [CK_tblBalanceLog_Balance>=0] CHECK(Balance >= 0)
	);
GO


CREATE TABLE tblActionLog (
	Id INT NOT NULL IDENTITY(1, 1),
	AdministratorId INT NOT NULL,
	[Date] DATETIME NOT NULL,
	[Target] NVARCHAR(128) NOT NULL,
	[Action] NVARCHAR(128) NOT NULL,
	AffectedRowIds NVARCHAR(MAX) NULL,
	CONSTRAINT UQ_tblActionLog_Id UNIQUE(Id),
	CONSTRAINT PK_tblActionLog_Id PRIMARY KEY(Id),
	CONSTRAINT FK_tblActionLog_AdministratorId_tblAdministrator_Id FOREIGN KEY(AdministratorId) REFERENCES tblAdministrator(Id)
	);

