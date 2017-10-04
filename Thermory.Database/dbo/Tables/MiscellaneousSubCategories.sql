CREATE TABLE [dbo].[MiscellaneousSubCategories] (
    [Id]                      UNIQUEIDENTIFIER CONSTRAINT [DF_MiscellaneousSubCategory_Id] DEFAULT (newsequentialid()) NOT NULL,
    [MiscellaneousCategoryId] UNIQUEIDENTIFIER NOT NULL,
    [Name]                    NVARCHAR (50)    NOT NULL,
    [SortOrder]               INT              NOT NULL,
    CONSTRAINT [PK_MiscellaneousSubCategory] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MiscellaneousSubCategory_MiscellaneousCategory] FOREIGN KEY ([MiscellaneousCategoryId]) REFERENCES [dbo].[MiscellaneousCategories] ([Id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_MiscellaneousSubCategory]
    ON [dbo].[MiscellaneousSubCategories]([MiscellaneousCategoryId] ASC, [SortOrder] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_MiscellaneousSubCategories]
    ON [dbo].[MiscellaneousSubCategories]([MiscellaneousCategoryId] ASC, [Name] ASC);

