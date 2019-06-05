alter FUNCTION FoodDelivery_FinalProject.getDetailsOrderDriver (@DriverID varchar(50), @reqID int) returns Table
AS
  return(SELECT TripID, TravelCost, EstimatedTime, cl.Name, Contact, Photo, City FROM FoodDelivery_FinalProject.Trip as tp join FoodDelivery_FinalProject.Request as rq
	on tp.RequestID=rq.RequestID join FoodDelivery_FinalProject.Client as cl on rq.ClientID=cl.LoginName
	where DriverID=@DriverID and rq.RequestID=@reqID
)

select * from FoodDelivery_FinalProject.getDetailsOrderDriver('Cremilde_Medeiros100000069',8)
