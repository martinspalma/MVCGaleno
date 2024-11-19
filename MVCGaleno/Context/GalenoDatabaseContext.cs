using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCGaleno.Models;

namespace MVCGaleno.Context
{
    public class GalenoDatabaseContext : DbContext
    {
        public GalenoDatabaseContext(DbContextOptions<GalenoDatabaseContext> options) : base(options)
        {
        }
        public DbSet<Afiliado> Afiliados { get; set; }
        public DbSet<Turno> Turnos { get; set; }
        public DbSet<PrestadorMedico> Medicos { get; set; }
        public DbSet<Cita> Citas { get; set; }
       
        

    }
}

