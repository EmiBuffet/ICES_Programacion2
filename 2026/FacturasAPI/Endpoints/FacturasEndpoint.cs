namespace FacturasAPI.Endpoints;
using FacturasAPI.Logica;
using FacturasAPI.Entidades;
public static class FacturasEndpoint
{
    public static void MapFacturasEndpoints(this WebApplication app)
    {
        app.MapGet("/api/facturas", async (IFacturasLogica logica) =>
        {
            var facturas = await logica.GetFacturasAsync();
            return Results.Ok(facturas);
        });

        app.MapGet("/api/facturas/{id}", async (int id, IFacturasLogica logica) =>
        {
            var factura = await logica.GetFacturaByIdAsync(id);
            return factura is not null ? Results.Ok(factura) : Results.NotFound();
        });

        app.MapPost("/api/facturas", async (Facturas factura, IFacturasLogica logica) =>
        {
            await logica.AddFacturaAsync(factura);
            return Results.Created($"/api/facturas/{factura.Id}", factura);
        });

        app.MapPut("/api/facturas/{id}", async (int id, Facturas factura, IFacturasLogica logica) =>
        {
            if (id != factura.Id)
                return Results.BadRequest();

            var existingFactura = await logica.GetFacturaByIdAsync(id);
            if (existingFactura is null)
                return Results.NotFound();

            await logica.UpdateFacturaAsync(factura);
            return Results.NoContent();
        });

        app.MapDelete("/api/facturas/{id}", async (int id, IFacturasLogica logica) =>
        {
            var existingFactura = await logica.GetFacturaByIdAsync(id);
            if (existingFactura is null)
                return Results.NotFound();

            await logica.DeleteFacturaAsync(id);
            return Results.NoContent();
        });
    }
}