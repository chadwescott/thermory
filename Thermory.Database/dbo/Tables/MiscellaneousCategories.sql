CREATE TABLE [dbo].[MiscellaneousCategories] (
    [Id]        UNIQUEIDENTIFIER CONSTRAINT [DF_MiscellaneousCategory_Id] DEFAULT (newsequentialid()) NOT NULL,
    [Name]      NVARCHAR (50)    NOT NULL,
    [SortOrder] INT              NOT NULL,
    CONSTRAINT [PK_MiscellaneousCategory] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_MiscellaneousCategory]
    ON [dbo].[MiscellaneousCategories]([SortOrder] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_MiscellaneousCategories]
    ON [dbo].[MiscellaneousCategories]([Name] ASC);

