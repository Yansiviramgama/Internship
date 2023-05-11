Drop DATABASE YV325YansiViramgama
CREATE DATABASE YV325YansiViramgama
	USE YV325YansiViramgama

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
	CREATE TABLE Category(
		CAT_ID INT,
		CAT_NAME VARCHAR(200),
	)
	CREATE TABLE Items(
		It_ID INT,
		It_NAME VARCHAR(200),
		It_Category INT,
		It_Rate DECIMAL(9,2),		
		It_DOM DATE,
		It_DOE DATE
	)
	CREATE TABLE ModeOfPayment(
		Mod_ID INT,
		Mod_NAME VARCHAR(200),
	)
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
	CREATE TABLE OrderDetails(
		OD_ID INT,
		OD_OrderId INT,
		OD_ItemId INT,
		OD_OrderQty INT,
		OD_OrderAmount DECIMAL(9,2)
	)
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

EXEC SP_AddEditCustomer null,'Hemang','HD123','Dholu123','hemang@gmail.com','2002-08-08','Bhuj',951;
EXEC SP_AddEditCustomer null,'Bhvya','kingBhavya','HeyBhavya','bhavya@gmail.com','2002-04-03','Palanpur',944;
EXEC SP_AddEditCustomer null,'Chirag','PatelChirag','Ch123','chirag@gmail.com','2001-12-03','Siddpur',940;
EXEC SP_AddEditCustomer null,'Vishal','Vishal','vishal@123','vishal@gmail.com','2000-01-25','Modasa',840;
EXEC SP_AddEditCustomer null,'Darshan','dj152','darshan123','darshan@gmail.com','1999-12-24','Bhuj',942;
EXEC SP_AddEditCustomer null,'Raj','rj152','rajnariya','raj@gmail.com','2001-04-15','Rajkot',912;
EXEC SP_AddEditCustomer null,'Krishna','kisu','helloKisu','kisu@gmail.com','1998-11-12','Rajkot',941;
EXEC SP_AddEditCustomer null,'Vasu','dekivadiya123','vasu3','vasu@gmail.com','2000-11-24','Manavadar',842;
EXEC SP_AddEditCustomer null,'Shyam','ladani08','shayam@123','shyam@gmail.com','2000-12-24','Rajkot',912;
EXEC SP_AddEditCustomer null,'Yansi','yasi02','yanshi@23','yansi03@gmail.com','2002-04-02','amreli',892;

EXEC SP_AddEditCustomer 8,'Vasu','dekivadiya123','vasu78','vasu@gmail.com','2000-11-24','Manavadar',842;

SELECT * FROM Customer

CREATE OR ALTER PROCEDURE SP_AddEditSalesman(
@Id INT,
@Name VARCHAR(255),
@UserName VARCHAR(255),
@Password VARCHAR(255),
@Email VARCHAR(255),
@DOB DATE,
@Address VARCHAR(500),
@ContactNo INT 
)
AS
BEGIN
IF(@Id IS NULL)
BEGIN
INSERT INTO
Salesman(Sal_NAME ,Sal_USERNAME ,Sal_PASSWORD ,Sal_EMAIL ,Sal_DOB ,Sal_ADDRESS ,Sal_CONTACT_NO ) VALUES
(@Name,@UserName,@Password,@Email,@DOB,@Address,@ContactNo);
END
ELSE
BEGIN
UPDATE Salesman SET Sal_NAME =@Name, Sal_USERNAME = @UserName, Sal_PASSWORD = 
@Password, Sal_EMAIL = @Email, Sal_DOB = @DOB, Sal_ADDRESS = @Address,Sal_CONTACT_NO = @ContactNo WHERE
Sal_ID  = @Id;
END
END

EXEC SP_AddEditSalesman null,'Keyur','keyur05','keyur@11','keyur01@gmail.com','1997-08-02','Rajkot',815;
EXEC SP_AddEditSalesman null,'Abhinav','Abhinav02','abhinav@11453','Abhinav@gmail.com','1995-02-05','Ahmedabad',945;
EXEC SP_AddEditSalesman null,'Hardik','hardik05','hardik233','hardik@gmail.com','1992-10-11','Amreli',745;
EXEC SP_AddEditSalesman null,'Gill','gill','gill@113','gill@gmail.com','2000-02-22','Junagadh',945;
EXEC SP_AddEditSalesman null,'Raj','raj065','raj@113','raj@gmail.com','1999-08-24','Manavadar',845;
EXEC SP_AddEditSalesman null,'Dev','dev','dev@113','dev@gmail.com','2000-05-03','Bhuj',945;
EXEC SP_AddEditSalesman null,'Vatsal','vatsal','vatsal@113','vatsal@gmail.com','2002-02-02','Rajkot',951;
EXEC SP_AddEditSalesman null,'Varshil','darshil','darshil@113','darshil@gmail.com','1998-01-01','Siddpur',943;
EXEC SP_AddEditSalesman null,'Poojan','poojan','poojan@113','poojan@gmail.com','1998-12-12','Palanpur',845;
EXEC SP_AddEditSalesman null,'Manish','manish','manish@113','manish02@gmail.com','1998-12-02','Bhuj',845;
---Update
EXEC SP_AddEditSalesman 9,'Poojan','poojii','poojan@113','poojan@gmail.com','1998-12-12','Palanpur',845;

