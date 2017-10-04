CREATE TABLE [dbo].[OrderTypes] (
    [Id]   UNIQUEIDENTIFIER CONSTRAINT [DF_OrderTypes_Id] DEFAULT (newsequentialid()) NOT NULL,
    [Name] NVARCHAR (50)    NOT NULL,
    CONSTRAINT [PK_OrderTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_OrderTypes]
    ON [dbo].[OrderTypes]([Name] ASC);

