using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JuFlores.Models
{
    public class Artigos
    {
        //dados dos artigos 
        public int Id { get; set; }


        //nome do Artigos
        [Required(ErrorMessage = "O Nome é de preenchimento obrigatório.")]
        public string Nome { get; set; }


        // Nome do responsável ou criador do artigo 
        [Required(ErrorMessage = "O Nome é de preenchimento obrigatório.")]
        public string Criador { get; set; }

    }
}
