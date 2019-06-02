CREATE FUNCTION FoodDelivery_FinalProject.getDriverDetails(@DriverID varchar(50))returns Table
AS
	Return(Select Name,Contact,Photo,FoodDelivery_FinalProject.Driver.LicensePlate,Model from FoodDelivery_FinalProject.Driver JOIN FoodDelivery_FinalProject.Vehicle ON FoodDelivery_FinalProject.Driver.LicensePlate=FoodDelivery_FinalProject.Vehicle.LicensePlate
	WHERE FoodDelivery_FinalProject.Driver.LoginName=@DriverID) 