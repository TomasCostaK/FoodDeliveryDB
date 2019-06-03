create Function FoodDelivery_FinalProject.getRestaurantOrderComplex(@username VARCHAR(50),@op varchar(20)) returns @rTable Table(
	RequestID			INT	,
	PaymentType			VARCHAR(25) ,
	TotalCost			Varchar(10)	,
	MealName			VARCHAR(45) ,
	RestaurantID		INT ,
	MainIngredient		VARCHAR(15) , 
	SideIngredient		VARCHAR(15) , 
	Drink				VARCHAR(15) , 
	EstimatedTime		VARCHAR(20),
	RequestStatus		BINARY(1),
	MealCost			VARCHAR(20)
)
AS
BEGIN
	IF(@op='All')
	BEGIN
		INSERT INTO @rTable SELECT T4.RequestID,PaymentType,TotalCost,MealName,RestaurantID,MainIngredient,SideIngredient,Drink,EstimatedTime,RequestStatus,MealCost
		FROM
		(SELECT T2.RequestID,Type as PaymentType,TotalCost,T2.Name as MealName,RestaurantID,MainIngredient,SideIngredient,Drink,RequestStatus,MealCost FROM
		FoodDelivery_FinalProject.getRestaurantOrder(@username) as T2 JOIN FoodDelivery_FinalProject.Meal on T2.Name=FoodDelivery_FinalProject.Meal.Name) as T4 JOIN  FoodDelivery_FinalProject.Trip ON  FoodDelivery_FinalProject.Trip.RequestID=T4.RequestID
	
	END
	ELSE IF(@op='Delivered')
	BEGIN
		INSERT INTO @rTable SELECT T4.RequestID,PaymentType,TotalCost,MealName,RestaurantID,MainIngredient,SideIngredient,Drink,EstimatedTime,RequestStatus,MealCost
		FROM
		(SELECT T2.RequestID,Type as PaymentType,TotalCost,T2.Name as MealName,RestaurantID,MainIngredient,SideIngredient,Drink,RequestStatus,MealCost FROM
		FoodDelivery_FinalProject.getRestaurantOrder(@username) as T2 JOIN FoodDelivery_FinalProject.Meal on T2.Name=FoodDelivery_FinalProject.Meal.Name) as T4 JOIN  FoodDelivery_FinalProject.Trip ON  FoodDelivery_FinalProject.Trip.RequestID=T4.RequestID WHERE RequestStatus=0x01
	

	END
	ELSE IF(@op='In transit')
	BEGIN
		INSERT INTO @rTable SELECT T4.RequestID,PaymentType,TotalCost,MealName,RestaurantID,MainIngredient,SideIngredient,Drink,EstimatedTime,RequestStatus,MealCost
		FROM
		(SELECT T2.RequestID,Type as PaymentType,TotalCost,T2.Name as MealName,RestaurantID,MainIngredient,SideIngredient,Drink,RequestStatus,MealCost FROM
		FoodDelivery_FinalProject.getRestaurantOrder(@username) as T2 JOIN FoodDelivery_FinalProject.Meal on T2.Name=FoodDelivery_FinalProject.Meal.Name) as T4 JOIN  FoodDelivery_FinalProject.Trip ON  FoodDelivery_FinalProject.Trip.RequestID=T4.RequestID WHERE RequestStatus=0x00
	

	END

	RETURN	
END
