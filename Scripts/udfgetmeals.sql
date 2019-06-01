alter function FoodDelivery_FinalProject.getMeals(@option varchar(30),@restID int)  returns @RTable  Table (
	Name				VARCHAR(50) NOT NULL,
	MealCost			DECIMAL(5,2) NOT NULL,
	MainIngredient		VARCHAR(15),
	SideIngredient		VARCHAR(15),
	Drink				VARCHAR(15))

as
begin
		if @option='Price Ascending'
			begin
				insert into @RTable select Name,MealCost,MainIngredient,SideIngredient,Drink from FoodDelivery_FinalProject.Meal where RestaurantID=@restID order by MealCost ASC
			end
		if @option='Price Descending'
			begin
				insert into @RTable select Name,MealCost,MainIngredient,SideIngredient,Drink from FoodDelivery_FinalProject.Meal where RestaurantID=@restID order by MealCost DESC
			end
		if @option='Name Ascending'
			begin
				insert into @RTable select Name,MealCost,MainIngredient,SideIngredient,Drink from FoodDelivery_FinalProject.Meal where RestaurantID=@restID order by Name ASC
			end
		if @option='Name Descending'
			begin
				insert into @RTable select Name,MealCost,MainIngredient,SideIngredient,Drink from FoodDelivery_FinalProject.Meal where RestaurantID=@restID order by Name Desc
			end
		if @option='None'
			begin
				insert into @RTable select Name,MealCost,MainIngredient,SideIngredient,Drink from FoodDelivery_FinalProject.Meal where RestaurantID=@restID order by Name Asc
			end
return 
end

select * from FoodDelivery_FinalProject.getMeals('Price Descending',22) where Name LIKE '%ados%'