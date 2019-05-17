# coding: utf-8
import random
import string

insert_meals = open('insert_meals.txt', 'w', encoding='utf8')
listRestaurants = open('listRestaurants.txt', 'r', encoding='utf8')
meals_cost_file = open('meal_cost.txt', 'w', encoding='utf8')
name_restID = open('name_restID.txt', 'w', encoding='utf8')

restaurant_IDS = []

main_ingredients = ['ham','salad','borrego']
side_ingredients = ['fries','cheese']
drinks = ['water','cola','wine']

for line in listRestaurants:   
  restaurant_IDS.append(line.strip('\n'))

# 100 meals
for i in range(100):

    # meal
    main_ingredient = random.choice(main_ingredients)
    side_ingredient = random.choice(side_ingredients)
    drink = random.choice(drinks)

    #name
    meal_name = main_ingredient + '_' + side_ingredient
    #Rest_ID
    rest_ID = random.choice(restaurant_IDS)
    #cost
    meal_cost = round(random.uniform(4.0, 15.0),2)

    #Escrever meals cost
    meals_cost_file.write("{}\n".format(meal_cost))
    #for belongs table
    name_restID.write("{} , {}\n".format(meal_name, rest_ID))

    #final
    insert_meals.write("INSERT INTO FoodDelivery_FinalProject.Meal VALUES ('{}','{}','{}','{}','{}','{}');\n".format(meal_name, rest_ID ,meal_cost, main_ingredient, side_ingredient, drink))

insert_meals.close()
name_restID.close()
meals_cost_file.close()