select * from Salesman
CREATE OR ALTER PROCEDURE SP_AddEditCategory(
@Id INT,
@Name VARCHAR(255)
)
AS
BEGIN
IF(@Id IS NULL)
BEGIN
INSERT INTO Category(CAT_NAME ) VALUES (@Name);
END
ELSE
BEGIN
UPDATE Category SET CAT_NAME=@Name WHERE CAT_ID= @Id;
END
END

EXEC SP_AddEditCategory null,'Glossary'
EXEC SP_AddEditCategory null,'Milk'
EXEC SP_AddEditCategory null,'Watches'
EXEC SP_AddEditCategory null,'Shoes'
EXEC SP_AddEditCategory null,'TV'
EXEC SP_AddEditCategory null,'Mobiles'
EXEC SP_AddEditCategory null,'Computer'
EXEC SP_AddEditCategory null,'Chair'
EXEC SP_AddEditCategory null,'FoodItem'
EXEC SP_AddEditCategory null,'Lights'

EXEC SP_AddEditCategory 2,'DairyProduct'

CREATE PROCEDURE SP_AddEditItems(
@Id INT,
@Name VARCHAR(255),
@Category INT,
@Rate DECIMAL(10,2),
@DOM DATE,
@DOE DATE)
AS
BEGIN
IF(@Id IS NULL)
BEGIN
INSERT INTO Items(It_NAME ,It_Category ,It_Rate ,It_DOM ,It_DOE ) VALUES(@Name,@Category,@Rate, @DOM, @DOE);
END
ELSE
BEGIN
UPDATE Items SET It_NAME  =@Name, It_Category  = @Category, It_Rate  = @Rate, It_DOM  = @DOM, It_DOE= @DOE WHERE It_ID  = @Id;
END
END

EXEC SP_AddEditItems null,'Syska Lights',10, 100, '2022-05-16', '2022-12-31'
EXEC SP_AddEditItems null,'Amul Milk',2, 25, '2023-04-01', '2023-04-03'
EXEC SP_AddEditItems null,'Nike Jordan',4, 2500, '2020-04-01', '2023-12-03'
EXEC SP_AddEditItems null,'Titan',3, 3200, '2019-04-01', '2023-12-03'
EXEC SP_AddEditItems null,'Shoap',1, 10, '2022-12-01', '2023-12-02'
EXEC SP_AddEditItems null,'Wheat',1, 150, '2023-01-01', '2023-06-03'
EXEC SP_AddEditItems null,'Logitech Mouse',7, 250, '2019-04-01', '2024-12-03'
EXEC SP_AddEditItems null,'Samasung TV',5, 25000, '2018-04-01', null
EXEC SP_AddEditItems null,'Samasung A50',6, 18000, '2019-04-01', null
EXEC SP_AddEditItems null,'Gaming chair',8, 1500, '2019-12-12', '2023-12-11'
select * from Items

CREATE OR ALTER PROCEDURE SP_AddEditModeOfPayment(
@Id INT,
@Name VARCHAR(255)
)
AS
BEGIN
IF(@Id IS NULL)
BEGIN
INSERT INTO ModeOfPayment(Mod_NAME  ) VALUES (@Name);
END
ELSE
BEGIN
UPDATE ModeOfPayment SET Mod_NAME =@Name WHERE Mod_ID = @Id;
END
END

EXEC SP_AddEditModeOfPayment null,'Cash'
EXEC SP_AddEditModeOfPayment null,'UPI'
EXEC SP_AddEditModeOfPayment null,'Credit Card'
EXEC SP_AddEditModeOfPayment null,'debit Card'
EXEC SP_AddEditModeOfPayment null,'Net Banking'
EXEC SP_AddEditModeOfPayment null,'EMI'
EXEC SP_AddEditModeOfPayment null,'Cash'
EXEC SP_AddEditModeOfPayment null,'UPI'
EXEC SP_AddEditModeOfPayment null,'debit Card'
EXEC SP_AddEditModeOfPayment null,'Credit Card'
select * from ModeOfPayment

