create FUNCTION FoodDelivery_FinalProject.getTripbyDriver (@DriverID varchar(50)) returns Table
AS
  return(SELECT * FROM   FoodDelivery_FinalProject.Trip where DriverID=@DriverID)