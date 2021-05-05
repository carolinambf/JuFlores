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

        public string nome { get; set; }

        /// <summary>
        /// Tipo da peça 
        /// </summary>
        [StringLength(20, MinimumLength = 6, ErrorMessage = "O {0} deve estar compreendido entre {1} e {2} caracteres.")]
        public string tipo { get; set; }



        public float preco { get; set; }

        /// <summary>
        /// Descrição das peças 
        /// </summary>
        [StringLength(250, MinimumLength = 6, ErrorMessage = "O {0} deve estar compreendido entre {1} e {2} caracteres.")]
        public string descricao { get; set; }


        public int id { get; set; }

    }
}
