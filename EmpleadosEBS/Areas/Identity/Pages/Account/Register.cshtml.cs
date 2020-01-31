using EmpleadosEBS.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace EmpleadosEBS.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _signInRole;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(

            UserManager<User> userManager,
            RoleManager<IdentityRole> userRol,
            SignInManager<User> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {


            _userManager = userManager;
            _signInRole = userRol;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public User IdentityUser { get; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Usuario")]
            public string UserName { get; set; }

            [Required]
            [Display(Name = "Telefono")]
            public string PhoneNumber { get; set; }

            
            [Display(Name = "Registro")]
            [DataType(DataType.Date)]
            public DateTime Registro { get; set; }


            [Display(Name = "Rol dentro de la empresa")]
            public string Role { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "El {0} debe tener al menos {2} y con un máximo de {1} caracteres.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Contraseña")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirme Contraseña")]
            [Compare("Password", ErrorMessage = "La contraseña y la confirmacion de la contraseña no coinciden")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                //tarea que busca en la base de datos por el email si existe un usuario registrado con ese nombre
                Task<User> checkAppUser = _userManager.FindByEmailAsync(Input.Email);
                checkAppUser.Wait();



                if (checkAppUser.Result == null)
                {
                    User newUser = new User
                    {
                        Registro = DateTime.Now,
                        UserName = Input.UserName,
                        Email = Input.Email,
                        PhoneNumber = Input.PhoneNumber
                    };

                    Task<IdentityResult> taskCreateAppUser = _userManager.CreateAsync(newUser, Input.Password);
                    taskCreateAppUser.Wait();

                    if (taskCreateAppUser.Result.Succeeded)
                    {
                        User appUser = newUser;

                        _logger.LogInformation("Usuario Creado.");

                        await _signInManager.SignInAsync(newUser, isPersistent: false);

                        //Si el campo de Role esta vacio se asigna el Role "Cliente"
                        if (string.IsNullOrEmpty(Input.Role)) 
                        {
                            Task<IdentityResult> newUserRole = _userManager.AddToRoleAsync(appUser, "Cliente");
                            newUserRole.Wait();
                        }
                        else
                        {
                            Task<IdentityResult> newUserRole = _userManager.AddToRoleAsync(appUser, Input.Role);
                            newUserRole.Wait();
                        }

                        return LocalRedirect(returnUrl);
                    }

                }
                else
                {
                    //informa que el usuario esta registrado en la base de datos
                    ModelState.AddModelError(string.Empty, "El usuario existe en la base de datos.");
                    return Page();
                }
            }
            // Si llegamos tan lejos, algo falla, volver a mostrar El formulario.
            return Page();
        }
    }
}
