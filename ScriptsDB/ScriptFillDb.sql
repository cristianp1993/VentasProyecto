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

-- Insertar 'Equipamiento de seguridad' si no existe
INSERT INTO t_categoria (cat_nombre)
SELECT 'Equipamiento de seguridad'
WHERE NOT EXISTS (
    SELECT 1 FROM t_categoria WHERE cat_nombre = 'Equipamiento de seguridad'
);

-- Insertar 'Equipamiento de deportes acuáticos' si no existe
INSERT INTO t_categoria (cat_nombre)
SELECT 'Equipamiento de deportes acuáticos'
WHERE NOT EXISTS (
    SELECT 1 FROM t_categoria WHERE cat_nombre = 'Equipamiento de deportes acuáticos'
);

-- Insertar 'Equipamiento de escalada' si no existe
INSERT INTO t_categoria (cat_nombre)
SELECT 'Equipamiento de escalada'
WHERE NOT EXISTS (
    SELECT 1 FROM t_categoria WHERE cat_nombre = 'Equipamiento de escalada'
);

-- Insertar 'Accesorios de bicicleta' si no existe
INSERT INTO t_categoria (cat_nombre)
SELECT 'Accesorios de bicicleta'
WHERE NOT EXISTS (
    SELECT 1 FROM t_categoria WHERE cat_nombre = 'Accesorios de bicicleta'
);

-- Insertar 'Equipos de deportes de invierno' si no existe
INSERT INTO t_categoria (cat_nombre)
SELECT 'Equipos de deportes de invierno'
WHERE NOT EXISTS (
    SELECT 1 FROM t_categoria WHERE cat_nombre = 'Equipos de deportes de invierno'
);

INSERT INTO t_producto(cat_id, pro_nombre, pro_descripcion, pro_valor_unitario, pro_stock, pro_url_img)
SELECT 5, 'Tienda de campana', 'Refugio portátil y resistente para tus aventuras al aire libre. Fácil de montar y transportar.', 1434000, 47, 'https://rep-file.sagerp.cloud/Pruebas/VentureSales/tienda_campana.jpeg'
WHERE NOT EXISTS (
    SELECT 1 FROM t_producto WHERE pro_nombre = 'Tienda de campana'
);

INSERT INTO t_producto(cat_id, pro_nombre, pro_descripcion, pro_valor_unitario, pro_stock, pro_url_img)
SELECT 2, 'Kayak infable + remo desarmable', 'Kayak ligero y compacto con remo desmontable. Perfecto para explorar ríos y lagos.', 2270000, 29, 'https://rep-file.sagerp.cloud/Pruebas/VentureSales/kayak.jpg'
WHERE NOT EXISTS (
    SELECT 1 FROM t_producto WHERE pro_nombre = 'Kayak infable + remo desarmable'
);

INSERT INTO t_producto(cat_id, pro_nombre, pro_descripcion, pro_valor_unitario, pro_stock, pro_url_img)
SELECT 5, 'Tablas snowboarding', 'Tablas de snowboarding de alta calidad para deslizarte con estilo en la nieve. Disponibles en varios tamaños.', 1919830, 35, 'https://rep-file.sagerp.cloud/Pruebas/VentureSales/snowboard.jpeg'
WHERE NOT EXISTS (
    SELECT 1 FROM t_producto WHERE pro_nombre = 'Tablas snowboarding'
);

INSERT INTO t_producto(cat_id, pro_nombre, pro_descripcion, pro_valor_unitario, pro_stock, pro_url_img)
SELECT 2, 'Tablas puddle surf', 'Tablas estables y duraderas para disfrutar del paddle surf. Ideal para todos los niveles.', 4474600, 22, 'https://rep-file.sagerp.cloud/Pruebas/VentureSales/paddle_surf.jpeg'
WHERE NOT EXISTS (
    SELECT 1 FROM t_producto WHERE pro_nombre = 'Tablas puddle surf'
);

INSERT INTO t_producto(cat_id, pro_nombre, pro_descripcion, pro_valor_unitario, pro_stock, pro_url_img)
SELECT 4, 'GPS', 'Dispositivo de navegación preciso por satélite. Ideal para excursiones y viajes en terrenos desconocidos.', 728000, 58, 'https://rep-file.sagerp.cloud/Pruebas/VentureSales/GPS.jpeg'
WHERE NOT EXISTS (
    SELECT 1 FROM t_producto WHERE pro_nombre = 'GPS'
);

