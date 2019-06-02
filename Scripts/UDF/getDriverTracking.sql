alter FUNCTION FoodDelivery_FinalProject.getDriverTracking(@DriverID varchar(50))returns Table
AS
	Return(SELECT * FROM   FoodDelivery_FinalProject.Tracking WHERE DriverID=@DriverID) 