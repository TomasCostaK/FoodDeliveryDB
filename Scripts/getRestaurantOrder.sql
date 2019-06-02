alter FUNCTION FoodDelivery_FinalProject.getRestaurantOrder (@RestaurantID int) returns Table
AS

	Return(Select mealName,RestaurantID,MainIngredient,SideIngredient,Drink,t2.RequestID,ClientID,PaymentID,TotalCost,RequestStatus
	from 
	(Select t1.mealName,t1.RestaurantID,t1.MainIngredient,t1.SideIngredient,t1.Drink,RequestID
	From
	(SELECT  FoodDelivery_FinalProject.Restaurant.RestaurantID, FoodDelivery_FinalProject.Meal.Name as mealName, MealCost,MainIngredient,SideIngredient,Drink
	FROM FoodDelivery_FinalProject.Restaurant JOIN  FoodDelivery_FinalProject.Meal on FoodDelivery_FinalProject.Restaurant.RestaurantID= FoodDelivery_FinalProject.Meal.RestaurantID) as t1 join  FoodDelivery_FinalProject.Belongs ON  t1.RestaurantID= FoodDelivery_FinalProject.Belongs.RestaurantID AND t1.mealName= FoodDelivery_FinalProject.Belongs.Name) as t2 JOIN  FoodDelivery_FinalProject.Request ON  FoodDelivery_FinalProject.Request.RequestID=t2.RequestID WHERE RestaurantID=@RestaurantID)

Select *
from FoodDelivery_FinalProject.getRestaurantOrder(23)