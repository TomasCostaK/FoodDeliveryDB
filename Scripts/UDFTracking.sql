select * from FoodDelivery_FinalProject.Driver where LoginName = 'tomas12'

CREATE FUNCTION FoodDelivery_FinalProject.getTracking (@driverID VARCHAR(50)) returns Table
AS
	RETURN(SELECT GPS_Latitude,GPS_Longitude,Date,Hour FROM FoodDelivery_FinalProject.Tracking Where DriverID=@driverID)


