using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JuFlores.Models
{
    /// <summary>
    /// Relaciona os objetos entre as classes Utilizadores e objetos das classes Artigos
    /// </summary>
    public class UtilizadoresArtigos
    {
        ///fk para os utilziadores 
        [ForeignKey(nameof(Utilizador))]
        public int UtilizadorFK { get; set; }
        public Utilizadores Utilizador { get; set; }






        ///fk para os artigos 
        [ForeignKey(nameof(Artigo))]
        public int ArtigoFK { get; set; }
        public Utilizadores Artigo { get; set; }
    }
}
