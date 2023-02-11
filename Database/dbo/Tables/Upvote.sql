CREATE TABLE dbo.Upvote
(
	SiteUserId int NOT NULL,
	UserPostId int NOT NULL,
	
)
GO
ALTER TABLE dbo.Upvote ADD CONSTRAINT FK_Upvote_SiteUser FOREIGN KEY(SiteUserId)
REFERENCES dbo.SiteUser (Id)
GO
ALTER TABLE dbo.Upvote CHECK CONSTRAINT FK_Upvote_SiteUser
GO
ALTER TABLE dbo.Upvote ADD CONSTRAINT FK_Upvote_UserPost FOREIGN KEY(UserPostId)
REFERENCES dbo.UserPost(Id)
GO
ALTER TABLE dbo.Upvote CHECK CONSTRAINT FK_Upvote_UserPost
GO