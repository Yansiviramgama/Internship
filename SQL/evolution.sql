--Instruction to follow 
	--Insert Data Accurate and Appropiate.
	--Choose data type wisely.
	-- No reference taking.
	--No Drop of any table should be done.
	--Insert at least 10 data in the table.
	--Not to use any previous tables.
	--Where there is Update there should be Transactions
	-- Good to Have Insert Data with Storeprocuder
drop table Customer
drop table Salesman
drop table OrderDetails
drop table Orders
drop table ModeOfPayment
drop table Items
drop table Category

	Drop DATABASE YV325YansiViramgama
	CREATE DATABASE YV325YansiViramgama
	USE YV325YansiViramgama
	SELECT * FROM sys.objects WHERE type = 'u'
--Create Table Customer [5]
	--Id
	--Name
	--UserName (Must be unique)
	--Password
	--Email (Must be unique)
	--DOB
	--Address
	--ContactNo

	CREATE TABLE Customer(
		CUT_ID INT,
		CUT_NAME VARCHAR(200),
		CUT_USERNAME VARCHAR(200) UNIQUE,
		CUT_PASSWORD VARCHAR(200),
		CUT_EMAIL VARCHAR(200) UNIQUE,
		CUT_DOB DATE,
		CUT_ADDRESS VARCHAR(200),
		CUT_CONTACT_NO INT
	)
	
--Create Table Salesman
	--Id
	--Name
	--UserName
	--Password
	--Email
	--DOB
	--Address
	--ContactNo
	CREATE TABLE Salesman(
		Sal_ID INT,
		Sal_NAME VARCHAR(200),
		Sal_USERNAME VARCHAR(200),
		Sal_PASSWORD VARCHAR(200),
		Sal_EMAIL VARCHAR(200),
		Sal_DOB DATE,
		Sal_ADDRESS VARCHAR(200),
		Sal_CONTACT_NO INT
	)
ALTER TABLE Customer drop column CUT_ID;
ALTER TABLE Customer ADD CUT_ID INT NOT NULL IDENTITY(1,1); 
ALTER TABLE Customer ADD CONSTRAINT Cust_id_prime_key PRIMARY KEY (CUT_ID);
--Create Table Category
	--Id
	--Name
	CREATE TABLE Category(
		CAT_ID INT,
		CAT_NAME VARCHAR(200),
	)
--Create Table Items
	--Id
	--Name
	--Category (link to category)
	--Rate
	--DOM (Date of Manufacture)
	--DOE (Date of Expire) 
	CREATE TABLE Items(
		It_ID INT,
		It_NAME VARCHAR(200),
		It_Category INT,
		It_Rate DECIMAL(9,2),		
		It_DOM DATE,
		It_DOE DATE
	)
--Create Table ModeOfPayment
	--Id
	--Name
	CREATE TABLE ModeOfPayment(
		Mod_ID INT,
		Mod_NAME VARCHAR(200),
	)
--Create Table Orders
	--Id
	--OrderNo (Must be Unique)
	--Customer (link to customer)
	--OrderQty 
	--Bill Amount
	--DOD (Date of Delivery)
	--Salesman (link to salesman)
	--DeliveryAddress
	--City
	--ContactNo
	--MOP (Mode of Payment) (link to payment)
	--OrderStatus (By default 0 if I cancel then should be update to 1)
	CREATE TABLE Orders(
		Ord_ID INT,
		Ord_No INT UNIQUE,
		Ord_Customer INT,
		Ord_OrderQty INT,
		Ord_Bill_Amount DECIMAL(9,2),
		Ord_DOB DATE,
		Ord_Salesman INT,
		Ord_Delivery_ADDRESS VARCHAR(200),
		Ord_City VARCHAR(200),
		Ord_CONTACT_NO INT,
		Ord_MOP INT,
		Ord_OrderStatus INT DEFAULT 0
	)	
--Create Table OrderDetails
	--Id
	--OrderId (link to order)
	--ItemId (link to Items)
	--OrderQty (Order Quantity per Items)
	--OrderAmount (Qty * Rate)
	CREATE TABLE OrderDetails(
		OD_ID INT,
		OD_OrderId INT,
		OD_ItemId INT,
		OD_OrderQty INT,
		OD_OrderAmount DECIMAL(9,2)
	)

