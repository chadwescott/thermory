CREATE TABLE [dbo].[LumberCategories] (
    [Id]        UNIQUEIDENTIFIER CONSTRAINT [DF_LumberCategory_Id] DEFAULT (newsequentialid()) NOT NULL,
    [Name]      NVARCHAR (50)    NOT NULL,
    [SortOrder] INT              NOT NULL,
    CONSTRAINT [PK_LumberCategory] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_LumberCategories_1]
    ON [dbo].[LumberCategories]([SortOrder] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_LumberCategories]
    ON [dbo].[LumberCategories]([Name] ASC);

