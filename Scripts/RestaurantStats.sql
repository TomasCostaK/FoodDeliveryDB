--Mais Cara
--select top 1 * from FoodDelivery_FinalProject.getMeals('None',22) order by MealCost desc

--Mais Barata
--select top 1 * from FoodDelivery_FinalProject.getMeals('None',22) order by MealCost asc

--Preço feito
 --select sum(distinct TotalCost) as sumCost from (FoodDelivery_FinalProject.Meal as mls join FoodDelivery_FinalProject.Belongs as blg 
	--on mls.RestaurantID=blg.RestaurantID) join FoodDelivery_FinalProject.Request as req
	--on blg.RequestID=req.RequestID where mls.RestaurantID=22

select top 1 ClientID, count(ClientID) as RequestsNo from (FoodDelivery_FinalProject.Meal as mls join FoodDelivery_FinalProject.Belongs as blg 
	on mls.RestaurantID=blg.RestaurantID) join FoodDelivery_FinalProject.Request as req
	on blg.RequestID=req.RequestID join FoodDelivery_FinalProject.Client as cl
	on cl.LoginName=req.ClientID
	where mls.RestaurantID=22
	group by ClientID




--select * from FoodDelivery_FinalProject.Client