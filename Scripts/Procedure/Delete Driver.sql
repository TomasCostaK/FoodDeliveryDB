CREATE PROCEDURE FoodDelivery_FinalProject.DeleteDriver

    @DriverID varchar(50),
    

    @responseMessage NVARCHAR(250) OUTPUT
AS
BEGIN
    SET NOCOUNT ON
	
	BEGIN TRY

		delete from FoodDelivery_FinalProject.Driver where LoginName=@DriverID

		SET @responseMessage='Account deleted'

	END TRY
	BEGIN CATCH
		SET @responseMessage=ERROR_MESSAGE() 
	END CATCH
	
END