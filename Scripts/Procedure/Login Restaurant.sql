create procedure FoodDelivery_FinalProject.restaurantLoginProcedure
		@pLoginName NVARCHAR(50),
		@pPassword NVARCHAR(MAX),
		@responseMessage NVARCHAR(250)='' OUTPUT
	AS
	BEGIN

		SET NOCOUNT ON

		DECLARE @LoginName VARCHAR(50)

		IF EXISTS (SELECT TOP 1 RestaurantID FROM FoodDelivery_FinalProject.Restaurant WHERE RestaurantID=@pLoginName)
		BEGIN
			SET @LoginName=(SELECT RestaurantID FROM FoodDelivery_FinalProject.Restaurant WHERE RestaurantID=@pLoginName AND PasswordHash=HASHBYTES('SHA2_512', @pPassword+CAST(Salt AS NVARCHAR(36))))

		   IF(@LoginName IS NULL)
			   SET @responseMessage='Incorrect password'
		   ELSE 
			   SET @responseMessage='Restaurant Login'
		END
		
	

	END