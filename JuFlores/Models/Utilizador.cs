using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JuFlores.Models
{
    public class Utilizador
    {
        //Dados dos utilizadores

        public Utilizador()
        {
            
        }


        [Key]
        public int ID { get; set; }

        [Required]
        public string nome { get; set; }

        [Required]
        public string password { get; set; }

        public string email { get; set; }
        public string morada { get; set; }

        //******************FK************************

        public ICollection<Teste_Realizado> ListaTestesRealizados { get; set; }
        public ICollection<Teste> ListaTestesCriados { get; set; }
    }
}
