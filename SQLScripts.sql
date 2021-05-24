--Create Database
create database Product 
--Use Database
use Product

--Create table for Products

create table tblProducts
( id int identity(1,1) primary key,
 pname varchar(100),
 pdesc varchar(8000),
 price float,
 )
 
 
 --Indert values in Products table
 
 insert into tblProducts
 values
 ('Table','This is a table for 10', 500.56),
 ('Chair','This is a chair', 2000),
 ('Laptop','The Laptop can be used for oficial purpose only.', 40000),
 ('Desktop','This system is not allowed outside premises.', 30000),
 ('Table','This is a table for 5', 10000),
 ('Desk','The Desk can be transeffered.', 5000.36),
 ('Table Furniture','This is a table for 10', 500.56),
 ('Furniture','This is a home Decor Furniture', 2000),
 ('Bed','Bed is for 8000.', 80000),
 ('Sofa','Sofa can be used for official purpose.', 1500),
 ('Wires','This consist of 25sq m.', 600),
 ('Desk-IT','The Desk can be used for official set-up.', 3600.36)
 
 
 
 
 
 
 
 