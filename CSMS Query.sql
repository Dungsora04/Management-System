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
select * from Brand
select * from Category
select * from Customer
select * from Orders
select * from OrdersInfo
select * from Roles
select * from Users


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


CREATE PROC Category_Insert
@Category_Name NVARCHAR(150), 
@Category_Status NVARCHAR(15)
AS
BEGIN
INSERT INTO Category (Category_Name, Category_Status) OUTPUT inserted.Category_Id VALUES (@Category_Name, @Category_Status)
END
GO
CREATE PROC Category_Update
@Category_Id INT, 
@Category_Name NVARCHAR(150), 
@Category_Status NVARCHAR(15)
AS
BEGIN
UPDATE Category SET Category_Name = @Category_Name, Category_Status = @Category_Status where Category_Id = CAST(@Category_Id AS int)
--and not exists(select * from Category where Category_Name = @Category_Name)
END

GO
CREATE PROC Category_Delete
@Category_Id INT
AS
BEGIN
DELETE FROM Category WHERE Category_Id = @Category_Id
END

GO
CREATE PROC Category_Select_All
AS
BEGIN
	SELECT * FROM Category
END
GO
CREATE PROC Category_Select_ByID
@Category_Id INT
AS
BEGIN
SELECT * FROM Category WHERE Category_Id = @Category_Id
END

CREATE PROC Category_Select_ByName
@Category_Name NVARCHAR(150)
AS
BEGIN
SELECT * FROM Category WHERE Category_Name LIKE CONCAT('%', @Category_Name, '%');
END




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
--Drop proc User_Select_All


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


-- Product





CREATE PROCEDURE Product_Select_All
AS
BEGIN
    SELECT 
        p.Product_Id, 
        p.Product_Name, 
        p.Product_Image,
        p.Product_Price,
        b.Brand_Name, 
        c.Category_Name, 
        p.Product_Quantity, 
        p.Product_Warranty, 
        p.Product_Status, 
        p.Product_Details,
        p.Brand_Id, 
        p.Category_Id
    FROM 
        Product p
    INNER JOIN 
        Brand b ON b.Brand_Id = p.Brand_Id
    INNER JOIN 
        Category c ON c.Category_Id = p.Category_Id;
END;

CREATE PROC Product_Select_ByName
@Product_Name NVARCHAR(150)
AS
BEGIN
SELECT * FROM Product WHERE Product_Name LIKE CONCAT('%', @Product_Name, '%');
END

CREATE PROC Product_Insert
    @Product_Name NVARCHAR(150),
    @Product_Image IMAGE,
    @Product_Price INT,
    @Product_Quantity INT,
    @Brand_Id INT,
    @Category_Id INT,
    @Product_Warranty INT,
    @Product_Status VARCHAR(50),
    @Product_Details VARCHAR(150)
AS
BEGIN
    INSERT INTO Product (
        Product_Name,
        Product_Image,
        Product_Price,
        Product_Quantity,
        Brand_Id,
        Category_Id,
        Product_Warranty,
        Product_Status,
        Product_Details
    )
    OUTPUT inserted.Product_Id
    VALUES (
        @Product_Name,
        @Product_Image,
        @Product_Price,
        @Product_Quantity,
        @Brand_Id,
        @Category_Id,
        @Product_Warranty,
        @Product_Status,
        @Product_Details
    );
END;

CREATE PROC Product_Update
    @Product_Id INT,
    @Product_Name NVARCHAR(150),
    @Product_Image IMAGE,
    @Product_Price INT,
    @Product_Quantity INT,
    @Brand_Id INT,
    @Category_Id INT,
    @Product_Warranty INT,
    @Product_Status VARCHAR(50),
    @Product_Details VARCHAR(150)
AS
BEGIN
    UPDATE Product SET
        Product_Name = @Product_Name,
        Product_Image = @Product_Image,
        Product_Price = @Product_Price,
        Product_Quantity = @Product_Quantity,
        Brand_Id = @Brand_Id,
        Category_Id = @Category_Id,
        Product_Warranty = @Product_Warranty,
        Product_Status = @Product_Status,
        Product_Details = @Product_Details
    WHERE Product_Id = @Product_Id; 
