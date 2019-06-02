--Mais Cara
--select top 1 * from FoodDelivery_FinalProject.getMeals('None',22) order by MealCost desc

--Mais Barata
--select top 1 * from FoodDelivery_FinalProject.getMeals('None',22) order by MealCost asc

create function FoodDelivery_FinalProject.getMoneyMade(@restID int) returns Table
as
	return(select sum(distinct TotalCost) as moneyMade from (FoodDelivery_FinalProject.Meal as mls join FoodDelivery_FinalProject.Belongs as blg 
	on mls.RestaurantID=blg.RestaurantID) join FoodDelivery_FinalProject.Request as req
	on blg.RequestID=req.RequestID where mls.RestaurantID=@restID)

select * from FoodDelivery_FinalProject.getMoneyMade(22)

--Melhor Cliente
--insert into @RTable 
create function FoodDelivery_FinalProject.BestClient(@restID int) returns Table
as
	return(select top 1 ClientID, count(ClientID) as RequestsNo from (FoodDelivery_FinalProject.Meal as mls join FoodDelivery_FinalProject.Belongs as blg 
	on mls.RestaurantID=blg.RestaurantID) join FoodDelivery_FinalProject.Request as req
	on blg.RequestID=req.RequestID join FoodDelivery_FinalProject.Client as cl
	on cl.LoginName=req.ClientID
	where mls.RestaurantID=@restID
	group by ClientID)

select * from FoodDelivery_FinalProject.BestClient(22)

--N de clientes
create function FoodDelivery_FinalProject.NumClients(@restID int) returns Table
as
	return(select count(distinct ClientID) as RequestsNo from (FoodDelivery_FinalProject.Meal as mls join FoodDelivery_FinalProject.Belongs as blg 
	on mls.RestaurantID=blg.RestaurantID) join FoodDelivery_FinalProject.Request as req
	on blg.RequestID=req.RequestID join FoodDelivery_FinalProject.Client as cl
	on cl.LoginName=req.ClientID
	where mls.RestaurantID=@restID)

select * from FoodDelivery_FinalProject.NumClients(22)

--most sold main
create function FoodDelivery_FinalProject.SoldMain(@restID int) returns Table
as
	return(select top 1 MainIngredient, count(MainIngredient) as NumMain from (FoodDelivery_FinalProject.Meal as mls join FoodDelivery_FinalProject.Belongs as blg 
	on mls.RestaurantID=blg.RestaurantID)
	where mls.RestaurantID=@restID
	group by MainIngredient)

--most sold side
create function FoodDelivery_FinalProject.SoldSide(@restID int) returns Table
as
	return(select top 1 SideIngredient, count(SideIngredient) as NumSide from (FoodDelivery_FinalProject.Meal as mls join FoodDelivery_FinalProject.Belongs as blg 
	on mls.RestaurantID=blg.RestaurantID)
	where mls.RestaurantID=@restID and SideIngredient <> 'NULL'
	group by SideIngredient)

select * from FoodDelivery_FinalProject.soldSide(22)

--most sold main
create function FoodDelivery_FinalProject.SoldDrink(@restID int) returns Table
as
	return(select top 1 Drink, count(Drink) as NumDrink from (FoodDelivery_FinalProject.Meal as mls join FoodDelivery_FinalProject.Belongs as blg 
	on mls.RestaurantID=blg.RestaurantID)
	where mls.RestaurantID=@restID
	group by Drink)


select * from FoodDelivery_FinalProject.getMoneyMade(22)
select * from FoodDelivery_FinalProject.BestClient(22)
select * from FoodDelivery_FinalProject.NumClients(22)
select * from FoodDelivery_FinalProject.soldMain(22)
select * from FoodDelivery_FinalProject.soldSide(22)
select * from FoodDelivery_FinalProject.soldDrink(22)

--select * from FoodDelivery_FinalProject.Client