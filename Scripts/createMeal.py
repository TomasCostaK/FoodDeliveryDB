# coding: utf-8
import random
import string

insert_meals = open('insert_meals.txt', 'w', encoding='utf8')
listRestaurants = open('listRestaurants.txt', 'r', encoding='utf8')
meals_cost_file = open('meal_cost.txt', 'w', encoding='utf8')
name_restID = open('name_restID.txt', 'w', encoding='utf8')

restaurant_IDS = []

probSide = 0.7

main_ingredients = ['fiambre','salada','borrego','cozido','panados','frango','porco','pato','coelho','salmao','dourada','pescada','sushi','atum']
side_ingredients = ['batatas_fritas','arroz_feijao','arroz_branco','massa','carbonara','gelatina']
drinks = ['agua','coca-cola','pepsi','sprite','fanta','vinho_branco','vinho_tinto','sumo_laranja']

for line in listRestaurants:   
  restaurant_IDS.append(line.strip('\n'))

# 100 meals
for i in range(100):

    # meal
    main_ingredient = random.choice(main_ingredients)
    side_ingredient = random.choice(side_ingredients)
    drink = random.choice(drinks)

    #Rest_ID
    rest_ID = random.choice(restaurant_IDS)
    #cost
    meal_cost = round(random.uniform(4.0, 15.0),2)

    rand = random.uniform(0,1)
    #final
    if rand < probSide:
        meal_name = main_ingredient + '_' + side_ingredient + '_' + drink
        insert_meals.write("INSERT INTO FoodDelivery_FinalProject.Meal VALUES ('{}','{}','{}','{}','{}','{}');\n".format(meal_name, rest_ID ,meal_cost, main_ingredient, side_ingredient, drink))
    else:
        meal_name = main_ingredient + '_' + drink
        insert_meals.write("INSERT INTO FoodDelivery_FinalProject.Meal VALUES ('{}','{}','{}','{}',NULL,'{}');\n".format(meal_name, rest_ID ,meal_cost, main_ingredient,drink))
    
    #Escrever meals cost
    meals_cost_file.write("{}\n".format(meal_cost))
    #for belongs table
    name_restID.write("{} , {}\n".format(meal_name, rest_ID))

insert_meals.close()
name_restID.close()
meals_cost_file.close()

