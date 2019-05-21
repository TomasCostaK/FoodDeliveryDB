# coding: utf-8
import random
import string

insert_restaurants = open('insert_restaurants.txt', 'w', encoding='utf8')
listRestaurants = open('listRestaurants.txt', 'w', encoding='utf8')

restaurant_names = ['O','Taberna','Casa','Restaurante','Adega','Bar','A','Madeira de', 'Janela do', 'Vale', 'Churrasqueira','Sitio dos']
last_restaurant_names = ['Verdes', 'Xisto','Topazio','Bordeus','Paris','Vinho','Negro','Branca','Deuses','Luminosa','Gavião','Nordico','Migas','Sardinhas','Bombeiro','Vermelho','Luz']
restaurant_type = ['Vegan','Grelhados','Snack-Bar','Tapas','Vintage','Outdoor','Fast Casual','Buffet','Fast Food','Cafe']

postal_codes = {
                'Aveiro': [3020, 4550],
                'Beja': [7230,7960],
                'Braga': [4615,4905],
                'Bragança': [5140,5385],
                'Castelo Branco': [6000,6320],
                'Coimbra': [3000,6285],
                'Évora': [2965,7490],
                'Faro': [8000,8970],
                'Guarda': [3570,6440],
                'Leiria': [2400,3280],
                'Lisboa': [1000,2799],
               
                'Portalegre': [6040,7480],
                'Porto': [4000,5040],
                'Santarém': [2000,6120],
                'Setúbal': [2100,7595],
                'Viana do Castelo': [4900,4990],
                'Vila Real': [4870,5470],
                'Viseu': [3360,5130],
                }

Street={
                'Aveiro': ['Rua de Ovar','Rua de São joão da Madeira'],
                'Beja': ['Rua da Casa Pia','Rua do Touro'],
                'Braga': ['Rua dos Galos','Avenida 31 de Janeiro'],
                'Bragança': ['Rua 5 de outubro','Avenida Humberto Delgado'],
                'Castelo Branco': ['Avenida 1 de maio','Rua do Relógio'],
                'Coimbra': ['Rua do Brasil','Rua Santa Teresa'],
                'Évora': ['Rua Nova','Rua de Olivença'],
                'Faro': ['Rua Cesar Pola','Rua Gaspar Leão'],
                'Guarda': ['Rua do Cativo','Rua 31 de Janeiro'],
                'Leiria': ['Avenida 25 de abril','Rua de São Francisco'],
                'Lisboa': ['Avenida 5 de outubro','Rua de Santa Marta'],
               
                'Portalegre': ['Rua da Amargura','Avenida da Liberdade'],
                'Porto': ['Avenida da Boavista','Rua da Boavista'],
                'Santarém':['Praça do Municipio','Rua dos Moinhos'],
                'Setúbal': ['Avenida 22 de dezembro','Praça de Bocage'],
                'Viana do Castelo': ['Rua do Loureiro','Rua dos anjinhos'],
                'Vila Real': ['Rua de Santo António','Rua Diogo Cão'],
                'Viseu': ['Rua Formosa','Avenida 25 de abril'],
                }



ids = 0
phones = 256000000

#adicionar phone numbers

# 30 restaurants
for i in range(30):
    # id
    id = ids
    ids+=1

    # name
    rest_name = random.choice(restaurant_names)
    rest_lname = random.choice(last_restaurant_names)

    # type
    rest_type = random.choice(restaurant_type)

    # city and postal code and street
    city = random.choice(list(postal_codes))
    postal_code = random.randint(postal_codes[city][0],postal_codes[city][1])
    r=random.randint(0, 1)
    street=Street[city][r]

    # phone
    phone = phones
    phones+=1
    #Ordem: ID, name, contact, street, city, postalcode, type
    listRestaurants.write("{}\n".format(ids))
    insert_restaurants.write("INSERT INTO FoodDelivery_FinalProject.Restaurant VALUES ('{}','{}','{}','{}','{}', '{}');\n".format(rest_name+" "+rest_lname,phone,street,city, postal_code, rest_type))

insert_restaurants.close()
listRestaurants.close()
