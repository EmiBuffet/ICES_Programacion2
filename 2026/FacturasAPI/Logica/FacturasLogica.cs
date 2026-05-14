using FacturasAPI.Repositorios;
using FacturasAPI.Entidades;

namespace FacturasAPI.Logica;

public class FacturasLogica : IFacturasLogica
{
    private readonly IFacturasRepositorio _facturasRepositorio;

    public FacturasLogica(IFacturasRepositorio facturasRepositorio)
    {
        _facturasRepositorio = facturasRepositorio;
    }

    public Task AddFacturaAsync(Facturas factura)
    {
        return _facturasRepositorio.AddFacturaAsync(factura);
    }

    public Task DeleteFacturaAsync(int id)
    {
        return _facturasRepositorio.DeleteFacturaAsync(id);
    }

    public Task<Facturas> GetFacturaByIdAsync(int id)
    {
        return _facturasRepositorio.GetFacturaByIdAsync(id);
    }

    public Task<IEnumerable<Facturas>> GetFacturasAsync()
    {
        return _facturasRepositorio.GetFacturasAsync();
    }

    public Task UpdateFacturaAsync(Facturas factura)
    {
        return _facturasRepositorio.UpdateFacturaAsync(factura);
    }
}