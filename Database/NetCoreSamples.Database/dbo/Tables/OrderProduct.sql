CREATE TABLE [dbo].[OrderProduct]
(
	[OrderId] INT NOT NULL,
	[ProductId] INT NOT NULL,
	[Quantity] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([OrderId], [ProductId]),
    CONSTRAINT [FK_OrderProduct_Order] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Order] ([Id]),
    CONSTRAINT [FK_OrderProduct_Product] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([Id])
)
