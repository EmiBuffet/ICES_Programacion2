namespace FacturasFront.Controllers;
using Microsoft.AspNetCore.Mvc;
using FacturasFront.Models;

public class FacturasController : Controller
{
    public IActionResult ConsultasFacturas()
    {
        ViewData["Titulo"] = "Consultas Facturas";

        using var client = new HttpClient();

        var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:5170/api/facturas");

        List<Facturas> factura = client.SendAsync(request).Result.Content.ReadFromJsonAsync<List<Facturas>>().Result;

        return View(factura);
    }
    [HttpGet]
    public IActionResult RegistrarFactura()
    {
        ViewData["Titulo"] = "Registrar Factura";
        return View();
    }

    [HttpPost]
    public void RegistrarFactura(Facturas factura)
    {
        using var client = new HttpClient();
        System.Console.WriteLine(factura.Cliente);
        System.Console.WriteLine(factura.Monto);
        System.Console.WriteLine(factura.Fecha);
        var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5170/api/facturas");
        request.Content = factura != null ? new StringContent(System.Text.Json.JsonSerializer.Serialize(factura),
         System.Text.Encoding.UTF8, "application/json") : null;

        client.SendAsync(request);
    }
}
