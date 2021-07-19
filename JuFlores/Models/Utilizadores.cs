using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JuFlores.Models
{
    public class Utilizadores : IdentityUser
    {
        //Dados dos utilizadores

        public Utilizadores()
        {
            
        }


        //[Key]
        //public int ID { get; set; }

        //nome do utilizador 
        [Required(ErrorMessage = "O Nome é de preenchimento obrigatório.")]
         public string Nome { get; set; }

        //[Required]
        //public string Password { get; set; }

        //email do user
        //[StringLength(40, MinimumLength = 6, ErrorMessage = "O {0} deve estar compreendido entre {1} e {2} caracteres.")]
        //[RegularExpression("[a-z0-9.]+@", ErrorMessage = "Escreva um email.")]
        //public string email { get; set; }

        //morada
        [Required(ErrorMessage = "A Morada é de preenchimento obrigatório.")]
        [StringLength(60, ErrorMessage = "A {0} não deve ser maior que {1} caracteres.")]
        public string Morada { get; set; }

        //******************FK************************

       
    }
}
