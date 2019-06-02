alter table [FoodDelivery_FinalProject].[Client]
add CardNumber2 varbinary(128) null

UPDATE [FoodDelivery_FinalProject].[Client]
SET CardNumber=EncryptByPassPhrase('12345', '1000000000000000')

ALTER TABLE [FoodDelivery_FinalProject].[Client]
DROP column CardNumber

SELECT CardNumber FROM FoodDelivery_FinalProject.Client


ALTER PROCEDURE FoodDelivery_FinalProject.AddUser

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
        VALUES(@pName,@Contact,@Image,@Street,@City,@PostalCode,EncryptByPassPhrase('12345',@CardNumber),@CardExpirationDate,@salt, HASHBYTES('SHA2_512', @pPassword+CAST(@salt AS NVARCHAR(36))),@pLogin)

       SET @responseMessage='Success'

    END TRY
    BEGIN CATCH
        SET @responseMessage=ERROR_MESSAGE() 
    END CATCH

END

CREATE FUNCTION FoodDelivery_FinalProject.getProfile (@LoginName VARCHAR(50)) returns Table
AS
	RETURN(SELECT Name,Contact,Photo,Street,City,PostalCode,CONVERT(CHAR(16), DecryptByPassPhrase('12345', CardNumber)), FROM FoodDelivery_FinalProject.Client Where LoginName=@LoginName)


