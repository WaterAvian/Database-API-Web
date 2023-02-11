CREATE PROCEDURE dbo.UpsertSiteUser
	@Id int,
	@Firstname varchar(50),
	@Lastname varchar(50),
	@Password varchar(50)
AS
IF IsNull(@Id, 0) = 0
	INSERT INTO SiteUser (Firstname, Lastname, Password)
	VALUES (@Firstname, @Lastname, @password);
ELSE
	UPDATE SiteUser
	SET Firstname = @Firstname, LastName = @Lastname
	WHERE Id = @Id;
