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

    public class TipoAmbienteBL : SPERepository<TipoAmbiente>
    {
        public TipoAmbienteBL(TransactionRepository transaction = null)
            : base(transaction)
        {

        }

        public void InserirTipoAmbiente(TipoAmbiente tipoAmbiente)
        {
            var existe = Get(e => string.Compare(e.Descr.ToLower(), tipoAmbiente.Descr.ToLower(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0);

            if (existe.Any())
                throw new CustomException("Erro ao cadastrar Tipo de Ambiente. Já existe um Tipo de Ambiente cadastrado com essa Descrição.");


            Add(tipoAmbiente);
        }

        public void AtualizarTipoAmbiente(TipoAmbiente tipoAmbiente)
        {
            var existe = Get(e => string.Compare(e.Descr.ToLower(), tipoAmbiente.Descr.ToLower(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0
                                                   && e.IdTipoAmbiente != tipoAmbiente.IdTipoAmbiente);

            if (existe.Any())
                throw new CustomException("Erro ao editar Tipo de Ambiente. Já existe um Tipo de Ambiente cadastrado com essa Descrição.");

            //Pegar o recurso original
            var tipoOriginal = Get(e => e.IdTipoAmbiente == tipoAmbiente.IdTipoAmbiente, null, "Ambiente,Componente").SingleOrDefault();
            tipoOriginal.Descr = tipoAmbiente.Descr;

            Update(tipoAmbiente);
        }

        public void ApagarTipoAmbiente(int id)
        {
            TipoAmbiente tipoAmbiente = Get(e => e.IdTipoAmbiente == id, null, "Ambiente,Componente").FirstOrDefault();

            if (tipoAmbiente.Ambiente.Any() || tipoAmbiente.Componente.Any())
                throw new CustomException("Erro ao excluir Tipo de Ambiente. Este Tipo de Ambiente encontra-se vinculado a outros membros.");

            Delete(tipoAmbiente);
        }

    }
}
