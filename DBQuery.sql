CREATE TABLE Products (
	Product_no INT NOT NULL PRIMARY KEY,
	Product_name VARCHAR(50) NOT NULL,
	Quantity INT,
	Price VARCHAR(50)
);

create table Transactions(
	Id INT NOT NULL PRIMARY KEY,
	Quantity INT,
	Product_no INT FOREIGN KEY REFERENCES Products(Product_no)
);