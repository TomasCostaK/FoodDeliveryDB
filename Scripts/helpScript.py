listDrivers = open('listDrivers.txt', 'w', encoding='utf8')
listClients = open('listClients.txt', 'w', encoding='utf8')

ids = ids = 100000001
clids = ids = 100000001
for i in range(100):
    #Escrever drivers e clients
    listDrivers.write("{}\n".format(ids))
    listClients.write("{}\n".format(clids))

    ids+=1
    clids+=1

listDrivers.close()
listClients.close()
