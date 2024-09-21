using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Web2.Models
{
    public class Usuario : IdentityUser
    {
        public string Nome { get; set; } = string.Empty;

        public ICollection<Pergunta> Perguntas { get; set; } = new List<Pergunta>();

        public ICollection<Resposta> Respostas { get; set; } = new List<Resposta>();

        public ICollection<Curtida> Curtidas { get; set; } = new List<Curtida>();
    }
}
