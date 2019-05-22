# coding: utf-8
import random

insert_tracking = open('insert_tracking.txt', 'w', encoding='utf8')

f = open('driver_id.txt', 'r',encoding='utf8')
driver_id = []
for line in f:
    driver_id.append(line.strip('\n'))


GPS={
                'Aveiro': ['40.64427','-8.64554'],
                'Beja': ['38.0150600',' -7.8632300'],
                'Braga': ['41.5503200','-8.4200500'],
                'Bragança': ['41.8058200', '-6.7571900'],
                'Castelo Branco': ['39.8221900','-7.4908700'],
                'Coimbra': ['40.2056400','-8.4195500'],
                'Évora': ['38.5666700',' -7.9000000'],
                'Faro': ['37.0193700','-7.9322300'],
                'Guarda': ['40.5373300','-7.2657500'],
                'Leiria': ['39.7436200',' -8.8070500'],
                'Lisboa': ['38.7166700',' -9.1333300'],
               
                'Portalegre': ['39.2937900','-7.4312200'],
                'Porto': ['41.1496100','-8.6109900'],
                'Santarém':['39.2333300','-8.6833300'],
                'Setúbal': ['38.5244000','-8.8882000'],
                'Viana do Castelo': ['41.6932300','-8.8328700'],
                'Vila Real': ['41.3006200',' -7.7441300'],
                'Viseu': ['40.6610100',' -7.9097100'],
                }
month=['JAN','FEV','APR','MAY','JUN','JUL','AUG','SET']




# males
for i in range(100):

    city=random.choice(list(GPS))

    #latitude
    latitude=GPS[city][0]
    
    #longitude
    longitude=GPS[city][1]

    #date
    date='2019-'+str(random.randint(1,6))+"-"+str(random.randint(1,27))

    #hour
    hour=str(random.randint(0,23))+":"+str(random.randint(0,59))+":"+str(random.randint(0,59))

    #driver ID
    driver=driver_id[i]
    print(driver)


    insert_tracking.write("INSERT INTO FoodDelivery_FinalProject.Tracking VALUES ('{}','{}','{}','{}','{}');\n".format(latitude,longitude,date,hour,driver))