INSERT INTO t_producto(cat_id, pro_nombre, pro_descripcion, pro_valor_unitario, pro_stock, pro_url_img)
SELECT 1, 'Chaleco salvavida', 'Chaleco de seguridad para mantenerte a flote en cualquier actividad acuática.', 67626, 41, 'https://rep-file.sagerp.cloud/Pruebas/VentureSales/salvavidas.webp'
WHERE NOT EXISTS (
    SELECT 1 FROM t_producto WHERE pro_nombre = 'Chaleco salvavida'
);

INSERT INTO t_producto(cat_id, pro_nombre, pro_descripcion, pro_valor_unitario, pro_stock, pro_url_img)
SELECT 2, 'Longboard surfskate', 'Patineta larga que imita la sensación de surfear. Perfecta para surfistas y amantes del skate.', 700000, 18, 'https://rep-file.sagerp.cloud/Pruebas/VentureSales/longboard_surfskate.jpg'
WHERE NOT EXISTS (
    SELECT 1 FROM t_producto WHERE pro_nombre = 'Longboard surfskate'
);

INSERT INTO t_producto(cat_id, pro_nombre, pro_descripcion, pro_valor_unitario, pro_stock, pro_url_img)
SELECT 2, 'Cometas kitesurf', 'Velas de kitesurf para deslizarte sobre el agua aprovechando el viento. Diversión asegurada.', 1841625, 25, 'https://rep-file.sagerp.cloud/Pruebas/VentureSales/kite_surf.jpg'
WHERE NOT EXISTS (
    SELECT 1 FROM t_producto WHERE pro_nombre = 'Cometas kitesurf'
);

INSERT INTO t_producto(cat_id, pro_nombre, pro_descripcion, pro_valor_unitario, pro_stock, pro_url_img)
SELECT 2, 'Bolsas secas para equipos electrónicos', 'Protege tus dispositivos electrónicos del agua con estas bolsas impermeables. Imprescindible para aventuras acuáticas.', 25000, 63, 'https://rep-file.sagerp.cloud/Pruebas/VentureSales/bolsas_secas.jpg'
WHERE NOT EXISTS (
    SELECT 1 FROM t_producto WHERE pro_nombre = 'Bolsas secas para equipos electrónicos'
);

INSERT INTO t_producto(cat_id, pro_nombre, pro_descripcion, pro_valor_unitario, pro_stock, pro_url_img)
SELECT 1, 'Arnés de escalada', 'Arnés de seguridad para escalada, diseñado para proporcionar soporte y confort.', 309000, 19, 'https://rep-file.sagerp.cloud/Pruebas/VentureSales/arnes_escalada.jpg'
WHERE NOT EXISTS (
    SELECT 1 FROM t_producto WHERE pro_nombre = 'Arnés de escalada'
);

INSERT INTO t_producto(cat_id, pro_nombre, pro_descripcion, pro_valor_unitario, pro_stock, pro_url_img)
SELECT 5, 'Recipiente isotérmico porta comidas', 'Mantén tus alimentos a la temperatura ideal con este contenedor isotérmico. Perfecto para picnics y campamentos.', 145000, 74, 'https://rep-file.sagerp.cloud/Pruebas/VentureSales/recipiente.webp'
WHERE NOT EXISTS (
    SELECT 1 FROM t_producto WHERE pro_nombre = 'Recipiente isotérmico porta comidas'
);

INSERT INTO t_producto(cat_id, pro_nombre, pro_descripcion, pro_valor_unitario, pro_stock, pro_url_img)
SELECT 4, 'Bicicleta Electrica', 'Bicicleta con motor eléctrico que facilita el pedaleo. Ideal para desplazamientos largos y cómodos.', 3570000, 12, 'https://rep-file.sagerp.cloud/Pruebas/VentureSales/bicicleta_electrica.jpeg'
WHERE NOT EXISTS (
    SELECT 1 FROM t_producto WHERE pro_nombre = 'Bicicleta Electrica'
);

INSERT INTO t_producto(cat_id, pro_nombre, pro_descripcion, pro_valor_unitario, pro_stock, pro_url_img)
SELECT 1, 'Paracaídas de natación', 'Paracaídas para entrenamiento de resistencia en natación. Mejora tu técnica y fuerza.', 69000, 33, 'https://rep-file.sagerp.cloud/Pruebas/VentureSales/paracaidas_natacion.jpeg'
WHERE NOT EXISTS (
    SELECT 1 FROM t_producto WHERE pro_nombre = 'Paracaídas de natación'
);

