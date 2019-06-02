alter PROCEDURE FoodDelivery_FinalProject.AddRestaurant

    @pName NVARCHAR(25),
    @pPassword NVARCHAR(50),
	@Contact NCHAR(9),
	@Street NVARCHAR(30),
	@City	NVARCHAR(20),
	@PostalCode NVARCHAR(15),
	@Type NVARCHAR(15),
    @responseMessage NVARCHAR(250) OUTPUT
AS
BEGIN
    SET NOCOUNT ON
	
    DECLARE @salt UNIQUEIDENTIFIER=NEWID()
    BEGIN TRY

        INSERT INTO FoodDelivery_FinalProject.Restaurant (Name,Contact,Street,City,PostalCode,Type,Salt,PasswordHash )
		output inserted.RestaurantID 
        VALUES(@pName,@Contact,@Street,@City,@PostalCode,@Type,@salt, HASHBYTES('SHA2_512', @pPassword+CAST(@salt AS NVARCHAR(36))))

       
    END TRY
    BEGIN CATCH
        SET @responseMessage=ERROR_MESSAGE() 
    END CATCH

END

DECLARE @responseMessage NVARCHAR(250)

EXEC FoodDelivery_FinalProject.AddDriver
		  @LicensePlate='12-AA-33',
		  @Model='Tesla',
          @pLogin = N'Admin',
          @pPassword = N'123',
          @pName = N'Admin',
          @Contact='900000001',
		  @Image=NULL,
		  @Street='ola',
		  @City='adeus',
		  @PostalCode='4812',
		  
          @responseMessage=@responseMessage OUTPUT