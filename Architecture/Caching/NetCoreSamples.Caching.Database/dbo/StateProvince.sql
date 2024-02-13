CREATE TABLE [dbo].[StateProvince]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(100) NOT NULL,
	[CountryId] INT NOT NULL,
	CONSTRAINT [FK_StateProvince_Country] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Country]([Id])
)
