CREATE PROCEDURE FoodDelivery_FinalProject.UpdateRestaurant

    @RestaurantID INT,
	@RestaurantName NVARCHAR(25), 
	@Contact NCHAR(9),
	@Street NVARCHAR(30),
	@City	NVARCHAR(20),
	@PostalCode NVARCHAR(15),
	@Type NVARCHAR(20),

    @responseMessage NVARCHAR(250) OUTPUT
AS
BEGIN

    BEGIN TRY
		
			Update FoodDelivery_FinalProject.Restaurant
			SET Contact=@Contact,Street=@Street,City=@City,PostalCode=@PostalCode,Name=@RestaurantName,Type=@Type
			WHERE RestaurantID=@RestaurantID
			SET @responseMessage='Success'

		

    END TRY
    BEGIN CATCH
        SET @responseMessage=ERROR_MESSAGE() 
    END CATCH

END
