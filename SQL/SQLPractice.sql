USE YansiViramgama325

---Tables--
CREATE TABLE UserType1(
	ID INT PRIMARY KEY IDENTITY(1,1),
	UserTypeName VARCHAR(200) NOT NULL	
)
CREATE TABLE Country1(
	ID INT PRIMARY KEY IDENTITY(1,1),
	CountryName VARCHAR(200) NOT NULL	
)
CREATE TABLE States1(
	ID INT PRIMARY KEY IDENTITY(1,1),
	StateName VARCHAR(200) NOT NULL,
	CountryID INT FOREIGN KEY REFERENCES Country1(ID)
)
CREATE TABLE City1(
	ID INT PRIMARY KEY IDENTITY(1,1),
	CityName VARCHAR(200) NOT NULL,
	CountryID INT FOREIGN KEY REFERENCES Country1(ID),
	StateID INT FOREIGN KEY REFERENCES States1(ID)
)
CREATE TABLE User1(
	ID INT PRIMARY KEY IDENTITY(1,1),
	Firstname VARCHAR(200) NOT NULL,
	Lastname VARCHAR(200) NOT NULL,
	Email VARCHAR(200) UNIQUE,
	UPassword VARCHAR(200) CHECK (len(UPassword) >= 8 AND len(UPassword) <= 10) ,
	UserTypeID INT FOREIGN KEY REFERENCES UserType1(ID),
	UAddress VARCHAR(200),
	MobileNO BIGINT NOT NULL,
	CountryID INT FOREIGN KEY REFERENCES Country1(ID),
	StateID INT FOREIGN KEY REFERENCES States1(ID),
	CityID INT FOREIGN KEY REFERENCES City1(ID)
)

CREATE TABLE Diagnosis1(
	ID INT PRIMARY KEY IDENTITY(1,1),
	DiagnosisDetails VARCHAR(200) NOT NULL
)
CREATE TABLE Medicine1(
	ID INT PRIMARY KEY IDENTITY(1,1),
	MedicineName VARCHAR(200) NOT NULL,
	DaignosisID INT FOREIGN KEY REFERENCES Diagnosis1(ID)
)
CREATE TABLE TreatmentDetails1(
	ID INT PRIMARY KEY IDENTITY(1,1),
	PatientID INT FOREIGN KEY REFERENCES User1(ID),
	DoctorID INT FOREIGN KEY REFERENCES User1(ID),
	NurseID INT FOREIGN KEY REFERENCES User1(ID),
	Diagnosis INT FOREIGN KEY REFERENCES Diagnosis1(ID),
	Prescription INT FOREIGN KEY REFERENCES Medicine1(ID),
	TreatmentFee DECIMAL(10,2) NOT NULL,
	DOT DATE,
	Instruction VARCHAR(200)
)


--Insert Data Into UserType1--

CREATE OR ALTER PROC SP_InsertUserType (
@UserTypeName VARCHAR(200)
)
AS
BEGIN 
	INSERT INTO UserType1 VALUES (@UserTypeName)
END
EXEC SP_InsertUserType 'Patient'
EXEC SP_InsertUserType 'Doctor'
EXEC SP_InsertUserType 'Nurse'


--------------------------------------
CREATE TABLE UserType2(
	ID INT PRIMARY KEY IDENTITY(1,1),
	UserTypeName VARCHAR(200)	
)
CREATE OR ALTER PROC SP_InsertUserType2 (@UserName VARCHAR(200) = NUll)
AS
BEGIN 
IF(@UserName IS NOT NULL)
BEGIN
	INSERT INTO UserType2 VALUES (@UserName)
END
else 
begin 
	INSERT INTO UserType2 VALUES (@UserName)
	end
END
EXEC SP_InsertUserType2
SELECT*FROM UserType2

CREATE OR ALTER PROC SP_InsertUserType3 (@UserName VARCHAR(200) = NUll)
AS
BEGIN 
IF(@UserName IS NOT NULL)
BEGIN
	INSERT INTO UserType2 VALUES (@UserName)
END
else 
begin 
	INSERT INTO UserType2 VALUES (@UserName)
	end
END
EXEC SP_InsertUserType3 

-----------------------------------------

