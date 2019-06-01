alter Function FoodDelivery_FinalProject.getRestaurantByMeal(@MealName Varchar(50))Returns Table
AS 
	Return(SELECT FoodDelivery_FinalProject.Restaurant.Name, FoodDelivery_FinalProject.Restaurant.RestaurantID,Contact,Street,City,Type,FoodDelivery_FinalProject.Meal.Name as MealName, mainIngredient, sideIngredient,drink,MealCost
	FROM FoodDelivery_FinalProject.Meal join FoodDelivery_FinalProject.Restaurant ON FoodDelivery_FinalProject.Restaurant.RestaurantID=FoodDelivery_FinalProject.Meal.RestaurantID
	WHERE FoodDelivery_FinalProject.Meal.Name=@MealName)


