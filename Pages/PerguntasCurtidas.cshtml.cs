using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Web2.Data;
using Web2.Models;

namespace Web2.Pages
{
    public class PerguntasCurtidasModel : PageModel
    {
        private readonly AppDbContext _context;

        public PerguntasCurtidasModel(AppDbContext context)
        {
            _context = context;
        }

        public List<Pergunta> PerguntasCurtidas { get; set; }

        public async Task OnGetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);  // Obtém o ID do usuário logado

            // Seleciona todas as perguntas que o usuário curtiu
            PerguntasCurtidas = await _context.Curtidas
                .Where(c => c.UsuarioId == userId && c.PerguntaId != null)  // Filtra por curtidas em perguntas
                .Select(c => c.Pergunta)
                .Distinct()  // Evita duplicatas
                .Include(p => p.Usuario)  // Inclui os detalhes do usuário que postou a pergunta
                .ToListAsync();
        }
    }
}