--Insert Data Into Country1--

CREATE OR ALTER PROC SP_InsertCountry1 (
@CountryName VARCHAR(200)
)
AS
BEGIN 
	INSERT INTO Country1 VALUES (@CountryName)
END
EXEC SP_InsertCountry1 'India'
EXEC SP_InsertCountry1 'USA'
EXEC SP_InsertCountry1 'Australia'
EXEC SP_InsertCountry1 'Rasia'
EXEC SP_InsertCountry1 'China'

SELECT * FROM Country1

--Insert Data Into State1--

CREATE OR ALTER PROC SP_InsertState1 (
	@StateName VARCHAR(200),
	@CountryName VARCHAR(200) 
)
AS
BEGIN 
	DECLARE @CountryID INT
	 SELECT @CountryID = PC.ID FROM Country1 PC WHERE PC.CountryName = @CountryName
	INSERT INTO States1 VALUES (@StateName,@CountryID)
END
EXEC SP_InsertState1 'Gujrat','India'
EXEC SP_InsertState1 'Florida','USA'
EXEC SP_InsertState1 'Delhi','India'
EXEC SP_InsertState1 'Karnataka','India'
EXEC SP_InsertState1 'Ohio','USA'

SELECT * FROM States1


--Insert Data Into City1--

CREATE OR ALTER PROC SP_InsertCity1 (
	@CityName VARCHAR(200),
	@Country VARCHAR(200),
	@State VARCHAR(200)	 
)
AS
BEGIN 
	DECLARE @StateID INT, @CountryID INT
	SET @StateID = (SELECT PS.ID FROM States1 PS WHERE PS.StateName = @State)
	SET @CountryID = (SELECT PC.ID FROM Country1 PC WHERE PC.CountryName = @Country)
	
	INSERT INTO City1 VALUES (@CityName,@CountryID,@StateID)
END
EXEC SP_InsertCity1 'Vadodara','India','Gujrat'
EXEC SP_InsertCity1 'Tampa','USA','Florida'
EXEC SP_InsertCity1 'Firozabad','India','Delhi'
EXEC SP_InsertCity1 'Mysore','India','Karnataka'
EXEC SP_InsertCity1 'kent','USA','Ohio'

SELECT * FROM City1

--Insert Data Into User1--

CREATE OR ALTER PROC SP_InsertUser1 (
@Firstname VARCHAR(200),
	@Lastname VARCHAR(200),
	@Email VARCHAR(200),
	@UPassword VARCHAR(200),
	@UserType VARCHAR(200),
	@UAddress VARCHAR(200),
	@MobileNO BIGINT,
	@Country VARCHAR(200),
	@State VARCHAR(200),
	@City VARCHAR(200)
)
AS
BEGIN 
DECLARE @UserTypeID INT,@CountryID INT,@StateID INT,@CityID INT
SET @UserTypeID = (SELECT UT.ID FROM UserType1 UT WHERE UT.UserTypeName = @UserType)
SET @CountryID = (SELECT C.ID FROM Country1 C WHERE C.CountryName = @Country)
SET @StateID = (SELECT S.ID FROM States1 S WHERE S.StateName = @State)
SET @CityID = (SELECT PC.ID FROM City1 PC WHERE PC.CityName = @City)
	INSERT INTO User1 VALUES (@Firstname,@Lastname,@Email,@UPassword,@UserTypeID,@UAddress,@MobileNO,@CountryID,@StateID,@CityID)
END
EXEC SP_InsertUser1 'Yansi','Patel','yv24@gmail.com','Yv@243267','Nurse','jalaram nagar','9979238704','India','Gujrat','Vadodara'
EXEC SP_InsertUser1 'mansi','Akbari','mprt24@gmail.com','Mp@249067','Patient','ram nagar','9979238704','USA','Ohio','kent'
EXEC SP_InsertUser1 'shivangi','manjariya','fgf24@gmail.com','Yv@243267','Nurse','jalaram nagar','9979238704','USA','Ohio','kent'
EXEC SP_InsertUser1 'vivek','khatri','yty764@gmail.com','Yv@243267','Doctor','jalaram nagar','9979238704','India','Gujrat','Vadodara'
EXEC SP_InsertUser1 'Priya','Pankhaniya','e4544@gmail.com','Yv@243267','Doctor','jalaram nagar','9979238704','India','Gujrat','Vadodara'

