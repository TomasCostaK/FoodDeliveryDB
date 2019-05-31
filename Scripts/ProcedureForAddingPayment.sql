alter PROCEDURE FoodDelivery_FinalProject.addRequest
	
	
    @ClientID VARCHAR(50),
	@PaymentID Varchar(20),
	@RequestID int OUTPUT,

	@TotalCost decimal(5,2),
	



    @responseMessage NVARCHAR(250) OUTPUT
AS
BEGIN

    BEGIN TRY

        INSERT INTO FoodDelivery_FinalProject.Request (ClientID,PaymentID,TotalCost,RequestStatus)
		
		VALUES(@ClientID,@PaymentID,@TotalCost,0X00)
		SELECT  @RequestID=SCOPE_IDENTITY()


    END TRY
    BEGIN CATCH
        SET @responseMessage=ERROR_MESSAGE() 
    END CATCH

END

  

    



