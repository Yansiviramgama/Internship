
USE Yansi_KalmTrainingCompanyPortal

------------------------------------User Login Table --------------------------------

CREATE TABLE [User_Login_Details](
UserID INT IDENTITY (1,1) PRIMARY KEY,
[User_Name] VARCHAR(100) NOT NULL,
User_Email VARCHAR(100) UNIQUE,
User_Password VARCHAR(20) NOT NULL,
JWTToken NVARCHAR(max),
TokenDate DATETIME,
ISActive BIT DEFAULT 1,
IsDeleted BIT DEFAULT 0,
OTP BIGINT
)
INSERT INTO User_Login_Details ([User_Name],User_Email,User_Password)VALUES('Yansi','xyz36695@gmail.com','1010')

-----------------------------------------User Data By Email--------------------------------

CREATE PROC Sp_GetUserDataByEmail(@Email VARCHAR(200))
AS
BEGIN
	SELECT * FROM User_Login_Details WHERE User_Email = @Email
END

------------------------------------ User Login Response ------------------------------

CREATE PROC Sp_UserLogin
@UserEmail VARCHAR(200),
@UserPassword VARCHAR(200)
AS
BEGIN
	SELECT * FROM User_Login_Details U WHERE U.User_Password = @UserPassword AND U.User_Email = @UserEmail
END

---------------------------------- Update Valid Token----------------------------------

CREATE PROC Sp_UpdateToken
@UserId INT,
@Token NVARCHAR(max)
AS
BEGIN
	UPDATE User_Login_Details SET JWTToken = @Token, TokenDate = GETUTCDATE() WHERE UserID = @UserId
END

----------------------------------------Validate Token---------------------------------------

CREATE PROC Sp_ValidateToken
@UserID INT,
@Token NVARCHAR(max),
@ValidDate DATETIME
AS
BEGIN
	DECLARE @Count INT, @Date DATETIME
	 SET @Count = (SELECT COUNT(U.JWTToken) FROM User_Login_Details U WHERE U.UserID = @UserID AND U.ISActive = 1 AND U.IsDeleted = 0)
	 SET @Date = (SELECT U.TokenDate FROM User_Login_Details U WHERE U.UserID = @UserID AND U.JWTToken = @Token)
	IF(@Count= 1)
	 BEGIN
		IF(@ValidDate>@Date)
		 BEGIN
			SELECT 1
		 END
		ELSE
		 BEGIN
			SELECT 0
		 END
	 END
	ELSE
	 BEGIN
		SELECT 0
	 END
END
------------------------------------- Comapany Management ---------------------------

CREATE TABLE CompanyList(
	CompanyID INT PRIMARY KEY IDENTITY(1,1),
	ComanyName VARCHAR(200)
)
INSERT INTO CompanyList VALUES ('Shaligram Infotech'),('Tridhya Tech Limited'),('Satva Softech'),('Infosys'),('TCS'),('WebBetter'),('Shaligram Softech'),('ItPath Solution')

 ----------------------------------- All Comapanies List --------------------------------

 CREATE PROC Sp_AllCompaniesList
 AS
 BEGIN
	SELECT * FROM CompanyList
 END

 -------------------------------- Country List ---------------------

 CREATE TABLE Country(
	CountryID INT PRIMARY KEY IDENTITY(1,1),
	CountryName VARCHAR(200)
 )
 DROP TABLE Country
 INSERT INTO Country VALUES ('India'),('USA'),('UK'),('Australia'),('Rassia'),('Pakistan'),('Shrilanka')

 -------------------------------- All Country List----------------------

 CREATE PROC Sp_AllCountriesList
 AS
 BEGIN
	SELECT * FROM Country
 END

 -------------- Company Details Table---------------------

 CREATE TABLE CompanyDetails(
	ID INT PRIMARY KEY IDENTITY (1,1),
	CompanyID INT FOREIGN KEY REFERENCES CompanyList(CompanyID),
	Cityname VARCHAR(200),
	CountryId INT FOREIGN KEY REFERENCES Country(CountryID),
	PostCode VARCHAR(200),
	[Image] VARCHAR(max),
	Tool VARCHAR(100)
 )
 DROP TABLE CompanyDetails
 INSERT INTO CompanyDetails VALUES
 (1,'Ahemdabad',1,'H51OG','ShaligramInfotech.jpg','ChangeMatrix'),
 (2,'Ohio',2,'UI51OG','Tridhya.jpg','ChangeMatrix'),
 (3,'Melbourn',4,'H5ERFG','download.jpg','ChangeMatrix'),
 (4,'Ahemdabad',1,'WER57','Infosys.png','ChangeMatrix'),
 (5,'Ahemdabad',1,'BFD5F','Tcs.png','ChangeMatrix')

 -------------- Store Procedures For Company Details --------------------
