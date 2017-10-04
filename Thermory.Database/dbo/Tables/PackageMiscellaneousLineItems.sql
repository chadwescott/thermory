CREATE TABLE [dbo].[PackageMiscellaneousLineItems] (
    [Id]                     UNIQUEIDENTIFIER CONSTRAINT [DF_PackageMiscellaneousLineItems_Id] DEFAULT (newsequentialid()) NOT NULL,
    [PackageId]              UNIQUEIDENTIFIER NOT NULL,
    [MiscellaneousProductId] UNIQUEIDENTIFIER NOT NULL,
    [Quantity]               INT              NOT NULL,
    CONSTRAINT [PK_PackageMiscellaneousLineItems] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PackageMiscellaneousLineItems_MiscellaneousProducts] FOREIGN KEY ([MiscellaneousProductId]) REFERENCES [dbo].[MiscellaneousProducts] ([Id]),
    CONSTRAINT [FK_PackageMiscellaneousLineItems_Packages] FOREIGN KEY ([PackageId]) REFERENCES [dbo].[Packages] ([Id]),
    CONSTRAINT [IX_PackageMiscellaneousLineItems] UNIQUE NONCLUSTERED ([PackageId] ASC, [MiscellaneousProductId] ASC)
);

