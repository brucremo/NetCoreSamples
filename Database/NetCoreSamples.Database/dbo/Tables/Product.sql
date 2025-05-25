CREATE TABLE [dbo].[Product]
(
    [Id] INT IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (100) NOT NULL,
    [Description] NVARCHAR (MAX) NOT NULL,
    [Price] DECIMAL (18, 2) NOT NULL,
    [ImageUrl] NVARCHAR (MAX) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
