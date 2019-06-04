
create PROCEDURE FoodDelivery_FinalProject.addCode
	
	
    @Code VARCHAR(50),
	@StartDate DATE,
	@EndDate DATE,
	@Discount int,
	@RestaurantID int,




    @responseMessage NVARCHAR(250) OUTPUT
AS
BEGIN

    BEGIN TRY

        INSERT INTO FoodDelivery_FinalProject.Promotional(Code,StartDate,EndDate,Discount,RestaurantID)	
		VALUES(@Code,@StartDate,@EndDate,@Discount,@RestaurantID)

    END TRY
    BEGIN CATCH
        SET @responseMessage=ERROR_MESSAGE() 
    END CATCH

END