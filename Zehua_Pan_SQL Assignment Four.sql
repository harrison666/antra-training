--Assignment Day4 SQL
--Zehua Pan

/*
----- 1 -----
A view is a virtual table based on the result-set of an SQL statement. It does not have its own data, it just uses other tables' data.
Benefits:
1)Views hide data complexity. 
2)Views add security by restricting access to the columns of a table. 
3)Views simplify queries for the user. It is way to isolate application from changes in definitions of base tables.

----- 2 -----
Yes

----- 3 -----
 A stored procedure is a prepared SQL code that you can save, so the code can be reused over and over again. 
 Benefits:
 Stored procedures provide several advantages including better performance, higher productivity, ease of use, and increased scalability.

 ----- 4 -----
 View is simple showcasing data stored in the database tables whereas a stored procedure is a group of statements that can be executed. 
 A view is faster as it displays data from the tables referenced whereas a store procedure executes sql statements. A view is a simple way to save a complex SELECT in the database.

 ----- 5 -----
 The function must return a value but in Stored Procedure it is optional.
 Functions can have only input parameters for it whereas Procedures can have input or output parameters. 
 Functions can be called from Procedure whereas Procedures cannot be called from a Function.

 ----- 6 -----
 Yes

 ----- 7 -----
 Yes. If the stored procedure returns a result set, then it can be executed implicitly within a SELECT statement.

 ----- 8 -----
Triggers are a special type of stored procedure that get executed (fired) when a specific event happens.
Types of triggers: Insert trigger, Delete trigger, Update trigger, Instead of trigger

 ----- 9 -----
1)Create an audit trail of activity in the database. For example, you can track updates to the orders table by updating corroborating information to an audit table.
2)Implement a business rule. 
3)Derive additional data that is not available within a table or within the database. ...
4)Enforce referential integrity.

 ----- 10 -----
 Trigger and Procedure both perform a specified task on their execution. 
 The fundamental difference between Trigger and Procedure is that the Trigger executes automatically on occurrences of an event whereas, 
 the Procedure is executed when it is explicitly invoked.

 */

 USE Northwind

----- 1 -----
BEGIN TRAN
INSERT INTO Region VALUES
(5, 'Middle Earth')

INSERT INTO Territories VALUES
('00000', 'Gondor', 5)

INSERT INTO Employees (FirstName, LastName) VALUES 
('Aragorn', 'King')

DECLARE @employID INT
SELECT @employID = EmployeeID FROM Employees WHERE FirstName = 'Aragorn' AND LastName = 'King'
INSERT INTO EmployeeTerritories (EmployeeID, TerritoryID) VALUES 
(@employID, '00000')

--SELECT * FROM Employees
----- 2 -----
UPDATE Territories
SET TerritoryDescription = 'Arnor' WHERE TerritoryDescription = 'Gondor'

----- 3 -----
DELETE FROM EmployeeTerritories WHERE EmployeeID = @employID
DELETE FROM Employees WHERE EmployeeID = @employID
DELETE FROM Territories WHERE TerritoryDescription = 'Arnor'
DELETE FROM Region WHERE RegionID = 5
ROLLBACK

----- 4 -----
CREATE VIEW view_product_order_pan AS
SELECT p.ProductID, p.ProductName, SUM(od.Quantity) AS Total 
FROM Products p LEFT JOIN [Order Details] od
ON p.ProductID = od.ProductID
GROUP BY p.ProductID, p.ProductName

----- 5 -----
CREATE PROC sp_product_order_quantity_pan
@ProductID INT,
@TotalQuantity INT OUTPUT
AS
SELECT @TotalQuantity = Total FROM view_product_order_pan WHERE ProductID = @ProductID
GO

DECLARE @Total INT
EXEC sp_product_order_quantity_pan 1, @Total OUTPUT
SELECT @Total

----- 6 -----
CREATE PROC sp_product_order_city_pan
@ProductName VARCHAR(50)
AS
WITH ProductQuantityCity
AS
(SELECT p.ProductID, p.ProductName, p.Quantity, o.ShipCity
FROM Orders o LEFT JOIN
(SELECT od.OrderID, p.ProductID, p.ProductName, od.Quantity
FROM[Order Details] od LEFT JOIN Products p
ON od.ProductID = p.ProductID) p
ON o.OrderID = p.OrderID
),
ProductQuantityCityRanking
AS
(SELECT ProductID, ProductName, ShipCity, SUM(Quantity) AS TOTAL, RANK() OVER(PARTITION BY ProductID ORDER BY SUM(QUANTITY) DESC) rnk
FROM ProductQuantityCity
GROUP BY ProductID, ProductName, ShipCity)
SELECT * FROM ProductQuantityCityRanking WHERE rnk <= 5 AND ProductName = @ProductName
GO

--e.g.
exec sp_product_order_city_pan 'Chai'

