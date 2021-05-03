
/*
----- 1 -----
I would use join, because using join is more efficient and faster than using subqueries.

----- 2 -----
A CTE (Common Table Expression) is a temporary result set that you can reference within another SELECT, INSERT, UPDATE, or DELETE statement.
A CTE can be used to create a recursive query or to replace the the derived table to make it more readable.

----- 3 -----
The table variable is a special type of the local variable that helps to store data temporarily.
The table variable scope is within the batch. 
We can define a table variable inside a stored procedure and function as well. 
In this case, the table variable scope is within the stored procedure and function. 
We cannot use it outside the scope of the batch, stored procedure or function.

----- 4 -----
Delete is a DML command whereas truncate is DDL command. 
Truncate can be used to delete the entire data of the table without maintaining the integrity of the table. 
On the other hand , delete statement can be used for deleting the specific data.
TRUNCATE is faster than DELETE , as it doesn't scan every record before removing it. 
TRUNCATE TABLE locks the whole table to remove data from a table; thus, this command also uses less transaction space than DELETE .

----- 5 -----
An identity column is a column (also known as a field) in a database table that is made up of values generated by the database.
DELETE retains the identity and does not reset it to the seed value.
TRUNCATE command resets the identity to its seed value.

----- 6 -----
TRUNCATE always removes all the rows from a table, leaving the table empty and the table structure intact whereas DELETE may remove conditionally if the where clause is used. 
The rows deleted by TRUNCATE TABLE statement cannot be restored and you can not specify the where clause in the TRUNCATE statement.
*/

USE Northwind
----- 1 -----
SELECT DISTINCT e.City
FROM dbo.Employees e INNER JOIN dbo.Customers c
ON e.City = c.City

----- 2 -----
-- a --
SELECT DISTINCT City
FROM dbo.Customers 
WHERE City NOT IN
(SELECT DISTINCT e.City
FROM dbo.Employees e INNER JOIN dbo.Customers c
ON e.City = c.City) 

----- 2 -----
-- b --
SELECT DISTINCT City FROM dbo.Customers
EXCEPT
SELECT DISTINCT City FROM dbo.Employees

----- 3 -----
SELECT p.ProductID, p.ProductName, o.total
FROM dbo.Products p LEFT JOIN
(SELECT ProductID, SUM(Quantity) AS total
FROM dbo.[Order Details]
GROUP BY ProductID) o
ON p.ProductID = o.ProductID

----- 4 -----
WITH CustomerQuantity
AS
(
SELECT o.OrderID, o.CustomerID, od.Quantity
FROM dbo.Orders o LEFT JOIN dbo.[Order Details] od
ON o.OrderID = od.OrderID
)
SELECT c.City, SUM(q.Quantity) AS TotalQuantity
FROM dbo.Customers c LEFT JOIN CustomerQuantity q
ON c.CustomerID = q.CustomerID
GROUP BY c.City

----- 5 -----
SELECT City, COUNT(*) AS CustomerCount FROM dbo.Customers
GROUP BY City
HAVING COUNT(*) >= 2

----- 6 -----
WITH CustomerProducts
AS
(
SELECT o.OrderID, o.CustomerID, od.ProductID
FROM dbo.Orders o JOIN dbo.[Order Details] od
ON o.OrderID = od.OrderID
)
SELECT DISTINCT c.City
FROM dbo.Customers c JOIN CustomerProducts p
ON c.CustomerID = p.CustomerID
GROUP BY c.City,ProductID
HAVING COUNT(*) >= 2

----- 7 -----
SELECT DISTINCT c.CustomerID
FROM dbo.Customers c LEFT JOIN dbo.Orders o
ON c.CustomerID = o.CustomerID
WHERE c.City != o.ShipCity

----- 8 -----
WITH ProductQuantityCustomer
AS(
SELECT od.ProductID, od.Quantity, od.UnitPrice, o.CustomerID
FROM dbo.[Order Details] od LEFT JOIN dbo.Orders o
ON od.OrderID = o.OrderID
),
ProductQuantityCity
AS
(
SELECT p.ProductID, SUM(p.Quantity) AS TotalQuantity, c.City
FROM ProductQuantityCustomer p INNER JOIN dbo.Customers c
ON p.CustomerID = c.CustomerID
GROUP BY p.ProductID, c.city
),
CityRanking
AS
(
SELECT ProductID, City, DENSE_RANK() OVER(PARTITION BY ProductID ORDER BY TotalQuantity DESC) AS rnk
FROM ProductQuantityCity
),
Top5Product
AS
(
SELECT TOP 5 p.ProductID, SUM(p.Quantity) AS Quantity, AVG(p.UnitPrice) AS AvgPrice
FROM ProductQuantityCustomer p INNER JOIN dbo.Customers c
ON p.CustomerID = c.CustomerID
GROUP BY p.ProductID
ORDER BY SUM(p.Quantity) DESC
)
SELECT t.ProductID, Quantity, AvgPrice, cr.City
FROM Top5Product t LEFT JOIN CityRanking cr
On t.ProductID = cr.ProductID
WHERE cr.rnk = 1

