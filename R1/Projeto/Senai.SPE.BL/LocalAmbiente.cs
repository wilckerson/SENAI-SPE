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

    public class LocalAmbienteBL : SPERepository<LocalAmbiente>
    {
        public LocalAmbienteBL(TransactionRepository transaction)
            : base(transaction)
        {

        }

        public void InserirLocalAmbiente(LocalAmbiente localAmbiente)
        {
            var existe = Get(e => string.Compare(e.Descr.ToLower(), localAmbiente.Descr.ToLower(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0);

            if (existe.Any())
                throw new CustomException("Erro ao cadastrar Localidade. Já existe uma Localidade cadastrada com essa Descrição.");


            Add(localAmbiente);
        }

        public void AtualizarLocalAmbiente(LocalAmbiente localAmbiente)
        {
            var existe = Get(e => string.Compare(e.Descr.ToLower(), localAmbiente.Descr.ToLower(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0
                                                    && e.IdLoc != localAmbiente.IdLoc);

            if (existe.Any())
                throw new CustomException("Erro ao editar Localidade. Já existe uma Localidade cadastrada com essa Descrição.");

            //Pegar o local original
            var localOriginal = Get(e => e.IdLoc == localAmbiente.IdLoc, null, "Ambiente").SingleOrDefault();
            localOriginal.Descr = localAmbiente.Descr;

            Update(localAmbiente);
        }

        public void DeletarLocalAmbiente(int id)
        {
            LocalAmbiente localAmbiente = Get(a => a.IdLoc == id, null, "Ambiente").FirstOrDefault();
            if (localAmbiente.Ambiente.Any())
                throw new CustomException("Erro ao excluir Localidade. Esta Localidade já encontra-se vinculada a um Ambiente.");

            Delete(localAmbiente);
        }

    }
}
