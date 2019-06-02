
Alter FUNCTION FoodDelivery_FinalProject.getRequest(@RequestID int)returns Table
AS
	Return(Select FoodDelivery_FinalProject.Request.RequestID,PaymentID,TotalCost,TripID,TravelCost,EstimatedTime,Distance,DriverID
	from FoodDelivery_FinalProject.Request JOIN FoodDelivery_FinalProject.Trip ON FoodDelivery_FinalProject.Request.RequestID=FoodDelivery_FinalProject.Trip.RequestID
	Where FoodDelivery_FinalProject.Request.RequestID=@RequestID)