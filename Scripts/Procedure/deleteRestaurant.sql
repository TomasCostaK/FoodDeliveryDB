CREATE PROCEDURE FoodDelivery_FinalProject.DeleteRestaurant

    @RestaurantID int,
    

    @responseMessage NVARCHAR(250) OUTPUT
AS
BEGIN
    SET NOCOUNT ON
	
	BEGIN TRY

		delete from FoodDelivery_FinalProject.Restaurant where RestaurantID=@RestaurantID

		SET @responseMessage='Account deleted'

	END TRY
	BEGIN CATCH
		SET @responseMessage=ERROR_MESSAGE() 
	END CATCH
	
END