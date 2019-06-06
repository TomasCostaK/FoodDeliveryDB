# coding: utf-8
import random
import string

insert_request = open('insert_requestV2.txt', 'w', encoding='utf8')
#reqID_file = open('request_idV2.txt', 'w', encoding='utf8')
listTripCost = open('trip_costV2.txt', 'w', encoding='utf8')
id_meal= open('id_meal_requestV2.txt', 'w', encoding='utf8')
listClients = open('clientIDV2.txt', 'r', encoding='utf8')
listMealCost = open('meal_costV2.txt', 'r', encoding='utf8')
listPayments = open('pay_idsV2.txt', 'r', encoding='utf8')

Client_IDS = []
meal_costs = []
pay_IDS = []

#Fill clients
for line in listClients:
  Client_IDS.append(line.strip('\n'))

#Fill clients
for line in listMealCost:
  meal_costs.append(line.strip('\n'))

#Fill clients
for line in listPayments:
  pay_IDS.append(line.strip('\n'))



ids = 1
i = 0

# 100 meals
for i in range(10000):
    request_ID = ids
    ids+=1

    #Client_ID
    Client_ID = random.choice(Client_IDS)

    #Client_ID
    pay_ID = pay_IDS[i]

    #Cost1
    b=random.randint(0,(len(meal_costs)-1))

    cost1 = meal_costs[b]
    #Cost2
    cost2 = random.randint(7,20)

    #totalCost
    totalCost = round(float(cost1) + float(cost2),2)

    listTripCost.write("{}\n".format(cost2))
    id_meal.write("{}\n".format(b))
    #for belongs table
    #reqID_file.write("{}\n".format(request_ID))

    i+=1
    #final
    insert_request.write("INSERT INTO FoodDelivery_FinalProject.Request VALUES ('{}','{}','{}','{}');\n".format(Client_ID,pay_ID,totalCost,'0x01'))

insert_request.close()
listClients.close()

listPayments.close()
listMealCost.close()
listTripCost.close()

