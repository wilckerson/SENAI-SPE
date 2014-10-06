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
    
    public partial class Perfil
    {
        public Perfil()
        {
            this.Usuario = new HashSet<Usuario>();
            this.Funcionalidade = new HashSet<Funcionalidade>();
        }
    
        public int idPerfil { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> AprovarMatriz { get; set; }
        public Nullable<int> AprovarTurma { get; set; }
        public Nullable<int> ExpirarMatriz { get; set; }
    
        public virtual ICollection<Usuario> Usuario { get; set; }
        public virtual ICollection<Funcionalidade> Funcionalidade { get; set; }
    }
}
