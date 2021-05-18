/*
1.	What is a result set?
An SQL result set is a set of rows from a database, as well as metadata about the query such as the column names, and the types and sizes of each column.
2.	What is the difference between Union and Union All?
	1) Union drops duplicate records while Union All keeps duplicates.
	2) Union will sort the results set based on the first column of the first select statement.
	3) Union performs slower than Union All due to the sorting.
	4) Union cannot be used with recursive CTE, but Union All can be used with recursive CTE
3.	What are the other Set Operators SQL Server has?
INTERSECT returns any distinct values that are returned by both the query on the left and right sides of the INTERSECT operand.
EXCEPT query returns all rows which are in the first query but those are not returned in the second query.
4.	What is the difference between Union and Join?
UNION in SQL is used to combine the result-set of two or more SELECT statements. The data combined using UNION statement is into results into new distinct rows. JOIN combines data from many tables based on a matched condition between them. It combines data into new columns.
5.	What is the difference between INNER JOIN and FULL JOIN?
Inner join returns only the matching rows between both the tables, non-matching rows are eliminated. Full Join or Full Outer Join returns all rows from both the tables (left & right tables), including non-matching rows from both the tables.
6.	What is difference between left join and outer join?
Left join is one of outer join. Outer join has three types: Left Outer Join, Right Outer Join, and Full Outer Join
7.	What is cross join?
The CROSS JOIN is used to generate a paired combination of each row of the first table with each row of the second table. This join type is also known as cartesian join.
8.	What is the difference between WHERE clause and HAVING clause?
The difference between WHERE and HAVING clause are: The WHERE clause is used to filter rows before the grouping is performed. The HAVING clause is used to filter rows after the grouping is performed. It often includes the result of aggregate functions and is used with GROUP BY.
9.	Can there be multiple group by columns?
Yes. 
*/

USE AdventureWorks2019
----- 1 -----
--504
SELECT COUNT(*) FROM Production.Product 

----- 2 -----
--295
SELECT COUNT(*) 
FROM Production.Product 
WHERE ProductSubcategoryID IS NOT NULL

----- 3 -----
SELECT ProductSubcategoryID, COUNT(*) AS 'CountedProducts'
FROM Production.Product 
GROUP BY ProductSubcategoryID

----- 4 -----
--209
SELECT COUNT(*) 
FROM Production.Product 
WHERE ProductSubcategoryID IS NULL

----- 5 -----
SELECT ProductID, Count(*) AS TheSum
FROM Production.ProductInventory
GROUP BY ProductID

----- 6 -----
SELECT ProductID, Count(*) AS TheSum
FROM Production.ProductInventory
WHERE LocationID = 40
GROUP BY ProductID
HAVING COUNT(*) < 100

----- 7 -----
SELECT Shelf, ProductID, Count(*) AS TheSum
FROM Production.ProductInventory
WHERE LocationID = 40
GROUP BY ProductID, Shelf
HAVING COUNT(*) < 100

----- 8 -----
SELECT ProductID, AVG(Quantity) AS TheAvg
FROM Production.ProductInventory
WHERE LocationID = 10
GROUP BY ProductID

----- 9 -----
SELECT ProductID, Shelf, AVG(Quantity) AS TheAvg
FROM Production.ProductInventory
GROUP BY ProductID, Shelf

----- 10 -----
SELECT ProductID, Shelf, AVG(Quantity) AS TheAvg
FROM Production.ProductInventory
WHERE Shelf IS NOT NULL
GROUP BY ProductID, Shelf

----- 11 -----
SELECT Color, Class, COUNT(*) AS TheCount, AVG(ListPrice) AS AvgPrice
FROM Production.Product
WHERE Color IS NOT NULL AND Class IS NOT NULL
GROUP BY Color, Class

----- 12 -----
SELECT c.Name AS Country, s.Name AS Province
FROM person.CountryRegion c left join person.StateProvince s
ON c.CountryRegionCode = s.CountryRegionCode

----- 13 -----
SELECT c.Name AS Country, s.Name AS Province
FROM person.CountryRegion c left join person.StateProvince s
ON c.CountryRegionCode = s.CountryRegionCode
WHERE c.Name in ('Germany', 'Canada')


