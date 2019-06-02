alter FUNCTION FoodDelivery_FinalProject.getDriverbyOrder (@RequestID int) returns @rTable Table(
	[Name] varchar(40),
	DriverID varchar(50),
	Vehicle varchar(30),	
	LicensePlate char(8),
	Contact char(9),
	Photo varchar(max)
)
AS
BEGIN
	declare @DriverID varchar(50)
	set @DriverID=( Select DriverID
	FROM FoodDelivery_FinalProject.Request join FoodDelivery_FinalProject.Trip on FoodDelivery_FinalProject.Request.RequestID=FoodDelivery_FinalProject.Trip.RequestID
	Where FoodDelivery_FinalProject.Request.RequestID=@RequestID)

	INSERT INTO @rTable (Name,DriverID,Vehicle,LicensePlate,Contact,Photo) SELECT Name,LoginName, FoodDelivery_FinalProject.Vehicle.Model, FoodDelivery_FinalProject.Vehicle.LicensePlate,Contact,Photo FROM   FoodDelivery_FinalProject.Driver as t1 JOIN FoodDelivery_FinalProject.Vehicle ON FoodDelivery_FinalProject.Vehicle.LicensePlate= t1.LicensePlate where LoginName=@DriverID

	return

END