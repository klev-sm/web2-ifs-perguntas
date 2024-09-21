using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Web2.Data;
using Web2.Models;

namespace Web2.Pages
{
    public class CriarPerguntaModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Usuario> _userManager;

        public CriarPerguntaModel(AppDbContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            public string Titulo { get; set; }

            [Required]
            public string Descricao { get; set; }
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                // Obter o usuário logado
                var usuario = await _userManager.GetUserAsync(User);

                if (usuario == null)
                {
                    // Se o usuário não estiver logado, redireciona para a página de login
                    return RedirectToPage("/Account/Login");
                }

                // Criação de uma nova pergunta
                var pergunta = new Pergunta
                {
                    Titulo = Input.Titulo,
                    Descricao = Input.Descricao,
                    DataPostagem = DateTime.Now,
                    UsuarioId = usuario.Id // Associar a pergunta ao usuário logado
                };

                // Salvando no banco de dados
                _context.Perguntas.Add(pergunta);
                await _context.SaveChangesAsync();

                // Redirecionando para a lista de perguntas
                return RedirectToPage("/Perguntas");
            }

            return Page();
        }
    }
}
