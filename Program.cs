using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using ProyectoDivine.Src.Db;
using ProyectoDivine.Src.Services;
using ProyectoDivine.Src.Services.interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080);
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


// punto 6 de Readme
// Implementa los controladores y los hace publico para que puedan ser accedidos desde el exterior de la aplicación,
// permitiendo así que las rutas definidas en los controladores estén disponibles para recibir solicitudes HTTP y responder
builder.Services.AddControllers();
// Implementa los servicios de autenticación definidos en la interfaz IAuthServices, 
// utilizando la clase AuthServices como implementación concreta, 
//lo que permite que la lógica de autenticación esté disponible para ser utilizada en los controladores y otras
builder.Services.AddScoped<IAuthServices, AuthServices>();
// Punto 2 de Readme
// Variable par obtener la cadena de conexión desde appsettings.json 
var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
/// Configuración de CORS para permitir solicitudes desde el origen "http://localhost:5173",
/// lo que es útil para permitir que una aplicación frontend (como una aplicación React) pueda 
/// comunicarse con esta API sin restricciones de origen cruzado, 
/// permitiendo así el intercambio de datos entre el frontend y el backend de manera segura y controlada.
builder.Services.AddCors(options =>
{
    // Configuración de una política de CORS llamada "AllowAll" 
    // que permite solicitudes desde el origen "http://localhost:5173",
    options.AddPolicy("AllowAll", policy =>
    {
        // Configuración de la política de CORS para permitir solicitudes desde el origen "http://localhost:5173",
        // permitiendo así que una aplicación frontend (como una aplicación React) pueda comunicarse con esta API 
        // sin restricciones de origen cruzado,
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

// Punto 2 de Readme
// Configuración del contexto de la base de datos utilizando Entity Framework Core y PostgreSQL.
builder.Services.AddDbContext<ContextDb>(options =>
    options.UseNpgsql(ConnectionString)
);

// punto 5 de Readme
/// Configuración de la autenticación JWT para la aplicación. 
/// Se establece el esquema de autenticación como JwtBearer,
/// y se configuran los parámetros de validación del token, 
/// incluyendo la validación del emisor, audiencia, tiempo de vida y clave de firma.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // Configuración de los parámetros de validación del token JWT.
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            // Validación del emisor del token JWT.
            ValidateIssuer = true,
            // Validación de la audiencia del token JWT.
            ValidateAudience = true,
            // Validación del tiempo de vida del token JWT.
            ValidateLifetime = true,
            // Validación de la clave de firma del token JWT.
            ValidateIssuerSigningKey = true,
            // Configuración del emisor válido del token JWT, obtenido desde appsettings.json.
            ValidIssuer = builder.Configuration["Jwt:issuer"],
            // Configuración de la audiencia válida del token JWT, obtenida desde appsettings.json.
            ValidAudience = builder.Configuration["Jwt:audience"],
            // Configuración de la clave de firma del token JWT, utilizando una clave simétrica obtenida desde appsettings.json.
            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]!))
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = ctx =>
            {
                ctx.Token = ctx.Request.Cookies["token"];
                return Task.CompletedTask;
            }
        };
    });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
/// Sección para poblar la base de datos con datos iniciales utilizando la clase Seeder.
/// Se crea un alcance de servicio para obtener una instancia del contexto de la base de datos,
/// y se llama al método Seed de la clase Seeder para verificar si existen roles y usuarios en la base de datos,
/// y si no existen, crear un rol de administrador y un rol de usuario, así como un usuario administrador y un usuario normal con contraseñas hasheadas utilizando BCrypt.
using (var scope = app.Services.CreateScope())
{
    try
    {
        // Obtención de una instancia del contexto de la base de datos a través del alcance de servicio.
        var context = scope.ServiceProvider.GetRequiredService<ContextDb>();
        await new Seeder(context).Seed();
        Console.WriteLine("Base de datos poblada exitosamente.");
    }catch (Exception ex)
    {
        Console.WriteLine($"Error al poblar la base de datos: {ex.Message}");
    }
}

// Eso redirecciona a HTTPS, maneja autenticación y autorización, y mapea los controladores. Luego inicia la aplicación.

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();


