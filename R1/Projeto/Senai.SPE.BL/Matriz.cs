using Common;
using Repository;
using Senai.SPE.Domain;
using SPERepository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BusinessLogic
{

    public class MatrizBL : SPERepository<Matriz>
    {
        public MatrizBL(TransactionRepository transaction = null)
            : base(transaction)
        {

        }

        public Matriz InserirMatriz(Matriz matriz)
        {
            //areaAtuacao.DataInclusao = DateTime.Now.AddHours(-3);
            //perfil.UsuarioInclusaoId = this.UsuarioLogado.Id;
            //perfil.Ativo = true;
            var existe = Get(e => string.Compare(e.Nome.ToLower(), matriz.Nome.ToLower(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0 && e.CH == matriz.CH);

            if (existe.ToList().Count > 0)
                throw new CustomException("Erro ao cadastrar Matriz. Já existe uma Matriz cadastrada com esse Nome e CH.");


            return Add(matriz);
        }

        public void AtualizarMatriz(Matriz matriz)
        {
            //perfil.DataAlteracao = DateTime.Now.AddHours(-3);
            //perfil.UsuarioAlteracaoId = this.UsuarioLogado.Id;
            var existe2 = Get(e => string.Compare(e.Nome.ToLower(), matriz.Nome.ToLower(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0
                                                    && e.CH == matriz.CH).ToList();

            var existe = Get(e => string.Compare(e.Nome.ToLower(), matriz.Nome.ToLower(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0
                                                    && e.CH == matriz.CH
                                                    && e.IdMatriz != matriz.IdMatriz);

            if (existe.ToList().Count > 0)
                throw new CustomException("Erro ao cadastrar Matriz. Já existe uma Matriz cadastrada com esse Nome e CH.");


            Update(matriz);
        }

        public void AtualizarReprovacaoMatriz(Matriz matriz, DateTime date, string observ = "")
        {
            //perfil.DataAlteracao = DateTime.Now.AddHours(-3);
            //perfil.UsuarioAlteracaoId = this.UsuarioLogado.Id;
            var existe2 = Get(e => string.Compare(e.Nome.ToLower(), matriz.Nome.ToLower(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0
                                                    && e.CH == matriz.CH).ToList();

            var existe = Get(e => string.Compare(e.Nome.ToLower(), matriz.Nome.ToLower(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0
                                                    && e.CH == matriz.CH
                                                    && e.IdMatriz != matriz.IdMatriz);

            if (existe.ToList().Count > 0)
                throw new CustomException("Erro ao cadastrar Matriz. Já existe uma Matriz cadastrada com esse Nome e CH.");

            this.Update(matriz);
            this.Save();
            var ctx = new SPEContext();
            ctx.Configuration.LazyLoadingEnabled = false;
            ctx.Configuration.ProxyCreationEnabled = false;
            ReprovacaoMatriz RepMatriz = new ReprovacaoMatriz
            {
                IdMatriz = matriz.IdMatriz,
                Observacao = observ,
                Data = date,
                Tipo = matriz.Aprovado

            };
            ctx.ReprovacaoMatriz.Add(RepMatriz);
            ctx.SaveChanges();
            ctx.Dispose();
            Save();


            Update(matriz);
        }

        public void ApagarMatriz(int id)
        {
            Matriz matriz = Get(a => a.IdMatriz == id, null, "Turma, Modulo, Modulo.Componente,ReprovacaoMatriz").SingleOrDefault();

            if (matriz.Turma.Any() || matriz.Aprovado == 1 || matriz.ReprovacaoMatriz.Any())
                throw new CustomException("Erro ao excluir Matriz. Esta Matriz já está associada a uma Turma, Já está aprovada ou contém histórico de reprovações.");

            DeleteVinculoMatriz(matriz);
        }

        public void DeleteVinculoMatriz(Matriz matriz)
        {
            ModuloBL moduloBL = new ModuloBL();
            moduloBL.RemoverModulos(matriz.IdMatriz);

            //if (matriz.Turma.Any())
            //{
            //    throw new CustomException("Erro ao excluir Matriz. Esta Matriz já está associada a uma Turma, Ou contém histórico de aprovações e/ou reprovações.");
            //}
            //else if (matriz.Modulo.Any())
            //{
            //    var modulos = (from u in this.Context.Modulo
            //                   where u.IdMatriz == matriz.IdMatriz
            //                   select u).ToList();

            //    foreach (var modulo in modulos)
            //    {
            //        foreach (var componente in modulo.Componente.ToList())
            //        {
            //            modulo.Componente.Remove(componente);
            //        }
            //        this.Context.Modulo.Remove(modulo);
            //        this.Save();
            //    }

            //    this.Delete(matriz);
            //}
            matriz.Modulo = null;

            this.Delete(matriz);
        }
    }
}
