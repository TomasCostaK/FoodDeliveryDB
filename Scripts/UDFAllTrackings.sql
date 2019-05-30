select * from FoodDelivery_FinalProject.Tracking order By Date ASC
--drop procedure FoodDelivery_FinalProject.getAllTrackings

alter procedure FoodDelivery_FinalProject.getAllTrackings
@option varchar(30),
@driverID varchar(40)
as
	begin
		if @option='Least Recent'
			begin
				select GPS_Latitude,GPS_Longitude,[Date],[Hour] from FoodDelivery_FinalProject.Tracking where DriverID=@driverID order by Date ASC,Hour ASC 
			end
		if @option='Most Recent'
			begin
				select GPS_Latitude,GPS_Longitude,[Date],[Hour] from FoodDelivery_FinalProject.Tracking where DriverID=@driverID order by Date desc ,Hour Desc
			end
		if @option='None'
			begin
				select GPS_Latitude,GPS_Longitude,[Date],[Hour] from FoodDelivery_FinalProject.Tracking where DriverID=@driverID order by Date desc ,Hour Desc
			end
	end


exec FoodDelivery_FinalProject.getAllTrackings 'Least Recent','tomas12'
