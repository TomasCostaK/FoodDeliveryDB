select * from FoodDelivery_FinalProject.Tracking order By Date ASC
--drop procedure FoodDelivery_FinalProject.getAllTrackings

create procedure FoodDelivery_FinalProject.getAllTrackings
@option varchar(20)
as
	begin
		if @option='Date Ascending'
			begin
				select GPS_Latitude,GPS_Longitude,Date,Hour from FoodDelivery_FinalProject.Tracking order by Date ASC
			end
		if @option='Date Descending'
			begin
				select GPS_Latitude,GPS_Longitude,Date,Hour from FoodDelivery_FinalProject.Tracking order by Date Desc
			end
		if @option='Hour Ascending'
			begin
				select GPS_Latitude,GPS_Longitude,Date,Hour from FoodDelivery_FinalProject.Tracking order by Hour asc
			end
		if @option='Hour Descending'
			begin
				select GPS_Latitude,GPS_Longitude,Date,Hour from FoodDelivery_FinalProject.Tracking order by Date ASC
			end
		if @option='None'
			begin
				select GPS_Latitude,GPS_Longitude,Date,Hour from FoodDelivery_FinalProject.Tracking order by Hour asc
			end
	end


exec FoodDelivery_FinalProject.getAllTrackings 'Date Ascending'