use master;

CREATE TABLE Animal (
    Id int primary key IDENTITY,
    Name varchar(200) not null,
    Description varchar(200),
    Category varchar(200) not null,
    Area varchar(200) not null
);