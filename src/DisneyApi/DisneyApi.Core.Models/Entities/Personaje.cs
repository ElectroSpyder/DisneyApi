namespace DisneyApi.Core.Models.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Personaje: ObjetoBase
    {
        public Personaje()
        {
            PeliculasSeries = new HashSet<PeliculaSerie>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(150, ErrorMessage ="El nombre no puede superar los {1} caracteres")]
        public string Nombre { get; set; }

        [Range(1,120, ErrorMessage ="El valor ingresado no esta permitido")]
        public int Edad { get; set; }

        [Display(Name ="Peso en Kg.")]
        [Range(0.1, 150, ErrorMessage = "Entre {1}, {2}")]
        public double Peso { get; set; }

        [StringLength(1000,ErrorMessage ="La Historia debe ser corta, hasta {1} caracteres")]
        public string Historia { get; set; }

        public virtual ICollection<PeliculaSerie>  PeliculasSeries { get; set; }
    }
}
