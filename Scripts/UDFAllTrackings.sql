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

select * from FoodDelivery_FinalProject.Tracking
drop procedure FoodDelivery_FinalProject.getAllTrackings

create function FoodDelivery_FinalProject.getAllTrackings(@option varchar(30),@driverID varchar(40), @chunk varchar(20))  returns @RTable  Table (
	GPS_Latitude	DECIMAL(9,6)	NOT NULL,
	GPS_Longitude	DECIMAL(9,6)	NOT NULL,					
	[Date]			DATE,
	Hour			TIME			NOT NULL,
	DriverID		VARCHAR(50)		NOT NULL)

as
begin
		if @option='Least Recent'
			begin
				insert into @RTable select GPS_Latitude,GPS_Longitude,[Date],[Hour] from FoodDelivery_FinalProject.Tracking where DriverID=@driverID order by Date ASC,Hour ASC 
			end
		if @option='Most Recent'
			begin
				insert into @RTable select GPS_Latitude,GPS_Longitude,[Date],[Hour] from FoodDelivery_FinalProject.Tracking where DriverID=@driverID order by Date desc ,Hour Desc
			end
		if @option='None'
			begin
				insert into @RTable select GPS_Latitude,GPS_Longitude,[Date],[Hour] from FoodDelivery_FinalProject.Tracking where DriverID=@driverID order by Date desc ,Hour Desc
			end
return 
end