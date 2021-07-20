using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JuFlores.Models
{
    public class PecasFotos
    {
        [Key]
        public int Id { get; set; }

        ///fk para as peças
        [ForeignKey(nameof(Peca))]
        public int PecaFk { get; set; }
        public Pecas Peca { get; set; }

        public DateTime Data { get; set; }


        ///fk para as fotos
        [ForeignKey(nameof(Fotografia))]
        public int FotografiaFK { get; set; }
        public Fotografias Fotografia { get; set; }
    }
}
