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




