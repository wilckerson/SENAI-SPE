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

    public class TipoContratoBL : SPERepository<TipoContrato>
    {
        public TipoContratoBL(TransactionRepository transaction)
            : base(transaction)
        {

        }

        public TipoContrato Inserir(TipoContrato model)
        {
            var existe = Get(e => string.Compare(e.Descr.ToLower(), model.Descr.ToLower(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0 );

            if (existe.Any())
                throw new CustomException("Erro ao cadastrar Tipo de Contrato. Já existe um Tipo de Contrato cadastrado com esse Nome.");
            if (model.Descr.Length > 250)
                throw new CustomException("Erro ao cadastrar Tipo de Contrato. Campo Descrição tem mais de 250 caracteres.");
            return Add(model);
        }

        public void Atualizar(TipoContrato model)
        {
            var existe = Get(e => string.Compare(e.Descr.ToLower(), model.Descr.ToLower(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0 && e.IdTipoContrato != model.IdTipoContrato);

            if (existe.Any())
                throw new CustomException("Erro ao editar Tipo de Contrato. Já existe um Tipo de Contrato cadastrado com esse Nome.");
            if (model.Descr.Length> 250)
                throw new CustomException("Erro ao editar Tipo de Contrato. Campo Descrição tem mais de 250 caracteres.");
            

            Update(model);
        }

        public void Apagar(int id)
        {
            TipoContrato model = Get(a => a.IdTipoContrato == id, null, "Docente").SingleOrDefault();

            if (model.Docente.Any() )
                throw new CustomException("Erro ao excluir Tipo de Contrato. Existem docentes vinculados a esse tipo de contrato.");

            Delete(model);
        }
    }
}
