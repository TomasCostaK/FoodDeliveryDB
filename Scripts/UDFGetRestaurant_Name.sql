ALTER FUNCTION FoodDelivery_FinalProject.getRestaurantName (@RestaurantName VARCHAR(25)) returns Table
AS
	RETURN(SELECT * FROM FoodDelivery_FinalProject.Restaurant where Name LIKE '%'+@RestaurantName+'%')

SELECT * FROM   FoodDelivery_FinalProject.getProfile('Am�lia_Pereira78')
