ALTER FUNCTION FoodDelivery_FinalProject.getProfile (@LoginName VARCHAR(50)) returns Table
AS
	RETURN(SELECT Name,Contact,Photo,Street,City,PostalCode,CONVERT(NCHAR(16), DecryptByPassPhrase('12345', CardNumber)) as CardNumber,CardExpirationDate,LoginName FROM FoodDelivery_FinalProject.Client Where LoginName=@LoginName)

