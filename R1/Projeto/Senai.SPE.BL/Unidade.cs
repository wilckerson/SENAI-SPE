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

    public class UnidadeBL : SPERepository<Unidade>
    {
        public UnidadeBL(TransactionRepository transaction = null)
            : base(transaction)
        {

        }

        public void InserirUnidade(Unidade unidade)
        {
            var existe = Get(e => string.Compare(e.Descr.ToLower(), unidade.Descr.ToLower(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0
                                                    || e.Codigo == unidade.Codigo);

            if (existe.Any())
                throw new CustomException("Erro ao cadastrar Unidade. Já existe uma Unidade cadastrada com esse Nome ou Código.");

            Add(unidade);
        }

        public void AtualizarUnidade(Unidade unidade)
        {
            var existe = Get(e => (string.Compare(e.Descr.ToLower(), unidade.Descr.ToLower(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0
                                                    || e.Codigo == unidade.Codigo)
                                                    && e.IdUnidade != unidade.IdUnidade
                                                    );

            if (existe.Any())
                throw new CustomException("Erro ao cadastrar Unidade. Já existe uma Unidade cadastrada com esse Nome ou Código.");


            Update(unidade);
        }

        public void ApagarUnidade(int id)
        {
            Unidade unidade = Get(e => e.IdUnidade == id, null, "Turma").SingleOrDefault();

            if (unidade.Turma.Any())
            {
                throw new CustomException("Erro ao excluir Unidade. Esta Unidade encontra-se vinculada a uma Turma");
            }

            Delete(unidade);
        }
    }
}
