select * from FoodDelivery_FinalProject.Request

CREATE FUNCTION FoodDelivery_FinalProject.getRequests (@driverID VARCHAR(50)) returns Table
AS
	RETURN(SELECT GPS_Latitude,GPS_Longitude,Date,Hour FROM FoodDelivery_FinalProject.Tracking Where DriverID=@driverID)

Select * from FoodDelivery_FinalProject.Driver

