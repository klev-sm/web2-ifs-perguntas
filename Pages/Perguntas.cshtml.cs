using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web2.Data;
using Web2.Models;

namespace Web2.Pages
{
    public class PerguntasModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Usuario> _userManager;

        public PerguntasModel(AppDbContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<Pergunta> Perguntas { get; set; }

        // Método para obter o número de curtidas de uma pergunta
        public int GetCurtidasPerguntaCount(int perguntaId)
        {
            return _context.Curtidas.Count(c => c.PerguntaId == perguntaId);
        }

        public async Task OnGetAsync()
        {
            // Carregar as perguntas do banco de dados e ordenar da mais recente para a mais antiga
            Perguntas = await _context.Perguntas
                                      .Include(p => p.Usuario)
                                      .OrderByDescending(p => p.DataPostagem)
                                      .ToListAsync();
        }

        // Lógica de curtir e descurtir para perguntas
        public async Task<IActionResult> OnPostCurtirPerguntaAsync(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Account/Login");
            }

            var usuario = await _userManager.GetUserAsync(User);

            // Verifica se o usuário já curtiu essa pergunta
            var curtidaExistente = await _context.Curtidas
                                                 .FirstOrDefaultAsync(c => c.PerguntaId == id && c.UsuarioId == usuario.Id);

            if (curtidaExistente == null)
            {
                // Se não curtiu ainda, cria uma nova curtida
                var curtida = new Curtida
                {
                    PerguntaId = id,
                    UsuarioId = usuario.Id
                };

                _context.Curtidas.Add(curtida);
                await _context.SaveChangesAsync();
            }
            else
            {
                // Se já curtiu, remove a curtida (descurtir)
                _context.Curtidas.Remove(curtidaExistente);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
