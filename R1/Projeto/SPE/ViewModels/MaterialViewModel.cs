using Senai.SPE.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPE.ViewModel
{
    public class MaterialViewModel
    {
          public int IdMaterial { get; set; }
        public string Descr { get; set; }
        public Nullable<int> ID_CATEGORIA_MAT { get; set; }
    
        public virtual CatMaterial CatMaterial { get; set; }
        public virtual ICollection<Matriz> Matriz { get; set; }
        public virtual ICollection<Turma> Turma { get; set; }
    }
    
}