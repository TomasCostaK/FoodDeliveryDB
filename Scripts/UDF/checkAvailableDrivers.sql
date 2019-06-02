CREATE FUNCTION FoodDelivery_FinalProject.CheckAvailableDrivers ()returns Table
AS
	Return(SELECT count(LoginName) AS NavailableDrivers FROM  FoodDelivery_FinalProject.Driver Where Ocuppied=0x00)
SELECT *
FROM FoodDelivery_FinalProject.Belongs
Where RequestID=71