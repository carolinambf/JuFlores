using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JuFlores.Models;

namespace JuFlores.Data
{

    /// <summary>
    /// Represa a DB dos utilizadores de artigo 
    /// </summary>
    public class UtilizadorArtigosBD : DbContext
    {
        // especificar as tabelas da BD 
        public DbSet<Pecas> Pecas { get; set; }
        public DbSet<Artigos> Artigos { get; set; }

        public DbSet<Utilizadores> Utilizadores { get; set; }
        public DbSet<Fotografias> Fotografias { get; set; }
        public DbSet<UtilizadoresArtigos> UtilizadoresArtigos { get; set; }

    }
}
