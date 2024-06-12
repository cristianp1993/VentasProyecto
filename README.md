## Table of Contents
1. [Configuración FTP](#configuracion-ftp)
2. [Configuración JWT](#configuracion-jwt)

### Configuración FTP
***
Para configurar las credenciales de FTP, crea un archivo `ftpconfig.json` en el directorio raíz del proyecto con el siguiente formato:
```json
{
  "FtpIp": "your-ftp-server-ip",
  "FtpPort": 21,
  "FtpUsername": "your-ftp-username",
  "FtpPassword": "your-ftp-password"
}
```
### Configuración JWT
***
Este proyecto utiliza JSON Web Tokens (JWT) para la autenticacion. Para configurar el proyecto adecuadamente, sigue estos pasos:
**Crear el archivo `appsettings.local.json`**:
El contenido del archivo `appsettings.local.json` debe ser el siguiente:
```json
{
  "JwtIssuer": "url_proyecto",
  "JwtAudience": "url_proyecto/Usuario",
  "JwtSecretKey": "tuClaveSecretaSuperSegura"
}
```
