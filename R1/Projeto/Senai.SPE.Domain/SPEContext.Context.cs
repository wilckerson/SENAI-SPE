﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class SPEContext : DbContext
    {
        public SPEContext()
            : base("name=SPEContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<AgendaAmbiente> AgendaAmbiente { get; set; }
        public DbSet<AgendaComponente> AgendaComponente { get; set; }
        public DbSet<AgendaDocente> AgendaDocente { get; set; }
        public DbSet<AgendaEvento> AgendaEvento { get; set; }
        public DbSet<Agendamento> Agendamento { get; set; }
        public DbSet<Ambiente> Ambiente { get; set; }
        public DbSet<AreaAtuacao> AreaAtuacao { get; set; }
        public DbSet<CalendarioCivil> CalendarioCivil { get; set; }
        public DbSet<CalendarioLegenda> CalendarioLegenda { get; set; }
        public DbSet<CatMaterial> CatMaterial { get; set; }
        public DbSet<CatServico> CatServico { get; set; }
        public DbSet<CatUsuario> CatUsuario { get; set; }
        public DbSet<CBO> CBO { get; set; }
        public DbSet<Componente> Componente { get; set; }
        public DbSet<CR> CR { get; set; }
        public DbSet<Docente> Docente { get; set; }
        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<Funcionalidade> Funcionalidade { get; set; }
        public DbSet<LocalAmbiente> LocalAmbiente { get; set; }
        public DbSet<Material> Material { get; set; }
        public DbSet<Matriz> Matriz { get; set; }
        public DbSet<Modalidade> Modalidade { get; set; }
        public DbSet<Modulo> Modulo { get; set; }
        public DbSet<Perfil> Perfil { get; set; }
        public DbSet<Recurso> Recurso { get; set; }
        public DbSet<RecursoAmbiente> RecursoAmbiente { get; set; }
        public DbSet<ReprovacaoMatriz> ReprovacaoMatriz { get; set; }
        public DbSet<ReprovacaoTurma> ReprovacaoTurma { get; set; }
        public DbSet<Servico> Servico { get; set; }
        public DbSet<TipoAmbiente> TipoAmbiente { get; set; }
        public DbSet<TipoAtividade> TipoAtividade { get; set; }
        public DbSet<TipoContrato> TipoContrato { get; set; }
        public DbSet<TipoRecurso> TipoRecurso { get; set; }
        public DbSet<TipoUsuario> TipoUsuario { get; set; }
        public DbSet<Turma> Turma { get; set; }
        public DbSet<Turno> Turno { get; set; }
        public DbSet<UnCurricular> UnCurricular { get; set; }
        public DbSet<Unidade> Unidade { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
    
        [DbFunction("SPEContext", "Split")]
        public virtual IQueryable<Split_Result> Split(string delimiter, string list)
        {
            var delimiterParameter = delimiter != null ?
                new ObjectParameter("Delimiter", delimiter) :
                new ObjectParameter("Delimiter", typeof(string));
    
            var listParameter = list != null ?
                new ObjectParameter("List", list) :
                new ObjectParameter("List", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<Split_Result>("[SPEContext].[Split](@Delimiter, @List)", delimiterParameter, listParameter);
        }
    
        [DbFunction("SPEContext", "ufnListarCR")]
        public virtual IQueryable<ufnListarCR_Result> ufnListarCR(Nullable<int> pageIndex, Nullable<int> rowNumbers)
        {
            var pageIndexParameter = pageIndex.HasValue ?
                new ObjectParameter("PageIndex", pageIndex) :
                new ObjectParameter("PageIndex", typeof(int));
    
            var rowNumbersParameter = rowNumbers.HasValue ?
                new ObjectParameter("RowNumbers", rowNumbers) :
                new ObjectParameter("RowNumbers", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<ufnListarCR_Result>("[SPEContext].[ufnListarCR](@PageIndex, @RowNumbers)", pageIndexParameter, rowNumbersParameter);
        }
    
        [DbFunction("SPEContext", "ufnListarMatrizes")]
        public virtual IQueryable<ufnListarMatrizes_Result> ufnListarMatrizes(Nullable<int> idCodigo, string nome, Nullable<int> idModalidade, Nullable<int> idAreaAtuacao, Nullable<int> pageIndex, Nullable<int> pageNumber)
        {
            var idCodigoParameter = idCodigo.HasValue ?
                new ObjectParameter("IdCodigo", idCodigo) :
                new ObjectParameter("IdCodigo", typeof(int));
    
            var nomeParameter = nome != null ?
                new ObjectParameter("Nome", nome) :
                new ObjectParameter("Nome", typeof(string));
    
            var idModalidadeParameter = idModalidade.HasValue ?
                new ObjectParameter("IdModalidade", idModalidade) :
                new ObjectParameter("IdModalidade", typeof(int));
    
            var idAreaAtuacaoParameter = idAreaAtuacao.HasValue ?
                new ObjectParameter("IdAreaAtuacao", idAreaAtuacao) :
                new ObjectParameter("IdAreaAtuacao", typeof(int));
    
            var pageIndexParameter = pageIndex.HasValue ?
                new ObjectParameter("PageIndex", pageIndex) :
                new ObjectParameter("PageIndex", typeof(int));
    
            var pageNumberParameter = pageNumber.HasValue ?
                new ObjectParameter("PageNumber", pageNumber) :
                new ObjectParameter("PageNumber", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<ufnListarMatrizes_Result>("[SPEContext].[ufnListarMatrizes](@IdCodigo, @Nome, @IdModalidade, @IdAreaAtuacao, @PageIndex, @PageNumber)", idCodigoParameter, nomeParameter, idModalidadeParameter, idAreaAtuacaoParameter, pageIndexParameter, pageNumberParameter);
        }
    
        [DbFunction("SPEContext", "ufnListarComponentesPorModulo")]
        public virtual IQueryable<ufnListarComponentesPorModulo_Result> ufnListarComponentesPorModulo(string idModudo, string filtro, Nullable<int> pageIndex, Nullable<int> pageNumber)
        {
            var idModudoParameter = idModudo != null ?
                new ObjectParameter("IdModudo", idModudo) :
                new ObjectParameter("IdModudo", typeof(string));
    
            var filtroParameter = filtro != null ?
                new ObjectParameter("Filtro", filtro) :
                new ObjectParameter("Filtro", typeof(string));
    
            var pageIndexParameter = pageIndex.HasValue ?
                new ObjectParameter("PageIndex", pageIndex) :
                new ObjectParameter("PageIndex", typeof(int));
    
            var pageNumberParameter = pageNumber.HasValue ?
                new ObjectParameter("PageNumber", pageNumber) :
                new ObjectParameter("PageNumber", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<ufnListarComponentesPorModulo_Result>("[SPEContext].[ufnListarComponentesPorModulo](@IdModudo, @Filtro, @PageIndex, @PageNumber)", idModudoParameter, filtroParameter, pageIndexParameter, pageNumberParameter);
        }
    
        [DbFunction("SPEContext", "ufnListarTurmas")]
        public virtual IQueryable<ufnListarTurmas_Result> ufnListarTurmas(Nullable<int> idCR, string nome, Nullable<int> codigo, Nullable<int> modalidadeId, Nullable<int> pageIndex, Nullable<int> pageNumber)
        {
            var idCRParameter = idCR.HasValue ?
                new ObjectParameter("IdCR", idCR) :
                new ObjectParameter("IdCR", typeof(int));
    
            var nomeParameter = nome != null ?
                new ObjectParameter("Nome", nome) :
                new ObjectParameter("Nome", typeof(string));
    
            var codigoParameter = codigo.HasValue ?
                new ObjectParameter("Codigo", codigo) :
                new ObjectParameter("Codigo", typeof(int));
    
            var modalidadeIdParameter = modalidadeId.HasValue ?
                new ObjectParameter("ModalidadeId", modalidadeId) :
                new ObjectParameter("ModalidadeId", typeof(int));
    
            var pageIndexParameter = pageIndex.HasValue ?
                new ObjectParameter("PageIndex", pageIndex) :
                new ObjectParameter("PageIndex", typeof(int));
    
            var pageNumberParameter = pageNumber.HasValue ?
                new ObjectParameter("PageNumber", pageNumber) :
                new ObjectParameter("PageNumber", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<ufnListarTurmas_Result>("[SPEContext].[ufnListarTurmas](@IdCR, @Nome, @Codigo, @ModalidadeId, @PageIndex, @PageNumber)", idCRParameter, nomeParameter, codigoParameter, modalidadeIdParameter, pageIndexParameter, pageNumberParameter);
        }
    
        [DbFunction("SPEContext", "ufnListarAmbientes")]
        public virtual IQueryable<ufnListarAmbientes_Result> ufnListarAmbientes(Nullable<int> idTipoAmbiente, Nullable<int> pageIndex, Nullable<int> rowNumbers)
        {
            var idTipoAmbienteParameter = idTipoAmbiente.HasValue ?
                new ObjectParameter("IdTipoAmbiente", idTipoAmbiente) :
                new ObjectParameter("IdTipoAmbiente", typeof(int));
    
            var pageIndexParameter = pageIndex.HasValue ?
                new ObjectParameter("PageIndex", pageIndex) :
                new ObjectParameter("PageIndex", typeof(int));
    
            var rowNumbersParameter = rowNumbers.HasValue ?
                new ObjectParameter("RowNumbers", rowNumbers) :
                new ObjectParameter("RowNumbers", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<ufnListarAmbientes_Result>("[SPEContext].[ufnListarAmbientes](@IdTipoAmbiente, @PageIndex, @RowNumbers)", idTipoAmbienteParameter, pageIndexParameter, rowNumbersParameter);
        }
    
        [DbFunction("SPEContext", "ufnListarDocentes")]
        public virtual IQueryable<ufnListarDocentes_Result> ufnListarDocentes(Nullable<int> idTipoContrato, Nullable<int> pageIndex, Nullable<int> rowNumbers)
        {
            var idTipoContratoParameter = idTipoContrato.HasValue ?
                new ObjectParameter("IdTipoContrato", idTipoContrato) :
                new ObjectParameter("IdTipoContrato", typeof(int));
    
            var pageIndexParameter = pageIndex.HasValue ?
                new ObjectParameter("PageIndex", pageIndex) :
                new ObjectParameter("PageIndex", typeof(int));
    
            var rowNumbersParameter = rowNumbers.HasValue ?
                new ObjectParameter("RowNumbers", rowNumbers) :
                new ObjectParameter("RowNumbers", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<ufnListarDocentes_Result>("[SPEContext].[ufnListarDocentes](@IdTipoContrato, @PageIndex, @RowNumbers)", idTipoContratoParameter, pageIndexParameter, rowNumbersParameter);
        }
    
        [DbFunction("SPEContext", "ufnPegarMinMaxDatas")]
        public virtual IQueryable<ufnPegarMinMaxDatas_Result> ufnPegarMinMaxDatas(Nullable<int> idTurma)
        {
            var idTurmaParameter = idTurma.HasValue ?
                new ObjectParameter("IdTurma", idTurma) :
                new ObjectParameter("IdTurma", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<ufnPegarMinMaxDatas_Result>("[SPEContext].[ufnPegarMinMaxDatas](@IdTurma)", idTurmaParameter);
        }
    
        public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagramdefinition_Result> sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagramdefinition_Result>("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagrams_Result> sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagrams_Result>("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    
        [DbFunction("SPEContext", "Split1")]
        public virtual IQueryable<Split1_Result> Split1(string delimiter, string list)
        {
            var delimiterParameter = delimiter != null ?
                new ObjectParameter("Delimiter", delimiter) :
                new ObjectParameter("Delimiter", typeof(string));
    
            var listParameter = list != null ?
                new ObjectParameter("List", list) :
                new ObjectParameter("List", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<Split1_Result>("[SPEContext].[Split1](@Delimiter, @List)", delimiterParameter, listParameter);
        }
    
        [DbFunction("SPEContext", "ufnListarAmbientes1")]
        public virtual IQueryable<ufnListarAmbientes1_Result> ufnListarAmbientes1(Nullable<int> idTipoAmbiente, Nullable<int> pageIndex, Nullable<int> rowNumbers)
        {
            var idTipoAmbienteParameter = idTipoAmbiente.HasValue ?
                new ObjectParameter("IdTipoAmbiente", idTipoAmbiente) :
                new ObjectParameter("IdTipoAmbiente", typeof(int));
    
            var pageIndexParameter = pageIndex.HasValue ?
                new ObjectParameter("PageIndex", pageIndex) :
                new ObjectParameter("PageIndex", typeof(int));
    
            var rowNumbersParameter = rowNumbers.HasValue ?
                new ObjectParameter("RowNumbers", rowNumbers) :
                new ObjectParameter("RowNumbers", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<ufnListarAmbientes1_Result>("[SPEContext].[ufnListarAmbientes1](@IdTipoAmbiente, @PageIndex, @RowNumbers)", idTipoAmbienteParameter, pageIndexParameter, rowNumbersParameter);
        }
    
        [DbFunction("SPEContext", "ufnListarComponentesPorModulo1")]
        public virtual IQueryable<ufnListarComponentesPorModulo1_Result> ufnListarComponentesPorModulo1(string idModudo, string filtro, Nullable<int> pageIndex, Nullable<int> pageNumber)
        {
            var idModudoParameter = idModudo != null ?
                new ObjectParameter("IdModudo", idModudo) :
                new ObjectParameter("IdModudo", typeof(string));
    
            var filtroParameter = filtro != null ?
                new ObjectParameter("Filtro", filtro) :
                new ObjectParameter("Filtro", typeof(string));
    
            var pageIndexParameter = pageIndex.HasValue ?
                new ObjectParameter("PageIndex", pageIndex) :
                new ObjectParameter("PageIndex", typeof(int));
    
            var pageNumberParameter = pageNumber.HasValue ?
                new ObjectParameter("PageNumber", pageNumber) :
                new ObjectParameter("PageNumber", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<ufnListarComponentesPorModulo1_Result>("[SPEContext].[ufnListarComponentesPorModulo1](@IdModudo, @Filtro, @PageIndex, @PageNumber)", idModudoParameter, filtroParameter, pageIndexParameter, pageNumberParameter);
        }
    
        [DbFunction("SPEContext", "ufnListarCR1")]
        public virtual IQueryable<ufnListarCR1_Result> ufnListarCR1(Nullable<int> pageIndex, Nullable<int> rowNumbers)
        {
            var pageIndexParameter = pageIndex.HasValue ?
                new ObjectParameter("PageIndex", pageIndex) :
                new ObjectParameter("PageIndex", typeof(int));
    
            var rowNumbersParameter = rowNumbers.HasValue ?
                new ObjectParameter("RowNumbers", rowNumbers) :
                new ObjectParameter("RowNumbers", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<ufnListarCR1_Result>("[SPEContext].[ufnListarCR1](@PageIndex, @RowNumbers)", pageIndexParameter, rowNumbersParameter);
        }
    
        [DbFunction("SPEContext", "ufnListarDocentes1")]
        public virtual IQueryable<ufnListarDocentes1_Result> ufnListarDocentes1(Nullable<int> idTipoContrato, Nullable<int> pageIndex, Nullable<int> rowNumbers)
        {
            var idTipoContratoParameter = idTipoContrato.HasValue ?
                new ObjectParameter("IdTipoContrato", idTipoContrato) :
                new ObjectParameter("IdTipoContrato", typeof(int));
    
            var pageIndexParameter = pageIndex.HasValue ?
                new ObjectParameter("PageIndex", pageIndex) :
                new ObjectParameter("PageIndex", typeof(int));
    
            var rowNumbersParameter = rowNumbers.HasValue ?
                new ObjectParameter("RowNumbers", rowNumbers) :
                new ObjectParameter("RowNumbers", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<ufnListarDocentes1_Result>("[SPEContext].[ufnListarDocentes1](@IdTipoContrato, @PageIndex, @RowNumbers)", idTipoContratoParameter, pageIndexParameter, rowNumbersParameter);
        }
    
        [DbFunction("SPEContext", "ufnListarMatrizes1")]
        public virtual IQueryable<ufnListarMatrizes1_Result> ufnListarMatrizes1(Nullable<int> idCodigo, string nome, Nullable<int> idModalidade, Nullable<int> idAreaAtuacao, Nullable<int> pageIndex, Nullable<int> pageNumber)
        {
            var idCodigoParameter = idCodigo.HasValue ?
                new ObjectParameter("IdCodigo", idCodigo) :
                new ObjectParameter("IdCodigo", typeof(int));
    
            var nomeParameter = nome != null ?
                new ObjectParameter("Nome", nome) :
                new ObjectParameter("Nome", typeof(string));
    
            var idModalidadeParameter = idModalidade.HasValue ?
                new ObjectParameter("IdModalidade", idModalidade) :
                new ObjectParameter("IdModalidade", typeof(int));
    
            var idAreaAtuacaoParameter = idAreaAtuacao.HasValue ?
                new ObjectParameter("IdAreaAtuacao", idAreaAtuacao) :
                new ObjectParameter("IdAreaAtuacao", typeof(int));
    
            var pageIndexParameter = pageIndex.HasValue ?
                new ObjectParameter("PageIndex", pageIndex) :
                new ObjectParameter("PageIndex", typeof(int));
    
            var pageNumberParameter = pageNumber.HasValue ?
                new ObjectParameter("PageNumber", pageNumber) :
                new ObjectParameter("PageNumber", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<ufnListarMatrizes1_Result>("[SPEContext].[ufnListarMatrizes1](@IdCodigo, @Nome, @IdModalidade, @IdAreaAtuacao, @PageIndex, @PageNumber)", idCodigoParameter, nomeParameter, idModalidadeParameter, idAreaAtuacaoParameter, pageIndexParameter, pageNumberParameter);
        }
    
        [DbFunction("SPEContext", "ufnListarTurmas1")]
        public virtual IQueryable<ufnListarTurmas1_Result> ufnListarTurmas1(Nullable<int> idCR, string nome, Nullable<int> codigo, Nullable<int> modalidadeId, Nullable<int> pageIndex, Nullable<int> pageNumber)
        {
            var idCRParameter = idCR.HasValue ?
                new ObjectParameter("IdCR", idCR) :
                new ObjectParameter("IdCR", typeof(int));
    
            var nomeParameter = nome != null ?
                new ObjectParameter("Nome", nome) :
                new ObjectParameter("Nome", typeof(string));
    
            var codigoParameter = codigo.HasValue ?
                new ObjectParameter("Codigo", codigo) :
                new ObjectParameter("Codigo", typeof(int));
    
            var modalidadeIdParameter = modalidadeId.HasValue ?
                new ObjectParameter("ModalidadeId", modalidadeId) :
                new ObjectParameter("ModalidadeId", typeof(int));
    
            var pageIndexParameter = pageIndex.HasValue ?
                new ObjectParameter("PageIndex", pageIndex) :
                new ObjectParameter("PageIndex", typeof(int));
    
            var pageNumberParameter = pageNumber.HasValue ?
                new ObjectParameter("PageNumber", pageNumber) :
                new ObjectParameter("PageNumber", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<ufnListarTurmas1_Result>("[SPEContext].[ufnListarTurmas1](@IdCR, @Nome, @Codigo, @ModalidadeId, @PageIndex, @PageNumber)", idCRParameter, nomeParameter, codigoParameter, modalidadeIdParameter, pageIndexParameter, pageNumberParameter);
        }
    
        [DbFunction("SPEContext", "ufnPegarMinMaxDatas1")]
        public virtual IQueryable<ufnPegarMinMaxDatas1_Result> ufnPegarMinMaxDatas1(Nullable<int> idTurma)
        {
            var idTurmaParameter = idTurma.HasValue ?
                new ObjectParameter("IdTurma", idTurma) :
                new ObjectParameter("IdTurma", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<ufnPegarMinMaxDatas1_Result>("[SPEContext].[ufnPegarMinMaxDatas1](@IdTurma)", idTurmaParameter);
        }
    }
}