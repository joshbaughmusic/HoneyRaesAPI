\c HoneyRaes

INSERT INTO Customer (Name, Address) VALUES ('Dumb', '123 street');
INSERT INTO Customer (Name, Address) VALUES ('Dumber', '456 street');
INSERT INTO Customer (Name, Address) VALUES ('Dumbest', '789 street');

INSERT INTO Employee (Name, Specialty) VALUES ('Bob', 'Cheese');
INSERT INTO Employee (Name, Specialty) VALUES ('Loyd', 'Turning it off and on again');

INSERT INTO ServiceTicket (CustomerId, EmployeeId, Description, Emergency) VALUES (2, 1, 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.', true);
INSERT INTO ServiceTicket (CustomerId, EmployeeId, Description, Emergency, DateCompleted) VALUES (1, 2, 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.', false, '2023-04-20 00:00:00');
INSERT INTO ServiceTicket (CustomerId, EmployeeId, Description, Emergency) VALUES (3, 1, 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.', true);
INSERT INTO ServiceTicket (CustomerId, Description, Emergency, DateCompleted) VALUES (3, 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.', false, '2023-01-01 00:00:00');
INSERT INTO ServiceTicket (CustomerId, Description, Emergency, DateCompleted) VALUES (1, 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.', false, '2023-01-08 00:00:00');