END;
CREATE PROC Product_GetByPriceRange
    @lowPrice DECIMAL,
    @highPrice DECIMAL
AS
BEGIN
    SELECT *
    FROM Product
    WHERE Product_Price BETWEEN @lowPrice AND @highPrice;
END;

CREATE PROCEDURE Brand_GetAvailableForComboBox
AS
BEGIN
    SELECT Brand_Id, Brand_Name
    FROM Brand
    WHERE Brand_Status = 'Available'
    ORDER BY Brand_Name;
END;


CREATE PROCEDURE Category_GetAvailableForComboBox
AS
BEGIN
    SELECT Category_Id, Category_Name
    FROM Category
    WHERE Category_Status = 'Available'
    ORDER BY Category_Name;
END;

CREATE PROC User_Check_Password
@username NVARCHAR(150), 
@password NVARCHAR(15)
AS
BEGIN
SELECT * FROM Users WHERE Users_Name = @username AND Users_Password = @password
END

--SELECT * FROM Users WHERE Users_Name = 'admin' AND Users_Password = '1'	

GO
CREATE PROC User_Check_Email
@username NVARCHAR(150), 
@email NVARCHAR(15)
AS
BEGIN
SELECT Users_Password FROM Users WHERE Users_Name = @username AND Users_Email = @email
END

CREATE PROC Product_Select_data
AS
BEGIN
	SELECT Product_Id, Product_Name, Product_Image,Product_Price 
	FROM Product
	WHERE Product_Status = 'Available'
END
GO
drop PROC Product_Select_data
--------------------Customer-------------
GO
CREATE PROC Customer_Select_All
AS
BEGIN
	SELECT * FROM Customer
END

GO
CREATE PROC Customer_Insert
@Customer_Name NVARCHAR(150), 
@Customer_Number NVARCHAR(15)
AS
BEGIN
INSERT INTO Customer (Customer_Name, Customer_Number) OUTPUT inserted.Customer_Id VALUES (@Customer_Name, @Customer_Number)
END

GO
CREATE PROC Customer_Select_ByName
@Customer_Name NVARCHAR(150)
AS
BEGIN
SELECT * FROM Customer WHERE Customer_Name LIKE CONCAT('%', @Customer_Name, '%');
END


GO
CREATE PROC Customer_Get_Detail
@Customer_Id INT
AS
BEGIN
	SELECT o.Orders_Date,p.Product_Name, p.Product_Warranty, i.Orders_Quantity, p.Product_Price*i.Orders_Quantity AS Total 
	FROM OrdersInfo i INNER JOIN Product p ON i.Product_Id = p.Product_Id 
	INNER JOIN Orders o ON i.Orders_Id = o.Orders_Id
	where o.Customer_Id = @Customer_Id
END

GO
CREATE PROC Customer_Update
@Customer_Id INT, 
@Customer_Name NVARCHAR(150), 
@Customer_Number NVARCHAR(15)
AS
BEGIN
UPDATE Customer SET Customer_Name = @Customer_Name, Customer_Number = @Customer_Number where Customer_Id = @Customer_Id
END

GO
CREATE PROC Customer_Delete
@Customer_Id INT
AS
BEGIN
DELETE FROM Customer WHERE Customer_Id = @Customer_Id
END
drop PROC Product_Select_data
CREATE PROC Product_Select_data
AS
BEGIN
	SELECT Product_Id, Product_Name, Product_Image,Product_Price 
	FROM Product
	WHERE Product_Status = 'Available'
END
GO


go
CREATE PROC OrderInfo_Insert
@Orders_Id INT,
@Product_Id INT,
@Orders_Quantity INT,
@Warranty varchar(150)
AS
BEGIN
INSERT INTO OrdersInfo (Orders_Id, Product_Id, Orders_Quantity, Warranty)
OUTPUT inserted.OrdersInfo_Id Values (@Orders_Id, @Product_Id, @Orders_Quantity, @Warranty);
END
--
GO
CREATE PROC OrdersInfo_Delete
@Orders_Id INT
AS
BEGIN
DELETE FROM OrdersInfo WHERE Orders_Id = @Orders_Id
END

