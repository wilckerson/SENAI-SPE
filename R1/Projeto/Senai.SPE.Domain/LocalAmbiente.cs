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
    
    public partial class LocalAmbiente
    {
        public LocalAmbiente()
        {
            this.Ambiente = new HashSet<Ambiente>();
        }
    
        public int IdLoc { get; set; }
        public string Descr { get; set; }
    
        public virtual ICollection<Ambiente> Ambiente { get; set; }
    }
}