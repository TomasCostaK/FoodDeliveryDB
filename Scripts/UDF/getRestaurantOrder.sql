ALTER FUNCTION FoodDelivery_FinalProject.getRestaurantOrder(@username VARCHAR(50)) returns Table
AS
	RETURN(SELECT  FoodDelivery_FinalProject.Belongs.RequestID,Type,TotalCost,Name,RequestStatus 
FROM ( SELECT RequestID,Type,TotalCost,RequestStatus from FoodDelivery_FinalProject.Request JOIN  FoodDelivery_FinalProject.PaymentType ON  FoodDelivery_FinalProject.Request.PaymentID= FoodDelivery_FinalProject.PaymentType.PaymentID WHERE ClientID=@username) as T1 JOIN FoodDelivery_FinalProject.Belongs on T1.RequestID=FoodDelivery_FinalProject.Belongs.RequestID
)

