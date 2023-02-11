CREATE TABLE [dbo].PostGroupSiteUser
(
	SiteUserId int NOT NULL,
	PostGroupId int NOT NULL,
	CreateDate DateTime NOT NULL default getdate()
)
GO
ALTER TABLE dbo.PostGroupSiteUser ADD CONSTRAINT FK_PostGroupSiteUser_SiteUser FOREIGN KEY(SiteUserId)
REFERENCES dbo.SiteUser (Id)
GO
ALTER TABLE dbo.PostGroupSiteUser CHECK CONSTRAINT FK_PostGroupSiteUser_SiteUser
GO

ALTER TABLE dbo.PostGroupSiteUser ADD CONSTRAINT FK_PostGroupSiteUser_PostGroup FOREIGN KEY(PostGroupId)
REFERENCES dbo.PostGroup (Id)
GO
ALTER TABLE dbo.PostGroupSiteUser CHECK CONSTRAINT FK_PostGroupSiteUser_PostGroup
GO