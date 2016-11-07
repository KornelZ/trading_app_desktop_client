INSERT INTO dic_condition (name, Update_Who, Update_Date)VALUES('Brand New', 1, GETDATE());
INSERT INTO dic_condition (name, Update_Who, Update_Date)VALUES('Used', 1, GETDATE());
INSERT INTO dic_condition (name, Update_Who, Update_Date)VALUES('Damaged', 1, GETDATE());

INSERT INTO dic_Genre(name, genre_description, Update_Who, Update_Date)VALUES('RPG', 'A genre in which players assume the roles of characters in a fictional setting', 1, GETDATE());
INSERT INTO dic_Genre(name, genre_description, Update_Who, Update_Date)VALUES('FPS', 'A genre centered on gun and projectile weapon-based combat through a first-person perspective', 1, GETDATE());
INSERT INTO dic_Genre(name, genre_description, Update_Who, Update_Date)VALUES('RTS', 'A genre that focuses on skillful thinking and planning to achieve victory in real time', 1, GETDATE());
INSERT INTO dic_Genre(name, genre_description, Update_Who, Update_Date)VALUES('Adventure', 'A genre in which the player assumes the role of protagonist in an interactive story driven by exploration and puzzle-solving', 1, GETDATE());

INSERT INTO dic_Product_type(name, Update_Who, Update_Date)VALUES('Physicial Copy', 1, GETDATE());
INSERT INTO dic_Product_type(name, Update_Who, Update_Date)VALUES('Collector''s Edition ', 1, GETDATE());
INSERT INTO dic_Product_type(name, Update_Who, Update_Date)VALUES('Download Code', 1, GETDATE());

INSERT INTO dic_Offer_status(name, offer_status_description, Update_Who, Update_Date)VALUES('Created', 'Ready for customers', 1, GETDATE());
INSERT INTO dic_Offer_status(name, offer_status_description, Update_Who, Update_Date)VALUES('Pending', 'To be accepted by both parties', 1, GETDATE());
INSERT INTO dic_Offer_status(name, offer_status_description, Update_Who, Update_Date)VALUES('Finished', 'Accepted by both parties', 1, GETDATE());

INSERT INTO dic_Transaction_status(name, offer_transaction_description, Update_Who, Update_Date)VALUES('Created', 'Offer ready for customers', 1, GETDATE());
INSERT INTO dic_Transaction_status(name, offer_transaction_description, Update_Who, Update_Date)VALUES('Pending', 'To be accepted by both parties', 1, GETDATE());
INSERT INTO dic_Transaction_status(name, offer_transaction_description, Update_Who, Update_Date)VALUES('Finished', 'Accepted by both parties', 1, GETDATE());