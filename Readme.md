# Guía de Configuración — API con .NET + PostgreSQL (Neon) | Ayudantía 1

---

## Inicio del Proyecto

```bash
# Crear un nuevo proyecto Web API
dotnet new webapi

# Crear el .gitignore
dotnet new gitignore
```

> Agregar `appsettings.json` en cualquier parte del `.gitignore`.

---

## Estructura de Carpetas

```
└── 📁 Src
    ├── 📁 Controller     → Controladores por servicio
    ├── 📁 Db             → Base de datos, migraciones y Seeder
    ├── 📁 Dtos           → Data Transfer Objects (ampliable)
    ├── 📁 Model          → Modelos base obtenidos de la ERS
    ├── 📁 Services       → Lógica de negocio
    │   └── 📁 Interfaces → Interfaces de la lógica de negocio
    └── 📁 Utils          → Funciones y acciones reutilizables
```

---

## paso 1 — Instalar dependencias y conectar con Neon

### 1.1 Instalar el paquete de PostgreSQL

```bash
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
```

### 1.2 Configurar la conexión en Neon

1. Ir a [https://neon.com/](https://neon.com/) e iniciar sesión.
2. Crear un proyecto (**1 por equipo**).
3. Ir a **Dashboard → Connect**.
4. Cambiar el tipo de *Connection String* a `.NET`.
5. Revelar la contraseña y copiar el string completo.

### 1.3 Agregar la cadena de conexión en `appsettings.json`

```json
"ConnectionStrings": {
  "DefaultConnection": "Aquí va lo copiado de Neon"
}
```

---

## Paso 2 — Crear Modelos y Base de Datos

### 2.1 Crear los modelos

Dentro de la carpeta `Model`, crear los archivos:

- `Role.cs`
- `User.cs`

### 2.2 Crear el contexto de base de datos

Dentro de `Db`, crear el archivo `ContextDb.cs` con las entidades de la base de datos.

### 2.3 Instalar herramientas para migraciones

```bash
dotnet add package Microsoft.EntityFrameworkCore.Design
```

### 2.4 Configurar `Program.cs` y generar migraciones

```bash
# Crear la migración inicial (el nombre cambia según las acciones agregadas)
dotnet ef migrations add InitialCreate -o Src/Db/Migrations

# Aplicar los cambios a la base de datos
dotnet ef database update
```

>  Los cambios pueden verificarse en las tablas de Neon.

---

## Paso 3 — Crear los DTOs

Dentro de `Dtos`, organizar por modelo. Ejemplo:

```
└── 📁 Src
    └── 📁 Dtos
        └── 📁 Users
            └── UserDtos.cs
```

---

## Paso 4 — Crear los Servicios

Los servicios manejan la lógica de negocio. Las interfaces deben llevar el prefijo `I`.

```
└── 📁 Src
    └── 📁 Services
        ├── 📁 Interfaces
        │   └── IAuthServices.cs
        └── AuthServices.cs
```

---

## Paso 5 — Instalar paquetes para autenticación y JWT

```bash
# Encriptación de contraseñas
dotnet add package BCrypt.Net-Next

# Generación y validación de tokens JWT
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
```

### 5.1 Actualizar `appsettings.json` con la configuración JWT

```json
"Jwt": {
  "Key": "",
  "Issuer": "",
  "Audience": "",
  "ExpireMinutes": 0
}
```

### 5.2 Crear la función generadora de tokens

Dentro de `Utils`, crear el archivo:

```
└── 📁 Src
    └── 📁 Utils
        └── GenerateToken.cs
```

> Luego actualizar `Program.cs` para registrar y validar los paquetes instalados.

### 5.3 Completar la lógica de inicio de sesión

Con todo lo anterior listo, completar la implementación en `AuthServices.cs` dentro de la carpeta `Services`.

---

## Paso 6 — Crear el Controller

### 6.1 Crear el controlador de autenticación

Dentro de `Controller`, crear:

- `AuthController.cs`

### 6.2 Registrar el controller en `Program.cs`

Hacer público el controlador mediante su registro en `Program.cs`.

### 6.3 Crear el Seeder

Dentro de `Db`, crear el archivo Seeder con los datos iniciales e inicializarlo en `Program.cs`.

>  Verificar los datos insertados directamente en las tablas de Neon.


## Ayudantía 5 
### 7.1 Crear función
Dentro de los modelos se debe de crear un nuevo archivo para las funciones que contendran lo que consideren pertinente para completar la ERS

### 7.2 Agregar la función a la migraciones
Dentro de `Db` → `ContextDb` Deben de agregar el `DbSET` de función y realizar el siguiente proceso

```bash
dotnet ef migrations add CreateFuntio -o Src/Db/Migrations

dotnet ef database update
```

### 7.3 Create DTOs
Dentro de `Dtos` → `Funtion` (Carpeta creada), se debe de agregar el createFuntion con los datos apropiados (ver archivo correspondiente)

### 7.4 Configurar Cloudinary para las imagenes

#### 7.4.1 Página de cloudinary y obtener datos

Dirigirse a https://cloudinary.com/ y registrarse

dentro de cloudinary ir a `Home` → `Dashboard` 

De esta zona se obtendra el `Cloud Name`, se debe de guardar.

Para los demás datos deben dirigirse a `Go To Api Keys` → `Generate New Api Key` confirmas la operación y de ahí obtendras la Api Key y la Api SECRET

para finalizar, en al Appsettings.json se debe de actualizar con 

```json
  "Cloudinary":{
    "CloudName": "",
    "ApiKey": "",
    "ApiSecret": ""
  }
```
Rellenandolo con sus datos asociados.

#### 7.4.2 Instalar cloudinary en el proyecto
Instalar con el siguiente comando ↓

```Bash
dotnet add package CloudinaryDotNet
```

#### 7.4.3 Creación de servicio con los elementos para la utilización de imagenes de Cloudinary

Realizar o seguir el procedimiento descrito en `CloudinaryServices` para ver los datos.

#### 7.4.4 Public servicio

Realizar la publicación del servicicio en el "Main" O `Program.cs` con el siguiente linea

builder.Services.AddScoped<ICloudinaryServices, CloudinaryServices>();

### 7.5 Creación de servicio e interfaces para funcion

Primero se debe de crear la interfaces que respete la creación de un servicio dentro del programa, esta se ubicara en la carpeta `Interfaces` dentro de la carpeta `Services`, el contenido de varia con respecto a las demás interfaces.

Para la implementación del esta interfaces, crearemos su servicio, lo que agregaremos como parametro al contructor son: El contexto de la base de datos y el servicio de cloudinary.
Dentro de esta implementaremos la interface correspondiente de cloudinary

### 7.6 Crear controllador
Seguiremos el procedimiento adecuado para la creación de un controlador en especifico, dentro de esta solo tendremos los elementos ya vistos del controloador de `AuthController`, Lo nuevo serán las implementación `Authorize(Roles = "Admin")` que nos limitara el uso del controlador a las personas que tengan solo administrador.

### 7.5 Publicar Servicio de interfaces.
Se debe de replicar el paso `7.4.4` donde publicamos el servicio de cloudinary pero para la intercaes y servicio de funcion

### 7.6 Prueba en POSTMAN

La prueba en postman se debe de realizar con el formato de `Body` → `Form-Data` donde agregaremos los datos del dto `CreateFuntion`, en especifico, la imagen debe de ser un tipo `File`, además de la autorización ya vista


## Ayudantía 6 

### 8.1 Creación de cuenta en SendGrid

Para poder enviar correos con una API externa, deben registrarse en:

https://app.sendgrid.com/

Luego de registrarse, realizar los siguientes pasos:

#### 8.1.1 Crear API Key

Ir a:

``Settings`` → ``API Keys``

Crear una nueva API Key.

Importante: Guardar la API Key, ya que después no se podrá visualizar nuevamente.

#### 8.1.2 Crear template de correo

Ir a:

``Email API`` → ``Dynamic Templates`` → ``Create a Dynamic Template``

Asignar un nombre y crear una nueva versión.

Seleccionar template en blanco
Usar el Code Editor

**Si desean enviar datos dinámicos, usar ``{{variable}}``.**

Guardar los cambios antes de salir.


#### 8.1.3 Verificar correo de envío

Ir a:

``Settings`` → ``Sender Authentication`` → ``Single Sender Verification``

Ingresar los datos y verificar el correo.

**No olvidar confirmar el correo recibido.**

### 8.2 instalación de dependencias SendGrid

Debemos de instalar el paquete de sendgrid para el envio de correos el cual es 

```bash
dotnet add package SendGrid
```

Configurar appsettings.json:

```json
"SendGrid":{
    "ApiKey":"",
    "FromEmail":"",
    "FromName":""
  }
```
- ``ApiKey``: la generada anteriormente
- ``FromEmail``: correo verificado
- ``FromName``: nombre asociado al correo

### 8.3 Servicio de SendGrid

Crear un servicio con el siguiente método:

```c#
    Task<bool> SendEmailAsync(string toEmail, string subject, string nombre_funcion, string validacion_string);
```
Donde:

- ``toEmail``: correo destinatario
- ``subject``: asunto
- ``nombreFuncion`` y ``validacionString``: parámetros dinámicos

Implementar según la documentación.

#### 8.4 Publicación del servicio

Realizaremos la publicación del servicio igual que cualquier servicio realizado 

```c#
builder.Services.AddScoped<ISendGridEmailServices, SendGridEmailServices>();
```
Desde ahora en adelante, ya se puede ocupar el servicio en especifico.

#### 8.5 Prueba

Se puede probar el servicio en Postman.

En este caso, se modificó la función para que el estado cambie una vez que se genera un UUID aleatorio.
