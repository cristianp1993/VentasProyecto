IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
               WHERE TABLE_NAME = 't_usuario' AND COLUMN_NAME = 'usu_sesion_activa')
BEGIN   
    ALTER TABLE t_usuario
    ADD usu_sesion_activa bit DEFAULT 0; 
END
go
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
               WHERE TABLE_NAME = 't_usuario' AND COLUMN_NAME = 'usu_correo')
BEGIN   
    ALTER TABLE t_usuario
    ADD usu_correo character varying(200); 
END
go
INSERT INTO t_usuario (usu_nombre, usu_contrasenia, usu_rol, usu_correo)
SELECT 'Cristian', '123465', 'ADMINISTRADOR', 'cristian.piedrahita88@gmail.com'
WHERE NOT EXISTS (
    SELECT 1 FROM t_usuario WHERE usu_correo = 'cristian.piedrahita88@gmail.com'
);
go