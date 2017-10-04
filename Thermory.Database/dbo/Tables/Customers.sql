CREATE TABLE [dbo].[Customers] (
    [Id]   UNIQUEIDENTIFIER CONSTRAINT [DF_Customers_Id] DEFAULT (newsequentialid()) NOT NULL,
    [Name] NVARCHAR (100)   NOT NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Customers]
    ON [dbo].[Customers]([Name] ASC);

