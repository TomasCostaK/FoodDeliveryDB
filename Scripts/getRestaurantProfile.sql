CREATE FUNCTION FoodDelivery_FinalProject.getRestaurantProfile (@RestaurantID INT) returns Table
AS
	RETURN(SELECT Name,Contact,Street,City,PostalCode,Type FROM FoodDelivery_FinalProject.Restaurant Where RestaurantID=@RestaurantID)

SELECT * FROM   FoodDelivery_FinalProject.getRestaurantProfile(1)