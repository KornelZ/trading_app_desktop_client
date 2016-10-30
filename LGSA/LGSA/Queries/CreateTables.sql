--Naming rules words separete with _ not case sensitive
--Do not name table with plural version
--Users and Transactions are an exception as they are a keyword

drop table transactions
drop table Sell_Offer
drop table Buy_Offer
drop table Product
drop table dic_Genre
drop table dic_Product_type
drop table dic_condition
drop table dic_Offer_status
drop table dic_Transaction_status
drop table Users_Authetication
drop table users

CREATE TABLE users
(
ID int IDENTITY(1,1) PRIMARY KEY,
Last_Name varchar(255) NOT NULL,
First_Name varchar(255),
Update_Date dateTime NOT NULL,
Update_Who int NOT NULL,
constraint Users_Update_Who_fk foreign key (Update_Who) references Users(id)
)


CREATE TABLE dic_Genre
(
ID int IDENTITY(1,1) PRIMARY KEY,
name varchar(255) NOT NULL,
genre_description varchar(255),
Update_Date dateTime NOT NULL,
Update_Who int NOT NULL,
constraint dic_genre_Update_Who_fk foreign key (Update_Who) references Users(id)
)

CREATE TABLE dic_Product_type
(
ID int IDENTITY(1,1) PRIMARY KEY,
name varchar(255) NOT NULL,
Update_Date dateTime NOT NULL,
Update_Who int NOT NULL,
constraint dic_product_type_Update_Who_fk foreign key (Update_Who) references Users(id)
)

CREATE TABLE dic_condition
(
ID int IDENTITY(1,1) PRIMARY KEY,
name varchar(255) NOT NULL,
Update_Date dateTime NOT NULL,
Update_Who int NOT NULL,
constraint dic_condition_Update_Who_fk foreign key (Update_Who) references Users(id)
)

CREATE TABLE dic_Offer_status
(
ID int IDENTITY(1,1) PRIMARY KEY,
name varchar(255) NOT NULL,
offer_status_description varchar(255),
Update_Date dateTime NOT NULL,
Update_Who int NOT NULL,
constraint dic_offer_status_Update_Who_fk foreign key (Update_Who) references Users(id)
)

CREATE TABLE dic_Transaction_status
(
ID int IDENTITY(1,1) PRIMARY KEY,
name varchar(255) NOT NULL,
offer_transaction_description varchar(255),
Update_Date dateTime NOT NULL,
Update_Who int NOT NULL,
constraint dic_transaction_status_Update_Who_fk foreign key (Update_Who) references Users(id)
)

CREATE TABLE users_Authetication
(
ID int IDENTITY(1,1) PRIMARY KEY,
User_id int not null,
password varchar(255),
Update_Date dateTime NOT NULL,
Update_Who int NOT NULL,
constraint Users_Authentication_Update_Who_fk foreign key (Update_Who) references Users(id),
constraint Users_Authentication_user_id foreign key (user_id) references Users(id),
)

Create Table product
(
ID int IDENTITY(1,1) PRIMARY KEY,
Name varchar(255) NOT NULL,
rating float,
stock int default 1 not null,
sold_copies int default 0 not null,
genre_id int,
product_type_id int,
condition_id int,
Update_Date dateTime NOT NULL,
Update_Who int NOT NULL,
constraint Product_Update_Who_fk foreign key (Update_Who) references Users(id),
constraint Product_genre_fk foreign key (genre_id) references dic_Genre(id),
constraint Product_condition_fk foreign key (condition_id) references dic_condition(id),
constraint Product_product_type_fk foreign key (product_type_id) references dic_Product_Type(id)
)


CREATE TABLE buy_Offer
(
ID int IDENTITY(1,1) PRIMARY KEY,
buyer_id int not null,
price float,
amount int default 1 not null,
last_sell_date DateTime,	--possible naming problem
name varchar(255) NOT NULL,
sold_copies int default 0 not null,
product_id int not null,
status_id int not null,
Update_Date dateTime NOT NULL,
Update_Who int NOT NULL,
constraint buy_offer_Update_Who_fk foreign key (Update_Who) references Users(id),
constraint buy_offer_buyer_id_fk foreign key (buyer_id) references Users(id),
constraint buy_offer_product_id_fk foreign key (product_id) references product(id),
constraint buy_offer_status_id_fk foreign key (status_id) references dic_Offer_status(id)
)

CREATE TABLE sell_Offer
(
ID int IDENTITY(1,1) PRIMARY KEY,
seller_id int not null,
price float,
amount int default 1 not null,
last_sell_date DateTime,	--possible naming problem
name varchar(255) NOT NULL,
buyed_copies int default 0 not null,
product_id int not null,
status_id int not null,
Update_Date dateTime NOT NULL,
Update_Who int NOT NULL,
constraint sell_offer_Update_Who_fk foreign key (Update_Who) references Users(id),
constraint sell_offer_seller_id_fk foreign key (seller_id) references Users(id),
constraint sell_offer_product_id_fk foreign key (product_id) references product(id),
constraint sell_offer_status_id_fk foreign key (status_id) references dic_Offer_status(id)
)

CREATE TABLE transactions
(
ID int IDENTITY(1,1) PRIMARY KEY,
seller_id int not null,
buyer_id int not null,
buy_offer_id int not null,
sell_offer_id int not null,
transaction_Date dateTime NOT NULL,
status_id int not null,
Update_Date dateTime NOT NULL,
Update_Who int NOT NULL,
constraint Transaction_Update_Who_fk foreign key (Update_Who) references Users(id),
constraint Transaction_seller_id_fk foreign key (seller_id) references Users(id),
constraint Transaction_buyer_id_fk foreign key (buyer_id) references Users(id),
constraint Transaction_buy_offer_fk foreign key (buy_offer_id) references buy_offer(id),
constraint Transaction_sell_offer_fk foreign key (sell_offer_id) references sell_offer(id),
constraint Transaction_status_id_fk foreign key (status_id) references dic_Transaction_status(id)
)