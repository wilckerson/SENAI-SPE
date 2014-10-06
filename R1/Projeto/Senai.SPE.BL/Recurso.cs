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

    public class RecursoBL : SPERepository<Recurso>
    {
        public RecursoBL(TransactionRepository transaction = null)
            : base(transaction)
        {

        }

        public void InserirRecurso(Recurso recurso)
        {
            var existe = Get(e => string.Compare(e.Descr.ToLower(), recurso.Descr.ToLower(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0
                                                    && e.idTipoRecurso == recurso.idTipoRecurso);

            if (existe.Any())
                throw new CustomException("Erro ao cadastrar Tipo de Recurso. Já existe um Tipo de Recurso cadastrado com essa Descrição.");

            Add(recurso);
        }

        public void AtualizarRecurso(Recurso recurso)
        {
            var existe = Get(e => string.Compare(e.Descr.ToLower(), recurso.Descr.ToLower(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0
                                                    && e.idTipoRecurso == recurso.idTipoRecurso
                                                    && e.idRecurso != recurso.idRecurso);

            if (existe.Any())
                throw new CustomException("Erro ao cadastrar Recurso. Já existe um Recurso cadastrado com essa Descrição.");

            //Pegar o recurso original
            var recursoOriginal = Get(e => e.idRecurso == recurso.idRecurso, null, "RecursoAmbiente").SingleOrDefault();

            //checar se ele tem RecursoAmbiente cadastrado
            //if (recurso.RecursoAmbiente.Any())
            //    throw new CustomException("Erro ao editar Recurso. Este Recurso já está associado a um Ambiente.");

            recursoOriginal.Descr = recurso.Descr;
            recursoOriginal.idTipoRecurso = recurso.idTipoRecurso;

            Update(recursoOriginal);
        }

        public void ApagarRecurso(int id)
        {
            Recurso recurso = Get(e => e.idRecurso == id, null, "RecursoAmbiente").SingleOrDefault();
            if (recurso.RecursoAmbiente.Any())
                throw new CustomException("Erro ao excluir Recurso. Este Recurso já está associado a um Ambiente.");

            Delete(recurso);
        }

    }
}
