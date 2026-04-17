namespace ProyectoDivine.Src.Db;
using Microsoft.EntityFrameworkCore;
using ProyectoDivine.Src.Model;
/// <summary>
/// Clase que representa el contexto de la base de datos para la aplicación.
/// Hereda de DbContext, lo que permite interactuar con la base de datos utilizando Entity Framework Core.
/// Contiene DbSet para las entidades User y Role, lo que permite realizar interactuar con 
/// los modelos bases de datos correspondientes a estas entidades.
/// </summary>
/// <param name="options"></param>
public class ContextDb(DbContextOptions<ContextDb> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<Funtion> Functions { get; set; } = null!;


/// <summary>
/// Configura las relaciones entre las entidades User y Role utilizando Fluent API en el método OnModelCreating.
/// Establece una relación de uno a muchos entre Role y User, donde un Role puede tener muchos Users,
/// y cada User tiene un Role asociado a través de la clave foránea RoleId.
/// Además, se asegura de que la relación esté correctamente configurada para que Entity Framework Core 
/// pueda manejarla adecuadamente al interactuar con la base de datos.
/// </summary>
/// <param name="modelBuilder">
/// El constructor de modelos de Entity Framework Core.
/// </param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración de la relación entre User y Role
        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId);

        modelBuilder.Entity<Role>()
            .HasMany(u=>u.Users)
            .WithOne(r=>r.Role)
            .HasForeignKey(u=>u.RoleId);
    }
}