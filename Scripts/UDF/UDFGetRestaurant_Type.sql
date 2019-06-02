ALTER FUNCTION FoodDelivery_FinalProject.getRestaurantType () returns Table
AS
	RETURN(SELECT DISTINCT Type FROM FoodDelivery_FinalProject.Restaurant )
SELECT * FROM   FoodDelivery_FinalProject.getProfile('Am�lia_Pereira78')
