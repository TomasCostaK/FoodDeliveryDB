# coding: utf-8
import random

insert_payment = open('insert_payment.txt', 'w', encoding='utf8')



type_payment = ['MB Way','Credit Card','Debit Card','Paypal','Bitcoin','Money']




# males
for i in range(9300):
    type_pay=random.choice(type_payment)

    if(type_pay=="Money"):
        change='0.00'
    else:
        change='NULL'
    money_given=str(random.randint(10, 50))+"."+str(random.randint(0, 9))+str(random.randint(0, 9))
    insert_payment.write("INSERT INTO FoodDelivery_FinalProject.PaymentType VALUES ('{}','{}',{});\n".format(type_pay,money_given,change))

