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
    
    public partial class Docente
    {
        public Docente()
        {
            this.AgendaDocente = new HashSet<AgendaDocente>();
            this.AgendaEvento = new HashSet<AgendaEvento>();
            this.Agendamento = new HashSet<Agendamento>();
            this.AreaAtuacao = new HashSet<AreaAtuacao>();
            this.Componente = new HashSet<Componente>();
        }
    
        public int IdDocente { get; set; }
        public Nullable<int> IdAgendaComponente { get; set; }
        public Nullable<int> IdTipoContrato { get; set; }
        public Nullable<int> IdEmpresa { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public Nullable<byte> Sexo { get; set; }
        public string Tel { get; set; }
        public Nullable<short> NivelFuncao { get; set; }
        public string Nome { get; set; }
    
        public virtual AgendaComponente AgendaComponente { get; set; }
        public virtual ICollection<AgendaDocente> AgendaDocente { get; set; }
        public virtual ICollection<AgendaEvento> AgendaEvento { get; set; }
        public virtual ICollection<Agendamento> Agendamento { get; set; }
        public virtual TipoContrato TipoContrato { get; set; }
        public virtual Empresa Empresa { get; set; }
        public virtual ICollection<AreaAtuacao> AreaAtuacao { get; set; }
        public virtual ICollection<Componente> Componente { get; set; }
    }
}