namespace DisneyApi.Core.Models.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Personaje: ObjetoBase
    {
        [Key]
        public int Id { get; set; }

        [StringLength(150, ErrorMessage ="El nombre no puede superar los {1} caracteres")]
        public string Nombre { get; set; }

        [Range(1,120, ErrorMessage ="El valor ingresado no esta permitido")]
        public int Edad { get; set; }

        [Display(Name ="Peso en Kg.")]
        [Range(0.1, 150, ErrorMessage = "Entre {1}, {2}")]
        public double Peso { get; set; }

        [StringLength(300,ErrorMessage ="La Historia debe ser corta, hasta {1} caracteres")]
        public string Historia { get; set; }

        public virtual ICollection<PersonajePeliculaSerie>  PersonajePeliculasSeries { get; set; }
    }
}