--sp_rename 'CompanyList.ComanyName', 'CompanyName', 'COLUMN';

----------------------------------Company Details ---------------------------------------------------

 ALTER PROC Sp_CompanyDetails
 AS
 BEGIN
	SELECT CD.*,C.CountryName,CL.CompanyName FROM CompanyDetails CD INNER JOIN CompanyList CL ON CL.CompanyID = CD.CompanyID
	INNER JOIN Country C ON C.CountryID = CD.CountryId   
 END
--------------------------------------Company Details By Id---------------------------------------------

 ALTER PROC Sp_CompanyDetailsById(@Id INT)
 AS
 BEGIN
	SELECT CD.*,C.CountryName,CL.CompanyName FROM CompanyDetails CD INNER JOIN CompanyList CL ON CL.CompanyID = CD.CompanyID
	INNER JOIN Country C ON C.CountryID = CD.CountryId WHERE CD.ID = @Id  
 END

 ---------------------------------Add-Edit Company Details---------------------------------------------------

 ALTER PROC Sp_AddEditCompanyDetails(
	@ID INT,
	@CompanyID INT,
	@Cityname VARCHAR(200),
	@CountryId INT,
	@PostCode VARCHAR(200),
	@ComapanyLogo VARCHAR(max),
	@Tool VARCHAR(100)
 )
 AS
 BEGIN
	IF(@Id = 0 OR @ID IS NULL)
		BEGIN
			INSERT INTO CompanyDetails(CompanyID,Cityname,CountryId,PostCode,ComapanyLogo,Tool )
			VALUES (@CompanyID,@Cityname,@CountryId,@PostCode,@ComapanyLogo,@Tool)
		END
	ELSE
		BEGIN
			UPDATE CompanyDetails SET CompanyID = @CompanyID, Cityname = @Cityname, CountryId = @CountryId, PostCode = @PostCode, [Image] = @ComapanyLogo, Tool = @Tool WHERE ID = @ID
		END
 END

 ----------------------------------Delete Company Details --------------------

 CREATE PROC Sp_DeleteCompany(@ID int)
 AS
 BEGIN
	DELETE FROM CompanyDetails WHERE ID = @ID;
 END

 ----------------------------------OTP------------------------------------------
 
 -------------------------------Update OTP --------------------------------------

 ALTER PROC SP_UpdateOTP(
	@UserID INT,
	@OTP BIGINT
)
AS 
BEGIN
	BEGIN TRY
	BEGIN TRAN 
		UPDATE User_Login_Details
		SET OTP = @OTP WHERE UserID = @UserID
	COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH
END

-------------------------------------Verify OTP----------------------------------------------------

CREATE OR ALTER PROC SP_VerifyOTP(
	@OTP INT,
	@EMAIL VARCHAR(255)
)
AS
BEGIN
	IF EXISTS (SELECT * FROM User_Login_Details WHERE User_Email = @EMAIL AND OTP = @OTP)
	BEGIN
		SELECT 1
	END
	ELSE 
	BEGIN
		SELECT 0
	END
