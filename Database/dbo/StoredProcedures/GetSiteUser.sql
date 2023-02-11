CREATE PROCEDURE [dbo].[GetSiteUser]
	@Id int, 
	@Firstname varchar(50) output,
	@Lastname varchar(50) output
AS
	SELECT @Firstname = Firstname, @Lastname = Lastname 
	FROM dbo.SiteUser
	WHERE Id = @Id
