using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JuFlores.Models
{
    public class Pecas
    {
        //dados das fotografias 

        public string Nome { get; set; }

        /// <summary>
        /// Tipo da peça 
        /// </summary>
        [StringLength(20, MinimumLength = 6, ErrorMessage = "O {0} deve estar compreendido entre {1} e {2} caracteres.")]
        public string Tipo { get; set; }



        public decimal Preco { get; set; }

        /// <summary>
        /// Descrição das peças 
        /// </summary>
        [StringLength(250, MinimumLength = 6, ErrorMessage = "O {0} deve estar compreendido entre {1} e {2} caracteres.")]
        public string Descricao { get; set; }


        /// <summary>
        /// Chave primária 
        /// </summary>
        [Key]
        public int Id { get; set; }


        //add fotografias 
        //
    }
}
