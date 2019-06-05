CREATE SCHEMA FoodDelivery_FinalProject

CREATE TABLE FoodDelivery_FinalProject.Client(
	UserID				INT	IDENTITY(1,1) NOT NULL, --auto-increment feature
	Name				VARCHAR(40) NOT NULL,
	Contact				CHAR(9)	NOT NULL,
	Photo				VARBINARY(MAX),
	Street				VARCHAR(30) NOT NULL,
	City				VARCHAR(20) NOT NULL,
	PostalCode			VARCHAR(15) NOT NULL, --so we can expand to other countries with larger postal codes than Portugal
	CardNumber			CHAR(16),
	CardExpirationDate	CHAR(5),

	PRIMARY KEY(UserID)
		
)

CREATE TABLE FoodDelivery_FinalProject.Vehicle(
	LicensePlate	CHAR(8) NOT NULL,
	Model			VARCHAR(20) NOT NULL,

	PRIMARY KEY(LicensePlate)
		
)

CREATE TABLE FoodDelivery_FinalProject.VehicleType(
	LicensePlate	CHAR(8) NOT NULL,
	Type			VARCHAR(20) NOT NULL,

	PRIMARY KEY(LicensePlate,Type)
		
)

CREATE TABLE FoodDelivery_FinalProject.Tracking(
	GPS_Latitude	DECIMAL(9,6) NOT NULL,
	GPS_Longitude	DECIMAL(9,6) NOT NULL,					
	[Date]			DATE,
	Hour			TIME NOT NULL,
	DriverID		VARCHAR(50)NOT NULL,
	City			Varchar(20)
	
	PRIMARY KEY(DriverID,GPS_LATITUDE,GPS_LONGITUDE,Hour) 


	
		
)

CREATE TABLE FoodDelivery_FinalProject.Restaurant(
	RestaurantID		INT	IDENTITY(1,1) NOT NULL, --auto-increment feature
	Name				VARCHAR(25) NOT NULL,
	Contact				CHAR(9)	NOT NULL,
	Street				VARCHAR(30) NOT NULL,
	City				VARCHAR(20) NOT NULL,
	PostalCode			VARCHAR(15) NOT NULL, --so we can expand to other countries with larger postal codes than Portugal
	Type				VARCHAR(20),

	PRIMARY KEY(RestaurantID)
		
)

CREATE TABLE FoodDelivery_FinalProject.Driver(
	DriverID			VARCHAR(50) NOT NULL, --auto-increment feature
	Name				VARCHAR(40) NOT NULL,
	Contact				CHAR(9)	NOT NULL,
	Photo				VARBINARY(MAX),
	Street				VARCHAR(30) NOT NULL,
	City				VARCHAR(20) NOT NULL,
	PostalCode			VARCHAR(15) NOT NULL,
	 --so we can expand to other countries with larger postal codes than Portugal
	

	PRIMARY KEY(DriverID)
		
)

CREATE TABLE FoodDelivery_FinalProject.Trip(
	TripID				INT	IDENTITY(1,1) NOT NULL, --auto-increment feature
	DriverID			INT	NOT NULL,
	TravelCost			DECIMAL(3,2) NOT NULL,
	EstimatedTime		TIME,
	Distance			DECIMAL(5,3),--20.400 km
	RequestID			INT NOT NULL,

	PRIMARY KEY(TripID)
		
)

CREATE TABLE FoodDelivery_FinalProject.Request(
	RequestID			INT	IDENTITY(1,1) NOT NULL, --auto-increment feature
	ClientID			INT	NOT NULL,
	PaymentID			INT	NOT NULL,
	TotalCost			DECIMAL(3,2) NOT NULL,
	
	PRIMARY KEY(RequestID)
		
)
CREATE TABLE FoodDelivery_FinalProject.PaymentType(
	PaymentID			INT	IDENTITY(1,1) NOT NULL, --auto-increment feature
	Type				VARCHAR(10) NOT NULL,
	ValueGiven			DECIMAL(5,2),
	Change				DECIMAL(5,2),
	
	PRIMARY KEY(PaymentID)
		
)

CREATE TABLE FoodDelivery_FinalProject.Meal(
	Name				VARCHAR(30) NOT NULL,
	RestaurantID		INT	NOT NULL,
	MealCost			DECIMAL(3,2) NOT NULL,
	MainIngredient		VARCHAR(15),
	SideIngredient		VARCHAR(15),
	Drink				VARCHAR(15),

	PRIMARY KEY(Name)
		
)

CREATE TABLE FoodDelivery_FinalProject.Belongs(
	Name				VARCHAR(30) NOT NULL,
	RestaurantID		INT	NOT NULL,
	RequestID			INT	NOT NULL,


	PRIMARY KEY(Name,RestaurantID,RequestID)
		
)

CREATE TABLE FoodDelivery_FinalProject.Promotional(
	Code				varchar(50) NOT NULL,
	StartDate			DATE 
	EndDate				DATE,
	Discount			INT,
	RestaurantID		INT ,

	CHECK(Discount>0 AND Discount<=100)
	PRIMARY KEY(Code)
		
)

--Promotional
alter table FoodDelivery_FinalProject.Promotional add constraint RestaurantPromotional foreign key(RestaurantID) references FoodDelivery_FinalProject.Restaurant (RestaurantID);


--Tracking
alter table FoodDelivery_FinalProject.Tracking add constraint DriverTracking foreign key(DriverID) references FoodDelivery_FinalProject.Driver (DriverID);

--Driver
alter table FoodDelivery_FinalProject.Driver add constraint DriverLicense foreign key(LicensePlate) references FoodDelivery_FinalProject.Vehicle (LicensePlate);

--VehicleType
alter table FoodDelivery_FinalProject.VehicleType add constraint VehicleTypeLicensePlate foreign key(LicensePlate) references FoodDelivery_FinalProject.Vehicle (LicensePlate);

--Request
alter table FoodDelivery_FinalProject.Request add constraint ClientRequest foreign key(ClientID) references FoodDelivery_FinalProject.Client (UserID);
alter table FoodDelivery_FinalProject.Request add constraint PaymentRequest foreign key(PaymentID) references FoodDelivery_FinalProject.PaymentType(PaymentID);

--Trip
alter table FoodDelivery_FinalProject.Trip add constraint TripDriver foreign key(DriverID) references FoodDelivery_FinalProject.Driver(LoginName) on delete cascade;
alter table FoodDelivery_FinalProject.Trip add constraint TripRequest foreign key(RequestID) references FoodDelivery_FinalProject.Request(RequestID) ON DELETE CASCADE;

--Meal
alter table FoodDelivery_FinalProject.Meal add constraint MealRestaurant foreign key(RestaurantID) references FoodDelivery_FinalProject.Restaurant(RestaurantID) ON DELETE CASCADE;

--Belongs
alter table FoodDelivery_FinalProject.Belongs add constraint BelongsMeal foreign key(Name,RestaurantID) references FoodDelivery_FinalProject.Meal(Name,RestaurantID);
alter table FoodDelivery_FinalProject.Belongs ADD constraint BelongsRequest foreign key(RequestID) references FoodDelivery_FinalProject.Request(RequestID) ON DELETE CASCADE;



ALTER TABLE FoodDelivery_FinalProject.Driver
ADD LicensePlate		CHAR(8) NOT NULL

alter table FoodDelivery_FinalProject.Driver add constraint DriverLicensePlate foreign key(LicensePlate) references FoodDelivery_FinalProject.Vehicle (LicensePlate);
