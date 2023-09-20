
CREATE DATABASE ABMArticulos
GO 
USE ABMArticulos
GO 
CREATE TABLE Articulos(
id int not null primary key identity(1,1),
descripcion varchar(200) null,
fechaAlta datetime null,
estado int null
)
GO 
CREATE TABLE Articulos_Stock(
id int not null primary key identity(1,1),
idArticulo int not null foreign key references Articulos(id), 
stock float null,
estado int null
)
GO 
CREATE TABLE Logs(
id int not null primary key identity(1,1), 
usuario int null, 
fechaHora datetime null,
tipo varchar(20) null,
descripcion text null
)

