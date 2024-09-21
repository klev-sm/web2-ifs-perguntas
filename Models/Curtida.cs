namespace Web2.Models
{
    public class Curtida
    {
        public int ID { get; set; }

        public int? RespostaId { get; set; } 
        public Resposta Resposta { get; set; }

        public int? PerguntaId { get; set; } 
        public Pergunta Pergunta { get; set; }

        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; } 
    }
}
