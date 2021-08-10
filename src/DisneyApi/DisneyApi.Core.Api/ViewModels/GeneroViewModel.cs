using System.ComponentModel.DataAnnotations;

namespace DisneyApi.Core.Api.ViewModels
{
    public class GeneroViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public string ImagenUrl { get; set; }
    }
}
