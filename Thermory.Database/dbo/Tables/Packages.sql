CREATE TABLE [dbo].[Packages] (
    [Id]            UNIQUEIDENTIFIER CONSTRAINT [DF_OrderPackages_Id] DEFAULT (newsequentialid()) NOT NULL,
    [OrderId]       UNIQUEIDENTIFIER NOT NULL,
    [PackageNumber] INT              NOT NULL,
    [Height]        FLOAT (53)       NULL,
    [Length]        FLOAT (53)       NULL,
    [Width]         FLOAT (53)       NULL,
    CONSTRAINT [PK_Packages] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OrderPackages_Orders] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Orders] ([Id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_OrderPackages]
    ON [dbo].[Packages]([OrderId] ASC, [PackageNumber] ASC);

