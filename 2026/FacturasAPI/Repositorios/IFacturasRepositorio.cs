using FacturasAPI.Entidades;
namespace FacturasAPI.Repositorios
{
    public interface IFacturasRepositorio
    {
        Task<IEnumerable<Facturas>> GetFacturasAsync();
        Task<Facturas> GetFacturaByIdAsync(int id);
        Task AddFacturaAsync(Facturas factura);
        Task UpdateFacturaAsync(Facturas factura);
        Task DeleteFacturaAsync(int id);
    }
}