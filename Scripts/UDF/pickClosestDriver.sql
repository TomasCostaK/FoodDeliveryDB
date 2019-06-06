alter Function FoodDelivery_FinalProject.PickDriver() returns Table
AS
	rETURN(SELECT *
FROM
(Select T2.DriverID,T2.Date,T2.Hour,FoodDelivery_FinalProject.Tracking.GPS_Latitude,FoodDelivery_FinalProject.Tracking.GPS_Longitude
from
(Select t1.DriverID,t1.Date,max(FoodDelivery_FinalProject.Tracking.Hour) as [Hour]
from FoodDelivery_FinalProject.Tracking JOIN 
(Select DriverID,max([DATE]) as [Date]
from	FoodDelivery_FinalProject.Tracking
Group By DriverID) AS t1 on t1.DriverID=FoodDelivery_FinalProject.Tracking.DriverID AND t1.Date=FoodDelivery_FinalProject.Tracking.Date
Group By t1.DriverID,t1.Date) as T2 Join FoodDelivery_FinalProject.Tracking On FoodDelivery_FinalProject.Tracking.DriverID=T2.DriverID and FoodDelivery_FinalProject.Tracking.Date=T2.Date AND FoodDelivery_FinalProject.Tracking.Hour=t2.Hour) as T3
JOIN FoodDelivery_FinalProject.Driver ON T3.DriverID=FoodDelivery_FinalProject.Driver.LoginName
WHERE Ocuppied=0x00)