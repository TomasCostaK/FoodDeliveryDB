
alter PROCEDURE FoodDelivery_FinalProject.addTripforRequest
	
	
    @TripID int,
	@DriverID int,
	@TravelCost varchar(20),
	@EstimatedTime int,
	@Distance varchar(15),
	@RequestID int,



	



    @responseMessage NVARCHAR(250) OUTPUT
AS
BEGIN

    BEGIN TRY

        INSERT INTO FoodDelivery_FinalProject.Trip(TripID,DriverID,TravelCost,EstimatedTime,Distance,RequestID)
		VALUES(@TripID,@DriverID,@TravelCost,CONVERT(VARCHAR, DATEADD(second,@EstimatedTime,0),108),@Distance,@RequestID)
		


    END TRY
    BEGIN CATCH
        SET @responseMessage=ERROR_MESSAGE() 
    END CATCH

END
    