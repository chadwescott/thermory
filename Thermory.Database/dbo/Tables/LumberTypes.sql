CREATE TABLE [dbo].[LumberTypes] (
    [Id]                  UNIQUEIDENTIFIER CONSTRAINT [DF_LumberType_Id] DEFAULT (newsequentialid()) NOT NULL,
    [LumberSubCategoryId] UNIQUEIDENTIFIER NOT NULL,
    [Name]                NVARCHAR (50)    NOT NULL,
    [SortOrder]           INT              NOT NULL,
    CONSTRAINT [PK_LumberType] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LumberType_LumberSubCategory] FOREIGN KEY ([LumberSubCategoryId]) REFERENCES [dbo].[LumberSubCategories] ([Id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_LumberTypes_1]
    ON [dbo].[LumberTypes]([LumberSubCategoryId] ASC, [SortOrder] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_LumberTypes]
    ON [dbo].[LumberTypes]([LumberSubCategoryId] ASC, [Name] ASC);

