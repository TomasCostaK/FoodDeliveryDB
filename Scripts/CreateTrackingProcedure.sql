alter PROCEDURE FoodDelivery_FinalProject.AddTracking
	@pLogin NVARCHAR(50), 
	@gps_l1 varchar(30),
	@gps_l2 varchar(30),
	@City varchar(20)

AS
BEGIN
    SET NOCOUNT ON
	declare @date date
	declare @time time
	declare @gps_lati decimal(9,6)
	declare @gps_long decimal(9,6)
    BEGIN TRY
			select @gps_lati=convert(decimal(9,6),@gps_l1), @gps_long=convert(decimal(9,6),@gps_l2)
			select @date=convert(date, getdate()), @time=convert(varchar(8), convert(time, getdate()))
			Insert into FoodDelivery_FinalProject.Tracking (GPS_Latitude,GPS_Longitude,[Date],[Hour],DriverID,City) values (@gps_lati, @gps_long, @date, @time ,@pLogin,@City);
    END TRY
    BEGIN CATCH
		raiserror('Erro ocorrido',16,1)
    END CATCH

END

exec FoodDelivery_FinalProject.AddTracking 'tomas12', '38.015060','-7.863230'

select * from FoodDelivery_FinalProject.Tracking where GPS_Latitude like '%' + '40.644270' +'%'

