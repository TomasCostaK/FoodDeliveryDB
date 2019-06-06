# coding: utf-8
import random
import string

insert_belongs = open('deleteVehicle.txt', 'w', encoding='utf8')

listRequest = open('license_plates.txt', 'r', encoding='utf8')



requests = []



#Fill clients
for line in listRequest:
    requests.append(line.strip('\n'))




i = 0
# 100 meals
for i in range(999):

    
    request = requests[i]

    insert_belongs.write("Delete from FoodDelivery_FinalProject.Vehicle Where LicensePlate='{}';\n".format(request))
    i+=1


listRequest.close()