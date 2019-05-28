alter FUNCTION FoodDelivery_FinalProject.getRestaurantByType (@SelectedType VARCHAR(20), @SelectedCity  VARCHAR(20)) returns @RTable  Table (
	RestaurantID		INT	NOT NULL,
	Name				VARCHAR(25) NOT NULL,
	Contact				CHAR(9)	NOT NULL,
	Street				VARCHAR(30) NOT NULL,
	City				VARCHAR(20) NOT NULL,
	PostalCode			VARCHAR(15) NOT NULL, --so we can expand to other countries with larger postal codes than Portugal
	Type				VARCHAR(20))
AS
BEGIN
  IF @SelectedType='Todos'
  BEGIN
	IF @SelectedCity='Todos'
	BEGIN
		
		INSERT INTO @RTable SELECT * FROM FoodDelivery_FinalProject.Restaurant 																						
	END
	ELSE
	BEGIN
		INSERT INTO @RTable SELECT * FROM FoodDelivery_FinalProject.Restaurant where City=@SelectedCity 
	END
	
  END
  ELSE
  BEGIN
	IF @SelectedCity='Todos'
	BEGIN
		INSERT INTO @RTable SELECT * FROM FoodDelivery_FinalProject.Restaurant WHERE Type=@SelectedType 
	END
	ELSE
	BEGIN
		INSERT INTO @RTable SELECT * FROM FoodDelivery_FinalProject.Restaurant WHERE Type=@SelectedType AND City=@SelectedCity
	END
	
  END
 
  RETURN 
END