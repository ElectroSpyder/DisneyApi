namespace DisneyApi.Core.Models.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Genero : ObjetoBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage ="El Nombre es requerido")]
        public string Nombre { get; set; }       
        public ICollection<PeliculaSerie> PeliculaSeries { get; set; }
    }
}
