CREATE TABLE Products (
	Product_id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Product_no INT NOT NULL,
	Product_name VARCHAR(55) NOT NULL,
	Quantity INT,
	Price DECIMAL(15,2),
	Created_at DATETIME NOT NULL DEFAULT GETDATE()
);


CREATE TABLE Transactions(
	Transaction_id INT PRIMARY KEY,
	Total_price DECIMAL(15,2),
	Created_at DATETIME NOT NULL DEFAULT GETDATE()
);


CREATE TABLE Detail_transaction(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Transaction_id INT FOREIGN KEY REFERENCES Transactions(Transaction_id),
	Product_name VARCHAR(55),
	Quantity INT,
	Total_price DECIMAL(15,2)
);

CREATE TABLE Stock_history(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Product_no VARCHAR(55),
	Quantity INT,
	Created_at DATETIME NOT NULL DEFAULT GETDATE()
);

--TODO make trigger when insert detail_transaction then update stock in product
--CREATE TRIGGER update_stock_product ON Detail_transaction
--FOR INSERT 
--AS 
--	DECLARE @quantity INT
--	DECLARE @product_name VARCHAR
--BEGIN transaction
--	SELECT @quantity = Quantity FROM INSERTED
--	UPDATE Products set Quantity = 2  WHERE Product_name = @product_name 
--END
--if @@ERROR = 0
--commit transaction
--else
--rollback transaction
--GO

UPDATE Products SET Quantity += 5 WHERE Product_name = 'Yurivtsev'

SELECT * FROM Products WHERE Quantity < 1

insert into Products (Product_no, Product_name, Price, Quantity) values (1, 'Milka ', 5, 20);
insert into Products (Product_no, Product_name, Price, Quantity) values (2, 'Indomie', 2.5, 20);
insert into Products (Product_no, Product_name, Price, Quantity) values (3, 'Nugget', 2.75, 20);
insert into Products (Product_no, Product_name, Price, Quantity) values (4, 'Corn', 3, 20);
insert into Products (Product_no, Product_name, Price, Quantity) values (5, 'Chicken', 4, 20);
insert into Products (Product_no, Product_name, Price, Quantity) values (6, 'Vickar', 42, 20);
insert into Products (Product_no, Product_name, Price, Quantity) values (7, 'Yurivtsev', 9, 20);
insert into Products (Product_no, Product_name, Price, Quantity) values (8, 'Bayle', 89, 20);
insert into Products (Product_no, Product_name, Price, Quantity) values (9, 'Canada', 72, 20);

insert into Transactions (Total_price) VALUES (20)

DROP TABLE Products
DROP TABLE Transactions
DROP TABLE Detail_transaction
DROP TABLE Stock_history

TRUNCATE TABLE Transactions

SELECT TOP 1 * FROM Transactions ORDER BY Transaction_id DESC
SELECT COUNT(Transaction_id) FROM Transactions WHERE CONVERT(VARCHAR(25), Created_at,126) LIKE '%2022-10-28%'
SELECT * FROM Transactions WHERE CONVERT(VARCHAR(25), Created_at,126) LIKE '%2022-11-19%' 
SELECT * FROM Detail_transaction WHERE Transaction_id = 1

SELECT Stock_history.Product_no, Products.Product_name, Stock_history.Quantity, Stock_history.Created_at
FROM Stock_history
INNER JOIN Products ON Stock_history.Product_no = Products.Product_no

SELECT * FROM Stock_history
SELECT * FROM Products

DELETE FROM Stock_history Where Id = 4
