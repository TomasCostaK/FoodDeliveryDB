ALTER FUNCTION FoodDelivery_FinalProject.getRestaurant () returns Table
AS
	RETURN(SELECT * FROM FoodDelivery_FinalProject.Restaurant )