CREATE OR ALTER PROCEDURE SP_AddEditOrders(
	@Id INT,
	@OrderNo INT,
	@Customer INT,
	@OrderQty INT,
	@BillAmount DECIMAL(10,2),
	@DOD DATE,
	@Salesman INT,
	@DeliveryAddress VARCHAR(500),
	@City VARCHAR(255),
	@ContactNo VARCHAR(25),
	@MOP INT,
	@OrderStatus INT=0
)
AS
BEGIN
IF(@Id IS NULL)
BEGIN
INSERT INTO
Orders(Ord_No ,Ord_Customer ,Ord_OrderQty ,Ord_Bill_Amount ,Ord_DOB ,Ord_Salesman ,Ord_Delivery_ADDRESS, Ord_City , Ord_CONTACT_NO ,Ord_MOP ,Ord_OrderStatus ) 
VALUES(@OrderNo,@Customer,@OrderQty, @BillAmount, @DOD, @Salesman,@DeliveryAddress, @City, @ContactNo, @MOP,@OrderStatus);
END
ELSE
BEGIN
UPDATE Orders SET Ord_No = @OrderNo, Ord_Customer= @Customer, Ord_OrderQty =@OrderQty, Ord_Bill_Amount = @BillAmount, Ord_DOB= @DOD, Ord_Salesman = @Salesman, Ord_Delivery_ADDRESS = @DeliveryAddress, Ord_City = @City, Ord_CONTACT_NO = @ContactNo, Ord_MOP = @MOP, Ord_OrderStatus = @OrderStatus WHERE Ord_ID  = @Id;
END
END

EXEC SP_AddEditOrders null,1001,5,2,200,'2023-04-03', 3, 'Bhuj', 'Bhuj', 942,1
EXEC SP_AddEditOrders null,1002,2,1,2500,'2023-04-01', 4, 'Palnpur', 'Palanpur', 944,1
EXEC SP_AddEditOrders null,1004,10,10,100,'2023-04-02', 5, 'Junagadh', 'Junagadh', 892,3
EXEC SP_AddEditOrders null,1006,9,1,150,'2023-04-01', 6, 'Rajkot', 'Rajkot', 912,1
EXEC SP_AddEditOrders null,1007,9,1,2500,'2023-04-03', 7, 'Rajkot', 'Rajkot', 912,1
EXEC SP_AddEditOrders null,1008,1,1,18000,'2023-04-02', 8, 'Bhuj', 'Bhuj', 951,2
EXEC SP_AddEditOrders null,1009,2,4,1000,'2023-04-03',9, 'Palanpur', 'Palanpur', 944,2
EXEC SP_AddEditOrders null,1010,1,1,3200,'2023-04-02', 10, 'Bhuj', 'Bhuj', 951,2
EXEC SP_AddEditOrders null,1011,2,1,25000,'2023-04-05', 11, 'Rajkot', 'Rajkot', 912,2
EXEC SP_AddEditOrders null,1005,5,1,3200,'2023-04-05', 12, 'Manavadar', 'Manavadar', 842,3

CREATE OR ALTER PROCEDURE SP_AddEditOrderDetails(
@Id INT,
@OrderId INT,
@ItemId INT,
@OrderQty INT
)
AS
BEGIN
IF(@Id IS NULL)
BEGIN
declare @OrderAmount int
set @OrderAmount = (select (i.It_Rate  * @OrderQty) from Items I where i.It_ID  = @ItemId)
INSERT INTO OrderDetails(OD_OrderId ,OD_ItemId ,OD_OrderQty ,OD_OrderAmount )
VALUES(@OrderId,@ItemId,@OrderQty,@OrderAmount);
END
ELSE
BEGIN
UPDATE OrderDetails SET OD_OrderId = @OrderId, OD_ItemId = @ItemId, 
OD_OrderQty = @OrderQty, OD_OrderAmount = @OrderAmount WHERE OD_ID = @Id;
END
END

