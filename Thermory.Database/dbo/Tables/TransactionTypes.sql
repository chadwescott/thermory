CREATE TABLE [dbo].[TransactionTypes] (
    [Id]          UNIQUEIDENTIFIER CONSTRAINT [DF_TransactionTypes_Id] DEFAULT (newsequentialid()) NOT NULL,
    [Name]        NVARCHAR (50)    NOT NULL,
    [Description] NVARCHAR (50)    NOT NULL,
    CONSTRAINT [PK_TransactionTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_TransactionTypes]
    ON [dbo].[TransactionTypes]([Name] ASC);

