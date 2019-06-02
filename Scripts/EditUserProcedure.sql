alter PROCEDURE FoodDelivery_FinalProject.EditDriver
	@pLogin NVARCHAR(50), 
    @pName NVARCHAR(40),
	@Contact NCHAR(9),
	@Image NVARCHAR(MAX)=NULL,
	@Street NVARCHAR(30),
	@City	NVARCHAR(20),
	@PostalCode NVARCHAR(15),

    @responseMessage NVARCHAR(250) OUTPUT
AS
BEGIN
    SET NOCOUNT ON

    BEGIN TRY
		
			Update FoodDelivery_FinalProject.Driver
			SET Name=@pName,Contact=@Contact,Street=@Street,City=@City,PostalCode=@PostalCode
			WHERE LoginName=@pLogin
			SET @responseMessage='worked'

		IF @Image <>'nothing'
		BEGIN
			
			Update FoodDelivery_FinalProject.Driver
			SET Photo=@Image
			WHERE LoginName=@pLogin
			SET @responseMessage='worked'

		END

    END TRY
    BEGIN CATCH
        SET @responseMessage=ERROR_MESSAGE() 
    END CATCH

END

