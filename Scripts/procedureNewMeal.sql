alter PROCEDURE FoodDelivery_FinalProject.AddMeal
	@name VARCHAR(50), 
	@restID int,
	@cost varchar(20),
	@main varchar(30) ,
	@side varchar(30),
	@drink varchar(30) 
as
BEGIN
    SET NOCOUNT ON
	declare @mealcost decimal(5,2)
    BEGIN TRY
			select @mealcost=convert(decimal(5,2),@cost)
			print @mealcost
			Insert into FoodDelivery_FinalProject.Meal (Name,RestaurantID,MealCost,MainIngredient,SideIngredient,Drink) values (@name,@restID,@mealcost,@main,@side,@drink);
    END TRY
    BEGIN CATCH
		raiserror('Erro ocorrido',16,1)
    END CATCH

END

exec FoodDelivery_FinalProject.AddMeal 'massa_picapica',22, '8.40', 'massa', 'bolonhesa', 'fanta'

select * from FoodDelivery_FinalProject.getMeals('None',22)