alter Function FoodDelivery_FinalProject.getRestaurantMeal(@RequestID int)returns Table
AS 
	Return(SELECT FoodDelivery_FinalProject.Meal.Name, MealCost,MainIngredient,SideIngredient,Drink,FoodDelivery_FinalProject.Meal.RestaurantID
	FROM FoodDelivery_FinalProject.Belongs JOIN FoodDelivery_FinalProject.Meal ON FoodDelivery_FinalProject.Belongs.Name=FoodDelivery_FinalProject.Meal.Name WHERE FoodDelivery_FinalProject.Belongs.RequestID=@RequestID)

