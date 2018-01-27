-- =============================================
-- Author:		Chad Wescott
-- Create date: 1/27/2018
-- Description:	Update Lumber SubCategories and reorder when sort order changes
-- =============================================
CREATE PROCEDURE [SaveLumberSubCategories]
	@id UNIQUEIDENTIFIER,
	@categoryId UNIQUEIDENTIFIER,
	@name NVARCHAR(50),
	@width int,
	@thickness int,
	@sortOrder int,
	@bundleSize int,
	@weight float
AS
BEGIN TRAN
	IF @id = '00000000-0000-0000-0000-000000000000'
	BEGIN
		UPDATE [LumberSubCategories] SET [SortOrder] = [SortOrder] + 1 WHERE [SortOrder] >= @sortOrder
		INSERT [LumberSubCategories] ([Name], [Width], [Thickness], [SortOrder], [BundleSize], [Weight])
		VALUES (@name, @width, @thickness, @sortOrder, @bundleSize, @weight)
	END
	ELSE
	BEGIN
		DECLARE @currentSortOrder int
		SELECT @currentSortOrder = [SortOrder] FROM [LumberSubCategories] WHERE [Id] = @id
	
		IF @currentSortOrder IS NULL
			RETURN
	
		IF @currentSortOrder = @sortOrder
			UPDATE [LumberSubCategories]
			SET
				[Name] = @name,
				[Width] = @width,
				[Thickness] = @thickness,
				[BundleSize] = @bundleSize,
				[Weight] = @weight
			WHERE [Id] = @id
		ELSE
		BEGIN
			UPDATE [LumberSubCategories] SET [SortOrder] = -1 WHERE [Id] = @id

			IF @currentSortOrder < @sortOrder
				UPDATE [LumberSubCategories] SET [SortOrder] = [SortOrder] - 1 WHERE [SortOrder] > @currentSortOrder AND [SortOrder] <= @sortOrder
			ELSE IF @currentSortOrder > @sortOrder
				UPDATE [LumberSubCategories] SET [SortOrder] = [SortOrder] + 1 WHERE [SortOrder] >= @sortOrder AND [SortOrder] < @currentSortOrder

			UPDATE [LumberSubCategories] SET [Name] = @name, [SortOrder] = @sortOrder WHERE [Id] = @id
		END
	END
COMMIT TRAN