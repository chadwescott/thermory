CREATE VIEW [dbo].[InventoryTransaction_V]
AS
SELECT
	T.Id AS TransactionId,
	T.CreatedOn,
	T.OrderId,
	U.UserName,
	U.FirstName,
	U.LastName,
	TT.Name AS TransactionType,
	O.OrderNumber,
	OT.Name AS OrderType,
	LC.Name AS LumberCategory,
	LS.Name AS LumberSubCategory,
	LT.Name AS LumberType,
	LP.[Length] AS LengthInMillimeters,
	LS.Width AS WidthInMillimeters,
	LD.PreviousQuantity AS PreviousLumberQuantity,
	LD.NewQuantity AS NewLumberQuantity,
	LD.PreviousQuantity - LD.NewQuantity AS LumberQuantityDelta,
	MC.Name AS MiscellaneousCategory,
	MS.Name AS MiscellaneousSubCategory,
	MP.Name AS MiscellaneousProduct,
	MD.PreviousQuantity AS PreviousMiscellaneousQuantity,
	MD.NewQuantity AS NewMiscellaneousQuantity,
	MD.PreviousQuantity - MD.NewQuantity AS MiscellaneousQuantityDelta
FROM
	dbo.InventoryTransactions AS T
	INNER JOIN dbo.UserProfile AS U ON T.UserId = U.UserId
	INNER JOIN dbo.TransactionTypes AS TT ON T.TransactionTypeId = TT.Id
	LEFT JOIN dbo.Orders AS O ON T.OrderId = O.Id
	LEFT JOIN dbo.OrderTypes AS OT ON O.OrderTypeId = OT.Id
	LEFT JOIN dbo.LumberTransactionDetails AS LD ON T.Id = LD.InventoryTransactionId
	LEFT JOIN dbo.LumberProducts AS LP ON LD.LumberProductId = LP.Id
	LEFT JOIN dbo.LumberTypes AS LT ON LP.LumberTypeId = LT.Id
	LEFT JOIN dbo.LumberSubCategories AS LS ON LT.LumberSubCategoryId = LS.Id
	LEFT JOIN dbo.LumberCategories AS LC ON LS.LumberCategoryId = LC.Id
	LEFT JOIN dbo.MiscellaneousTransactionDetails AS MD ON T.Id = MD.InventoryTransactionId
	LEFT JOIN dbo.MiscellaneousProducts AS MP ON MD.MiscellaneousProductId = MP.Id
	LEFT JOIN dbo.MiscellaneousSubCategories AS MS ON MP.MiscellaneousSubCategoryId = MS.Id
	LEFT JOIN dbo.MiscellaneousCategories AS MC ON MS.MiscellaneousCategoryId = MC.Id