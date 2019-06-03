select * from FoodDelivery_FinalProject.Belongs
WHERE RequestID NOT IN
(SELECT RequestID from FoodDelivery_FinalProject.Request)



SELECT OBJECT_NAME(object_id) AS ConstraintName,

SCHEMA_NAME(schema_id) AS SchemaName,

OBJECT_NAME(parent_object_id) AS TableName,

type_desc AS ConstraintType

FROM sys.objects

WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(parent_object_id)='Trip'


delete from SELECT * from FoodDelivery_FinalProject.PaymentType join FoodDelivery_FinalProject.Request ON FoodDelivery_FinalProject.PaymentType.PaymentID=FoodDelivery_FinalProject.Request.PaymentID
WHERE  RequestID  NOT IN (SELECT RequestID from FoodDelivery_FinalProject.Trip)
SELECT * FROM FoodDelivery_FinalProject.Restaurant WHERE RestaurantID NOT IN (SELECT RestaurantID from FoodDelivery_FinalProject.Meal)
Select * from FoodDelivery_FinalProject.Meal