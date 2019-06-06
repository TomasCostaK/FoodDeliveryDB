
create function FoodDelivery_FinalProject.getOrders(@driver VARCHAR(50), @choice binary) returns table
as
	return (select *
			from (	select Trip.RequestID, ClientID, TravelCost, EstimatedTime, RequestStatus,City,Street
					from FoodDelivery_FinalProject.Driver join FoodDelivery_FinalProject.Trip on Driver.LoginName = Trip.DriverID
							join FoodDelivery_FinalProject.Request on FoodDelivery_FinalProject.Trip.RequestID = FoodDelivery_FinalProject.Request.RequestID
					where DriverID=@driver and RequestStatus=@choice
					) as subQ
			)

