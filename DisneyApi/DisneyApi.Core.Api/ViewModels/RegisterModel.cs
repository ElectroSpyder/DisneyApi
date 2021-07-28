namespace DisneyApi.Core.Api.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    
    public class RegisterModel
    {
        [Required(ErrorMessage ="El correo es requerido")]
        public string Email { get; set; }
        [Required(ErrorMessage ="La contraseña es requerida")]
        public string Password { get; set; }
    }
}
