# coding: utf-8
import random
import string

insert_belongs = open('insert_belongsV2.txt', 'w', encoding='utf8')

listRequest = open('request_idV2.txt', 'r', encoding='utf8')
listRestName = open('name_restIDV2.txt', 'r', encoding='utf8')
meal_Request = open('id_meal_requestV2.txt', 'r', encoding='utf8')

names = []
restaurants = []
requests = []
meal_request_ID=[]

#Fill clients
for line in listRestName:
    line = line.strip('\n')
    line = line.split(',')
    names.append(line[0])
    restaurants.append(line[1])

#Fill clients
for line in listRequest:
    requests.append(line.strip('\n'))

for line in meal_Request:
    meal_request_ID.append(line.strip('\n'))


i = 0
# 100 meals
for i in range(10000):

    indice=meal_request_ID[i]
    name = names[int(indice)]
    restaurant = restaurants[int(indice)]
    request = requests[i]

    insert_belongs.write("INSERT INTO FoodDelivery_FinalProject.Belongs VALUES ('{}','{}','{}');\n".format(name, restaurant, request))
    i+=1

insert_belongs.close()

listRestName.close()
listRequest.close()