----- 7 -----
BEGIN TRAN
CREATE PROC sp_move_employees_pan
AS
IF EXISTS(SELECT * FROM EmployeeTerritories et LEFT JOIN Territories t ON et.TerritoryID = t.TerritoryID WHERE t.TerritoryDescription = 'Troy')
BEGIN
	DECLARE @regionID INT
	SELECT @regionID = RegionID FROM Region
	INSERT INTO Territories VALUES
	('00000', 'Stevens Point', @regionID)
	UPDATE EmployeeTerritories 
	SET TerritoryID = '00000' WHERE TerritoryID IN 
	(SELECT et.TerritoryID FROM EmployeeTerritories et LEFT JOIN Territories t ON et.TerritoryID = t.TerritoryID WHERE t.TerritoryDescription = 'Troy')
END


----- 8 -----
CREATE TRIGGER trg_update_territory ON EmployeeTerritories
AFTER INSERT AS
DECLARE @emp_count INT, @ToryID VARCHAR(5)
SELECT @emp_count = COUNT(*) FROM INSERTED WHERE inserted.TerritoryID = (SELECT TerritoryID FROM Territories WHERE TerritoryDescription = 'Stevens Point')
SELECT @ToryID = TerritoryID FROM Territories WHERE TerritoryDescription = 'Troy'
IF @emp_count > 100
BEGIN
	UPDATE EmployeeTerritories
	SET TerritoryID = @ToryID WHERE TerritoryID = '00000'
END
ROLLBACK

----- 9 -----
CREATE TABLE city_pan (
ID INT IDENTITY(1,1) PRIMARY KEY,
City VARCHAR(50)
)
GO

CREATE TABLE people_pan (
ID INT IDENTITY(1,1) PRIMARY KEY,
Name VARCHAR(50),
City INT FOREIGN KEY REFERENCES city_pan(ID)
)
GO

INSERT INTO city_pan VALUES 
('Seattle'),
('Green Bay')
GO

INSERT INTO people_pan VALUES 
('Aaron Rodgers', 2),
('Russell Wilson', 1),
('Jody Nelson', 2)
GO

IF EXISTS(SELECT * FROM people_pan WHERE city = (SELECT ID FROM city_pan WHERE city = 'Seattle'))
BEGIN 
	UPDATE city_pan 
	SET City = 'Madison' WHERE City = 'Seattle'
END
ELSE 
BEGIN
	DELETE FROM city_pan
	WHERE City = 'Seattle' 
END

BEGIN TRY
CREATE VIEW Packers_zehua_pan AS
SELECT * FROM people_pan WHERE city in (SELECT ID FROM city_pan WHERE city = 'GREEN BAY')
END TRY

DROP VIEW Packers_zehua_pan
DROP TABLE city_pan
DROP TABLE people_pan

----- 10 -----
CREATE PROC sp_birthday_employees_pan AS
CREATE TABLE birthday_employees_pan (
ID INT IDENTITY(1,1) PRIMARY KEY,
LastName VARCHAR(50),
FirstName VARCHAR(50),
Birthday Date
)
GO

INSERT INTO birthday_employees_pan 
SELECT LastName, FirstName, BirthDate FROM Employees WHERE MONTH(BirthDate) = 2

DROP TABLE birthday_employees_pan
GO


----- 11 -----
CREATE PROC sp_pan_1 AS
WITH CustomerKind
AS 
(SELECT o.CustomerID, COUNT(od.ProductID) as Kinds FROM Orders o LEFT JOIN [Order Details] od
ON o.OrderID = od.OrderID
GROUP BY o.CustomerID)
SELECT City, COUNT(c.CustomerID)  FROM Customers c LEFT JOIN CustomerKind ck
ON c.CustomerID = ck.CustomerID
WHERE Kinds < 2 OR Kinds IS NULL
GROUP BY City
HAVING COUNT(c.CustomerID) >= 2
GO

CREATE PROC sp_pan_2 AS
SELECT City, COUNT(c.CustomerID)  FROM Customers c LEFT JOIN 
(SELECT o.CustomerID, COUNT(od.ProductID) as Kinds FROM Orders o LEFT JOIN [Order Details] od
ON o.OrderID = od.OrderID
GROUP BY o.CustomerID) ck
ON c.CustomerID = ck.CustomerID
WHERE Kinds < 2 OR Kinds IS NULL
GROUP BY City
HAVING COUNT(c.CustomerID) >= 2
GO

----- 12 -----
--Because the logic behind these two methods are the same.

----- 14 -----
CREATE TABLE Names (
[First Name] VARCHAR(50),
[Last Name] VARCHAR(50),
[Middle Name] VARCHAR(50)
)
INSERT INTO Names VALUES 
('John', 'Green', NULL),
('Mike', 'While', 'M')
GO

SELECT CONCAT([First Name], ' ', [Last Name], ' ', [Middle Name]) FROM Names

----- 15 -----
CREATE TABLE Students (
Student VARCHAR(50),
Marks INT,
SEX VARCHAR(1)
)
INSERT INTO Students VALUES 
('Ci', 70, 'F'),
('Bob', 80, 'M'),
('Li', 90, 'F'),
('Mi', 95, 'M')
GO

SELECT TOP 1 Student, MARKS FROM Students WHERE Sex = 'F'
ORDER BY Marks DESC

----- 16 -----
SELECT Student, MARKS, SEX, RANK() OVER(PARTITION BY Sex ORDER BY Marks DESC) AS rnk FROM Students


