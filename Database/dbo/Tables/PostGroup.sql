CREATE TABLE dbo.PostGroup
(
	Id int IDENTITY(1,1) NOT FOR REPLICATION NOT NULL PRIMARY KEY,
	SiteUserId int NOT NULL,
	GroupName varchar(100) NOT NULL,
	Description varchar(500) NOT NULL,
	CreateDate DateTime NOT NULL default getdate(),
) ON [PRIMARY]
GO

ALTER TABLE dbo.PostGroup ADD CONSTRAINT FK_PostGroup_SiteUser FOREIGN KEY(SiteUserId)
REFERENCES dbo.SiteUser (Id)
GO
ALTER TABLE dbo.PostGroup CHECK CONSTRAINT FK_PostGroup_SiteUser
GO


