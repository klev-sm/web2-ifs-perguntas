using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web2.Data;
using Web2.Models;

namespace Web2.Pages
{
    public class PerguntasModel : PageModel
    {
        private readonly AppDbContext _context;

        public PerguntasModel(AppDbContext context)
        {
            _context = context;
        }

        public List<Pergunta> Perguntas { get; set; }

        public async Task OnGetAsync()
        {
            Perguntas = await _context.Perguntas
                                      .Include(p => p.Usuario) 
                                      .OrderByDescending(p => p.DataPostagem) 
                                      .ToListAsync();
        }
    }
}
