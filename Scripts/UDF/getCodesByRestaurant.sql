ALTER FUNCTION [FoodDelivery_FinalProject].[getCodeByRestaurant] (@RestaurantID int) returns Table
AS
	return(Select * from FoodDelivery_FinalProject.Promotional where RestaurantID=@RestaurantID)