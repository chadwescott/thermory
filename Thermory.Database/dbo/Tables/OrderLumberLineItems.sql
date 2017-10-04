CREATE TABLE [dbo].[OrderLumberLineItems] (
    [Id]              UNIQUEIDENTIFIER CONSTRAINT [DF_OrderLumberQuantities_Id] DEFAULT (newsequentialid()) NOT NULL,
    [OrderId]         UNIQUEIDENTIFIER NOT NULL,
    [LumberProductId] UNIQUEIDENTIFIER NOT NULL,
    [Quantity]        INT              NOT NULL,
    CONSTRAINT [PK_OrderLumberLineItems] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OrderLumberLineItems_LumberProducts] FOREIGN KEY ([LumberProductId]) REFERENCES [dbo].[LumberProducts] ([Id]),
    CONSTRAINT [FK_OrderLumberLineItems_Orders] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Orders] ([Id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_OrderLumberLineItems]
    ON [dbo].[OrderLumberLineItems]([OrderId] ASC, [LumberProductId] ASC);

