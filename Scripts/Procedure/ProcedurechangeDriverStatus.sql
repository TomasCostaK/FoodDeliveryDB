CREATE PROCEDURE FoodDelivery_FinalProject.changeStatusDriver
	
	
	@check int,
	@DriverID varchar(50)
   
AS
BEGIN

    
		IF(@check=1)
		BEGIN
			UPDATE FoodDelivery_FinalProject.Driver
			SET Ocuppied=0X01
			where LoginName=@DriverID
		END
		ELSE
		BEGIN
			UPDATE FoodDelivery_FinalProject.Driver
			SET Ocuppied=0X00
			where LoginName=@DriverID
		END
        
	
END