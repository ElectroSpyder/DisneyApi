namespace DisneyApi.Core.Api.ViewModels
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;

    public class LoginViewModel
    {
        
        [Required(ErrorMessage = "Debe ingresa Contraseña")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Debe ingresar Email")]
        [EmailAddress(ErrorMessage = "Correo invalido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe Repetir Email")]
        [EmailAddress(ErrorMessage = "Correo invalido")]
        public string EmailRepiter { get; set; }
    }
}
