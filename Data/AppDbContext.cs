using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Web2.Models;

namespace Web2.Data
{
    public class AppDbContext : IdentityDbContext<Usuario>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Pergunta> Perguntas { get; set; }
        public DbSet<Resposta> Respostas { get; set; }
        public DbSet<Curtida> Curtidas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Pergunta>()
                .HasOne(p => p.Usuario)
                .WithMany(u => u.Perguntas)
                .HasForeignKey(p => p.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade); // Exclusão em cascata permitida para o relacionamento com Usuario

            modelBuilder.Entity<Resposta>()
                .HasOne(r => r.Usuario)
                .WithMany(u => u.Respostas)
                .HasForeignKey(r => r.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade); // Exclusão em cascata permitida para o relacionamento com Usuario

            modelBuilder.Entity<Resposta>()
                .HasOne(r => r.Pergunta)
                .WithMany(p => p.Respostas)
                .HasForeignKey(r => r.PerguntaId)
                .OnDelete(DeleteBehavior.NoAction); // Desativar exclusão em cascata para evitar ciclos
        }
    }

}
