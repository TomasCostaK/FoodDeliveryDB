ALTER TABLE  FoodDelivery_FinalProject.Client
ADD PasswordHash BINARY(64) NOT NULL
SELECT *
FROM FoodDelivery_FinalProject.Client

ALTER PROCEDURE FoodDelivery_FinalProject.AddDriver
	
	@Model NVARCHAR(20),
	@LicensePlate NCHAR(8), 
    @pLogin NVARCHAR(50), 
    @pPassword NVARCHAR(50),
    @pName NVARCHAR(40),
	@Contact NCHAR(9),
	@Image NVARCHAR(MAX)=NULL,
	@Street NVARCHAR(30),
	@City	NVARCHAR(20),
	@PostalCode NVARCHAR(15),
	
	

    @responseMessage NVARCHAR(250) OUTPUT
AS
BEGIN
    SET NOCOUNT ON

    DECLARE @salt UNIQUEIDENTIFIER=NEWID()

	BEGIN TRY

        INSERT INTO FoodDelivery_FinalProject.Vehicle (LicensePlate,Model )
        VALUES(@LicensePlate,@Model)

       SET @responseMessage='Success'

    END TRY
    BEGIN CATCH
        SET @responseMessage=ERROR_MESSAGE() 
    END CATCH

    BEGIN TRY

        INSERT INTO FoodDelivery_FinalProject.Driver (LoginName,Name,Contact,Photo,Street,City,PostalCode,PasswordHash,Salt,LicensePlate,Ocuppied)
        VALUES(@pLogin,@pName,@Contact,@Image,@Street,@City,@PostalCode,HASHBYTES('SHA2_512', @pPassword+CAST(@salt AS NVARCHAR(36))),@salt,@LicensePlate,0x00)

       SET @responseMessage='Success'

    END TRY
    BEGIN CATCH
        SET @responseMessage=ERROR_MESSAGE() 
    END CATCH

END

DECLARE @responseMessage NVARCHAR(250)

EXEC FoodDelivery_FinalProject.AddUser
          @pLogin = N'Admin',
          @pPassword = N'123',
          @pName = N'Admin',
          @Contact='900000001',
		  @Image=NULL,
		  @Street='ola',
		  @City='adeus',
		  @PostalCode='4812',
		  
          @responseMessage=@responseMessage OUTPUT