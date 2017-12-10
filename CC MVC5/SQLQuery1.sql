Create database CuentasxCobrar
go

USE CuentasxCobrar
create table Cuentas(
IDDocumentos int primary  key identity,
Descripcion varchar(90),
Cuentacontable int,
Estado bit);


create table Clientes
(
IDClientes int primary key identity,
Nombre varchar(40),
Cedula VarChar(15) Not Null,
Credito float,
Limite Float Null,
Estado bit
);

create table Transacciones(
IDTransacciones int primary key identity,
TipoMovimiento char(2),
IDTipoDocumento int,
IDCliente int,
Fecha datetime, 
Monto decimal(5,0));

create table Asientos(
IDAsientos int identity,
Descripcion varchar(20),
IDClientes int, 
Cuenta int,
TipoMovimiento char(2),
FechaAsiento datetime, 
MontoAsiento decimal(4,0),
Estado bit);

Create table CuentasContable
(
IDCuentasContables int identity,
CuentasContables int
)
 go

 Insert into CuentasContable values (001002)
 Insert into Cuentas values ('Nota de credito', 001002,1)
 insert into Clientes values ('Angel Lizander', 00100882963, 13000,0)
 insert into Asientos values('Esto es un asiento', 2, 1300, 'DB', 230323, 1300, 1)

 create table Usuarios(
 Usuario varchar(8),
 Password varchar(10),
 Privilegio char(2))
 go
 Insert into Usuarios values ('Angel',1234,'AA')
