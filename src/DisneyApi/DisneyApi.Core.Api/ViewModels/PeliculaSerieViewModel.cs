namespace DisneyApi.Core.Api.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class PeliculaSerieViewModel
    {
        [Required]
        [StringLength(150, ErrorMessage = "El nombre no puede superar los {1} caracteres")]
        public string Titulo { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FechaCreacion { get; set; }

        [Display(Name = "Calificación")]
        [Range(1, 5, ErrorMessage = "El rango permitido es del 1 al 5")]
        public int Calificacion { get; set; }

        [Display(Name = "Genero")]
        public string GeneroNombre { get; set; }

        //public ICollection<PersonajeViewModel> Personajes { get; set; } = null;
    }
}
