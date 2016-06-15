CREATE DATABASE db_ventas
go
USE db_ventas
go

CREATE TABLE CATEGORIA (
       IDCATEGORIA          int identity(1,1) NOT NULL,
       NOMBRE               varchar(50) NOT NULL,
       DESCRIPCION          text NULL,
       ESTADO               varchar(1) NOT NULL
       PRIMARY KEY (IDCATEGORIA)
)
go


CREATE TABLE PRODUCTO (
       IDPRODUCTO           int  identity(1,1) NOT NULL,
       IDCATEGORIA          int NOT NULL,
       NOMBRE               varchar(50) NOT NULL,
       DESCRIPCION          varchar(50) NULL,
       PRECIO               int NOT NULL,
       STOCK                int NOT NULL,
       PORTADA              CHAR(1) NOT NULL,
       IMPORTANCIA          int NOT NULL,
       IMAGEN               varchar(50) NULL,
       ESTADO               varchar(1) NULL,
       PRIMARY KEY (IDPRODUCTO), 
       FOREIGN KEY (IDCATEGORIA)
                             REFERENCES CATEGORIA
)
go


CREATE TABLE TIPO_USUARIO (
       IDTIPOUSUARIO        int identity(1,1) NOT NULL,
       NOMBRE		    varchar(50) NOT NULL,
       ESTADO               varchar(1) NOT NULL,
       PRIMARY KEY (IDTIPOUSUARIO)
)
go


CREATE TABLE USUARIO (
       IDUSUARIO            varchar(20) NOT NULL,
       IDTIPOUSUARIO        int NOT NULL,
       PASSWORD             varchar(50) NOT NULL,
       NOMBRE               varchar(20) NOT NULL,
       APELLIDOS            varchar(50) NOT NULL,
       EMAIL                varchar(50) NOT NULL,
       ESTADO               varchar(1) NOT NULL
       PRIMARY KEY (IDUSUARIO), 
       FOREIGN KEY (IDTIPOUSUARIO)
                             REFERENCES TIPO_USUARIO
)
go


CREATE TABLE PEDIDO (
       IDPEDIDO             int identity(1,1) NOT NULL,
       FECHA                datetime NOT NULL,
       ESTADO               varchar(20) NOT NULL,
       IDUSUARIO            varchar(20) NOT NULL,
       PRIMARY KEY (IDPEDIDO), 
       FOREIGN KEY (IDUSUARIO)
                             REFERENCES USUARIO
)
go


CREATE TABLE DETALLE_PEDIDO (
       IDPEDIDO             int NOT NULL,
       IDPRODUCTO           int NOT NULL,
       PRECIO               int NOT NULL,
       CANTIDAD             int NOT NULL,
       PRIMARY KEY (IDPEDIDO, IDPRODUCTO), 
       FOREIGN KEY (IDPRODUCTO)
                             REFERENCES PRODUCTO, 
       FOREIGN KEY (IDPEDIDO)
                             REFERENCES PEDIDO
)
go