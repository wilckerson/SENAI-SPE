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
    
    public partial class Usuario
    {
        public Usuario()
        {
            this.Matriz = new HashSet<Matriz>();
            this.Turma = new HashSet<Turma>();
            this.AreaAtuacao = new HashSet<AreaAtuacao>();
        }
    
        public int IdUsuario { get; set; }
        public Nullable<int> IdPerfil { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public string CPF { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public Nullable<System.DateTime> UltimoAcesso { get; set; }
        public Nullable<int> Status { get; set; }
    
        public virtual ICollection<Matriz> Matriz { get; set; }
        public virtual Perfil Perfil { get; set; }
        public virtual ICollection<Turma> Turma { get; set; }
        public virtual ICollection<AreaAtuacao> AreaAtuacao { get; set; }
    }
}
