ALTER PROCEDURE FoodDelivery_FinalProject.UpdateUser

    @pLogin NVARCHAR(50), 
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

    BEGIN TRY
		
			Update FoodDelivery_FinalProject.Client 
			SET Contact=@Contact,Street=@Street,City=@City,PostalCode=@PostalCode,CardNumber=EncryptByPassPhrase('12345',@CardNumber),CardExpirationDate=@CardExpirationDate
			WHERE LoginName=@pLogin

		IF @Image <>'nothing'
		BEGIN
			
			Update FoodDelivery_FinalProject.Client 
			SET Photo=@Image
			WHERE LoginName=@pLogin

		END

    END TRY
    BEGIN CATCH
        SET @responseMessage=ERROR_MESSAGE() 
    END CATCH

END

