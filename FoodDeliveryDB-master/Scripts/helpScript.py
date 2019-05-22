listDrivers = open('listDrivers.txt', 'w', encoding='utf8')
listClients = open('listClients.txt', 'w', encoding='utf8')
listpay = open('pay_ids.txt', 'w', encoding='utf8')

ids = ids = 100000001
clids = ids = 100000001
pay_ID = 6 
for i in range(31):
    listpay.write("{}\n".format(pay_ID))

    ids+=1
    clids+=1
    pay_ID+=1

listDrivers.close()
listpay.close()
listClients.close()
