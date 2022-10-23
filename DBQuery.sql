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
insert into Products (Product_no, Product_name, Price, Quantity) values (12, 'Whittam', 44, 7);
insert into Products (Product_no, Product_name, Price, Quantity) values (13, 'Mulrooney', 61, 60);
insert into Products (Product_no, Product_name, Price, Quantity) values (14, 'Pawelczyk', 42, 65);
insert into Products (Product_no, Product_name, Price, Quantity) values (15, 'Brownstein', 86, 63);
insert into Products (Product_no, Product_name, Price, Quantity) values (16, 'Streeten', 13, 90);
insert into Products (Product_no, Product_name, Price, Quantity) values (17, 'Flanagan', 80, 61);
insert into Products (Product_no, Product_name, Price, Quantity) values (18, 'Veneur', 4, 95);
insert into Products (Product_no, Product_name, Price, Quantity) values (19, 'Caunter', 65, 82);
insert into Products (Product_no, Product_name, Price, Quantity) values (20, 'Loos', 96, 99);
insert into Products (Product_no, Product_name, Price, Quantity) values (21, 'De''Ath', 35, 28);
insert into Products (Product_no, Product_name, Price, Quantity) values (22, 'Eaklee', 65, 100);
insert into Products (Product_no, Product_name, Price, Quantity) values (23, 'Kenyon', 60, 15);
insert into Products (Product_no, Product_name, Price, Quantity) values (24, 'Manser', 62, 4);
insert into Products (Product_no, Product_name, Price, Quantity) values (25, 'Phillpot', 94, 67);
insert into Products (Product_no, Product_name, Price, Quantity) values (26, 'Lenoir', 17, 9);
insert into Products (Product_no, Product_name, Price, Quantity) values (27, 'Well', 18, 7);
insert into Products (Product_no, Product_name, Price, Quantity) values (28, 'Buddock', 68, 71);
insert into Products (Product_no, Product_name, Price, Quantity) values (29, 'Levane', 65, 31);
insert into Products (Product_no, Product_name, Price, Quantity) values (30, 'Deftie', 28, 13);
insert into Products (Product_no, Product_name, Price, Quantity) values (31, 'Jobke', 73, 42);
insert into Products (Product_no, Product_name, Price, Quantity) values (32, 'Rainbow', 17, 19);
insert into Products (Product_no, Product_name, Price, Quantity) values (33, 'Pashen', 15, 40);
insert into Products (Product_no, Product_name, Price, Quantity) values (34, 'Mil', 73, 5);
insert into Products (Product_no, Product_name, Price, Quantity) values (35, 'Vlasin', 6, 94);
insert into Products (Product_no, Product_name, Price, Quantity) values (36, 'Chopin', 82, 78);
insert into Products (Product_no, Product_name, Price, Quantity) values (37, 'Dahler', 8, 17);
insert into Products (Product_no, Product_name, Price, Quantity) values (38, 'Ponceford', 89, 7);
insert into Products (Product_no, Product_name, Price, Quantity) values (39, 'Costan', 69, 27);
insert into Products (Product_no, Product_name, Price, Quantity) values (40, 'Wainwright', 26, 7);
insert into Products (Product_no, Product_name, Price, Quantity) values (41, 'Cayser', 6, 82);
insert into Products (Product_no, Product_name, Price, Quantity) values (42, 'Raleston', 66, 77);
insert into Products (Product_no, Product_name, Price, Quantity) values (43, 'Sallenger', 13, 5);
insert into Products (Product_no, Product_name, Price, Quantity) values (44, 'Klamp', 29, 58);
insert into Products (Product_no, Product_name, Price, Quantity) values (45, 'Cisland', 19, 61);
insert into Products (Product_no, Product_name, Price, Quantity) values (46, 'Reynold', 99, 98);
insert into Products (Product_no, Product_name, Price, Quantity) values (47, 'Gabel', 89, 43);
insert into Products (Product_no, Product_name, Price, Quantity) values (48, 'Biddle', 86, 8);
insert into Products (Product_no, Product_name, Price, Quantity) values (49, 'Glencrash', 47, 69);
insert into Products (Product_no, Product_name, Price, Quantity) values (50, 'Sandeford', 52, 65);