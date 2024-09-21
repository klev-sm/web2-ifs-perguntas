using System.ComponentModel.DataAnnotations;

namespace Web2.Models
{
    public class Curtida
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int PerguntaId { get; set; }
        public Pergunta Pergunta { get; set; }

        [Required]
        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
