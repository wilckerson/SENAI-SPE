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
    
    public partial class CalendarioLegenda
    {
        public CalendarioLegenda()
        {
            this.CalendarioCivil = new HashSet<CalendarioCivil>();
        }
    
        public int IdCalendario { get; set; }
        public string Descricao { get; set; }
        public string Cor { get; set; }
    
        public virtual ICollection<CalendarioCivil> CalendarioCivil { get; set; }
    }
}
