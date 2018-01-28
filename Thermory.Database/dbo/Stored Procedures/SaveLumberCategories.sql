﻿-- =============================================
-- Author:		Chad Wescott
-- Create date: 1/3/2018
-- Description:	Update Lumber Categories and reorder when sort order changes
-- =============================================
CREATE PROCEDURE [SaveLumberCategories]
	@id UNIQUEIDENTIFIER,
	@name NVARCHAR(50),
	@sortOrder int
AS
BEGIN
	IF @id = '00000000-0000-0000-0000-000000000000'
	BEGIN
		BEGIN TRAN
			UPDATE [LumberCategories] SET [SortOrder] = [SortOrder] + 1 WHERE [SortOrder] >= @sortOrder
			INSERT [LumberCategories] ([Name], [SortOrder]) VALUES (@name, @sortOrder)
		COMMIT TRAN
	END
	ELSE
	BEGIN
		DECLARE @currentSortOrder int
		SELECT @currentSortOrder = [SortOrder] FROM [LumberCategories] WHERE [Id] = @id
	
		IF @currentSortOrder IS NULL
			RETURN
	
		IF @currentSortOrder = @sortOrder
			UPDATE [LumberCategories] SET [Name] = @name WHERE [Id] = @id
		ELSE
		BEGIN
			BEGIN TRAN
				UPDATE [LumberCategories] SET [SortOrder] = -1 WHERE [Id] = @id

				IF @currentSortOrder < @sortOrder
					UPDATE [LumberCategories] SET [SortOrder] = [SortOrder] - 1 WHERE [SortOrder] > @currentSortOrder AND [SortOrder] <= @sortOrder
				ELSE IF @currentSortOrder > @sortOrder
					UPDATE [LumberCategories] SET [SortOrder] = [SortOrder] + 1 WHERE [SortOrder] >= @sortOrder AND [SortOrder] < @currentSortOrder

				UPDATE [LumberCategories] SET [Name] = @name, [SortOrder] = @sortOrder WHERE [Id] = @id
			COMMIT TRAN
		END
	END
END