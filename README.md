# Tutorial para crear API REST en .NET 10 con Minimal APIs, Entiframework, Scalar y Arquitectura Limpia
## Requisitos: VS Code, .NET 8 o superior (recomendado .NET 10) y SQL Server

### 1. Crear el proyecto Web API
```
dotnet new webapi -n EscuelaApi
cd EscuelaApi
```
### 2. Crear las 5 carpetas de la estructura
```
mkdir Datos, Entidades, Repositorios, Logica, Endpoints
mkdir Logica/DTOs
```
### 3. Instalar paquetes para EntityFramework con SQL Server y Scalar
```
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Scalar.AspNetCore
```
### 4. En program.cs agregar:
```
using Scalar.AspNetCore;
...
app.MapScalarApiReference();
```
En este punto probar levantar la aplicación con
```
dotnet run
```
Deberia levantar en:  
http://localhost:5001/scalar (Documentación interactiva)  
http://localhost:5001/openapi/v1.json (Documentación estandar OpenApi)

* Modificar puerto por el que levanto.
### 5. Crear clase en Entidades.  Esta clase es el mapeo con la tabla de la base de datos (con EntityFramework)
```
namespace EscuelaApi.Entidades;

public class Alumno
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Legajo { get; set; } = string.Empty;
    public DateTime FechaRegistro { get; set; } = DateTime.Now;
}
```
### 6. Crear DB Context. 
```
using Microsoft.EntityFrameworkCore;
using EscuelaApi.Entidades;

namespace EscuelaApi.Datos;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Alumno> Alumnos => Set<Alumno>();
}
```
### 7. Crear Repositorio
```
using Microsoft.EntityFrameworkCore;
using EscuelaApi.Datos;
using EscuelaApi.Entidades;

namespace EscuelaApi.Repositorios;

public interface IAlumnoRepository {
    Task<IEnumerable<Alumno>> ObtenerTodos();
    Task Agregar(Alumno alumno);
}

public class AlumnoRepository : IAlumnoRepository {
    private readonly AppDbContext _db;
    public AlumnoRepository(AppDbContext db) => _db = db;

    public async Task<IEnumerable<Alumno>> ObtenerTodos() => await _db.Alumnos.ToListAsync();
    public async Task Agregar(Alumno alumno) {
        _db.Alumnos.Add(alumno);
        await _db.SaveChangesAsync();
    }
}
```
### 8. Crear DTO en Logica/DTO
```
namespace EscuelaApi.Logica.DTOs;

public record AlumnoDto(int Id, string Nombre, string Legajo);
public record AlumnoCreateDto(string Nombre, string Legajo);
```
### 9. Crear clase de Logica en carpeta Logica
```
using EscuelaApi.Repositorios;
using EscuelaApi.Logica.DTOs;
using EscuelaApi.Entidades;

namespace EscuelaApi.Logica;

public class AlumnoLogica
{
    private readonly IAlumnoRepository _repo;
    public AlumnoService(IAlumnoRepository repo) => _repo = repo;

    public async Task<IEnumerable<AlumnoDto>> ListarTodos()
    {
        var alumnos = await _repo.ObtenerTodos();
        return alumnos.Select(a => new AlumnoDto(a.Id, a.Nombre, a.Legajo));
    }

    public async Task Crear(AlumnoCreateDto dto)
    {
        var nuevoAlumno = new Alumno { 
            Nombre = dto.Nombre, 
            Legajo = dto.Legajo 
        };
        await _repo.Agregar(nuevoAlumno);
    }
}
```
### 10. Crear API en Endpoints.
```
using EscuelaApi.Logica;
using EscuelaApi.Logica.DTOs;

namespace EscuelaApi.Endpoints;

public static class AlumnoEndpoints
{
    public static void MapAlumnoEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/alumnos");

        group.MapGet("/", async (AlumnoService service) => 
            Results.Ok(await service.ListarTodos()));

        group.MapPost("/", async (AlumnoCreateDto dto, AlumnoService service) => {
            await service.Crear(dto);
            return Results.Created();
        });
    }
}
```
### 11. En Program.cs agregar:
```
// 1. DBContext
builder.Services.AddDbContext<AppDbContext>(opt => 
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Inyección de Dependencias
builder.Services.AddScoped<IAlumnoRepository, AlumnoRepository>();
builder.Services.AddScoped<AlumnoService>();

// 3. Registrar Rutas
app.MapAlumnoEndpoints();
```
### 12. Ejecutar migraciones y levantar la aplicación
```
dotnet ef migrations add Inicial
dotnet ef database update
dotnet run
```
