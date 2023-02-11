CREATE PROCEDURE [dbo].GetAllPostGroups
AS
	SELECT Id, SiteUserId, GroupName
	FROM dbo.PostGroup;
RETURN 
