using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JuFlores.Models
{
    public class UtilizadoresInputModel
    {
        [Display(Name = "Id")]
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Morada")]
        public string Morada { get; set; }

        [Display(Name = "Número de telefone")]
        public string NumeroTelefone { get; set; }

        [Display(Name = "Função")]
        public string RoleId { get; set; }
    }
}
