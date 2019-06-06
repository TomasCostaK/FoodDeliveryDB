CREATE VIEW FoodDelivery_FinalProject.CheckAvailableDrivers
AS
	SELECT count(LoginName) AS NavailableDrivers FROM  FoodDelivery_FinalProject.Driver Where Ocuppied=0x00