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

    public class CBOBL : SPERepository<CBO>
    {
        public CBOBL(TransactionRepository transaction)
            : base(transaction)
        {

        }

        public void InserirCBO(CBO CBO)
        {
            var existe = this.Get(e => string.Compare(e.Descrricao.ToLower(), CBO.Descrricao.ToLower(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0
                                                    || string.Compare(e.Codigo.ToLower(), CBO.Codigo.ToLower(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0);

            if (existe.Any())
                throw new CustomException("Erro ao cadastrar CBO. Já existe um CBO cadastrado com essa Descrição ou Codigo.");


            this.Add(CBO);
        }

        public void AtualizarCBO(CBO CBO)
        {
            var existe = this.Get(e => (string.Compare(e.Descrricao.ToLower(), CBO.Descrricao.ToLower(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0
                                                    || string.Compare(e.Codigo.ToLower(), CBO.Codigo.ToLower(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0)
                                                    && (e.IdCBO != CBO.IdCBO));

            if (existe.Any())
                throw new CustomException("Erro ao atualizar CBO. Já existe um CBO cadastrado com essa Descrição ou Codigo.");


            this.Update(CBO);
        }

        public void DeletarCBO(int id)
        {
            CBO cbo = this.Get(e => e.IdCBO == id, null, "Matriz").SingleOrDefault();

            if (cbo.Matriz.Any())
            {
                throw new CustomException("Erro ao excluir CBO. Já existem Matrizes criadas com este CBO.");
            }


            this.Delete(cbo);
        }
    }
}
