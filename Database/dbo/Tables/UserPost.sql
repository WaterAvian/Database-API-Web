CREATE TABLE dbo.UserPost
(
	Id int IDENTITY(1,1) NOT FOR REPLICATION NOT NULL PRIMARY KEY,
	PostGroupId int NOT NULL,
	SiteUserId int NOT NULL,
	Text varchar(50) NOT NULL,
	CreateDate DateTime NOT NULL default getdate(),
) ON [PRIMARY]
GO

ALTER TABLE dbo.UserPost ADD CONSTRAINT FK_UserPost_SiteUser FOREIGN KEY(SiteUserId)
REFERENCES dbo.SiteUser (Id)
GO

ALTER TABLE dbo.UserPost CHECK CONSTRAINT FK_UserPost_SiteUser
GO

ALTER TABLE dbo.UserPost ADD CONSTRAINT FK_UserPost_PostGroup FOREIGN KEY(PostGroupId)
REFERENCES dbo.PostGroup (Id)
GO

ALTER TABLE dbo.UserPost CHECK CONSTRAINT FK_UserPost_PostGroup
GO