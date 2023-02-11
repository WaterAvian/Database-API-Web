CREATE PROCEDURE [dbo].GetAllSiteUsers	
AS
	SELECT Id, Firstname, Lastname
	FROM dbo.SiteUser;
RETURN 
