# coding: utf-8
import random
import string

insert_trips = open('insert_trips.txt', 'w', encoding='utf8')

trips_cost_file = open('trip_cost.txt', 'w', encoding='utf8')
listDrivers = open('driver_id.txt', 'r', encoding='utf8')
listRequests = open('request_id.txt', 'r', encoding='utf8')

driver_IDS = []
request_IDS = []

for line in listDrivers:
  driver_IDS.append(line.strip('\n'))

for line in listRequests:
  request_IDS.append(line.strip('\n'))

ids = 1
ind = 0

# 100 meals
for i in range(31):
    Trip_ID = ids
    ids+=1

    #Trip_ID
    driver_ID = random.choice(driver_IDS)
    req_ID = request_IDS[ind]
    #cost
    trip_cost = round(random.uniform(5.0, 15.0),2)

    #Distance
    distance=round(random.uniform(1.0, 20.0),3)
    #time formart = 00:00:00.0000000
    time= str(random.randint(0,23))+':'+str(random.randint(0,59))+':'+str(random.randint(00,59))
    #for belongs table
    trips_cost_file.write("{}\n".format(trip_cost))

    ind += 1
    #final
    insert_trips.write("INSERT INTO FoodDelivery_FinalProject.Trip VALUES ('{}','{}','{}','{}','{}');\n".format(driver_ID,trip_cost,time,distance,req_ID))

insert_trips.close()
listDrivers.close()
listRequests.close()
trips_cost_file.close()
