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
    
    public partial class CBO
    {
        public CBO()
        {
            this.Matriz = new HashSet<Matriz>();
        }
    
        public int IdCBO { get; set; }
        public string Descrricao { get; set; }
        public string Codigo { get; set; }
        public Nullable<int> Tipo { get; set; }
    
        public virtual ICollection<Matriz> Matriz { get; set; }
    }
}
