using System.ComponentModel.DataAnnotations;

namespace Web2.Models
{
    public class Pergunta
    {
        public int ID { get; set; }
        [Required]
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataPostagem { get; set; }

        public string UsuarioId { get; set; } 
        public Usuario Usuario { get; set; }

        public ICollection<Resposta> Respostas { get; set; } = new List<Resposta>();
    }

}
