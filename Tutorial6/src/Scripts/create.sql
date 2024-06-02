-- Created by Vertabelo (http://vertabelo.com)
-- Last modification date: 2024-05-27 09:15:31.466

-- tables
-- Table: Animal
CREATE TABLE Animal (
    Id int IDENTITY NOT NULL,
    Name varchar(100)  NOT NULL,
    Description varchar(2000)  NULL,
    AnimalTypesId int  NOT NULL,
    CONSTRAINT Animal_pk PRIMARY KEY  (Id)
);

-- Table: AnimalTypes
CREATE TABLE AnimalTypes (
    Id int IDENTITY NOT NULL,
    Name varchar(150)  NOT NULL,
    CONSTRAINT Name_uk UNIQUE (Name),
    CONSTRAINT AnimalTypes_pk PRIMARY KEY  (Id)
);

-- Table: Employee
CREATE TABLE Employee (
    Id int IDENTITY NOT NULL,
    Name varchar(200)  NOT NULL,
    PhoneNumber varchar(20)  NOT NULL,
    Email varchar(200)  NOT NULL,
    CONSTRAINT Employee__uk UNIQUE (PhoneNumber, Email),
    CONSTRAINT Employee_pk PRIMARY KEY  (Id)
);

-- Table: Visit
CREATE TABLE Visit (
    Id int IDENTITY NOT NULL,
    EmployeeId int  NOT NULL,
    AnimalId int  NOT NULL,
    Date varchar(100)  NOT NULL,
    CONSTRAINT Visit_pk PRIMARY KEY  (Id)
);

-- foreign keys
-- Reference: Animal_AnimalTypes (table: Animal)
ALTER TABLE Animal ADD CONSTRAINT Animal_AnimalTypes
    FOREIGN KEY (AnimalTypesId)
    REFERENCES AnimalTypes (Id);

-- Reference: Visit_Animal (table: Visit)
ALTER TABLE Visit ADD CONSTRAINT Visit_Animal
    FOREIGN KEY (AnimalId)
    REFERENCES Animal (Id);

-- Reference: Visit_Employee (table: Visit)
ALTER TABLE Visit ADD CONSTRAINT Visit_Employee
    FOREIGN KEY (EmployeeId)
    REFERENCES Employee (Id);

-- End of file.

