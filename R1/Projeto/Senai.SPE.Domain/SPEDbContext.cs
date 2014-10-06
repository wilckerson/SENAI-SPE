using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Senai.SPE.Domain.Entity;

namespace Senai.SPE.Domain
{
    public class SPEDbContext : DbContext
    {
        public SPEDbContext()
            : base("SPEContext")
        {

        }

        public DbSet<Unidade> Unidades { get; set; }
        public DbSet<Componente> Componentes { get; set; }
    }
}
