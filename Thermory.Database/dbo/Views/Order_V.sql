CREATE VIEW [dbo].[Order_V]
AS
SELECT        dbo.Orders.Id AS OrderId, dbo.Orders.OrderNumber, dbo.OrderTypes.Name AS OrderType, dbo.UserProfile.FirstName + ' ' + dbo.UserProfile.LastName AS CreatedBy,
                          dbo.InventoryTransactions.CreatedOn, dbo.Customers.Name AS CustomerName, dbo.OrderStatus.Name AS STATUS
FROM            dbo.Orders INNER JOIN
                         dbo.OrderTypes ON dbo.Orders.OrderTypeId = dbo.OrderTypes.Id INNER JOIN
                         dbo.InventoryTransactions ON dbo.Orders.Id = dbo.InventoryTransactions.OrderId INNER JOIN
                         dbo.TransactionTypes ON dbo.InventoryTransactions.TransactionTypeId = dbo.TransactionTypes.Id INNER JOIN
                         dbo.UserProfile ON dbo.InventoryTransactions.UserId = dbo.UserProfile.UserId INNER JOIN
                         dbo.OrderStatus ON dbo.Orders.OrderStatusId = dbo.OrderStatus.Id LEFT OUTER JOIN
                         dbo.Customers ON dbo.Orders.CustomerId = dbo.Customers.Id
WHERE        (dbo.TransactionTypes.Name = 'OrderCreate')