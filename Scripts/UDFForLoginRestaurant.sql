CREATE FUNCTION FoodDelivery_FinalProject.restaurantLogin (@RestaurantID int) returns int
AS
BEGIN
	DECLARE @ret int; 
	IF EXISTS (SELECT TOP 1 RestaurantID  FROM FoodDelivery_FinalProject.Restaurant WHERE  RestaurantID=@RestaurantID)
		SET @ret=1;
	ELSE 
		SET @ret=0;
	RETURN @ret;
END

print(FoodDelivery_FinalProject.restaurantLogin (2313123))
