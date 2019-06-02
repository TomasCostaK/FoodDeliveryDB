ALTER FUNCTION FoodDelivery_FinalProject.getRestaurant () returns Table
AS
	RETURN(SELECT * FROM FoodDelivery_FinalProject.Restaurant )

SELECT * FROM   FoodDelivery_FinalProject.getProfile('Am�lia_Pereira78')
