# coding: utf-8
import random

insert_veichle = open('insert_veichle.txt', 'w', encoding='utf8')

f = open('license_plates.txt', 'r')
license_plates = []
for line in f:
    license_plates.append(line.strip('\n'))

models = ['Ford','BMW','Mercedes','Audi','Tesla','Honda','Fiat','Seat','Opel','Volkswagen','Mini','Smart']




# males
for i in range(100):
    

    insert_veichle.write("INSERT INTO FoodDelivery_FinalProject.Vehicle VALUES ('{}','{}');\n".format(license_plates[i],random.choice(models)))
