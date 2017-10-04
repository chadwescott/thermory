CREATE TABLE [dbo].[PackageLumberLineItems] (
    [Id]              UNIQUEIDENTIFIER CONSTRAINT [DF_OrderPackageLumberLineItems_Id] DEFAULT (newsequentialid()) NOT NULL,
    [PackageId]       UNIQUEIDENTIFIER NOT NULL,
    [LumberProductId] UNIQUEIDENTIFIER NOT NULL,
    [Quantity]        INT              NOT NULL,
    CONSTRAINT [PK_PackageLumberLineItems] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OrderPackageLumberLineItems_LumberProducts] FOREIGN KEY ([LumberProductId]) REFERENCES [dbo].[LumberProducts] ([Id]),
    CONSTRAINT [FK_OrderPackageLumberLineItems_OrderPackages] FOREIGN KEY ([PackageId]) REFERENCES [dbo].[Packages] ([Id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_PackageLumberLineItems]
    ON [dbo].[PackageLumberLineItems]([LumberProductId] ASC, [PackageId] ASC);

