# time formar = 00:00:00.0000000

# coding: utf-8
import random
import string

insert_trips = open('insert_meals.txt', 'w', encoding='utf8')

trips_cost_file = open('trip_cost.txt', 'w', encoding='utf8')
listDrivers = open('listDrivers.txt', 'r', encoding='utf8')

driver_IDS = []

for line in listDrivers:   
  driver_IDS.append(line.strip('\n'))

ids = 1000

# 100 meals
for i in range(100):
	id = ids
	ids+=1

    #Trip_ID
    driver_ID = random.choice(driver_IDS)
    #cost
    trip_cost = round(random.uniform(5.0, 15.0),2)

    #Distance
    distance=
    #time
    time=
    #for belongs table
    trip_cost_file.write("{}\n".format(trip_cost))

    #final
    insert_trips.write("INSERT INTO FoodDelivery_FinalProject.Meal VALUES ('{}','{}','{}','{}','{}');\n".format(Trip_ID,driver_ID,trip_cost,time,distance))

insert_trips.close()
travel_cost.close()
trips_cost_file.close()

