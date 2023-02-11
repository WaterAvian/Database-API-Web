/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

--SET IDENTITY_INSERT SiteUser ON;
--GO
--
--INSERT INTO SiteUser (Id, Firstname, Lastname, Password)
--VALUES
--	(1, 'Bird', 'Lake', 'lakePass')
--	, (2, 'Mick', 'Owen', 'mickPass')
--
--SET IDENTITY_INSERT dbo.SiteUser OFF;
--GO

SET IDENTITY_INSERT PostGroup ON;
GO

INSERT INTO PostGroup (Id, SiteUserId, GroupName, Description)
VALUES
	(1, 1, 'General', 'Default Group')

SET IDENTITY_INSERT dbo.PostGroup OFF;
GO