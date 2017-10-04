CREATE TABLE [dbo].[LumberProducts] (
    [Id]                    UNIQUEIDENTIFIER CONSTRAINT [DF_LumberProduct_Id] DEFAULT (newsequentialid()) NOT NULL,
    [Length]                INT              NOT NULL,
    [IsActive]              BIT              CONSTRAINT [DF_LumberProduct_IsActive] DEFAULT ((1)) NOT NULL,
    [LumberTypeId]          UNIQUEIDENTIFIER NULL,
    [Quantity]              INT              CONSTRAINT [DF_LumberProduct_Quantity] DEFAULT ((0)) NOT NULL,
    [IncludeInCalculations] BIT              CONSTRAINT [DF_LumberProducts_IncludeInCalculations] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_LumberProduct] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LumberProduct_LumberType] FOREIGN KEY ([LumberTypeId]) REFERENCES [dbo].[LumberTypes] ([Id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_LumberProducts]
    ON [dbo].[LumberProducts]([LumberTypeId] ASC, [Length] ASC);

