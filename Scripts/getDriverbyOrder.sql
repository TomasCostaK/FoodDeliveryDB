create FUNCTION FoodDelivery_FinalProject.getDriverDriverbyOrder (@RequestID int) returns @rTable Table(
	[Name] varchar(40),
	Veichle varchar(30),
	Location varchar(20),
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

	INSERT INTO @rTable (Name,Veichle,Location,LicensePlate,Contact,Photo) SELECT Name,Veichle,Location,LicensePlate,Contact,Photo FROM   FoodDelivery_FinalProject.getDriver(@DriverID) JOIN FoodDelivery_FinalProject.Vehicle ON FoodDelivery_FinalProject.Vehicle.DriverID=

	return

END
