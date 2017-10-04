CREATE TABLE [dbo].[Orders] (
    [Id]              UNIQUEIDENTIFIER CONSTRAINT [DF_Orders_Id] DEFAULT (newsequentialid()) NOT NULL,
    [OrderNumber]     VARCHAR (50)     NOT NULL,
    [CustomerId]      UNIQUEIDENTIFIER NULL,
    [OrderStatusId]   UNIQUEIDENTIFIER NULL,
    [OrderTypeId]     UNIQUEIDENTIFIER NOT NULL,
    [PackagingTypeId] UNIQUEIDENTIFIER NULL,
    [MinutesToPull]   INT              NULL,
    [MinutesToLoad]   INT              NULL,
    [ShipDate]        DATE             NULL,
    [Notes]           NVARCHAR (MAX)   NULL,
    [ShipToAddressId] UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Orders_CustomerAddresses] FOREIGN KEY ([ShipToAddressId]) REFERENCES [dbo].[Addresses] ([Id]),
    CONSTRAINT [FK_Orders_Customers] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customers] ([Id]),
    CONSTRAINT [FK_Orders_OrderStatuses] FOREIGN KEY ([OrderStatusId]) REFERENCES [dbo].[OrderStatus] ([Id]),
    CONSTRAINT [FK_Orders_OrderTypes] FOREIGN KEY ([OrderTypeId]) REFERENCES [dbo].[OrderTypes] ([Id]),
    CONSTRAINT [FK_Orders_PackagingTypes] FOREIGN KEY ([PackagingTypeId]) REFERENCES [dbo].[PackagingTypes] ([Id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_OrdersOrderNumber]
    ON [dbo].[Orders]([OrderNumber] ASC);

