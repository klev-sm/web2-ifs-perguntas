using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Web2.Data;
using Web2.Models;

public class PostPerguntaModel : PageModel
{
    private readonly AppDbContext _context;
    private readonly UserManager<Usuario> _userManager;

    public PostPerguntaModel(AppDbContext context, UserManager<Usuario> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [BindProperty]
    public PerguntaInputModel Input { get; set; }

    public class PerguntaInputModel
    {
        [Required]
        public string Titulo { get; set; }

        [Required]
        public string Descricao { get; set; }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.GetUserAsync(User);
            var pergunta = new Pergunta
            {
                Titulo = Input.Titulo,
                Descricao = Input.Descricao,
                DataPostagem = DateTime.Now,
                UsuarioId = user.Id
            };

            _context.Perguntas.Add(pergunta);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }

        return Page();
    }
}
