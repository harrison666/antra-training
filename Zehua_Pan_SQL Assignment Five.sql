--SQL Assignment Five
--Zehua

/*
----- 1 -----
A database object is any defined object in a database that is used to store or reference data. 
Some examples of database objects include tables, views, clusters, sequences, indexes, and synonyms.


----- 2 -----
Index is a physical structure contains pointers to the data.

The advantages of indexes are as follows:
Their use in queries usually results in much better performance.
They make it possible to quickly retrieve (fetch) data.
They can be used for sorting. A post-fetch-sort operation can be eliminated.
Unique indexes guarantee uniquely identifiable records in the database.

The disadvantages of indexes are as follows:
They decrease performance on inserts, updates, and deletes.
They take up space (this increases with the number of fields used and the length of the fields).
Some databases will monocase values in fields that are indexed.

----- 3 -----
Two type of Index:
Clustered indexes define the physical sorting of a database table¡¯s rows in the storage media. For this reason, each database table may have only one clustered index.
Non-clustered indexes are created outside of the database table and contain a sorted list of references to the table itself.

----- 4 -----
Yes. Unique, Primary Key 

----- 5 -----
No. A clustered index sorts and stores the data rows in the table based on the index key values. 
Therefore only one clustered index can be created on each table because the data rows themselves can only be sorted in one order.

----- 6 -----
Yes. The order matters. It will sort by first column first.

----- 7 -----
Yes

----- 8 -----
Database Normalization is a process of organizing data to minimize redundancy (data duplication), which in turn ensures data consistency. 
1£©Data in each column should be atomic, no multiples values separated by comma.
The table does not contain any repeating column group
Identify each record using primary key.
2£©The table must meet all the conditions of 1NF
Move redundant data to separate table
Create relationships between these tables using foreign keys
3£©Table must meet all the conditions of 1NF and 2nd.
Does not contain columns that are not fully dependent on primary key.

----- 9 -----
Denormalization is a database optimization technique in which we add redundant data to one or more tables. 
This can help us avoid costly joins in a relational database.

----- 10 -----
We can apply Entity integrity to the Table by specifying a primary key, unique key, and not null. 
Referential integrity ensures the relationship between the Tables. We can apply this using a Foreign Key constraint.

----- 11 -----
Not Null Constraint.
Check Constraint.
Default Constraint.
Unique Constraint.
Primary Constraint.
Foreign Constraint.

----- 12 -----
Primary Key is a column that is used to uniquely identify each tuple of the table. It is used to add integrity constraints to the table. 
Only one primary key is allowed to be used in a table. 
Unique key is a constraint that is used to uniquely identify a tuple in a table.
There can be multiple unique constraints in a table.

----- 13 -----
Foreign key is a column that creates a relationship between two tables. 
It helps you to uniquely identify a record in the table. It is a field in the table that is a primary key of another table.

----- 14 -----
Yes

----- 15 -----
A foreign ky doesn't have to be unique and it can be null

----- 16 -----
No

----- 17 -----
In a database management system, a transaction is a single unit of logic or work, sometimes made up of multiple operations. 
Any logical calculation done in a consistent mode in a database is known as a transaction.
Read uncommitted (0)
Read committed (1)
Repeatable read (2)
Serializable (3)
*/

----- 1 -----
Create table customer(cust_id int,  iname varchar (50)) 
GO
Create table orders(order_id int,cust_id int,amount money,order_date smalldatetime)
GO

SELECT c.iname, o.total_order  FROM customer c LEFT JOIN 
(SELECT cust_id, SUM(order_id) AS total_order FROM orders 
WHERE YEAR(order_date) = '2002'
GROUP BY cust_id) o
ON c.cust_id = o.cust_id

----- 2 -----
Create table person (id int, firstname varchar(100), lastname varchar(100))
GO
SELECT * FROM person WHERE lastname LIKE 'A%'
DROP TABLE person

----- 3 -----
Create table person(person_id int primary key, manager_id int null, name varchar(100)not null) 
GO

SELECT p.person_id, p.name, m.report_to FROM person p LEFT JOIN 
(SELECT manager_id, COUNT(*) AS report_to FROM person GROUP BY manager_id) m
ON p.person_id = m.manager_id
WHERE p.manager_id IS NULL 


----- 4 -----
---insert ,update, delete


----- 5 -----
CREATE TABLE Companys(
ID INT PRIMARY KEY IDENTITY(1, 1),
Name VARCHAR(50)
)
GO
CREATE TABLE Locations(
ID INT PRIMARY KEY IDENTITY(1, 1),
Adress VARCHAR(50)
)
GO
CREATE TABLE Contacts(
ID INT PRIMARY KEY IDENTITY(1, 1),
Name VARCHAR(50),
Suite VARCHAR(50),
Mail VARCHAR(50)
)
GO
ALTER TABLE Contacts 
  ADD CONSTRAINT uq_contact UNIQUE(Suite, Mail);
GO

CREATE TABLE Divisions(
ID INT PRIMARY KEY IDENTITY(1, 1),
Name VARCHAR(50),
CompanyID INT FOREIGN KEY REFERENCES Companys(ID),
LocationID INT FOREIGN KEY REFERENCES Locations(ID),
ContactID INT FOREIGN KEY REFERENCES Contacts(ID)
)





