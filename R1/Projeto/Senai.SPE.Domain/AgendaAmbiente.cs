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
    
    public partial class AgendaAmbiente
    {
        public int IdAgendaAmbiente { get; set; }
        public Nullable<int> IdAmbiente { get; set; }
        public Nullable<System.DateTime> DataIni { get; set; }
        public Nullable<System.DateTime> DataFim { get; set; }
        public Nullable<System.DateTime> HoraIni { get; set; }
        public Nullable<System.DateTime> HoraFim { get; set; }
        public string DiasSemana { get; set; }
        public Nullable<int> IdModulo { get; set; }
        public Nullable<int> IdComponente { get; set; }
        public Nullable<int> IdTurma { get; set; }
    
        public virtual Ambiente Ambiente { get; set; }
        public virtual Componente Componente { get; set; }
        public virtual Modulo Modulo { get; set; }
        public virtual Turma Turma { get; set; }
    }
}