SELECT * FROM User1

--Insert Data Into Daignosis--

CREATE OR ALTER PROC SP_InsertDaignosis1 (
	@DiagnosisDetails VARCHAR(200)	 
)
AS
BEGIN 	
	INSERT INTO Diagnosis1 VALUES (@DiagnosisDetails)
END
EXEC SP_InsertDaignosis1 'Fever'
EXEC SP_InsertDaignosis1 'Cold'
EXEC SP_InsertDaignosis1 'Headache'
EXEC SP_InsertDaignosis1 'Stomaache'
EXEC SP_InsertDaignosis1 'Vomit'

SELECT * FROM Diagnosis1

--Insert Data Into Medicin ---

CREATE OR ALTER PROC SP_InsertMedicine1 (
    @Medicinename VARCHAR(200),
	@DiagnosisDetails VARCHAR(200)	 
)
AS
BEGIN 
DECLARE @DaignosisID INT 
SET @DaignosisID = (SELECT D.ID FROM Diagnosis1 D WHERE D.DiagnosisDetails = @DiagnosisDetails)
	INSERT INTO Medicine1 VALUES (@Medicinename, @DaignosisID)
END
EXEC SP_InsertMedicine1 'Dolo600','Fever'
EXEC SP_InsertMedicine1 'Levocetirizine','Cold'
EXEC SP_InsertMedicine1 'Panadol','Headache'
EXEC SP_InsertMedicine1 'Liquiprin','Stomaache'
EXEC SP_InsertMedicine1 'paracetamol','Vomit'

SELECT * FROM Medicine1

--Insert Data Into TreatmentDetails1 ---

CREATE OR ALTER PROC SP_TreatmentDetails1 (
    @Patient VARCHAR(200),
	@Doctor VARCHAR(200),
	@Nurse VARCHAR(200),
	@Diagnosis VARCHAR(200),
	@TreatmentFee DECIMAL(10,2),
	@DOT DATE,
	@Instruction VARCHAR(200)	 
)
AS
BEGIN 
DECLARE @PatientID INT,@DoctorID INT ,@NurseID INT ,@DaignosisID INT ,@MedicineID INT ,@Prescription VARCHAR(200)
SET @PatientID = (SELECT U.ID FROM User1 U  WHERE U.Firstname = @Patient)
SET @DoctorID = (SELECT U.ID FROM User1 U  WHERE U.Firstname = @Doctor)
SET @NurseID = (SELECT U.ID FROM User1 U  WHERE U.Firstname = @Nurse)
SET @DaignosisID = (SELECT D.ID FROM Diagnosis1 D WHERE D.DiagnosisDetails = @Diagnosis)
SET @Prescription = (SELECT M.MedicineName FROM Medicine1 M WHERE M.DaignosisID = @DaignosisID)
SET @MedicineID = (SELECT M.ID FROM Medicine1 M WHERE M.MedicineName = @Prescription)
	INSERT INTO TreatmentDetails1 VALUES (@PatientID,@DoctorID,@NurseID,@DaignosisID,@MedicineID,@TreatmentFee,@DOT,@Instruction)
END
EXEC SP_TreatmentDetails1 'mansi','vivek','Yansi','Fever',456.89,'2012-12-02','Do Go OutSide'
EXEC SP_TreatmentDetails1 'mansi','Priya','Yansi','Cold',986.00,'2002-09-08','Do not Drink Cold Water'
EXEC SP_TreatmentDetails1 'mansi','vivek','shivangi','Headache',200.45,'2022-02-04','Do Not be Stressed'
EXEC SP_TreatmentDetails1 'mansi','Priya','shivangi','Stomaache',679.09,'2021-06-07','Do not eat FastFood'
EXEC SP_TreatmentDetails1 'mansi','vivek','Yansi','Vomit',123.56,'2020-10-01','Drink Liquid Only'

SELECT * FROM TreatmentDetails1
DELETE FROM TreatmentDetails1
--- get doctors information ---