--Questions : 

--1. After Creating Table , need to update All table with Primary Key , Clustor Key  and Add Identity [5]
--primary key 
ALTER TABLE Customer DROP COLUMN CUT_ID 
ALTER TABLE Customer ADD CUT_ID INT NOT NULL IDENTITY(1,1)
ALTER TABLE Customer ADD CONSTRAINT CUT_ID_Primary_Key PRIMARY KEY (CUT_ID)

ALTER TABLE Salesman DROP COLUMN Sal_ID 
ALTER TABLE Salesman ADD Sal_ID INT NOT NULL IDENTITY(1,1)
ALTER TABLE Salesman ADD CONSTRAINT Sal_ID_Primary_Key PRIMARY KEY (Sal_ID)

ALTER TABLE Category DROP COLUMN CAT_ID 
ALTER TABLE Category ADD CAT_ID INT NOT NULL IDENTITY(1,1)
ALTER TABLE Category ADD CONSTRAINT CAT_ID_Primary_Key PRIMARY KEY (CAT_ID)

ALTER TABLE Items DROP COLUMN It_ID 
ALTER TABLE Items ADD It_ID INT NOT NULL IDENTITY(1,1)
ALTER TABLE Items ADD CONSTRAINT It_ID_Primary_Key PRIMARY KEY (It_ID)

ALTER TABLE ModeOfPayment DROP COLUMN Mod_ID 
ALTER TABLE ModeOfPayment ADD Mod_ID INT NOT NULL IDENTITY(1,1)
ALTER TABLE ModeOfPayment ADD CONSTRAINT Mod_ID_Primary_Key PRIMARY KEY (Mod_ID)

ALTER TABLE Orders DROP COLUMN Ord_ID 
ALTER TABLE Orders ADD Ord_ID INT NOT NULL IDENTITY(1,1)
ALTER TABLE Orders ADD CONSTRAINT Ord_ID_Primary_Key PRIMARY KEY (Ord_ID)

ALTER TABLE OrderDetails DROP COLUMN OD_ID 
ALTER TABLE OrderDetails ADD OD_ID INT NOT NULL IDENTITY(1,1)
ALTER TABLE OrderDetails ADD CONSTRAINT OD_ID_Primary_Key PRIMARY KEY (OD_ID)
--FOREIGN KEY
ALTER TABLE Items ADD CONSTRAINT itm_cat_ref_key FOREIGN KEY (It_Category) REFERENCES
Category(CAT_ID);

ALTER TABLE Orders ADD CONSTRAINT ord_cut_ref_key FOREIGN KEY (Ord_Customer) REFERENCES
Customer(CUT_ID);

ALTER TABLE Orders ADD CONSTRAINT ord_sal_ref_key FOREIGN KEY (Ord_Salesman) REFERENCES
Salesman(Sal_ID);

ALTER TABLE Orders ADD CONSTRAINT ord_mop_ref_key FOREIGN KEY (Ord_MOP) REFERENCES
ModeOfPayment(Mod_ID);

ALTER TABLE OrderDetails ADD CONSTRAINT od_ord_ref_key FOREIGN KEY (OD_OrderId) REFERENCES
Orders(Ord_ID);

ALTER TABLE OrderDetails ADD CONSTRAINT od_itm_ref_key FOREIGN KEY (OD_ItemId) REFERENCES
Items(It_ID);

--2. Create a Parameterized Store Procedure to Insert/Update Data in each of tables listed above. (If Primary key of that table is passed then Update else Insert) [8]
--	E.g : SP_AddEditCustomer (By using this procedure I will be able to insert or update data)

