using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JuFlores.Models
{
    public class PecasArtigos
    {

        //primary Key 
        [Key]
        public int Id { get; set; }

        ///fk para os Pecas 
        [ForeignKey(nameof(Peca))]
        public int PecaFK { get; set; }
        public Pecas Peca { get; set; }


        public float Qtd { get; set; }


        ///fk para os artigos 
        [ForeignKey(nameof(Artigo))]
        public int ArtigoFK { get; set; }
        public Artigos Artigo { get; set; }
    }
}