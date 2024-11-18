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

IF NOT EXISTS (SELECT * FROM [dbo].[Country])
BEGIN
    INSERT INTO [dbo].[Country] ([Name]) VALUES (N'United States');
    INSERT INTO [dbo].[Country] ([Name]) VALUES (N'Canada');
END

IF NOT EXISTS (SELECT * FROM [dbo].[StateProvince])
BEGIN
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Alabama', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Florida', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'New York', 1);

    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Alberta', 2);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Ontario', 2);
END

IF NOT EXISTS (SELECT * FROM [dbo].[User])
BEGIN
    INSERT INTO [dbo].[User] ([Username], [StateProvinceId]) VALUES (N'John Doe', 1);
    INSERT INTO [dbo].[User] ([Username], [StateProvinceId]) VALUES (N'John McDoe', 5);
END