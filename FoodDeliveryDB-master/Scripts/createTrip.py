# coding: utf-8
import random
import string

insert_trips = open('insert_trips.txt', 'w', encoding='utf8')

trips_cost_file = open('trip_cost.txt', 'w', encoding='utf8')
listDrivers = open('driver_id.txt', 'r', encoding='utf8')

driver_IDS = []

for line in listDrivers:
  driver_IDS.append(line.strip('\n'))

ids = 1

# 100 meals
for i in range(100):
    Trip_ID = ids
    ids+=1

    #Trip_ID
    driver_ID = random.choice(driver_IDS)
    #cost
    trip_cost = round(random.uniform(5.0, 15.0),2)

    #Distance
    distance=round(random.uniform(1.0, 20.0),3)
    #time formart = 00:00:00.0000000
    time= str(random.randint(0,23))+':'+str(random.randint(0,59))+':'+str(random.randint(00,59))
    #for belongs table
    trips_cost_file.write("{}\n".format(trip_cost))

    #final
    insert_trips.write("INSERT INTO FoodDelivery_FinalProject.Trip VALUES ('{}','{}','{}','{}');\n".format(driver_ID,trip_cost,time,distance))

insert_trips.close()
listDrivers.close()
trips_cost_file.close()
