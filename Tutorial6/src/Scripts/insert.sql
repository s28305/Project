--Animal types table--
INSERT INTO AnimalTypes (Name)
Values 
('Cat'), ('Dog'), ('Parrot'), ('Rabbit'), ('Turtle'), ('Hamster'), ('Mouse'), ('Rat');

--Animal table--
Insert into Animal (Name, Description, AnimalTypesId)
Values
('Rumble', NULL, 1),
('Alex', 'British', 1),
('Ronald', 'German Sheperd', 2),
('Churchill', NULL, 5),
('Three', NULL, 6);

--Employee table--

Insert into Employee (Name, PhoneNumber, Email)
Values
('John Doe', '+48634846332', 'john.doe@mail.com'),
('Alice Doubling', '+1 (415) 444-1356', 'alice.doubling@mail.com');

--Visit table--

Insert into Visit(EmployeeId, AnimalId, Date)
Values
(1,1, '2024-06-16T13:00:00'),
(2,4, '2024-06-16T13:00:00');

Insert into UserType(Name)
Values ('User')