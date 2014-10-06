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

    public class CRBL : SPERepository<CR>
    {
        public CRBL(TransactionRepository transaction)
            : base(transaction)
        {

        }

        public CRBL()
        {
            // TODO: Complete member initialization
        }

        public IQueryable<ufnListarCR1_Result> ListarCR(int pageIndex, int numberRows = 1)
        {
            return Context.ufnListarCR1(pageIndex, numberRows);
        }

        public void InserirCR(CR cr)
        {
            //areaAtuacao.DataInclusao = DateTime.Now.AddHours(-3);
            //perfil.UsuarioInclusaoId = this.UsuarioLogado.Id;
            //perfil.Ativo = true;
            var existe = this.Get(e => string.Compare(e.Nome.ToLower(), cr.Nome.ToLower(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0
                                                    || string.Compare(e.Codigo.ToLower(), cr.Codigo.ToLower(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0);

            if (existe.Any())
                throw new CustomException("Erro ao cadastrar CR. Já existe um CR cadastrado com esse Nome ou Codigo.");

            this.Add(cr);
        }

        public void AtualizarCR(CR cr)
        {
            //perfil.DataAlteracao = DateTime.Now.AddHours(-3);
            //perfil.UsuarioAlteracaoId = this.UsuarioLogado.Id;
            var existe = this.Get(e => (string.Compare(e.Nome.ToLower(), cr.Nome.ToLower(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0
                                                    || string.Compare(e.Codigo.ToLower(), cr.Codigo.ToLower(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0)
                                                    && (e.IdCR != cr.IdCR));

            if (existe.Any())
                throw new CustomException("Erro ao atualizar CR. Já existe um CR cadastrado com esse Nome ou Codigo.");

            //CR do banco para atualizar
            //var crUpdate = this.GetById(cr.IdCR);
            var crUpdate = this.Get(e => e.IdCR == cr.IdCR, null, "Turma").SingleOrDefault();

            //if (cr.Turma.Any())
            //    throw new CustomException("Erro ao atualizar CR. Já existem turmas criadas para esta CR.");


            crUpdate.IdModalidade = cr.IdModalidade;
            crUpdate.Nome = cr.Nome;
            crUpdate.Codigo = cr.Codigo;

            this.Update(crUpdate);
        }

        public void DeletarCR(int id)
        {
            CR cr = GetById(id);
            if (cr.Turma.Any())
            {
                throw new CustomException("Erro ao excluir CR. Já existem turmas criadas com esta CR.");
            }

            this.Delete(cr);
        }

    }
}
