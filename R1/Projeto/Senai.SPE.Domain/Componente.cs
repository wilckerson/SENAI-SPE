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
    
    public partial class Componente
    {
        public Componente()
        {
            this.AgendaAmbiente = new HashSet<AgendaAmbiente>();
            this.AgendaComponente = new HashSet<AgendaComponente>();
            this.AgendaDocente = new HashSet<AgendaDocente>();
            this.Agendamento = new HashSet<Agendamento>();
            this.UnCurricular = new HashSet<UnCurricular>();
            this.AreaAtuacao = new HashSet<AreaAtuacao>();
            this.Docente = new HashSet<Docente>();
            this.TipoAmbiente = new HashSet<TipoAmbiente>();
            this.Modulo = new HashSet<Modulo>();
        }
    
        public int IdComponente { get; set; }
        public string Nome { get; set; }
        public Nullable<short> CH { get; set; }
    
        public virtual ICollection<AgendaAmbiente> AgendaAmbiente { get; set; }
        public virtual ICollection<AgendaComponente> AgendaComponente { get; set; }
        public virtual ICollection<AgendaDocente> AgendaDocente { get; set; }
        public virtual ICollection<Agendamento> Agendamento { get; set; }
        public virtual ICollection<UnCurricular> UnCurricular { get; set; }
        public virtual ICollection<AreaAtuacao> AreaAtuacao { get; set; }
        public virtual ICollection<Docente> Docente { get; set; }
        public virtual ICollection<TipoAmbiente> TipoAmbiente { get; set; }
        public virtual ICollection<Modulo> Modulo { get; set; }
    }
}
