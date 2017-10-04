CREATE TABLE [dbo].[PackagingTypes] (
    [Id]     UNIQUEIDENTIFIER CONSTRAINT [DF_PackagingTypes_Id] DEFAULT (newsequentialid()) NOT NULL,
    [Name]   NVARCHAR (50)    NOT NULL,
    [Weight] FLOAT (53)       NULL,
    CONSTRAINT [PK_PackagingTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_PackagingTypes]
    ON [dbo].[PackagingTypes]([Name] ASC);

