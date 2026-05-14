using FacturasAPI.Datos;
using FacturasAPI.Entidades;
namespace FacturasAPI.Repositorios;

public class FacturasRepositorio : IFacturasRepositorio
{
    private readonly FacturasDBContext _context;

    public FacturasRepositorio(FacturasDBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Facturas>> GetFacturasAsync()
    {
        return await _context.Facturas.AsAsyncEnumerable().ToListAsync();
    }

    public async Task<Facturas> GetFacturaByIdAsync(int id)
    {
        return await _context.Facturas.FindAsync(id);
    }

    public async Task AddFacturaAsync(Facturas factura)
    {
        _context.Facturas.Add(factura);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateFacturaAsync(Facturas factura)
    {
        _context.Facturas.Update(factura);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteFacturaAsync(int id)
    {
        var factura = await _context.Facturas.FindAsync(id);
        if (factura != null)
        {
            _context.Facturas.Remove(factura);
            await _context.SaveChangesAsync();
        }
    }
}