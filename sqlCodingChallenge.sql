/*
CREATE TABLE Products
    ([ID] VARCHAR(30) PRIMARY KEY,
     [ProdName] VARCHAR(30),
     [Price] INT
    )
;
CREATE TABLE Customers
    ([ID] VARCHAR(30) PRIMARY KEY,
     [FirstName] VARCHAR(50),
	 [LastName] VARCHAR(50),
     [CardNumber]VARCHAR(30)
    )
;

CREATE TABLE ORDERS
    ([ID] VARCHAR(30) PRIMARY KEY,
     [ProductID] varchar(30) foreign key references Products(ID),
     [CustomerID] varchar(30) foreign key references Customers(ID)
    )
;

INSERT INTO ORDERS
    ([ID],[ProductID],[CustomerID])

VALUES
    ('1','11','111'),
	('2','11','222'),
	('3','22','333')

;

INSERT INTO Products
    ([ID],[ProdName],[Price])

VALUES
    ('11','Iphone','200'),
	('22','Razr','50'),
	('33','Galaxy s8','1000')

;

INSERT INTO Customers
    ([ID],[FirstName],[LastName],[CardNumber])

VALUES
    ('111','John','Doe','1234567'),
	('222','Tina','Smith','7654321'),
	('333','Mary','Grace','10101010')

;


SELECT * FROM ORDERS WHERE CustomerID =
(SELECT ID FROM Customers WHERE FirstName = 'Tina' AND 
LastName = 'Smith');


*/
SELECT (SELECT COUNT(ID) FROM ORDERS WHERE ProductID = (SELECT 
ProductID FROM Products WHERE ProdName = 'Iphone'))
*
(SELECT Price
FROM Products
WHERE ProdName = 'Iphone'); 
