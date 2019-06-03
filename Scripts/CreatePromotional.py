# coding: utf-8
import random
import uuid
insert_tracking = open('insert_promotional.txt', 'w', encoding='utf8')

f = open('listRestaurants.txt', 'r',encoding='utf8')
restaurant_id = []
for line in f:
    restaurant_id.append(line.strip('\n'))







# males
for i in range(300):

    

    #date
    start_date='2019-'+str(random.randint(1,5))+"-"+str(random.randint(1,27))

    end_date='2019-'+str(random.randint(6,8))+"-"+str(random.randint(1,27))
    

    discount=random.randint(1,50)

    #restaurant ID
    restaurantID=restaurant_id[random.randint(0,len(restaurant_id)-1)]
    
    #codeID
    code=str(uuid.uuid4())


    check=random.randint(0,1)
    if(check==0):
        restaurantID='null'

    insert_tracking.write("INSERT INTO FoodDelivery_FinalProject.Promotional VALUES ('{}','{}','{}','{}','{}');\n".format(code[:8],start_date,end_date,discount,restaurantID))