END

------------------------------------Update Password---------------------------

CREATE PROC SP_Update_Password(
	@EMAIL VARCHAR(255),
	@NEWPASSWORD VARCHAR(255)
)
AS 
BEGIN
	BEGIN TRY
		BEGIN TRAN
			IF EXISTS (SELECT * FROM User_Login_Details WHERE User_Email = @EMAIL AND ISActive = 1 AND IsDeleted = 0)
			BEGIN
				UPDATE User_Login_Details
				SET User_Password = @NEWPASSWORD 
				WHERE User_Email = @EMAIL
				SELECT 2
			END
			ELSE 
			BEGIN
				SELECT 1
			END
		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH
END

----------------------------------Pagination ---------------------------

ALTER PROCEDURE Sp_DataInDataTable (  
    @sortColumn VARCHAR(50)  
    ,@sortOrder VARCHAR(50)  
    ,@OffsetValue INT  
    ,@PagingSize INT  
    ,@SearchText VARCHAR(50)  
    )  
AS  
BEGIN  
SELECT CD.ID, CD.[Image],CL.CompanyName,C.CountryName,CD.Cityname,CD.PostCode,CD.Tool,count(ID) OVER () AS FilterTotalCount  FROM CompanyDetails CD INNER JOIN CompanyList CL ON CL.CompanyID = CD.CompanyID
	INNER JOIN Country C ON C.CountryID = CD.CountryId     
    WHERE (  
            (  
                @SearchText <> ''  
                AND (  
                    CompanyName LIKE '%' + @SearchText + '%'  
                    OR Cityname LIKE '%' + @SearchText + '%'
					OR CountryName LIKE '%' + @SearchText + '%'
					OR PostCode LIKE '%' + @SearchText + '%'
                    )  
                )  
            OR (@SearchText = '')  
            )  
    ORDER BY CASE   
            WHEN @sortOrder <> 'ASC'  
                THEN ''  
            WHEN @sortColumn = 'CompanyName'  
                THEN CompanyName  
            END ASC  
        ,CASE   
            WHEN @sortOrder <> 'Desc'  
                THEN ''  
            WHEN @sortColumn = 'CompanyName'  
                THEN CompanyName  
            END DESC  
        ,CASE   
            WHEN @sortOrder <> 'ASC'  
                THEN 0  
            WHEN @sortColumn = 'CountryName'  
                THEN CountryName  
            END ASC  
        ,CASE   
            WHEN @sortOrder <> 'DESC'  
                THEN 0  
            WHEN @sortColumn = 'CountryName'  
                THEN CountryName  
            END DESC  
        ,CASE   
            WHEN @sortOrder <> 'ASC'  
                THEN ''  
            WHEN @sortColumn = 'Cityname'  
                THEN Cityname  
            END ASC  
        ,CASE   
            WHEN @sortOrder <> 'DESC'  
                THEN ''  
            WHEN @sortColumn = 'Cityname'  
                THEN Cityname  
            END DESC  
        ,CASE   
            WHEN @sortOrder <> 'ASC'  
                THEN ''  
            WHEN @sortColumn = 'PostCode'  
                THEN PostCode  
            END ASC  
        ,CASE   
            WHEN @sortOrder <> 'DESC'  
                THEN ''  
            WHEN @sortColumn = 'PostCode'  
                THEN PostCode  
            END DESC  
        ,CASE   
            WHEN @sortOrder <> 'ASC'  
                THEN ''  
            WHEN @sortColumn = 'Tool'  
                THEN Tool  
            END ASC  
        ,CASE   
            WHEN @sortOrder <> 'DESC'  
                THEN ''  
            WHEN @sortColumn = 'Tool'  
                THEN Tool  
            END DESC OFFSET @OffsetValue ROWS  
  
    FETCH NEXT @PagingSize ROWS ONLY  
END 

EXEC Sp_DataInDataTable 'CompanyName','ASC',0,10,'USA'