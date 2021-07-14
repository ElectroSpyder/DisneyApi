namespace DisneyApi.Core.Models.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Imagen
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(255)]
        public string NombreImagen { get; set; }
        [StringLength(100)]
        public string ContentType { get; set; }
        public byte[] Contentt { get; set; }
        public FileType FileType { get; set; }


    }
}