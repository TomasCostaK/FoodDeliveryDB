alter FUNCTION FoodDelivery_FinalProject.getClientProfilebyOrder (@RequestID int) returns @rTable Table(
	[Name] varchar(40),
	Street varchar(30),
	City varchar(20),
	contact char(9),
	Photo varchar(max)
)
AS
BEGIN
	declare @ClientID varchar(50)
	set @ClientID=( Select ClientID
	FROM FoodDelivery_FinalProject.Request
	Where RequestID=@RequestID)

	INSERT INTO @rTable (Name,Street,City,contact,Photo) SELECT Name, Street,City,Contact,Photo FROM   FoodDelivery_FinalProject.getProfile(@ClientID)

	return

END