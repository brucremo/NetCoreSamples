CREATE TABLE [dbo].[City] 
(
    [Id] INT IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (100) NOT NULL,
    [StateProvinceId] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_City_StateProvince] FOREIGN KEY ([StateProvinceId]) REFERENCES [dbo].[StateProvince] ([Id])
);

