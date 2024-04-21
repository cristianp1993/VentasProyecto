USE master;
GO

CREATE DATABASE VENTAS_DB;
------------------------------ SE CREA LA NUEVA BASE DE DATOS

USE VENTAS_DB;
GO
------------------------------ SE CAMBIA A LA NUEVA BASE DE DATOS PARA USARLA Y AÑADIR LAS TABLAS

CREATE TABLE t_ciudad(
	ciu_id INT IDENTITY(1,1) PRIMARY KEY,
	ciu_nombre  NVARCHAR(50)
);

CREATE TABLE t_persona(
	per_id INT IDENTITY(1,1) PRIMARY KEY,
	ciu_id  INT,
	per_nombre NVARCHAR(50),
	per_direccion  NVARCHAR(50),
	per_telefono NVARCHAR(50),
	per_cuenta_bancaria  NVARCHAR(50),
	per_correo NVARCHAR(50),
	per_nit INTEGER,
	per_tipo NVARCHAR(50),
	FOREIGN KEY (ciu_id) REFERENCES t_ciudad(ciu_id)
);

CREATE TABLE t_notificaciones(
	not_id  INT IDENTITY(1,1) PRIMARY KEY,
	per_id INT,
	not_fecha date,
	not_descripcion NVARCHAR(150),
	FOREIGN KEY (per_id) REFERENCES t_persona(per_id)
);

CREATE TABLE t_usuario(
	usu_id INT IDENTITY(1,1) PRIMARY KEY,
	usu_nombre NVARCHAR(50),
	usu_contrasenia NVARCHAR(50),
	usu_rol NVARCHAR(50)
);

CREATE TABLE t_venta(
	ven_id  INT IDENTITY(1,1) PRIMARY KEY,
	per_id INT,
	usu_id INT,
	ven_fecha date,
	ven_metodo_pago NVARCHAR(100),
	ven_total integer,
	ven_numero_transaccion integer,
	FOREIGN KEY (per_id) REFERENCES t_persona(per_id),
	FOREIGN KEY (usu_id) REFERENCES t_usuario(usu_id)
);

CREATE TABLE t_categoria(
	cat_id INT IDENTITY(1,1) PRIMARY KEY,
	cat_nombre NVARCHAR(50)
);

CREATE TABLE t_producto(
	pro_id  INT IDENTITY(1,1) PRIMARY KEY,
	cat_id INT,
	pro_nombre NVARCHAR(50),
	pro_descripcion NVARCHAR(150),
	pro_valor_unitario INTEGER, 
	pro_stock INTEGER,
	FOREIGN KEY (cat_id) REFERENCES t_categoria(cat_id)
);

CREATE TABLE t_detalle_venta(
	det_id  INT IDENTITY(1,1) PRIMARY KEY,
	ven_id INT,
	pro_id INT,
	det_cantidad INTEGER,
	det_productos_vendidos NVARCHAR(200),
	det_valor_total INTEGER,
	FOREIGN KEY (ven_id) REFERENCES t_venta(ven_id),
	FOREIGN KEY (pro_id) REFERENCES t_producto(pro_id)
);

CREATE TABLE t_pedido(
	 ped_id INT IDENTITY(1,1) PRIMARY KEY,
	 pro_id INT,
	 per_id INT,
	 ped_cant_solicitada INTEGER,
	 ped_cant_entregada INTEGER,
	 ped_fecha DATE,
	 FOREIGN KEY (pro_id) REFERENCES t_producto(pro_id),
	 FOREIGN KEY (per_id) REFERENCES t_persona(per_id)
);

