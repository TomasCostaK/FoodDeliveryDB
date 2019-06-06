CREATE PROCEDURE FoodDelivery_FinalProject.DeleteUser

    @pLogin NVARCHAR(50),
    

    @responseMessage NVARCHAR(250) OUTPUT
AS
BEGIN
    SET NOCOUNT ON
	
	BEGIN TRY

		delete from FoodDelivery_FinalProject.Client where LoginName=@pLogin

		SET @responseMessage='Account deleted'

	END TRY
	BEGIN CATCH
		SET @responseMessage=ERROR_MESSAGE() 
	END CATCH
	
END