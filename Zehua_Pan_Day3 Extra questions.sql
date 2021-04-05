-- Zehua Pan
----- 1 -----
-- find the detail of top 3 customers from every city who have placed maximum number of orders
USE Northwind

WITH OrderCount
AS
(
SELECT CustomerId, COUNT(orderid) AS 'total' FROM dbo.Orders GROUP BY CustomerId
),
CustomerRanking
AS
(
SELECT c.ContactName, c.Phone, c.City, oc.total, 
DENSE_RANK() OVER(PARTITION BY c.city ORDER BY oc.total DESC) rnk 
FROM OrderCount oc INNER JOIN Customers c  
ON oc.CustomerID = c.CustomerID
)
SELECT * FROM CustomerRanking where rnk <= 3

----- 2 -----
CREATE TABLE A ( LocName text, Distance integer) 
INSERT INTO A VALUES 
('A', 0),
('B', 50),
('C', 150),
('D', 299)

/*
WITH cte_dis
AS
(
SELECT LocName, Distance FROM A WHERE Distance = 0
UNION ALL
SELECT LocName, Distance -  FROM cte_dis
)
*/

----- 3 -----
CREATE TABLE Employee2 (Name text, Salary integer, Department text) 
INSERT INTO Employee2 VALUES 
('Smith', 2000, 'HR'),
('Smith', 2000, 'HR'),
('Peter', 3000, 'QA'),
('Peter', 3000, 'QA'),
('Peter', 3000, 'QA'),
('John', 3400, 'QA'),
('John', 3400, 'QA'),
('John', 3400, 'QA');

Alter Table Employee2
Add ID Int Identity(1, 1);


WITH cte
AS 
(
SELECT Name, Salary, Department,
ROW_NUMBER() OVER(PARTITION BY Salary ORDER BY ID) AS DuplicateCount
FROM Employee2
)
DELETE FROM cte
WHERE DuplicateCount > 1;

SELECT * FROM Employee2