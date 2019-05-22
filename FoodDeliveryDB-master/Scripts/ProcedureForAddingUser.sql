ALTER TABLE  FoodDelivery_FinalProject.Client
ALTER COLUMN Photo VARCHAR(MAX) 

SELECT *
FROM FoodDelivery_FinalProject.Client

CREATE PROCEDURE FoodDelivery_FinalProject.AddUser

    @pLogin NVARCHAR(50), 
    @pPassword NVARCHAR(50),
    @pName NVARCHAR(40),
	@Contact NCHAR(9),
	@Image NVARCHAR(MAX)=NULL,
	@Street NVARCHAR(30),
	@City	NVARCHAR(20),
	@PostalCode NVARCHAR(15),
	@CardNumber NCHAR(16)=NULL,
	@CardExpirationDate NCHAR(5)=NULL,

    @responseMessage NVARCHAR(250) OUTPUT
AS
BEGIN
    SET NOCOUNT ON

    DECLARE @salt UNIQUEIDENTIFIER=NEWID()
    BEGIN TRY

        INSERT INTO FoodDelivery_FinalProject.Client (Name,Contact,Photo,Street,City,PostalCode,CardNumber,CardExpirationDate,Salt,PasswordHash,LoginName )
        VALUES(@pName,@Contact,@Image,@Street,@City,@PostalCode,@CardNumber,@CardExpirationDate,@salt, HASHBYTES('SHA2_512', @pPassword+CAST(@salt AS NVARCHAR(36))),@pLogin)

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
		  @CardNumber=NULL,
		  @CardExpirationDate=NULL,
          @responseMessage=@responseMessage OUTPUT