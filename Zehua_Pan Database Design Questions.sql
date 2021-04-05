/*
Database Design Questions
Zehua Pan
*/

----- 1 -----
CREATE DATABASE Company
GO
USE COMPANY

CREATE TABLE Office(
OfficeID INT IDENTITY(1,1) PRIMARY KEY,
OfficeName VARCHAR(20) UNIQUE NOT NULL,
City VARCHAR(20) NOT NULL,
Country VARCHAR(30) NOT NULL,
Address VARCHAR(50),
PhoneNumber VARCHAR(20),
DirectorName VARCHAR(20)
)

CREATE TABLE Project(
ProjectCode INT IDENTITY(1,1) PRIMARY KEY,
Title VARCHAR(50) UNIQUE NOT NULL, 
StartDate DATE NOT NULL,
EndDate DATE,
AssignedBudget MONEY,
PersonInCharge VARCHAR(20),
OfficeID INT FOREIGN KEY REFERENCES Office(OfficeID)
)

CREATE TABLE City(
City VARCHAR(20) NOT NULL,
Country VARCHAR(30) NOT NULL,
NumberOfInhabitant INT
)


----- 2 -----
CREATE DATABASE LendingCompany
GO
USE LendingCompany

CREATE TABLE Lender(
ID INT IDENTITY(1,1) PRIMARY KEY,
Name VARCHAR(20) NOT NULL,
AvailableMoney Money
)

CREATE TABLE Borrower(
ID INT IDENTITY(1,1) PRIMARY KEY,
Name VARCHAR(20) NOT NULL,
Company VARCHAR(50),
RiskValue INT,
)

CREATE TABLE Loan(
LoanCode INT IDENTITY(1,1) PRIMARY KEY,
Amount Money NOT NULL,
RefundDeadline Date NOT NULL,
InterestRate FLOAT NOT NULL,
Purpose VARCHAR(50),
BorrowerID INT FOREIGN KEY REFERENCES Borrower(ID)
)

CREATE TABLE Contribution(
ID INT IDENTITY(1,1) PRIMARY KEY,
LenderID INT FOREIGN KEY REFERENCES Lender(ID),
LoanCode INT FOREIGN KEY REFERENCES Loan(LoanCode),
Amount Money NOT NULL
)


----- 3 -----
CREATE DATABASE Restaurant
GO
USE Restaurant

CREATE TABLE Course(
ID INT IDENTITY(1,1) PRIMARY KEY,
Name VARCHAR(30) NOT NULL,
Description VARCHAR(100),
Photo IMAGE,
FinalPrice MONEY NOT NULL
)

CREATE TABLE Category(
ID INT IDENTITY(1,1) PRIMARY KEY,
Name VARCHAR(30) NOT NULL,
Description VARCHAR(100),
EmployeeInCharge VARCHAR(30) NOT NULL,
CourseID INT FOREIGN KEY REFERENCES Course(ID)
)

CREATE TABLE Recipe(
RecipeID INT,
IngredientID INT,
PRIMARY KEY (RecipeID, IngredientID),
RecipeName VARCHAR(50) UNIQUE NOT NULL,
IngredientName VARCHAR(50) UNIQUE NOT NULL,
RequiredAmount INT NOT NULL,
Unit VARCHAR(20) NOT NULL,
CurrentAmount INT NOT NULL,
)
