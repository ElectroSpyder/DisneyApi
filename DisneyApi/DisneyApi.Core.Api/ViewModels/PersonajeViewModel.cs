namespace DisneyApi.Core.Api.ViewModels
{

    public class PersonajeViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
       
        public int Edad { get; set; }
        
        public double Peso { get; set; }
       
        public string Historia { get; set; }

        public string ImagenUrl { get; set; }

        public string ImagenTitulo { get; set; }

    }
}
