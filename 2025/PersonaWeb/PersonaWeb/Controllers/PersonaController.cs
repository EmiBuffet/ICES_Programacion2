using Microsoft.AspNetCore.Mvc;
using PersonaWeb.Models;

namespace PersonaWeb.Controllers
{
    public class PersonaController : Controller
    {

        private readonly IConfiguration _configuration;

        public PersonaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        ///Persona/ConsultaPersona
        public IActionResult ConsultarPersona()
        {
            var endpoint = _configuration.GetValue<string>("BackendEndpoint");

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(endpoint + "personas/");

            PersonaDto personaDto = client.GetAsync("1").Result.Content.ReadFromJsonAsync<PersonaDto>().Result;

            var persona = new ViewModels.Persona
            {
                Nombre = personaDto.nombre,
                Apellido = ""
            };
            return View(persona);
        }
    }
}
