using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuFlores.Models
{
    public class Artigo
    {
        //dados dos artigos 
        public int id { get; set; }
        //nome do Artigo
        public string nome { get; set; }
        // Nome do responsável ou criador do artigo 
        public string criador { get; set; }

    }
}
