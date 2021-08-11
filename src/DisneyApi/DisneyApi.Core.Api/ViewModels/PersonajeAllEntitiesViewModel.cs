

namespace DisneyApi.Core.Api.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class PersonajeAllEntitiesViewModel
    {
        [Required]
        [StringLength(150, ErrorMessage = "El nombre no puede superar los {1} caracteres")]
        public string Nombre { get; set; }

        [Required]
        [Range(5, 120, ErrorMessage = "Debe Ingresar una edad entre {1} y {2} ")]
        public int Edad { get; set; }

        [Required]
        [Display(Name = "Peso en Kg.")]
        [Range(0.1, 150, ErrorMessage = "Entre {1}, {2}")]
        public double Peso { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = "La Historia debe ser corta, hasta {1} caracteres")]
        public string Historia { get; set; }

        public string ImagenUrl { get; set; }

        public string ImagenTitulo { get; set; }
        public ICollection<PeliculaSerieViewModel> PeliculasSeries { get; set; }
    }
}
