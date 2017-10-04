CREATE TABLE [dbo].[InventoryTransactions] (
    [Id]                UNIQUEIDENTIFIER CONSTRAINT [DF_InventoryTransaction_Id] DEFAULT (newsequentialid()) NOT NULL,
    [TransactionTypeId] UNIQUEIDENTIFIER NOT NULL,
    [UserId]            INT              NOT NULL,
    [CreatedOn]         DATETIME         CONSTRAINT [DF_InventoryTransaction_CreatedOn] DEFAULT (getdate()) NOT NULL,
    [OrderId]           UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_InventoryTransaction] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InventoryTransaction_UserProfile] FOREIGN KEY ([UserId]) REFERENCES [dbo].[UserProfile] ([UserId]),
    CONSTRAINT [FK_InventoryTransactions_Orders] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Orders] ([Id]),
    CONSTRAINT [FK_InventoryTransactions_TransactionTypes] FOREIGN KEY ([TransactionTypeId]) REFERENCES [dbo].[TransactionTypes] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_InventoryTransaction_CreatedOn]
    ON [dbo].[InventoryTransactions]([CreatedOn] ASC);