GO
CREATE PROC Customer_Get_Detail
@Customer_Id INT
AS
BEGIN
	SELECT o.Orders_Date,p.Product_Name, Warranty, i.Orders_Quantity, p.Product_Price*i.Orders_Quantity AS Total 
	FROM OrdersInfo i INNER JOIN Product p ON i.Product_Id = p.Product_Id 
	INNER JOIN Orders o ON i.Orders_Id = o.Orders_Id
	where o.Customer_Id = @Customer_Id
END
CREATE PROC Customer_Check_Exist
@Customer_Name NVARCHAR(150), 
@Customer_Number NVARCHAR(15)
AS
BEGIN
SELECT * FROM Customer WHERE Customer_Name = @Customer_Name AND Customer_Number = @Customer_Number
END

GO
CREATE PROC Customer_Select_All
AS
BEGIN
	SELECT * FROM Customer
END

GO
CREATE PROC Customer_Insert
@Customer_Name NVARCHAR(150), 
@Customer_Number NVARCHAR(15)
AS
BEGIN
INSERT INTO Customer (Customer_Name, Customer_Number) OUTPUT inserted.Customer_Id VALUES (@Customer_Name, @Customer_Number)
END

GO
CREATE PROC Customer_Select_ByName
@Customer_Name NVARCHAR(150)
AS
BEGIN
SELECT * FROM Customer WHERE Customer_Name LIKE CONCAT('%', @Customer_Name, '%');
END

CREATE PROC Customer_Select_ByNumber
@Customer_Number varchar(15)
AS
BEGIN
SELECT *
FROM Customer
WHERE Customer_Number LIKE CONCAT('%', @Customer_Number, '%')
END
GO
CREATE PROC Customer_Get_Detail
@Customer_Id INT
AS
BEGIN
	SELECT o.Orders_Date,p.Product_Name, Warranty, i.Orders_Quantity, p.Product_Price*i.Orders_Quantity AS Total 
	FROM OrdersInfo i INNER JOIN Product p ON i.Product_Id = p.Product_Id 
	INNER JOIN Orders o ON i.Orders_Id = o.Orders_Id
	where o.Customer_Id = @Customer_Id
END
CREATE PROC Customer_Check_Exist
@Customer_Name NVARCHAR(150), 
@Customer_Number NVARCHAR(15)
AS
BEGIN
SELECT * FROM Customer WHERE Customer_Name = @Customer_Name AND Customer_Number = @Customer_Number
END

GO
CREATE PROC Customer_Get_Detail
@Customer_Id INT
AS
BEGIN
	SELECT o.Orders_Date,p.Product_Name, p.Product_Warranty, i.Orders_Quantity, p.Product_Price*i.Orders_Quantity AS Total 
	FROM OrdersInfo i INNER JOIN Product p ON i.Product_Id = p.Product_Id 
	INNER JOIN Orders o ON i.Orders_Id = o.Orders_Id
	where o.Customer_Id = @Customer_Id
END

GO
CREATE PROC Customer_Update
@Customer_Id INT, 
@Customer_Name NVARCHAR(150), 
@Customer_Number NVARCHAR(15)
AS
BEGIN
UPDATE Customer SET Customer_Name = @Customer_Name, Customer_Number = @Customer_Number where Customer_Id = @Customer_Id
END

GO
CREATE PROC Customer_Delete
@Customer_Id INT
AS
BEGIN
	DELETE FROM OrdersInfo
	WHERE Orders_Id IN (
		SELECT Orders_Id FROM Orders WHERE Customer_Id = @Customer_Id
	);
	DELETE FROM Orders WHERE Customer_Id = @Customer_Id;
	DELETE FROM Customer WHERE Customer_Id = @Customer_Id;
END

CREATE PROC Customer_Select_ByNumber
@Customer_Number varchar(15)
AS
BEGIN
SELECT *
FROM Customer
WHERE Customer_Number LIKE CONCAT('%', @Customer_Number, '%')
END
