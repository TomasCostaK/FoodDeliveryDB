

alter PROCEDURE FoodDelivery_FinalProject.addRequest
	
	
    @ClientID VARCHAR(50),
	@PaymentID Varchar(20),
	@TotalCost decimal(5,2),
	



    @responseMessage NVARCHAR(250) OUTPUT
AS
BEGIN

    BEGIN TRY

        INSERT INTO FoodDelivery_FinalProject.Request (ClientID,PaymentID,TotalCost,RequestStatus)
		OUTPUT INSERTED.RequestID
		VALUES('Amélia_Pereira78',6,100.3,0X00)
		


    END TRY
    BEGIN CATCH
        SET @responseMessage=ERROR_MESSAGE() 
    END CATCH

END

    



