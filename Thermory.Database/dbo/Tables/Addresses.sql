CREATE TABLE [dbo].[Addresses] (
    [Id]           UNIQUEIDENTIFIER CONSTRAINT [DF_Addresses_Id] DEFAULT (newsequentialid()) NOT NULL,
    [CustomerId]   UNIQUEIDENTIFIER NOT NULL,
    [Name]         NVARCHAR (100)   NOT NULL,
    [AddressLine1] NVARCHAR (500)   NULL,
    [AddressLine2] NVARCHAR (500)   NULL,
    CONSTRAINT [PK_Addresses] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Addresses_Customers] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customers] ([Id]),
    CONSTRAINT [IX_Addresses] UNIQUE NONCLUSTERED ([CustomerId] ASC, [Name] ASC)
);

