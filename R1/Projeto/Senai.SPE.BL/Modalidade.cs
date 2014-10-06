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

    public class ModalidadeBL : SPERepository<Modalidade>
    {
        public ModalidadeBL(TransactionRepository transaction = null)
            : base(transaction)
        {

        }

        public void InserirModalidade(Modalidade modalidade)
        {
            var existe = Get(e => string.Compare(e.Nome.ToLower(), modalidade.Nome.ToLower(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0);

            if (existe.Any())
                throw new CustomException("Erro ao cadastrar Modalidade. Já existe uma Modalidade cadastrada com esse Nome.");


            Add(modalidade);
        }

        public void AtualizarModalidade(Modalidade modalidade)
        {
            var existe = Get(e => string.Compare(e.Nome.ToLower(), modalidade.Nome.ToLower(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0
                                                                        && e.IdModalidade != modalidade.IdModalidade);

            if (existe.Any())
                throw new CustomException("Erro ao editar Modalidade. Já existe uma Modalidade cadastrada com esse Nome.");


            Update(modalidade);
        }

        public void ApagarModalidade(int id)
        {
            Modalidade modalidade = Get(e => e.IdModalidade == id, null, "CR,Matriz,Unidade").SingleOrDefault();

            if (modalidade.CR.Any() || modalidade.Matriz.Any() || modalidade.Unidade.Any())
                throw new CustomException("Erro ao excluir Modalidade. Já existem associaçãos para esta Modalidade.");


            Delete(modalidade);
        }

    }
}
