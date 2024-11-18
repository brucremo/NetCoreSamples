CREATE TABLE [dbo].[User] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [Username]        NVARCHAR (100) NOT NULL,
    [StateProvinceId] INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_User_StateProvince] FOREIGN KEY ([StateProvinceId]) REFERENCES [dbo].[StateProvince] ([Id])
);

