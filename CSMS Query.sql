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


CREATE PROC Brand_Insert
@Brand_Name NVARCHAR(150), 
@Brand_Status NVARCHAR(15)
AS
BEGIN
INSERT INTO Brand (Brand_Name, Brand_Status) OUTPUT inserted.Brand_Id VALUES (@Brand_Name, @Brand_Status)
END


GO
CREATE PROC Brand_Update
@Brand_Id INT, 
@Brand_Name NVARCHAR(150), 
@Brand_Status NVARCHAR(15)
AS
BEGIN
UPDATE Brand SET Brand_Name = @Brand_Name, Brand_Status = @Brand_Status where Brand_Id = CAST(@Brand_Id AS int)
--and not exists(select * from Brand where Brand_Name = @Brand_Name)
END

GO
CREATE PROC Brand_Delete
@Brand_Id INT
AS
BEGIN
DELETE FROM Brand WHERE Brand_Id = @Brand_Id
END

GO
CREATE PROC Brand_Select_All
AS
BEGIN
	SELECT * FROM Brand
END

GO
CREATE PROC Brand_Select_ByID
@Brand_Id INT
AS
BEGIN
SELECT * FROM Brand WHERE Brand_Id = @Brand_Id
END

CREATE PROC Brand_Select_ByName
@Brand_Name NVARCHAR(150)
AS
BEGIN
SELECT * FROM Brand WHERE Brand_Name LIKE CONCAT('%', @Brand_Name, '%');
END

UPDATE Brand SET Brand_Name = '123', Brand_Status = 'all' where Brand_Id = CAST(16 AS int)
--and not exists(select * from Brand where Brand_Name = '12313')






--User--
SELECT u.Users_Id, u.Users_Name, u.Users_Email, u.Users_Password, r.Roles_Name FROM Users u
Left JOIN Roles r
ON u.Roles_Id = r.Roles_Id
GO
CREATE PROC User_Select_All
AS
BEGIN
 SELECT u.Users_Id, u.Users_Name, u.Users_Email, u.Users_Password, r.Roles_Name FROM Users u
Left JOIN Roles r
ON u.Roles_Id = r.Roles_Id
END
Drop proc User_Select_All


CREATE PROC User_Insert
@Users_Name NVARCHAR(150), 
@Users_Email NVARCHAR(150),
@Users_Password NVarchar(150),
@Roles_Id int
AS
BEGIN
INSERT INTO Users(Users_Name,Users_Email, Users_Password, Roles_Id) OUTPUT inserted.Users_Id VALUES (@Users_Name,@Users_Email, @Users_Password, @Roles_Id)
END

GO
CREATE PROC User_Delete
@Users_Id INT
AS
BEGIN
DELETE FROM Users WHERE Users_Id = @Users_Id
END

GO
CREATE PROC User_Select_ByID
@Users_Id INT
AS
BEGIN
SELECT * FROM Users WHERE Users_Id = @Users_Id
END

CREATE PROC User_Select_ByName
@Users_Name NVARCHAR(150)
AS
BEGIN
SELECT * FROM Users WHERE Users_Name LIKE CONCAT('%', @Users_Name, '%');
END

GO
CREATE PROC User_Update
@Users_Name NVARCHAR(150), 
@Users_Email NVARCHAR(150),
@Users_Password NVarchar(150),
@Roles_Id int,
@Users_Id int
AS
BEGIN
UPDATE Users SET Users_Name = @Users_Name, Users_Email = @Users_Email, Users_Password = @Users_Password, Roles_Id = @Roles_Id where Users_Id = CAST(@Users_Id AS int)
END