CREATE PROCEDURE FoodDelivery_FinalProject.addBelong
	
	
    @Name VARCHAR(50),
	@RestaurantID VARCHAR(50),
	@RequestID VARCHAR(50),
	



    @responseMessage NVARCHAR(250) OUTPUT
AS
BEGIN

    BEGIN TRY

        INSERT INTO FoodDelivery_FinalProject.Belongs(Name,RestaurantID,RequestID)
		VALUES(@Name,@RestaurantID,@RequestID)
		


    END TRY
    BEGIN CATCH
        SET @responseMessage=ERROR_MESSAGE() 
    END CATCH

END



