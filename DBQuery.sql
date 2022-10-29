﻿CREATE TABLE Products (
	Product_id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Product_no INT NOT NULL,
	Product_name VARCHAR(55) NOT NULL,
	Quantity INT,
	Price DECIMAL(15,2)
);

DROP TABLE Transactions
DROP TABLE Detail_transaction

SELECT TOP 1 * FROM Transactions ORDER BY Transaction_id DESC
SELECT COUNT(Transaction_id) FROM Transactions WHERE CONVERT(VARCHAR(25), Created_at,126) LIKE '%2022-10-28%'

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

--TODO make trigger when insert detail_transaction then update stock in product

insert into Products (Product_no, Product_name, Price, Quantity) values (1, 'Vallow', 77, 59);
insert into Products (Product_no, Product_name, Price, Quantity) values (2, 'Slewcock', 98, 96);
insert into Products (Product_no, Product_name, Price, Quantity) values (3, 'Alsop', 65, 45);
insert into Products (Product_no, Product_name, Price, Quantity) values (4, 'Maleney', 60, 16);
insert into Products (Product_no, Product_name, Price, Quantity) values (5, 'Ismail', 4, 72);
insert into Products (Product_no, Product_name, Price, Quantity) values (6, 'Vickar', 42, 100);
insert into Products (Product_no, Product_name, Price, Quantity) values (7, 'Yurivtsev', 9, 1);
insert into Products (Product_no, Product_name, Price, Quantity) values (8, 'Bayle', 89, 7);
insert into Products (Product_no, Product_name, Price, Quantity) values (9, 'Canada', 72, 84);
insert into Products (Product_no, Product_name, Price, Quantity) values (10, 'Samweyes', 96, 63);
insert into Products (Product_no, Product_name, Price, Quantity) values (11, 'Norwich', 36, 88);

insert into Transactions (Total_price) VALUES (20)