using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JuFlores.Models
{
    public class ArtigosFotos
    {
        [Key]
        public int Id { get; set; }

        ///fk para o Artigo
        [ForeignKey(nameof(Artigo))]
        public int ArtigoFk { get; set; }
        public Artigos Artigo { get; set; }

        public DateTime Data { get; set; }


        ///fk para as fotos
        [ForeignKey(nameof(Fotografia))]
        public int FotografiaFK { get; set; }
        public Fotografias Fotografia { get; set; }
    }
}