ALTER PROC SP_GetDoctor 
(@userID INT )
AS
BEGIN
	IF(@userID IS NOT NULL)
	BEGIN
		SELECT CONCAT('Dr. ',U.Firstname,' ',U.Lastname) AS DoctorName ,CONCAT(C1.CityName,' ',S.StateName,' ',C.CountryName) AS [Address] ,U.MobileNO 
		FROM User1 U INNER JOIN UserType1 UT ON U.UserTypeID = UT.ID 
		INNER JOIN Country1 C ON C.ID = U.CountryID 
		INNER JOIN States1 S ON S.ID = U.StateID
		INNER JOIN City1 C1 ON C1.ID = U.CityID  WHERE U.ID = @userID AND UT.UserTypeName = 'Doctor'

	END
	ELSE
	BEGIN
	SELECT CONCAT('Dr. ',U.Firstname,' ',U.Lastname) AS DoctorName ,CONCAT(C1.CityName,' ',S.StateName,' ',C.CountryName) AS [Address] ,U.MobileNO 
		FROM User1 U INNER JOIN UserType1 UT ON U.UserTypeID = UT.ID 
		INNER JOIN Country1 C ON C.ID = U.CountryID 
		INNER JOIN States1 S ON S.ID = U.StateID
		INNER JOIN City1 C1 ON C1.ID = U.CityID WHERE UT.UserTypeName = 'Doctor'
	END
END

EXEC SP_GetDoctor null

--- Function Return Medicine as per Diagnosis ---

CREATE FUNCTION Fn_Medicin_Diagnosis 
(@Daignosis VARCHAR(200))
RETURNS VARCHAR(255)
AS
BEGIN
	declare @var varchar(255);
	select @var = stuff(
(select  ',' + M.MedicineName  from Medicine1 M where M.DaignosisID in (
select Id from Diagnosis1 D where D.DiagnosisDetails in (select [value] from string_split(@Daignosis,',')) ) for xml path(''))
,1,1,'')
	
	RETURN @var
	
END

CREATE FUNCTION LAST_INSERT_ID()
RETURNS INT
AS 
BEGIN
RETURN (SELECT MAX(ID) FROM User1 )
END

SELECT dbo.LAST_INSERT_ID() 


--- 4th --

CREATE OR ALTER PROC Sp_AllInfo (
	@Firstname VARCHAR(200),
	@Lastname VARCHAR(200),
	@Email VARCHAR(200),
	@UPassword VARCHAR(200),
	@UserType VARCHAR(200),
	@UAddress VARCHAR(200),
	@MobileNO BIGINT,
	@City VARCHAR(200)	,
	@Doctor VARCHAR(200),
	@Nurse VARCHAR(200),
	@Diagnosis VARCHAR(200),
	@TreatmentFee DECIMAL(10,2),
	@DOT DATE,
	@Instruction VARCHAR(200)
)
AS 
BEGIN 
	DECLARE 
	@Country VARCHAR(200),
	@State VARCHAR(200)
	SET @State = (SELECT S.StateName FROM States1 S INNER JOIN City1 C ON C.StateID = S.ID WHERE C.CityName = @City )
	SET @Country = (SELECT C.CountryName FROM Country1 C INNER JOIN City1 CT ON CT.CountryID = C.ID WHERE CT.CityName = @City )
	EXEC SP_InsertUser1 @Firstname,@Lastname,@Email,@UPassword,@UserType,@UAddress,@MobileNO,@Country,@State,@City

	
 
	DECLARE @ID INT,@Patient  VARCHAR(200)
	SET @ID = (SELECT dbo.LAST_INSERT_ID())
	SET @Patient =(SELECT U.Firstname FROM User1 U WHERE U.ID = @ID)
	EXEC SP_TreatmentDetails1 @Patient,@Doctor,@Nurse,@Diagnosis,@TreatmentFee,@DOT,@Instruction
	
	SELECT CONCAT(U.Firstname,' ',U.Lastname) AS PatientName,
	@Doctor AS DoctorName,@Nurse AS NurseName,
	dbo.Fn_Medicin_Diagnosis(@Diagnosis) AS MEDICINE,
	TD.DOT,
	CONCAT(U.UAddress,' ',C.CityName,' ',S.StateName,' ',C1.CountryName) AS [Address],
	U.MobileNO,
	CONCAT('Rs ',TD.TreatmentFee)
	FROM User1 U 
	INNER JOIN UserType1 UT ON U.UserTypeID = UT.ID 
	INNER JOIN TreatmentDetails1 TD ON U.ID = TD.PatientID 
	INNER JOIN City1 C ON U.CityID = C.ID
	INNER JOIN States1 S ON U.StateID = S.ID
	INNER JOIN Country1 C1 ON U.CountryID = C1.ID
	WHERE U.Firstname = @Firstname
	
