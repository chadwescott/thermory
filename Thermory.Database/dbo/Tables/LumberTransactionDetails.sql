CREATE TABLE [dbo].[LumberTransactionDetails] (
    [Id]                     UNIQUEIDENTIFIER CONSTRAINT [DF_LumberTransactionDetail_Id] DEFAULT (newsequentialid()) NOT NULL,
    [InventoryTransactionId] UNIQUEIDENTIFIER NOT NULL,
    [LumberProductId]        UNIQUEIDENTIFIER NOT NULL,
    [PreviousQuantity]       INT              NOT NULL,
    [NewQuantity]            INT              NOT NULL,
    CONSTRAINT [PK_LumberTransactionDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InventoryTransaction_LumberTransactionDetail] FOREIGN KEY ([InventoryTransactionId]) REFERENCES [dbo].[InventoryTransactions] ([Id]),
    CONSTRAINT [FK_LumberTransactionDetail_LumberProduct] FOREIGN KEY ([LumberProductId]) REFERENCES [dbo].[LumberProducts] ([Id])
);

