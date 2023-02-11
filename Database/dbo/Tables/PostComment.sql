CREATE TABLE dbo.PostComment(
	[Id] INT NOT NULL PRIMARY KEY,
	SiteUserId int NOT NULL,
	UserPostId int NOT NULL,
	CommentText varchar(200),
	CreateDate DateTime NOT NULL default getdate(),
)

GO
ALTER TABLE dbo.PostComment ADD CONSTRAINT FK_PostComment_SiteUser FOREIGN KEY(SiteUserId)
REFERENCES dbo.SiteUser (Id)
GO
ALTER TABLE dbo.PostComment CHECK CONSTRAINT FK_PostComment_SiteUser
GO

ALTER TABLE dbo.PostComment ADD CONSTRAINT FK_PostComment_UserPost FOREIGN KEY(UserPostId)
REFERENCES dbo.UserPost(Id)
GO
ALTER TABLE dbo.PostComment CHECK CONSTRAINT FK_PostComment_UserPost
GO
