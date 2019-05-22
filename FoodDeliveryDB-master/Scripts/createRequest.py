# coding: utf-8
import random
import string

insert_request = open('insert_request.txt', 'w', encoding='utf8')
reqID_file = open('request_id.txt', 'w', encoding='utf8')

listClients = open('client_id.txt', 'r', encoding='utf8')
listMealCost = open('meal_cost.txt', 'r', encoding='utf8')
listPayments = open('pay_ids.txt', 'r', encoding='utf8')
listTripCost = open('trip_cost.txt', 'r', encoding='utf8')

Client_IDS = []
meal_costs = []
pay_IDS = []
trip_costs = []

#Fill clients
for line in listClients:
  Client_IDS.append(line.strip('\n'))

#Fill clients
for line in listMealCost:
  meal_costs.append(line.strip('\n'))

#Fill clients
for line in listPayments:
  pay_IDS.append(line.strip('\n'))

#Fill clients
for line in listTripCost:
  trip_costs.append(line.strip('\n'))

ids = 1
i = 0

# 100 meals
for i in range(31):
    request_ID = ids
    ids+=1

    #Client_ID
    Client_ID = Client_IDS[i]

    #Client_ID
    pay_ID = pay_IDS[i]

    #Cost1
    cost1 = meal_costs[i]
    #Cost2
    cost2 = trip_costs[i]

    #totalCost
    totalCost = round(float(cost1) + float(cost2),2)

    #for belongs table
    reqID_file.write("{}\n".format(request_ID))

    i+=1
    #final
    insert_request.write("INSERT INTO FoodDelivery_FinalProject.Request VALUES ('{}','{}','{}');\n".format(Client_ID,pay_ID,totalCost))

insert_request.close()
listClients.close()

listPayments.close()
listMealCost.close()
listTripCost.close()

reqID_file.close()
