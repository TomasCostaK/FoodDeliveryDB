Select *
FROM FoodDelivery_FinalProject.Client
 

UPDATE FoodDelivery_FinalProject.Tracking
SET CITY='Leiria'
WHERE GPS_Latitude='39.743620' AND GPS_Longitude='-8.807050'

Select *
from FoodDelivery_FinalProject.Client

Insert into FoodDelivery_FinalProject.Tracking (GPS_Latitude,GPS_Longitude,Date,Hour,DriverID,City)
Values (39.743620,-8.807050,'2019-06-1','04:14:57.0000000','José_Vasconcelos100000023','Leiria')


ALTER TRIGGER FoodDelivery_FinalProject.CheckDriverRequest on FoodDelivery_FinalProject.Tracking
AFTER INSERT,UPDATE
AS
	SET NOCOUNT ON;

	DECLARE @DriverID  CHAR(50)
    DECLARE @RequestID  varchar(50)
	DECLARE @TrackingCity  NVARCHAR(20)
	DECLARE @ClientCity NVARCHAR(20)

	SELECT @DriverID=DriverID FROM inserted;

	Select @RequestID=RequestID,@TrackingCity=TrackingCity,@ClientCity=City
	from
	(Select City as TrackingCity,DriverID,t4.RequestID,ClientID
	from
	(SELECT *
	FROM
	(Select top 1 LoginName, FoodDelivery_FinalProject.Tracking.City,FoodDelivery_FinalProject.Tracking.Date,FoodDelivery_FinalProject.Tracking.Hour
	FROM FoodDelivery_FinalProject.Tracking JOIN  FoodDelivery_FinalProject.Driver ON  FoodDelivery_FinalProject.Tracking.DriverID= FoodDelivery_FinalProject.Driver.LoginName
	WHERE DriverID=@DriverID order by Date DESC, Hour DESC)as t3 join  FoodDelivery_FinalProject.Trip ON T3.LoginName= FoodDelivery_FinalProject.Trip.DriverID) as t4 join FoodDelivery_FinalProject.Request on FoodDelivery_FinalProject.Request.RequestID=t4.RequestID where RequestStatus=0x00)
	as t5 JOIN FoodDelivery_FinalProject.Client ON FoodDelivery_FinalProject.Client.LoginName=t5.ClientID
	
	IF (@TrackingCity=@ClientCity)
		BEGIN
			print('ola')
			UPDATE FoodDelivery_FinalProject.Request
			SET RequestStatus=0x01
			WHERE RequestID=@RequestID

			UPDATE FoodDelivery_FinalProject.Driver
			SET Ocuppied=0x00
			Where LoginName=@DriverID
		END
