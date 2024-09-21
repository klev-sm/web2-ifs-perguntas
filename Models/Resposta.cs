namespace Web2.Models
{
    public class Resposta
    {
        public int ID { get; set; }
        public string Descricao { get; set; }
        public DateTime DataPostagem { get; set; }

        public int PerguntaId { get; set; }  
        public Pergunta Pergunta { get; set; } 

        public string UsuarioId { get; set; } 
        public Usuario Usuario { get; set; } 
    }

}
