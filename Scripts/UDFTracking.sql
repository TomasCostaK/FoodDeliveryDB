select * from FoodDelivery_FinalProject.Request

CREATE FUNCTION FoodDelivery_FinalProject.getRequests (@driverID VARCHAR(50)) returns Table
AS
	RETURN(SELECT GPS_Latitude,GPS_Longitude,Date,Hour FROM FoodDelivery_FinalProject.Tracking Where DriverID=@driverID)

Select * from FoodDelivery_FinalProject.Driver

CREATE FUNCTION FoodDelivery_FinalProject.getDriver (@LoginName VARCHAR(50)) returns Table
AS
	RETURN(SELECT Name,Contact,Photo,Street,City,PostalCode,LicensePlate FROM FoodDelivery_FinalProject.Driver Where LoginName=@LoginName)


SELECT * FROM   FoodDelivery_FinalProject.getDriver('tomas12')


--drop function  FoodDelivery_FinalProject.getOrders


create function FoodDelivery_FinalProject.getOrders(@driver VARCHAR(50)) returns table
as
	return (select *
			from (	select Trip.RequestID, ClientID, TravelCost, EstimatedTime, RequestStatus
					from FoodDelivery_FinalProject.Driver join FoodDelivery_FinalProject.Trip on Driver.LoginName = Trip.DriverID
							join FoodDelivery_FinalProject.Request on FoodDelivery_FinalProject.Trip.RequestID = FoodDelivery_FinalProject.Request.RequestID
					where DriverID=@driver
					) as subQ
			)

select * from FoodDelivery_FinalProject.Trip
select * from FoodDelivery_FinalProject.getOrders('Mariana_Vasconcelos100000080')

