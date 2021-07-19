using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JuFlores.Models
{
    public class Fotografias
    {
        [Key]
        public int Id { get; set; }
        //Dados das fotografias 
        public string Fotografia { get; set; }
        //Data upload foto 
        public DateTime Date { get; set; }

        [NotMapped]
        [DisplayName("Enviar Imagem")]
        public IFormFile FicheiroImagem { get; set; }
    }
}
