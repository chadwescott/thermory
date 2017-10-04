CREATE TABLE [dbo].[MiscellaneousProducts] (
    [Id]                         UNIQUEIDENTIFIER CONSTRAINT [DF_MiscellaneousProduct_Id] DEFAULT (newsequentialid()) NOT NULL,
    [MiscellaneousSubCategoryId] UNIQUEIDENTIFIER NOT NULL,
    [Name]                       NVARCHAR (50)    NOT NULL,
    [Description]                NVARCHAR (50)    NULL,
    [SortOrder]                  INT              NOT NULL,
    [Quantity]                   INT              CONSTRAINT [DF_MiscellaneousProduct_Quantity] DEFAULT ((0)) NOT NULL,
    [Weight]                     FLOAT (53)       NULL,
    CONSTRAINT [PK_MiscellaneousProduct] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MiscellaneousProduct_MiscellaneousSubCategory] FOREIGN KEY ([MiscellaneousSubCategoryId]) REFERENCES [dbo].[MiscellaneousSubCategories] ([Id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_MiscellaneousProduct]
    ON [dbo].[MiscellaneousProducts]([MiscellaneousSubCategoryId] ASC, [SortOrder] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_MiscellaneousProducts]
    ON [dbo].[MiscellaneousProducts]([MiscellaneousSubCategoryId] ASC, [Name] ASC);

