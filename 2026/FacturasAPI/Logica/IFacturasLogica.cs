using FacturasAPI.Entidades;

namespace FacturasAPI.Logica
{
    public interface IFacturasLogica
    {
        Task<IEnumerable<Facturas>> GetFacturasAsync();
        Task<Facturas> GetFacturaByIdAsync(int id);
        Task AddFacturaAsync(Facturas factura);
        //Task UpdateFacturaAsync(Facturas factura);
        Task DeleteFacturaAsync(int id);
        Task UpdateFacturaAsync(Facturas factura);
    }
}