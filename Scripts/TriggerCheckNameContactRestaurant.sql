ALTER TRIGGER FoodDelivery_FinalProject.[CheckNameContact] on FoodDelivery_FinalProject.[Restaurant] 
INSTEAD OF INSERT
AS
	SET NOCOUNT ON;

	DECLARE @Name as VARCHAR(50)
	DECLARE @Contact as CHAR(9)
    DECLARE @Street NVARCHAR(30)
	DECLARE @City	NVARCHAR(20)
	DECLARE @PostalCode NVARCHAR(15)
	DECLARE @pPassword binary(64)
	DECLARE @Salt UNIQUEIDENTIFIER
    DECLARE @Type   VARCHAR(20)

	SELECT @Name=[Name],@Salt=Salt,@pPassword=PasswordHash, @Contact=Contact,@Street=Street,@City=City,@PostalCode=PostalCode,@Type=[Type] FROM inserted;
	
	IF EXISTS (SELECT TOP 1 [Name], Contact FROM FoodDelivery_FinalProject.Restaurant WHERE Name=@Name and Contact=@Contact) 
		BEGIN
			RAISERROR('Restaurant already exists',16,1);
			ROLLBACK TRAN;
		END

    ELSE
        BEGIN
            
             INSERT INTO FoodDelivery_FinalProject.Restaurant ( Name,Contact,Street,City,PostalCode,Type,Salt,PasswordHash )
            VALUES(@Name,@Contact,@Street,@City,@PostalCode,@Type,@Salt,@pPassword)
        END





