EXEC SP_AddEditOrderDetails null,31, 1, 5
EXEC SP_AddEditOrderDetails null,22, 3, 1
EXEC SP_AddEditOrderDetails null,24, 5, 10
EXEC SP_AddEditOrderDetails null,26, 6, 1
EXEC SP_AddEditOrderDetails null,27, 3, 1
EXEC SP_AddEditOrderDetails null,28, 9, 1
EXEC SP_AddEditOrderDetails null,29, 7, 4
EXEC SP_AddEditOrderDetails null,30, 4, 1
EXEC SP_AddEditOrderDetails null,23, 8, 1
EXEC SP_AddEditOrderDetails null,25, 4, 1
SELECT * FROM OrderDetails
--3. Create a Parameterized Store Procedure to retrive all the OrderDetails as per Customer (If CustomerId not passed then retrive all the orders) ( Make sure to Add Everything in Single Procedure) [8]
--	Information I want : 
--		--CustomerName
--		--ItemName
--		--Item Rate
--		--Qty
--		--OrderAmount (Qty * ItemRate)
SELECT*FROM Orders
SELECT*FROM Customer
SELECT*FROM Items
SELECT*FROM OrderDetails
CREATE OR ALTER PROCEDURE SP_ORDERDETAILS
(@CustomerID INT) 
AS 
BEGIN
IF(@CustomerID IS NULL)
BEGIN
SELECT C.CUT_NAME,I.It_NAME,I.It_Rate,O.Ord_OrderQty, (O.Ord_OrderQty * I.It_Rate)
FROM Orders O INNER JOIN Customer C ON C.CUT_ID = O.Ord_Customer INNER JOIN OrderDetails OD ON O.Ord_ID = OD.OD_OrderId INNER JOIN Items I ON I.It_ID = OD.OD_ItemId
END
ELSE
BEGIN
SELECT C.CUT_NAME,I.It_NAME,I.It_Rate,O.Ord_OrderQty, OD.OD_OrderAmount
FROM Orders O INNER JOIN Customer C ON C.CUT_ID = O.Ord_Customer INNER JOIN OrderDetails OD ON O.Ord_ID = OD.OD_OrderId INNER JOIN Items I ON I.It_ID = OD.OD_ItemId WHERE C.CUT_ID = @CustomerID
END
END
EXEC SP_ORDERDETAILS 9

---4. Create a User Defined Function that will retrive TotalOrderAmount from the OrderDetails Table as per Customer. [8]
--	I just want total amount as per customerid I Passed.

CREATE FUNCTION FN_ORDERAMOUNT 
(@CUSTID INT)
RETURNS DECIMAL(9,2)
AS
BEGIN 
RETURN (SELECT SUM(OD.OD_OrderAmount) FROM OrderDetails OD INNER JOIN Orders O ON O.Ord_ID = OD.OD_OrderId WHERE O.Ord_Customer = @CUSTID)
END

SELECT dbo.FN_ORDERAMOUNT(10)

	
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

CREATE FUNCTION FN_OederAmount 
(@CUSTID INT)
RETURNS DECIMAL(9,2)
AS
BEGIN 
RETURN (SELECT SUM(OD.OD_OrderAmount) FROM OrderDetails OD INNER JOIN Orders O ON O.Ord_ID = OD.OD_OrderId WHERE O.Ord_Customer = @CUSTID)
END

CREATE FUNCTION FN_ORDERQTY
(@CUSTID INT)
RETURNS INT
AS
BEGIN 
RETURN (SELECT SUM(OD.OD_OrderQty) FROM OrderDetails OD INNER JOIN Orders O ON O.Ord_ID = OD.OD_OrderId WHERE O.Ord_Customer = @CUSTID)
END

CREATE OR ALTER PROCEDURE SP_ORDERINFORMATION
(@CustomerID INT) 
AS 
BEGIN
IF(@CustomerID IS NULL)
BEGIN
SELECT O.Ord_No,O.Ord_OrderQty,OD.OD_OrderAmount,S.Sal_NAME,O.Ord_Delivery_ADDRESS,O.Ord_City,O.Ord_CONTACT_NO,MP.Mod_NAME,O.Ord_DOB
FROM Orders O INNER JOIN Customer C ON C.CUT_ID = O.Ord_Customer INNER JOIN OrderDetails OD ON O.Ord_ID = OD.OD_OrderId INNER JOIN Items I ON I.It_ID = OD.OD_ItemId INNER JOIN Salesman S ON S.Sal_ID = O.Ord_Salesman INNER JOIN ModeOfPayment MP ON MP.Mod_ID = O.Ord_MOP
END
ELSE
BEGIN
SELECT O.Ord_No,O.Ord_OrderQty,OD.OD_OrderAmount,S.Sal_NAME,O.Ord_Delivery_ADDRESS,O.Ord_City,O.Ord_CONTACT_NO,MP.Mod_NAME,O.Ord_DOB
FROM Orders O INNER JOIN Customer C ON C.CUT_ID = O.Ord_Customer INNER JOIN OrderDetails OD ON O.Ord_ID = OD.OD_OrderId INNER JOIN Items I ON I.It_ID = OD.OD_ItemId INNER JOIN Salesman S ON S.Sal_ID = O.Ord_Salesman INNER JOIN ModeOfPayment MP ON MP.Mod_ID = O.Ord_MOP WHERE C.CUT_ID = @CustomerID
END
END

