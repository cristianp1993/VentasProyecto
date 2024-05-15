IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
               WHERE TABLE_NAME = 't_usuario' AND COLUMN_NAME = 'usu_sesion_activa')
BEGIN   
    ALTER TABLE t_usuario
    ADD usu_sesion_activa bit DEFAULT 0; 
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
               WHERE TABLE_NAME = 't_usuario' AND COLUMN_NAME = 'usu_correo')
BEGIN   
    ALTER TABLE t_usuario
    ADD usu_correo character varying(200); 
END
GO

INSERT INTO t_usuario (usu_nombre, usu_contrasenia, usu_rol, usu_correo)
SELECT 'Cristian', '123465', 'ADMINISTRADOR', 'cristian.piedrahita88@gmail.com'
WHERE NOT EXISTS (
    SELECT 1 FROM t_usuario WHERE usu_correo = 'cristian.piedrahita88@gmail.com'
);
GO

update t_usuario set usu_contrasenia='3d9188577cc9bfe9291ac66b5cc872b7' where usu_correo='cristian.piedrahita88@gmail.com'

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
               WHERE TABLE_NAME = 't_ciudad' AND COLUMN_NAME = 'ciu_depto')
BEGIN   
    ALTER TABLE t_ciudad
    ADD ciu_depto character varying(200); 
END
GO


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
               WHERE TABLE_NAME = 't_persona' AND COLUMN_NAME = 'per_tipo_documento')
BEGIN   
    ALTER TABLE t_persona
    ADD per_tipo_documento character varying(50); 
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
               WHERE TABLE_NAME = 't_persona' AND COLUMN_NAME = 'per_tipo_cuenta')
BEGIN   
    ALTER TABLE t_persona
    ADD per_tipo_cuenta character varying(50); 
END
GO
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
               WHERE TABLE_NAME = 't_producto' AND COLUMN_NAME = 'pro_url_img')
BEGIN   
    ALTER TABLE t_producto
    ADD pro_url_img character varying(500); 
END
GO