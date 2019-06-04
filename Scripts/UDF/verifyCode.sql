create FUNCTION FoodDelivery_FinalProject.verifyCode (@Code varchar(50)) returns Table
AS
	return(Select * from FoodDelivery_FinalProject.Promotional where Code=@Code)