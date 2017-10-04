CREATE TABLE [dbo].[OrderStatus] (
    [Id]          UNIQUEIDENTIFIER CONSTRAINT [DF_OrderStatuses_Id] DEFAULT (newsequentialid()) NOT NULL,
    [Name]        NVARCHAR (50)    NOT NULL,
    [OrderTypeId] UNIQUEIDENTIFIER NULL,
    [SortOrder]   INT              NULL,
    CONSTRAINT [PK_OrderStatuses] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OrderStatus_OrderTypes] FOREIGN KEY ([OrderTypeId]) REFERENCES [dbo].[OrderTypes] ([Id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_OrderStatus]
    ON [dbo].[OrderStatus]([OrderTypeId] ASC, [SortOrder] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_OrderStatuses]
    ON [dbo].[OrderStatus]([Name] ASC, [OrderTypeId] ASC);

