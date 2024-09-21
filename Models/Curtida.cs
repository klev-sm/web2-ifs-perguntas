using System.ComponentModel.DataAnnotations;

namespace Web2.Models
{
    public class Curtida
    {
        [Key]
        public int ID { get; set; }

        public int? RespostaId { get; set; } 
        public Resposta Resposta { get; set; }
        [Required]
        public int? PerguntaId { get; set; } 
        public Pergunta Pergunta { get; set; }
        [Required]
        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; } 
    }
}
