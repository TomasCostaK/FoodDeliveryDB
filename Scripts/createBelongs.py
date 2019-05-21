# coding: utf-8
import random
import string

insert_belongs = open('insert_belongs.txt', 'w', encoding='utf8')

listRequest = open('request_id.txt', 'r', encoding='utf8')
listRestName = open('name_restID.txt', 'r', encoding='utf8')

names = []
restaurants = []
costs = []

#Fill clients
for line in listRestName:
    line = line.strip('\n')
    line = line.split(',')
    names.append(line[0])
    costs.append(line[1])

#Fill clients
for line in listRequest:
    restaurants.append(line.strip('\n'))

i = 0
# 100 meals
for i in range(100):
    name = names[i]
    restaurant = random.choice(restaurants)
    cost = costs[i]

    insert_belongs.write("INSERT INTO FoodDelivery_FinalProject.Belongs VALUES ('{}','{}','{}');\n".format(name, restaurant, cost))
    i+=1

insert_belongs.close()

listRestName.close()
listRequest.close()