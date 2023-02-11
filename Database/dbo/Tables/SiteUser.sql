CREATE TABLE [dbo].SiteUser
(
	[Id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL PRIMARY KEY,
	Firstname varchar(50) NOT NULL,
	Lastname varchar(50) NOT NULL,
	Password varchar(50) NOT NULL
) ON [PRIMARY]
GO
