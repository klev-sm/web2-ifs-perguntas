using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Web2.Models;

namespace Web2.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<Usuario> _signInManager;

        public LoginModel(SignInManager<Usuario> signInManager)
        {
            _signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            public bool RememberMe { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            // Se o returnUrl estiver vazio, define para a página inicial
            returnUrl ??= Url.Content("~/");

            // Verifica se o retorno é para a página de login, em caso afirmativo, redireciona para a página inicial
            if (!string.IsNullOrEmpty(returnUrl) && returnUrl.Contains("/Account/Login"))
            {
                returnUrl = "/";
            }

            ViewData["ReturnUrl"] = returnUrl;
        }


        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            // Se o returnUrl estiver vazio, define para a página inicial
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    // Se o returnUrl for a página de login, redireciona para a página inicial
                    if (Url.IsLocalUrl(returnUrl) && !returnUrl.Contains("/Account/Login"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToPage("/Index");
                    }
                }

                ModelState.AddModelError(string.Empty, "Login inválido.");
            }

            // Se falhar, retorna para a página de login
            return Page();
        }
    }
}
