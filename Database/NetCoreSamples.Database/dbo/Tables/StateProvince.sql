CREATE TABLE [dbo].[StateProvince] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (100) NOT NULL,
    [CountryId] INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_StateProvince_Country] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Country] ([Id])
);

