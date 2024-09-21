using Microsoft.AspNetCore.Mvc.RazorPages;
using Web2.Models; // Certifique-se de que est� no namespace correto
using System.Collections.Generic;

namespace Web2.Pages
{
    public class PerguntasModel : PageModel
    {
        public List<Pergunta> Perguntas { get; set; }

        public void OnGet()
        {
            // Simula��o de perguntas carregadas. Substitua pela l�gica de banco de dados
            Perguntas = new List<Pergunta>
            {
                new Pergunta { ID = 1, Titulo = "O que � ASP.NET?", DataPostagem = DateTime.Now.AddDays(-2) },
                new Pergunta { ID = 2, Titulo = "Como usar Entity Framework?", DataPostagem = DateTime.Now.AddDays(-1) }
            };
        }
    }
}
