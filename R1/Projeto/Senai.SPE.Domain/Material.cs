//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Senai.SPE.Domain
{
    using System;
    using System.Collections.Generic;
    
    public partial class Material
    {
        public Material()
        {
            this.Matriz = new HashSet<Matriz>();
            this.Turma = new HashSet<Turma>();
        }
    
        public int IdMaterial { get; set; }
        public string Descr { get; set; }
        public Nullable<int> IdCategoriaMaterial { get; set; }
    
        public virtual CatMaterial CatMaterial { get; set; }
        public virtual ICollection<Matriz> Matriz { get; set; }
        public virtual ICollection<Turma> Turma { get; set; }
    }
}