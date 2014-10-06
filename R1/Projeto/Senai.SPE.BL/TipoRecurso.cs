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

    public class TipoRecursoBL : SPERepository<TipoRecurso>
    {
        public TipoRecursoBL(TransactionRepository transaction)
            : base(transaction)
        {

        }

        public void InserirTipoRecurso(TipoRecurso tipoRecurso)
        {
            var existe = Get(e => string.Compare(e.Descr.ToLower(), tipoRecurso.Descr.ToLower(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0);

            if (existe.Any())
                throw new CustomException("Erro ao cadastrar Tipo de Recurso. Já existe um Tipo de Recurso cadastrado com essa Descrição.");

            Add(tipoRecurso);
        }

        public void AtualizarTipoRecurso(TipoRecurso tipoRecurso)
        {
            var existe = Get(e => string.Compare(e.Descr.ToLower(), tipoRecurso.Descr.ToLower(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0
                                                    && e.idTipoRecurso != tipoRecurso.idTipoRecurso);

            if (existe.Any())
                throw new CustomException("Erro ao cadastrar Tipo de Recurso. Já existe um Tipo de Recurso cadastrado com essa Descrição.");


            Update(tipoRecurso);
        }

        public void DeletarTipoRecurso(int id)
        {
            TipoRecurso tipoRecurso = Get(a => a.idTipoRecurso == id, null, "Recurso").FirstOrDefault();
            if (tipoRecurso.Recurso.Any())
            {
                throw new CustomException("Erro ao excluir Tipo de Recurso. Este Tipo de Recurso encontra-se vinculado a um Recurso");
            }

            Delete(tipoRecurso);
        }
    }
}
