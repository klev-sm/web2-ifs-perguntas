using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Web2.Data;
using Web2.Models;

namespace Web2.Pages
{
    public class DetalhesPerguntaModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Usuario> _userManager;

        public DetalhesPerguntaModel(AppDbContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Pergunta Pergunta { get; set; }
        public List<Resposta> Respostas { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "A resposta não pode estar vazia.")]
        public string NovaResposta { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Pergunta = await _context.Perguntas
                                     .Include(p => p.Usuario)
                                     .FirstOrDefaultAsync(p => p.ID == id);

            if (Pergunta == null)
            {
                return NotFound();
            }

            Respostas = await _context.Respostas
                                      .Include(r => r.Usuario)
                                      .Where(r => r.PerguntaId == id)
                                      .ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Account/Login");
            }

            var usuario = await _userManager.GetUserAsync(User);

            if (!ModelState.IsValid)
            {
                // Recarregar a pergunta e respostas se houver erro de validação
                Pergunta = await _context.Perguntas
                                         .Include(p => p.Usuario)
                                         .FirstOrDefaultAsync(p => p.ID == id);

                Respostas = await _context.Respostas
                                          .Include(r => r.Usuario)
                                          .Where(r => r.PerguntaId == id)
                                          .ToListAsync();

                return Page();
            }

            // Adicionar a nova resposta ao banco de dados
            var resposta = new Resposta
            {
                Descricao = NovaResposta,
                PerguntaId = id,
                UsuarioId = usuario.Id,
                DataPostagem = System.DateTime.Now
            };

            _context.Respostas.Add(resposta);
            await _context.SaveChangesAsync();

            // Redirecionar para a própria página de detalhes após salvar a resposta
            return RedirectToPage("/DetalhesPergunta", new { id = id });
        }
    }
}
