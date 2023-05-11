USE YansiViramgama325


CREATE TABLE P_User(
	ID INT PRIMARY KEY IDENTITY(1,1),
	Firstname VARCHAR(200) NOT NULL,
	Lastname VARCHAR(200) NOT NULL,
	Email VARCHAR(200) UNIQUE,
	UPassword VARCHAR(200) CHECK (len(UPassword) >= 8 AND len(UPassword) <= 10) ,
	UserTypeID INT FOREIGN KEY REFERENCES P_UserType(ID),
	UAddress VARCHAR(200),
	MobileNO BIGINT NOT NULL,
	CountryID INT FOREIGN KEY REFERENCES P_Country(ID),
	StateID INT FOREIGN KEY REFERENCES P_States(ID),
	CityID INT FOREIGN KEY REFERENCES P_City(ID)
)
CREATE OR ALTER PROC SP_InsertU (
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
SET @UserTypeID = (SELECT UT.ID FROM P_UserType UT WHERE UT.UserTypeName = @UserType)
SET @CountryID = (SELECT C.ID FROM P_Country C WHERE C.CountryName = @Country)
SET @StateID = (SELECT S.ID FROM P_States S WHERE S.StateName = @State)
SET @CityID = (SELECT PC.ID FROM P_City PC WHERE PC.CityName = @City)
	INSERT INTO P_User VALUES (@Firstname,@Lastname,@Email,@UPassword,@UserTypeID,@UAddress,@MobileNO,@CountryID,@StateID,@CityID)
END
EXEC SP_InsertU 'Yansi','Patel','yv24@gmail.com','Yv@243267','Nurse','jalaram nagar','9979238704','India','Gujrat','Vadodara'
EXEC SP_InsertU 'mansi','Patel','mprt24@gmail.com','Mp@249067','Patient','ram nagar','9979238704','USA','Ohio','kent'
EXEC SP_InsertU 'shivangi','manjariya','fgf24@gmail.com','Yv@243267','Nurse','jalaram nagar','9979238704','USA','Ohio','kent'
EXEC SP_InsertU 'vivek','khatri','yty764@gmail.com','Yv@243267','Doctor','jalaram nagar','9979238704','India','Gujrat','Vadodara'
EXEC SP_InsertU 'priya','Patel','e4544@gmail.com','Yv@243267','Doctor','jalaram nagar','9979238704','India','Gujrat','Vadodara'
SELECT * FROM P_User
DELETE FROM P_User WHERE ID = 5
CREATE TABLE P_UserType(
	ID INT PRIMARY KEY IDENTITY(1,1),
	UserTypeName VARCHAR(200) NOT NULL	
)

CREATE OR ALTER PROC SP_InsertUT (
@UserTypeName VARCHAR(200)
)
AS
BEGIN 
	INSERT INTO P_UserType VALUES (@UserTypeName)
END
EXEC SP_InsertUT 'Patient'
EXEC SP_InsertUT 'Doctor'
EXEC SP_InsertUT 'Nurse'

SELECT*FROM P_UserType

DELETE FROM P_UserType WHERE ID = 5

CREATE TABLE P_Country(
	ID INT PRIMARY KEY IDENTITY(1,1),
	CountryName VARCHAR(200) NOT NULL	
)

CREATE OR ALTER PROC SP_InsertCountry (
@CountryName VARCHAR(200)
)
AS
BEGIN 
	INSERT INTO P_Country VALUES (@CountryName)
END
EXEC SP_InsertCountry 'India'
EXEC SP_InsertCountry 'USA'
EXEC SP_InsertCountry 'Australia'
EXEC SP_InsertCountry 'Rasia'
EXEC SP_InsertCountry 'China'
SELECT * FROM P_Country

CREATE TABLE P_States(
	ID INT PRIMARY KEY IDENTITY(1,1),
	StateName VARCHAR(200) NOT NULL,
	CountryID INT FOREIGN KEY REFERENCES P_Country(ID)
)
CREATE OR ALTER PROC SP_InsertState (
	@StateName VARCHAR(200),
	@CountryName VARCHAR(200) 
)
AS
BEGIN 
	DECLARE @CountryID INT
	 SELECT @CountryID = PC.ID FROM P_Country PC WHERE PC.CountryName = @CountryName
	INSERT INTO P_States VALUES (@StateName,@CountryID)
END
EXEC SP_InsertState 'Gujrat','India'
EXEC SP_InsertState 'Florida','USA'
EXEC SP_InsertState 'Delhi','India'
EXEC SP_InsertState 'Karnataka','India'
EXEC SP_InsertState 'Ohio','USA'
SELECT * FROM P_States
DELETE FROM P_States


CREATE TABLE P_City(
	ID INT PRIMARY KEY IDENTITY(1,1),
	CityName VARCHAR(200) NOT NULL,
	CountryID INT FOREIGN KEY REFERENCES P_Country(ID),
	StateID INT FOREIGN KEY REFERENCES P_States(ID)
)
CREATE OR ALTER PROC SP_InsertCity (
	@CityName VARCHAR(200),
	@Country VARCHAR(200),
	@State VARCHAR(200)
	
	 
)
AS
BEGIN 
	DECLARE @StateID INT, @CountryID INT
	SET @StateID = (SELECT PS.ID FROM P_States PS WHERE PS.StateName = @State)
	SET @CountryID = (SELECT PC.ID FROM P_Country PC WHERE PC.CountryName = @Country)
	
	INSERT INTO P_City VALUES (@CityName,@CountryID,@StateID)
END
EXEC SP_InsertCity 'Vadodara','India','Gujrat'
EXEC SP_InsertCity 'Tampa','USA','Florida'
EXEC SP_InsertCity 'Firozabad','India','Delhi'
EXEC SP_InsertCity 'Mysore','India','Karnataka'
EXEC SP_InsertCity 'kent','USA','Ohio'
SELECT * FROM P_City
DELETE FROM P_City

CREATE TABLE P_TreatmentDetails(
	ID INT PRIMARY KEY IDENTITY(1,1),
	PatientID INT FOREIGN KEY REFERENCES P_User(ID),
	DoctorID INT FOREIGN KEY REFERENCES P_User(ID),
	NurseID INT FOREIGN KEY REFERENCES P_User(ID),
	Diagnosis INT FOREIGN KEY REFERENCES P_Diagnosis(ID),
	Prescription INT FOREIGN KEY REFERENCES P_Medicine(ID),
	TreatmentFee DECIMAL(10,2) NOT NULL,
	DOT DATE,
	Instruction VARCHAR(200)
)
CREATE TABLE P_Diagnosis(
	ID INT PRIMARY KEY IDENTITY(1,1),
	DiagnosisDetails VARCHAR(200) NOT NULL
)
CREATE TABLE P_Medicine(
	ID INT PRIMARY KEY IDENTITY(1,1),
	MedicineName VARCHAR(200) NOT NULL,
	DaignosisID INT FOREIGN KEY REFERENCES P_Diagnosis(ID)
)
