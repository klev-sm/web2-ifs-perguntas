using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Web2.Data;
using Web2.Models;

var builder = WebApplication.CreateBuilder(args);

// Adiciona a string de conex�o para o AppDbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adiciona servi�os de autentica��o Negotiate (opcional, se for necess�rio)
builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
   .AddNegotiate();

// Configura o Identity
builder.Services.AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Adiciona autoriza��o padr�o
builder.Services.AddAuthorization(options =>
{
    // Por padr�o, todas as solicita��es ser�o autorizadas de acordo com a pol�tica padr�o.
    options.FallbackPolicy = null;
});

// Adiciona Razor Pages
builder.Services.AddRazorPages();

var app = builder.Build();
builder.Logging.AddConsole();

// Configura o pipeline de solicita��o HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts(); // HSTS � habilitado para produ��o
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Certifique-se de adicionar autentica��o
app.UseAuthorization();

app.MapRazorPages();

app.Run();
