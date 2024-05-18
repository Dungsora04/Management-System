CREATE TABLE Roles (
    Roles_Id INT IDENTITY(1,1),
    Roles_Name VARCHAR(150) UNIQUE,
    Descriptions VARCHAR(150),
	CONSTRAINT PK_Roles PRIMARY KEY (Roles_Id)	
)

CREATE TABLE Users (
    Users_Id INT IDENTITY(1,1),
    Users_Name VARCHAR(150) UNIQUE,
    Users_Email VARCHAR(150),
	Users_Password VARCHAR(150),
	Roles_Id INT,
	CONSTRAINT PK_Users PRIMARY KEY (Users_Id),
	FOREIGN KEY (Roles_Id) REFERENCES Roles (Roles_Id)
)



CREATE TABLE Brand(		
	Brand_Id INT IDENTITY(1,1),
	Brand_Name VARCHAR(150) UNIQUE,
	Brand_Status VARCHAR(15),
	CONSTRAINT PK_Brand PRIMARY KEY (Brand_Id)
);

CREATE TABLE Category
(
	Category_Id INT IDENTITY(1, 1),
	Category_Name VARCHAR(150) UNIQUE,
	Category_Status VARCHAR(15),
	CONSTRAINT PK_Category PRIMARY KEY (Category_Id)
);

CREATE TABLE Product(
	Product_Id INT IDENTITY(1, 1),
	Product_Name VARCHAR(150) UNIQUE,
	Product_Image IMAGE,
	Product_Price INT,
	Product_Quantity INT,
	Brand_Id INT,
	Category_Id INT,
	Product_Warranty Int,
	Product_Status varchar(50),
	CONSTRAINT PK_Product PRIMARY KEY (Product_Id),
	FOREIGN KEY (Brand_Id) REFERENCES Brand (Brand_Id),
	FOREIGN KEY (Category_Id) REFERENCES Category (Category_Id)
);
alter table Product add Product_Details varchar(150)
create table Customer 
(
	Customer_Id Int identity(1,1),
	Customer_Name varchar(150),
    Customer_Number varchar(15) unique,
	Constraint PK_Customer Primary Key (Customer_Id)
)
create Table Orders
(
   Orders_Id Int identity(1,1),
   Orders_Date Date,
   Customer_Id INT,
   Users_Id INT,
   Total_Amount int,
   Paid_Amount int,
   Due_Amount int, 
   Discount int,
   Grand_Total int,
   FOREIGN KEY (Users_Id) REFERENCES Users (Users_Id),
   FOREIGN KEY (Customer_Id) REFERENCES Customer (Customer_Id),
   Constraint PK_Orders Primary Key (Orders_Id)
);
DELETE FROM Orders WHERE Orders_Id = 1


create Table OrdersInfo
(	
	OrdersInfo_Id INT identity(1,1),
	Orders_Id INT,
	Product_Id INT,
	Orders_Quantity INT,
	Warranty varchar(150),
	Constraint PK_OrdersInfo Primary Key (OrdersInfo_Id),
	FOREIGN KEY (Orders_Id) REFERENCES Orders (Orders_Id),
	FOREIGN KEY (Product_Id) REFERENCES Product (Product_Id),

);
select Customer.Customer_Name
from Orders inner join Customer on Orders.Customer_Id = Customer.Customer_Id Like 'Gold'
drop table OrdersInfo

	INSERT INTO Users (Users_Name, Users_Email, Users_Password, Roles_Id)
	VALUES ('admin', '1', '1', 1);

INSERT INTO Roles (Roles_Name, Descriptions)
VALUES ('Admin', 'Administrator role with full access');
select * from Product