INSERT INTO t_producto(cat_id, pro_nombre, pro_descripcion, pro_valor_unitario, pro_stock, pro_url_img)
SELECT 1, 'Paracaídas de parapente', 'Equipo de parapente para volar utilizando corrientes de aire. Seguro y confiable.', 3727590, 16, 'https://rep-file.sagerp.cloud/Pruebas/VentureSales/paracaidismo.webp'
WHERE NOT EXISTS (
    SELECT 1 FROM t_producto WHERE pro_nombre = 'Paracaídas de parapente'
);

INSERT INTO t_producto(cat_id, pro_nombre, pro_descripcion, pro_valor_unitario, pro_stock, pro_url_img)
SELECT 2, 'Tabla de surf', 'Tabla de surf de alta calidad para disfrutar de las olas del océano. Disponible en varios tamaños.', 1408000, 27, 'https://rep-file.sagerp.cloud/Pruebas/VentureSales/tabla_surf.jpeg'
WHERE NOT EXISTS (
    SELECT 1 FROM t_producto WHERE pro_nombre = 'Tabla de surf'
);

INSERT INTO t_producto(cat_id, pro_nombre, pro_descripcion, pro_valor_unitario, pro_stock, pro_url_img)
SELECT 2, 'Careta de snorkel', 'Máscara de snorkel que permite respirar y ver claramente bajo el agua. Cómoda y ajustable.', 165000, 52, 'https://rep-file.sagerp.cloud/Pruebas/VentureSales/snorkel.jpeg'
WHERE NOT EXISTS (
    SELECT 1 FROM t_producto WHERE pro_nombre = 'Careta de snorkel'
);

INSERT INTO t_producto(cat_id, pro_nombre, pro_descripcion, pro_valor_unitario, pro_stock, pro_url_img)
SELECT 3, 'Piolet Quark', 'Piolet para escalada en hielo y montañismo. Herramienta esencial para terrenos difíciles.', 901620, 11, 'https://rep-file.sagerp.cloud/Pruebas/VentureSales/piolet-quark.jpg'
WHERE NOT EXISTS (
    SELECT 1 FROM t_producto WHERE pro_nombre = 'Piolet Quark'
);

INSERT INTO t_producto(cat_id, pro_nombre, pro_descripcion, pro_valor_unitario, pro_stock, pro_url_img)
SELECT 3, 'Ascension Jumar Ascendedor', 'Dispositivo de ascenso en cuerda para escalada. Seguro y eficiente.', 374000, 20, 'https://rep-file.sagerp.cloud/Pruebas/VentureSales/ascendedor.jpeg'
WHERE NOT EXISTS (
SELECT 1 FROM t_producto WHERE pro_nombre = 'Ascension Jumar Ascendedor'
);

INSERT INTO t_producto(cat_id, pro_nombre, pro_descripcion, pro_valor_unitario, pro_stock, pro_url_img)
SELECT 3, 'Crash Pads', 'Colchonetas portátiles para amortiguar caídas en escalada en bloque. Protégete durante tus escaladas.', 916196, 14, 'https://rep-file.sagerp.cloud/Pruebas/VentureSales/crashpad.jpg'
WHERE NOT EXISTS (
SELECT 1 FROM t_producto WHERE pro_nombre = 'Crash Pads'
);

INSERT INTO t_producto(cat_id, pro_nombre, pro_descripcion, pro_valor_unitario, pro_stock, pro_url_img)
SELECT 5, 'Esquis Freeride', 'Esquís diseñados para descensos en nieve profunda y fuera de pista. Máxima estabilidad y control.', 1457610, 30, 'https://rep-file.sagerp.cloud/Pruebas/VentureSales/esquis.jpeg'
WHERE NOT EXISTS (
SELECT 1 FROM t_producto WHERE pro_nombre = 'Esquis Freeride'
);

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
               WHERE TABLE_NAME = 't_producto' AND COLUMN_NAME = 'pro_estado')
BEGIN   
    ALTER TABLE t_producto
    ADD pro_estado character varying(10); 
END
GO

-- Insertar 'Equipamiento skate' si no existe
INSERT INTO t_categoria (cat_nombre)
SELECT 'Equipamiento skate'
WHERE NOT EXISTS (
    SELECT 1 FROM t_categoria WHERE cat_nombre = 'Equipamiento skate'
);

-- Actualizacion del producto con ID 7 a la categoria Equipamiento skate
UPDATE t_producto
SET cat_id = 6 WHERE pro_id = 7