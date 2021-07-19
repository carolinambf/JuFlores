using JuFlores.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JuFlores.Data
{
    public class JuFloresDB : IdentityDbContext
    //public class JuFloresDB : DbContext
    {
        // Onde está armazenada a BD    --> appsettings.json
        // Que tipo de BD é?            --> startup.cs
        public JuFloresDB(DbContextOptions<JuFloresDB> options) : base(options)
        {

        }
        /// <summary>
        /// método para assistir a criação da base de dados que representa o modelo
        /// </summary>
        /// <param name="modelBuilder">opção de configuração da criação do modelo</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // importar todo o comportamento deste método
            // definido na classe DbContext
            base.OnModelCreating(modelBuilder);


            //*********************************************************************
            // acrescentar novos dados às tabelas - seed das tabelas
            //*********************************************************************

            // adicionar os Roles
            modelBuilder.Entity<IdentityRole>().HasData(
               new IdentityRole { Id = "c", Name = "Cliente", NormalizedName = "CLIENTE" },
               new IdentityRole { Id = "f", Name = "Funcionario", NormalizedName = "FUNCIONARIO" },
               new IdentityRole { Id = "a", Name = "Administrador", NormalizedName = "ADMINISTRADOR" }
            );
        }


        // especificar as tabelas da BD 
        public DbSet<Pecas> Pecas { get; set; }
        public DbSet<Artigos> Artigos { get; set; }
        public DbSet<Utilizadores> Utilizadores { get; set; }
        public DbSet<Fotografias> Fotografias { get; set; }
        public DbSet<Favoritos> Favoritos { get; set; }
        public DbSet<PecasArtigos> PecasArtigos { get; set; }
    }
}
