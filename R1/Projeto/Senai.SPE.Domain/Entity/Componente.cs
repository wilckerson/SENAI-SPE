using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senai.SPE.Domain.Entity
{
    public class Componente
    {
        [Key]
        public int IdComponente { get; set; }
        public short? CH { get; set; }
        public string Nome { get; set; }
    }
}
