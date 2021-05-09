using JuFlores.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JuFlores.Data
{
    public class JuFloresDB : IdentityDbContext
    {
        public JuFloresDB(DbContextOptions<JuFloresDB> options)
            : base(options)
        {


        }



        // especificar as tabelas da BD 
        public DbSet<Pecas> Pecas { get; set; }
        public DbSet<Artigos> Artigos { get; set; }

        public DbSet<Utilizadores> Utilizadores { get; set; }
        public DbSet<Fotografias> Fotografias { get; set; }
        public DbSet<Favoritos> Favoritos { get; set; }


    }
}