----- 9 -----
-- a -- 
SELECT City
FROM dbo.Employees 
WHERE City NOT IN 
(SELECT c.City 
FROM dbo.Orders o INNER JOIN dbo.Customers c
ON o.CustomerID = c.CustomerID)
-- b -- 
SELECT City
FROM dbo.Employees 
EXCEPT
SELECT c.City 
FROM dbo.Orders o INNER JOIN dbo.Customers c
ON o.CustomerID = c.CustomerID

----- 10 -----
WITH ProductQuantityCustomer
AS(
SELECT od.ProductID, od.Quantity, od.UnitPrice, o.CustomerID
FROM dbo.[Order Details] od LEFT JOIN dbo.Orders o
ON od.OrderID = o.OrderID
),
QuantityCity
AS
(
SELECT SUM(p.Quantity) AS TotalQuantity, c.City
FROM ProductQuantityCustomer p INNER JOIN dbo.Customers c
ON p.CustomerID = c.CustomerID
GROUP BY c.city
),
Top1Quantity
AS
(
SELECT  TOP 1 * FROM QuantityCity
ORDER BY TotalQuantity DESC
),
Top1Order
As
(
SELECT TOP 1 e.City, COUNT(o.OrderID) AS OrderCount
FROM dbo.Employees e RIGHT JOIN dbo.Orders o
ON e.EmployeeID = o.EmployeeID
GROUP BY e.EmployeeID, e.City
ORDER BY OrderCount DESC
)
SELECT * FROM Top1Order o INNER JOIN Top1Quantity q
ON o.City = q.City

----- 11 -----
/*
Assuming each record has a unique id or create one if not, then use group by to identify the duplicate records, 
finnally use DELETE FROM table and WHERE CLAUSE to delete duplicate records whose id is not the minimum id in that group.
*/

----- 12 -----
TRUNCATE TABLE Employee
CREATE TABLE Employee ( empid integer, mgrid integer, deptid integer, salary integer) 
INSERT INTO Employee VALUES 
(1, NULL, 1, 8000),
(2, 1, 2, 6000),
(3, 1, 3, 6000),
(4, 2, 2, 3000),
(5, 3, 3, 3000)

CREATE TABLE Dept (deptid integer, deptname text)
INSERT INTO Dept VALUES 
(1, 'Management'),
(2, 'HR'),
(3, 'IT')

WITH cte_emp
AS
(
SELECT empid, mgrid, deptid, salary, 1 AS 'lvl' FROM Employee WHERE mgrid IS NUll
UNION ALL
SELECT e.empid, e.mgrid, e.deptid, e.salary, ce.lvl + 1
FROM Employee e INNER JOIN cte_emp ce
ON e.mgrid = ce.empid
)
SELECT * FROM cte_emp
WHERE lvl = (SELECT MAX(lvl) FROM cte_emp)

----- 13 -----
WITH EmpCount
AS
(SELECT e.deptid, COUNT(e.empid) AS DeptCount
FROM Employee e INNER JOIN Dept d
ON e.deptid = d.deptid
GROUP BY e.deptid),
EmpCountName
AS
(SELECT ec.deptid, ec.DeptCount, d.deptname FROM EmpCount ec INNER JOIN Dept d ON ec.deptid = d.deptid),
DeptRanking
AS
(SELECT deptid, deptname, DeptCount, RANK() OVER(ORDER BY DeptCount DESC) AS rnk FROM EmpCountName)
SELECT deptname, DeptCount FROM DeptRanking
WHERE rnk = 1

----- 14 -----
WITH EmpDept
AS
(SELECT d.deptid, d.deptname, e.empid, e.salary FROM Employee e INNER JOIN Dept d ON e.deptid = d.deptid),
SalaryRanking
AS
(SELECT deptname, empid, salary, RANK() OVER(PARTITION BY deptid ORDER BY salary DESC) AS rnk FROM EmpDept)
SELECT deptname, empid, salary FROM SalaryRanking
WHERE rnk <= 3
