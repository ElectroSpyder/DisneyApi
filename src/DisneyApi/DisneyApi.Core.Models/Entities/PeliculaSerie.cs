using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DisneyApi.Core.Models.Entities
{
    public class PeliculaSerie: ObjetoBase
    {
        public PeliculaSerie()
        {
            Personajes = new HashSet<Personaje>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [StringLength(150)]
        [Display(Name ="Título")]
        [Required(ErrorMessage ="El campo Título es requerido")]
        public string Titulo { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name ="Fecha de Creación")]
        public DateTime? FechaCreacion { get; set; }

        [Display(Name = "Calificación")]
        [Range(1,5,ErrorMessage ="El rango permitido es del 1 al 5")]
        public int Calificacion { get; set; }

        public int IdGenero { get; set; }
        public Genero Genero { get; set; }
        public virtual ICollection<Personaje> Personajes { get; set; }
    }
}