END

EXEC Sp_AllInfo 'Chirag','Patel','chiku23@gmail.com','Chiku789','Patient','ambli-bopal road','9852361047','Firozabad','vivek','Yansi','Headache',456.89,'2012-12-02','Do Go OutSide'

SELECT * FROM User1
SELECT * FROM TreatmentDetails1
DELETE FROM TreatmentDetails1 WHERE ID = 26

----USE OF CASE --- 
--CREATE OR 
ALTER PROC Sp_Description (@Daignosis1 VARCHAR(200))
AS
BEGIN 
DECLARE @Daignosis2 INT
SET @Daignosis2 = (SELECT ID FROM Diagnosis1 D WHERE D.DiagnosisDetails = @Daignosis1 )
UPDATE TreatmentDetails1 SET Instruction = 
CASE 
    WHEN @Daignosis1 = 'Fever' THEN 'Do not Go OutSide'
	 WHEN @Daignosis1 = 'Cold' THEN 'Do not Drink Cold Water'
	 WHEN @Daignosis1 = 'Headache' THEN 'Do Not be Stressed'
	 WHEN @Daignosis1 = 'Stomaache' THEN 'Do not eat FastFood'
	  WHEN @Daignosis1 = 'Vomit' THEN 'Drink Liquid Only'
END
WHERE Diagnosis = @Daignosis2 
END

EXEC Sp_Description 'Vomit'

SELECT * FROM TreatmentDetails1

---UNION OF TABLES 

CREATE PROC Sp_all 
AS
BEGIN 
	SELECT CityName FROM City1
	UNION ALL
	SELECT StateName FROM States1
	UNION ALL
	SELECT CountryName FROM Country1	
END
EXEC Sp_all

---EXISTS --
SELECT U.Firstname
	FROM User1 U 
	WHERE EXISTS
	(SELECT C.CountryName FROM Country1 C WHERE U.CountryID = C.ID AND C.CountryName = 'India' )

--- USE OF ANY

SELECT U.Firstname
FROM User1 U 
WHERE U.CountryID = ANY
  (SELECT C.ID
  FROM Country1 C
  WHERE C.CountryName = 'India')

---TEMP TABLE 

CREATE TABLE #UserType (
	ID INT,
	UserTypeName VARCHAR(200) 
)

SELECT * INTO #UserType1
FROM UserType1

SELECT * FROM #UserType1

---INJECTION 

SELECT * FROM User1 WHERE ID = 5 OR 1=0
SELECT * FROM User1 WHERE ID = 5 OR 1=1

--- LIMIT 

SELECT  TOP 3 * FROM User1 


--- JOINS 

SELECT * FROM User1 U INNER JOIN City1 C ON C.ID = U.CityID
SELECT * FROM User1 U FULL OUTER JOIN City1 C ON C.ID = U.CityID
SELECT * FROM User1 U RIGHT JOIN City1 C ON C.ID = U.CityID
SELECT * FROM User1 U LEFT JOIN City1 C ON C.ID = U.CityID

--- ASCII 

SELECT ASCII(U.Firstname) AS NumCodeOfFirstChar
FROM User1 U

SELECT CHARINDEX('t', 'Customer') AS MatchPosition

DECLARE @d DATETIME = '12/01/2018';
SELECT FORMAT (@d, 'd', 'en-US') AS 'US English Result',
               FORMAT (@d, 'd', 'no') AS 'Norwegian Result',
               FORMAT (@d, 'd', 'zu') AS 'Zulu Result'

--- CAST 