CREATE OR ALTER PROCEDURE SP_AddEditCustomer (
	@ID INT,
	@NAME VARCHAR(200),
	@UserName VARCHAR(200),
	@Password VARCHAR(200),
	@Email VARCHAR(200),
	@DOB DATE,
	@ADDRESS VARCHAR(200),
	@ContactNo INT
)
AS
BEGIN 
	IF(@ID IS NULL)
	BEGIN
		INSERT INTO Customer (CUT_NAME,CUT_USERNAME,CUT_PASSWORD,CUT_EMAIL,CUT_DOB,CUT_ADDRESS,CUT_CONTACT_NO) VALUES (@NAME,@UserName,@Password,@Email,@DOB,@ADDRESS,@ContactNo)
	END
	ELSE 
	BEGIN
		UPDATE Customer SET CUT_NAME = @NAME,CUT_USERNAME=@UserName,CUT_PASSWORD=@Password,CUT_EMAIL=@Email,CUT_DOB=@DOB,CUT_ADDRESS=@ADDRESS,CUT_CONTACT_NO =@ContactNo
	END
END

EXEC SP_AddEditCustomer null,'Hemang','HD123','Dholu123','hemang@gmail.com','2002-08-
08','Bhuj','951-025-0509';
EXEC SP_AddEditCustomer null,'Bhvya','kingBhavya','HeyBhavya','bhavya@gmail.com','2002-
04-03','Palanpur','944-123-4781';
EXEC SP_AddEditCustomer null,'Chirag','PatelChirag','Ch123','chirag@gmail.com','2001-
12-03','Siddpur','940-113-4111';
EXEC SP_AddEditCustomer null,'Vishal','Vishal','vishal@123','vishal@gmail.com','2000-
01-25','Modasa','840-114-4111';
EXEC SP_AddEditCustomer null,'Darshan','dj152','darshan123','darshan@gmail.com','1999-
12-24','Bhuj','942-742-0760';
EXEC SP_AddEditCustomer null,'Raj','rj152','rajnariya','raj@gmail.com','2001-04-
15','Rajkot','912-756-0760';
EXEC SP_AddEditCustomer null,'Krishna','kisu','helloKisu','kisu@gmail.com','1998-11-
12','Rajkot','941-120-0760';
EXEC SP_AddEditCustomer null,'Vasu','dekivadiya123','vasu3','vasu@gmail.com','2000-11-
24','Manavadar','842-745-0740';
EXEC SP_AddEditCustomer null,'Shyam','ladani08','shayam@123','shyam@gmail.com','2000-
12-24','Rajkot','912-122-0760';
EXEC SP_AddEditCustomer 
null,'Yanshi','yashi03','yanshi@123','yanshi03@gmail.com','2002-03-02','Junagadh','892-
151-0760';
--
--3. Create a Parameterized Store Procedure to retrive all the OrderDetails as per Customer (If CustomerId not passed then retrive all the orders) ( Make sure to Add Everything in Single Procedure) [8]
--	Information I want : 
--		--CustomerName
--		--ItemName
--		--Item Rate
--		--Qty
--		--OrderAmount (Qty * ItemRate)
--		
--4. Create a User Defined Function that will retrive TotalOrderAmount from the OrderDetails Table as per Customer. [8]
--	I just want total amount as per customerid I Passed.
--	
--5. Create a prameterized Store Procedure to retrive all the Order Information as per Customer. (If CustomerId not passed then retrive all the orders) [8]
--	Information I want : 
--		--CustomerName
--		--OrderNo
--		--OrderQty (Total Qty of all the Items) (Using Function)
--		--OrderAmount (Total Amount of Order) (Using Function)
--		--SalesmanName
--		--Address
--		--City
--		--ContactNo
--		--MOP Name
--		--DOD
--		
--6. Create a Parameterized Store Procedure to Cancel Order as per OrderNo (If I pass wrong orderno then message should be there) [8]
--
--7. Create a Parameterized Store Procedure for Login as per Customer/Salesman EmailId and Password.  [10]
--	(If credentials matched then show "Login Successfull" then "Invalid User Input" , If EmailId  is not having in the Table then Create Emaild with Password( Random) ).
--	
--8. Create a Parameterized Store Procedure for Delete Customer from the UserName	[5]
--	--All the orders linked to that customer should also be deleted.
--
--9. Create a OrderDetails using OrderId of Order in a Single Store Procedure. [10]
--
--10. Every Data suppose to be Dynamic (no any Hard Coded) [Addition 25 points] [ Example : Name would be Dynamic , amount would be Dyanmic , Date Would be Dynamic , OrderNo  would be Dynamic , etc... ]