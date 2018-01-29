-- =============================================
-- Author:		Chad Wescott
-- Create date: 1/29/2018
-- Description:	Save Lumber Types and reorder when sort order changes
-- =============================================
CREATE PROCEDURE [SaveLumberTypes]
	@id UNIQUEIDENTIFIER,
	@subCategoryId UNIQUEIDENTIFIER,
	@name NVARCHAR(50),
	@sortOrder int
AS
BEGIN
	IF @id = '00000000-0000-0000-0000-000000000000'
	BEGIN
		BEGIN TRAN
			UPDATE [LumberTypes] SET [SortOrder] = [SortOrder] + 1 WHERE [SortOrder] >= @sortOrder
			INSERT [LumberTypes] ([LumberSubCategoryId], [Name], [SortOrder]) VALUES (@subCategoryId, @name, @sortOrder)
		COMMIT TRAN
	END
	ELSE
	BEGIN
		DECLARE @currentSortOrder int
		SELECT @currentSortOrder = [SortOrder] FROM [LumberTypes] WHERE [Id] = @id
	
		IF @currentSortOrder IS NULL
			RETURN
	
		IF @currentSortOrder = @sortOrder
			UPDATE [LumberTypes] SET [Name] = @name WHERE [Id] = @id
		ELSE
		BEGIN
			BEGIN TRAN
				UPDATE [LumberTypes] SET [SortOrder] = -1 WHERE [Id] = @id

				IF @currentSortOrder < @sortOrder
					UPDATE [LumberTypes] SET [SortOrder] = [SortOrder] - 1 WHERE [SortOrder] > @currentSortOrder AND [SortOrder] <= @sortOrder
				ELSE IF @currentSortOrder > @sortOrder
					UPDATE [LumberTypes] SET [SortOrder] = [SortOrder] + 1 WHERE [SortOrder] >= @sortOrder AND [SortOrder] < @currentSortOrder

				UPDATE [LumberTypes] SET [Name] = @name, [SortOrder] = @sortOrder WHERE [Id] = @id
			COMMIT TRAN
		END
	END
END