EXEC SP_ORDERINFORMATION 9

--6. Create a Parameterized Store Procedure to Cancel Order as per OrderNo (If I pass wrong orderno then message should be there) [8]

CREATE OR ALTER PROCEDURE Sp_CancelOrder
(@ORDID INT)
AS
BEGIN
IF(@ORDID > (SELECT MAX(O.Ord_ID) FROM Orders O))
BEGIN
PRINT  'INVALID ORDER ID'
END
ELSE
BEGIN
	BEGIN TRAN
	UPDATE Orders SET Ord_OrderStatus=1 WHERE Ord_ID = @ORDID
	COMMIT TRAN
END
END
EXEC Sp_CancelOrder 3

--7. Create a Parameterized Store Procedure for Login as per Customer/Salesman EmailId and Password.  [10]
--	(If credentials matched then show "Login Successfull" then "Invalid User Input" , If EmailId  is not having in the Table then Create Emaild with Password( Random) ).
CREATE OR ALTER PROCEDURE Sp_LOGIN
(@EMAILID VARCHAR(200),@PASSWORD VARCHAR(200))
AS
BEGIN
IF((1=(SELECT COUNT(*) FROM Customer C WHERE C.CUT_EMAIL = @EMAILID AND C.CUT_PASSWORD = @PASSWORD))OR(1=(SELECT COUNT(*) FROM Salesman S WHERE S.Sal_EMAIL = @EMAILID AND S.Sal_PASSWORD = @PASSWORD)))
BEGIN
PRINT 'LOGIN SUCCESSFULL'
END
ELSE IF((1=(SELECT COUNT(*) FROM Customer C WHERE C.CUT_EMAIL = @EMAILID))OR(1=(SELECT COUNT(*) FROM Salesman S WHERE S.Sal_EMAIL = @EMAILID)))
BEGIN
PRINT 'INVALID USER INPUT'
END
ELSE
BEGIN
	EXEC SP_AddEditCustomer null,null,null,'YP@243116','yansipatel24@gmail.com',null,null,null
END
END

EXEC Sp_LOGIN 'bhavya@gmail.com','HeyBhavya'
	
--8. Create a Parameterized Store Procedure for Delete Customer from the UserName	[5]
--	--All the orders linked to that customer should also be deleted.

create procedure deleteCust(
@CustUser varchar(255)
)
as
begin
declare @custid int;
set @custid = (select C.CUT_ID from Customer C where C.CUT_USERNAME =@CustUser)
DELETE FROM OrderDetails WHERE OD_OrderId IN (SELECT O.Ord_ID FROM Orders O WHERE O.Ord_Customer = @custid)
DELETE FROM Orders WHERE Ord_Customer = @custid
DELETE FROM Customer WHERE CUT_ID= @custid
end

EXEC Sp_DELETECUSTOMER 8

--SELECT * FROM Customer
--CREATE OR ALTER PROCEDURE Sp_CUSERNAME
--(@USERNAME VARCHAR(200))
--AS
--BEGIN
--	DECLARE @CUID INT
--	SET @CUID = (SELECT C.CUT_ID FROM Customer C  WHERE CUT_USERNAME = @USERNAME )
--	DELETE FROM Customer WHERE CUT_ID = @CUID
--	DELETE FROM Orders WHERE Ord_Customer = @CUID
--END
--EXEC Sp_CUSERNAME 'HD123'
--9. Create a OrderDetails using OrderId of Order in a Single Store Procedure. [10]

CREATE PROCEDURE CreateOrderDeatil(
@ordId int
)
AS
BEGIN
DECLARE @INDEX INT
DECLARE @TOTAL_ITEM INT
SELECT @TOTAL_ITEM = COUNT(*) FROM Items
SET @INDEX = RAND() * @TOTAL_ITEM
DECLARE @ID INT
SELECT @ID = I.It_ID FROM Items I ORDER BY I.It_ID OFFSET @INDEX ROWS FETCH FIRST 1 ROWs
ONLY;
EXEC SP_AddEditOrderDetails null,@ordId, @ID, 13
END
exec CreateOrderDeatil 29
select * from OrderDetails

--10. Every Data suppose to be Dynamic (no any Hard Coded) [Addition 25 points] [ Example : Name would be Dynamic , amount would be Dyanmic , Date Would be Dynamic , OrderNo  would be Dynamic , etc... ]

