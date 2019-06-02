alter Function FoodDelivery_FinalProject.getRestaurantOrderComplex(@username VARCHAR(50)) returns Table
AS
	Return(SELECT T4.RequestID,PaymentType,TotalCost,MealName,RestaurantID,MainIngredient,SideIngredient,Drink,EstimatedTime,RequestStatus,MealCost
	FROM
	(SELECT T2.RequestID,Type as PaymentType,TotalCost,T2.Name as MealName,RestaurantID,MainIngredient,SideIngredient,Drink,RequestStatus,MealCost FROM
	FoodDelivery_FinalProject.getRestaurantOrder(@username) as T2 JOIN FoodDelivery_FinalProject.Meal on T2.Name=FoodDelivery_FinalProject.Meal.Name) as T4 JOIN  FoodDelivery_FinalProject.Trip ON  FoodDelivery_FinalProject.Trip.RequestID=T4.RequestID)
