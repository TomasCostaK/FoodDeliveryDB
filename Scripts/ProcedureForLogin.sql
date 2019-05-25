	go
	ALTER PROCEDURE FoodDelivery_FinalProject.uspLogin
		@pLoginName NVARCHAR(50),
		@pPassword NVARCHAR(MAX),
		@responseMessage NVARCHAR(250)='' OUTPUT
	AS
	BEGIN

		SET NOCOUNT ON

		DECLARE @LoginName VARCHAR(50)

		IF EXISTS (SELECT TOP 1 LoginName FROM FoodDelivery_FinalProject.Client WHERE LoginName=@pLoginName)
		BEGIN
			SET @LoginName=(SELECT LoginName FROM FoodDelivery_FinalProject.Client WHERE LoginName=@pLoginName AND PasswordHash=HASHBYTES('SHA2_512', @pPassword+CAST(Salt AS NVARCHAR(36))))

		   IF(@LoginName IS NULL)
			   SET @responseMessage='Incorrect password'
		   ELSE 
			   SET @responseMessage='Client Login'
		END
		ELSE IF EXISTS (SELECT TOP 1 LoginName FROM FoodDelivery_FinalProject.Driver WHERE LoginName=@pLoginName)
		BEGIN
			SET @LoginName=(SELECT LoginName FROM FoodDelivery_FinalProject.Driver WHERE LoginName=@pLoginName AND PasswordHash=HASHBYTES('SHA2_512', @pPassword+CAST(Salt AS NVARCHAR(36))))

		   IF(@LoginName IS NULL)
			   SET @responseMessage='Incorrect password'
		   ELSE 
			   SET @responseMessage='Driver Login'
		END
		ELSE
		   SET @responseMessage='Invalid login'
	

	END
	go

declare @pLoginName NVARCHAR(50)
declare @pPassword NVARCHAR(MAX)
declare @responseMessage NVARCHAR(250)='' 

exec FoodDelivery_FinalProject.uspLogin @pLoginName='Carolina_Raposo62', @pPassword='23', @responseMessage=@responseMessage OUTPUT

print @responseMessage

SELECT *
FROM FoodDelivery_FinalProject.Client