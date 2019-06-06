# coding: utf-8
import random

insert_payment = open('insert_paymentV2.txt', 'w', encoding='utf8')



type_payment = ['MB Way','Credit Card','Debit Card','Paypal','Bitcoin','Money']




# males
for i in range(10000):
    type_pay=random.choice(type_payment)

    if(type_pay=="Money"):
        change='0.00'
    else:
        change='NULL'
    insert_payment.write("INSERT INTO FoodDelivery_FinalProject.PaymentType VALUES ('{}','{}');\n".format(type_pay,'null'))

