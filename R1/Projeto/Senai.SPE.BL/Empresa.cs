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

    public class EmpresaBL : SPERepository<Empresa>
    {
        public EmpresaBL(TransactionRepository transaction)
            : base(transaction)
        {

        }

        public Empresa Inserir(Empresa model)
        {
            var existe = Get(e => string.Compare(e.NomeFantasia.ToLower(), model.NomeFantasia.ToLower(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0);

            if (existe.Any())
                throw new CustomException("Erro ao cadastrar Empresa. Já existe uma Emrpesa cadastrada com esse Nome Fantasia.");

            return Add(model);
        }

        public void Atualizar(Empresa model)
        {
            var existe = Get(e => e.IdEmpresa != model.IdEmpresa && string.Compare(e.NomeFantasia.ToLower(), model.NomeFantasia.ToLower(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0);

            if (existe.Any())
                throw new CustomException("Erro ao cadastrar Empresa. Já existe uma Empresa cadastrada com esse Nome Fantasia.");

            Update(model);
        }

        public void Apagar(int id)
        {
            Empresa model = Get(a => a.IdEmpresa == id, null, "AgendaComponente").SingleOrDefault();

            if (model.Docente.Any() )
                throw new CustomException("Erro ao excluir Empresa. Esta Empresa já encontra-se vinculada a uma ou mais Agendas de Componente.");

            Delete(model);
        }
    }
}
