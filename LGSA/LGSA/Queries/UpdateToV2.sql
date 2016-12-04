Create Table UserAddress 
(
ID int IDENTITY(1,1) PRIMARY KEY,
City varchar(255),
Street varchar(255),
Postal_Code varchar(255),
Update_Who int,
constraint User_Address_Update_Who_fk foreign key (Update_Who) references Users(id),
)


Alter Table users add Rating int

Alter Table users add UserName varchar(255)

alter Table transactions add Rating int

alter table users add Address_ID int

ALTER TABLE users
ADD CONSTRAINT Users_Address_ID_fk
FOREIGN KEY (Address_ID)
REFERENCES UserAddress(ID)

select * from UserAddress