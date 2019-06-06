ALTER PROCEDURE [FoodDelivery_FinalProject].[addPayment]
	
	
    @Type VARCHAR(10),
	@ValueGiven DECIMAL(5,2),
    @PaymentID int OUTPUT,
    @responseMessage NVARCHAR(250) OUTPUT
AS
BEGIN

    BEGIN TRY

        INSERT INTO FoodDelivery_FinalProject.PaymentType (Type,ValueGiven)
		VALUES(@Type,@ValueGiven)

		SELECT  @PaymentID=SCOPE_IDENTITY()


    END TRY
    BEGIN CATCH
        SET @responseMessage=ERROR_MESSAGE() 
    END CATCH

END

  

    



