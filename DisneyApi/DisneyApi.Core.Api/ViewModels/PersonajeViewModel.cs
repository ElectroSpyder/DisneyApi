﻿using Microsoft.AspNetCore.Http;

namespace DisneyApi.Core.Api.ViewModels
{

    public class PersonajeViewModel
    {
        public string Nombre { get; set; }
       
        public int Edad { get; set; }
        
        public double Peso { get; set; }
       
        public string Historia { get; set; }

        public IFormFile  Image { get; set; }

        public byte[] Contentt { get; set; }


    }
}