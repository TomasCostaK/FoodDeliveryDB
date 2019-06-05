CREATE FUNCTION FoodDelivery_FinalProject.getDriver (@LoginName VARCHAR(50)) returns Table
AS
	RETURN(SELECT Name,Contact,Photo,Street,City,PostalCode,LicensePlate FROM FoodDelivery_FinalProject.Driver Where LoginName=@LoginName)


SELECT * FROM   FoodDelivery_FinalProject.getDriver('tomas12')