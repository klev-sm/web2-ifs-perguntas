using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Web2.Data;
using Web2.Models;

var builder = WebApplication.CreateBuilder(args);

// Adiciona a string de conexão para o AppDbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adiciona serviços de autenticação Negotiate (opcional, se for necessário)
builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
   .AddNegotiate();

// Configura o Identity
builder.Services.AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Adiciona autorização padrão
builder.Services.AddAuthorization(options =>
{
    // Por padrão, todas as solicitações serão autorizadas de acordo com a política padrão.
    options.FallbackPolicy = null;
});

// Adiciona Razor Pages
builder.Services.AddRazorPages();

var app = builder.Build();
builder.Logging.AddConsole();

// Configura o pipeline de solicitação HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts(); // HSTS é habilitado para produção
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Certifique-se de adicionar autenticação
app.UseAuthorization();

app.MapRazorPages();

app.Run();
