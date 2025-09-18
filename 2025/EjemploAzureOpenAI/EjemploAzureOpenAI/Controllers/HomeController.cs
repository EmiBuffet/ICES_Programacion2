using EjemploAzureOpenAI.Models;
using EjemploAzureOpenAI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Mscc.GenerativeAI;
using System.Diagnostics;

namespace EjemploAzureOpenAI.Controllers
{

    public class HomeController : Controller
    {
        private readonly GoogleAI _googleAI;

        public HomeController(GoogleAI googleAI)
        {
            _googleAI = googleAI;
        }

        public IActionResult Index()
        {
            return View(new GeminiViewModel());
        }

        public async Task<IActionResult> GenerateContent(GeminiViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Prompt))
            {
                return View("Index", model);
            }

            try
            {
                // Define la configuración de generación
                var generationConfig = new GenerationConfig
                {
                    Temperature = 0.7f, // Valor de 0.0 a 1.0 (float)
                    MaxOutputTokens = 200 // Un número entero que define el límite
                };

                var generativeModel = _googleAI.GenerativeModel(
                    model: Model.Gemini15Flash,
                    generationConfig: generationConfig);

                // Define tu prompt por defecto
                var defaultPrompt = "Eres un asistente de IA formal. Responde a todas las preguntas con un tono profesional.";

                // Crea el historial de la conversación.
                var chat = generativeModel.StartChat();

                // Envía el prompt inicial para establecer el contexto.
                await chat.SendMessage(defaultPrompt);

                // Envía el prompt del usuario y obtén la respuesta
                var response = await chat.SendMessage(model.Prompt);
                model.Response = response.Text;
            }
            catch (Exception ex)
            {
                model.Response = $"Error: {ex.Message}";
            }

            return View("Index", model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