USE Northwind
----- 14 -----
SELECT *
FROM dbo.Products p RIGHT JOIN
(SELECT DISTINCT ProductID
FROM dbo.Orders a LEFT JOIN dbo.[Order Details] b
ON a.OrderID = b.OrderID
WHERE YEAR(a.OrderDate) >= YEAR(GETDATE()) - 25) o
ON p.ProductID = o.ProductID

----- 15 -----
SELECT TOP 5 ShipPostalCode, COUNT(*) AS SoldCount
FROM dbo.Orders
GROUP BY ShipPostalCode
ORDER BY COUNT(*) DESC

----- 16 -----
SELECT TOP 5 ShipPostalCode, COUNT(*) AS SoldCount
FROM dbo.Orders
WHERE YEAR(OrderDate) >= YEAR(GETDATE()) - 20
GROUP BY ShipPostalCode
ORDER BY COUNT(*) DESC

----- 17 -----
SELECT City, COUNT(*) AS CustomerCount
FROM dbo.Customers
GROUP BY City

----- 18 -----
SELECT City, COUNT(*) AS CustomerCount
FROM dbo.Customers
GROUP BY City
HAVING COUNT(*) > 10

----- 19 -----
SELECT DISTINCT c.CompanyName
FROM dbo.Orders o LEFT JOIN dbo.Customers c
ON o.CustomerID = c.CustomerID
WHERE DATEDIFF(d, o.OrderDate, '1998-01-01') > 0

----- 20 -----
SELECT c.CompanyName
FROM dbo.Orders o LEFT JOIN dbo.Customers c
ON o.CustomerID = c.CustomerID
ORDER BY DATEDIFF(d, o.OrderDate, GETDATE())

----- 21 -----
SELECT c.CompanyName, SUM(o.Quantity) AS BoughtCount
FROM 
(SELECT c.CompanyName, o.OrderID
FROM dbo.Orders o LEFT JOIN dbo.Customers c
ON o.CustomerID = c.CustomerID) c
LEFT JOIN dbo.[Order Details] o
ON c.OrderID = o.OrderID
GROUP BY c.CompanyName

----- 22 -----
SELECT c.CustomerID, SUM(o.Quantity) AS BoughtCount
FROM 
(SELECT c.CustomerID, o.OrderID
FROM dbo.Orders o LEFT JOIN dbo.Customers c
ON o.CustomerID = c.CustomerID) c
LEFT JOIN dbo.[Order Details] o
ON c.OrderID = o.OrderID
GROUP BY c.CustomerID
HAVING SUM(o.Quantity) > 100

----- 23 -----
SELECT a.CompanyName AS 'Supplier Company Name',
b.CompanyName AS 'Shipping Company Name'
FROM dbo.Suppliers a CROSS JOIN dbo.Shippers b

----- 24 -----
SELECT o.OrderDate, p.ProductName
FROM 
(SELECT o.OrderID, o.OrderDate, d.ProductID
FROM dbo.Orders o LEFT JOIN dbo.[Order Details] d
ON o.OrderID = d.OrderID) o
LEFT JOIN dbo.Products p
ON o.ProductID = o.ProductID

----- 25 -----
SELECT CONCAT(a.FirstName, ' ', a.LastName) AS employee1, 
CONCAT(b.FirstName, ' ', b.LastName) AS employee2,
a.Title
FROM dbo.Employees a INNER JOIN dbo.Employees b
ON a.Title = b.Title
WHERE a.EmployeeID != b.EmployeeID

----- 26 -----
SELECT COUNT(a.EmployeeID) AS NumReportTo,a.EmployeeID, a.LastName, a.FirstName
FROM dbo.Employees a INNER JOIN dbo.Employees b
ON a.EmployeeID = b.ReportsTo
GROUP BY a.EmployeeID, a.LastName, a.FirstName
HAVING COUNT(a.EmployeeID) > 2

----- 27 -----
SELECT City, CompanyName, ContactName, 'Customer' AS 'Type (Customer or Supplier)'
FROM dbo.Customers
UNION 
SELECT City, CompanyName, ContactName, 'Supplier' AS 'Type (Customer or Supplier)'
FROM dbo.Suppliers

----- 28 -----
/*
SELECT a.F1
FROM T1 a INNER JOIN T2 b
ON a.F1 = b.F2

the results will be 
|F1|
|2 |
|3 |
*/

----- 29 -----
/*
SELECT a.F1
FROM T1 a LEFT JOIN T2 b
ON a.F1 = b.F2

the result will be 
|F1|
|1 |
|2 |
|3 |
*/