CREATE Table Authors
(
   Id INT IDENTITY PRIMARY KEY,
   Author_name VARCHAR(50),
   Country VARCHAR(50)
)
CREATE Table Books
(
   Id INT IDENTITY PRIMARY KEY,
   Auhthor_id INT FOREIGN KEY REFERENCES Authors(Id),
   Price INT,
   Edition INT
)

DECLARE @Id INT
SET @Id = 1

WHILE @Id <= 12000
BEGIN 
   INSERT INTO Authors VALUES ('Author - ' + CAST(@Id AS VARCHAR(10)),
              'Country - ' + CAST(@Id AS VARCHAR(10)) + ' name')
   PRINT @Id
   SET @Id = @Id + 1
END

SELECT * FROM Authors

SELECT QUOTENAME('abcdef', '<>')

SELECT REPLACE('ABC ABC ABC', 'A', 'a')

SELECT REPLICATE('Oh No....', 5) 

SELECT REVERSE(U.Firstname) FROM User1 U

SELECT RIGHT(U.Firstname,3) FROM User1 U
SELECT LEFT(U.Firstname,3) FROM User1 U

SELECT TRANSLATE('Monday', 'Monday', 'Sunday')
SELECT TRANSLATE('3*[2+1]/{8-4}', '[]{}', '()()')

SELECT UNICODE(C.CountryName), C.CountryName FROM Country1 C

--Offset and fetch data 

SELECT * FROM Actor ORDER BY Actor_First_Name

SELECT * FROM Actor A ORDER BY A.Actor_First_Name OFFSET 0 ROWS 
FETCH NEXT 5 ROWS ONLY;

----Random Data 

CREATE TABLE [Sample] ( 
Id INT PRIMARY KEY IDENTITY(1,1), 
[Name] VARCHAR(50), 
Price INT
)

---VIEWS 

CREATE VIEW RandomSmallString
AS
SELECT CHAR(ROUND(97 + RAND()*26,122)) AS VALUE

CREATE VIEW RandomCapitalString
AS
SELECT CHAR(ROUND(65 + RAND()*26,90)) AS VALUE

CREATE VIEW RandomInterger
AS
SELECT ROUND(RAND()*10000,0) AS VALUE

--FUNCTION 

CREATE OR ALTER FUNCTION Fn_RandomString(@Random INT)
RETURNS VARCHAR(255)
AS
BEGIN
      DECLARE  @String VARCHAR(255), @Number INT 
	  SET @String =  ''
	  SET @Number = 0
	  WHILE(@Number < @Random)
	  BEGIN
	       SET @String = @String +( (SELECT [value] FROM RandomCapitalString )+(SELECT [value] FROM RandomSmallString ))
		    SET @Number = @Number + 1			
	  END
	  RETURN @String
END

PRINT dbo.Fn_RandomString(2)

--PROCEDURE 

CREATE PROC SP_Random 
@RandomNumber INT,@InsertNumber INT
AS
BEGIN 
	DECLARE @SampleName VARCHAR(200),@Num INT , @RandomNum INT
	SET @SampleName = (SELECT dbo.Fn_RandomString(@RandomNumber))
	SET @Num = 0
	SET @RandomNum = (SELECT [value] FROM RandomInterger)
	WHILE(@Num<@InsertNumber)
	BEGIN
		SET @SampleName = (SELECT dbo.Fn_RandomString(@RandomNumber))
		SET @RandomNum = (SELECT [value] FROM RandomInterger)
		INSERT INTO [Sample] VALUES (@SampleName,@RandomNum)
		SET @Num = @Num + 1
	END

END

EXEC SP_Random 2,1000

SELECT * FROM [Sample]


---Pagination 

--SELECT * FROM [Sample] ORDER BY [Sample].Price OFFSET 0 ROWS FETCH NEXT 10 ROWS ONLY;

CREATE OR ALTER PROCEDURE Sp_Pagination_Test 
  @PageNumber INT,
  @PageSize   INT 
AS
BEGIN
  SELECT *
    FROM [Sample]
    ORDER BY [Sample].Price
    OFFSET @PageSize * (@PageNumber - 1) ROWS
    FETCH NEXT @PageSize ROWS ONLY;
END
EXEC Sp_Pagination_Test 100,10
EXEC Sp_Pagination_Test 2,20
