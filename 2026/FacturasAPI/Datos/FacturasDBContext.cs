using FacturasAPI.Datos;
using FacturasAPI.Entidades;
using Microsoft.EntityFrameworkCore;
namespace FacturasAPI.Datos;

public class FacturasDBContext : DbContext
{
    public FacturasDBContext(DbContextOptions<FacturasDBContext> options) : base(options)
    {
    }
    public DbSet<Facturas> Facturas { get; set; }
}