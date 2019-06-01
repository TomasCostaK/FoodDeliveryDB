CREATE PROCEDURE FoodDelivery_FinalProject.changeStatusRequest
	
	
	@check int,
	@RequestID INT
   
AS
BEGIN

    
		IF(@check=1)
		BEGIN
			UPDATE FoodDelivery_FinalProject.Request
			SET RequestStatus=0X01
			where RequestID=@RequestID
		END
		ELSE
		BEGIN
			UPDATE FoodDelivery_FinalProject.Request
			SET RequestStatus=0X00
			where RequestID=@RequestID
		END
        
	
END
