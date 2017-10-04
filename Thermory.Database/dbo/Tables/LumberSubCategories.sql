CREATE TABLE [dbo].[LumberSubCategories] (
    [Id]               UNIQUEIDENTIFIER CONSTRAINT [DF_LumberSubCategory_Id] DEFAULT (newsequentialid()) NOT NULL,
    [LumberCategoryId] UNIQUEIDENTIFIER NOT NULL,
    [Name]             NVARCHAR (50)    NOT NULL,
    [Width]            INT              NOT NULL,
    [Thickness]        INT              NOT NULL,
    [SortOrder]        INT              NOT NULL,
    [BundleSize]       INT              NULL,
    [Weight]           FLOAT (53)       NULL,
    CONSTRAINT [PK_LumberSubCategory] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LumberSubCategory_LumberCategory] FOREIGN KEY ([LumberCategoryId]) REFERENCES [dbo].[LumberCategories] ([Id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_LumberSubCategories_1]
    ON [dbo].[LumberSubCategories]([LumberCategoryId] ASC, [SortOrder] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_LumberSubCategories_2]
    ON [dbo].[LumberSubCategories]([LumberCategoryId] ASC, [Name] ASC, [Width] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_LumberSubCategories_3]
    ON [dbo].[LumberSubCategories]([LumberCategoryId] ASC, [Name] ASC, [Thickness] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_LumberSubCategories]
    ON [dbo].[LumberSubCategories]([LumberCategoryId] ASC, [Name] ASC);

