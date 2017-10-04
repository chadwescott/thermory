CREATE TABLE [dbo].[MiscellaneousTransactionDetails] (
    [Id]                     UNIQUEIDENTIFIER CONSTRAINT [DF_MiscellaneousTransactionDetail_Id] DEFAULT (newsequentialid()) NOT NULL,
    [InventoryTransactionId] UNIQUEIDENTIFIER NOT NULL,
    [MiscellaneousProductId] UNIQUEIDENTIFIER NOT NULL,
    [PreviousQuantity]       INT              NOT NULL,
    [NewQuantity]            INT              NOT NULL,
    CONSTRAINT [PK_MiscellaneousTransactionDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InventoryTransaction_MiscellaneousTransactionDetail] FOREIGN KEY ([InventoryTransactionId]) REFERENCES [dbo].[InventoryTransactions] ([Id]),
    CONSTRAINT [FK_InventoryTransactionDetail_MiscellaneousProduct] FOREIGN KEY ([MiscellaneousProductId]) REFERENCES [dbo].[MiscellaneousProducts] ([Id])
);

