CREATE TABLE [dbo].[OrderMiscellaneousLineItems] (
    [Id]                     UNIQUEIDENTIFIER CONSTRAINT [DF_OrderMiscellaneousQuantities_Id] DEFAULT (newsequentialid()) NOT NULL,
    [OrderId]                UNIQUEIDENTIFIER NOT NULL,
    [MiscellaneousProductId] UNIQUEIDENTIFIER NOT NULL,
    [Quantity]               INT              NOT NULL,
    CONSTRAINT [PK_OrderMiscellaneousLineItems] PRIMARY KEY CLUSTERED ([OrderId] ASC, [MiscellaneousProductId] ASC),
    CONSTRAINT [FK_OrderMiscellaneousLineItems_MiscellaneousProducts] FOREIGN KEY ([MiscellaneousProductId]) REFERENCES [dbo].[MiscellaneousProducts] ([Id]),
    CONSTRAINT [FK_OrderMiscellaneousLineItems_Orders] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Orders] ([Id])
);

