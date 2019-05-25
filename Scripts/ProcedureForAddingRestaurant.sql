

ALTER PROCEDURE FoodDelivery_FinalProject.AddRestaurant
	
	
    @pName NVARCHAR(25),
	@Contact NCHAR(9),
	@Street NVARCHAR(30),
	@City	NVARCHAR(20),
	@PostalCode NVARCHAR(15),
	@Type NVARCHAR(15),
	
	

    @responseMessage NVARCHAR(250) OUTPUT
AS
BEGIN

    BEGIN TRY

        INSERT INTO FoodDelivery_FinalProject.Restaurant ([Name],Contact,Street,City,PostalCode,[Type] )
        VALUES(@pName,@Contact,@Street,@City,@PostalCode,@Type)

		SET @responseMessage='Success'

    END TRY
    BEGIN CATCH
        SET @responseMessage=ERROR_MESSAGE() 
    END CATCH

END
SELECT *
FROM FoodDelivery_FinalProject.Restaurant

DECLARE @responseMessage NVARCHAR(250)


EXEC FoodDelivery_FinalProject.AddRestaurant
          @pName = 'Harpa',
          @Contact='900000011',
			
		  @Street='sola',
		  @City='addeus',
		  @PostalCode='48312',
		  @Type='Fast Food',
		  
          @responseMessage=@responseMessage OUTPUT

print(@responseMessage)






