CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Username] NVARCHAR(100) NOT NULL,
	[StateProvinceId] INT NOT NULL,
	CONSTRAINT [FK_User_StateProvince] FOREIGN KEY ([StateProvinceId]) REFERENCES [dbo].[StateProvince]([Id])
)
