using Common;
using Repository;
using Senai.SPE.Domain;
using SPERepository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BusinessLogic
{

    public class CalendarioLegendaBL : SPERepository<CalendarioLegenda>
    {
        public CalendarioLegendaBL(TransactionRepository transaction = null)
            : base(transaction)
        {

        }

        public CalendarioLegenda Inserir(CalendarioLegenda model)
        {
            var existe = Get(e => string.Compare(e.Descricao.ToLower(), model.Descricao.ToLower(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0);

            if (existe.Any())
                throw new CustomException("Erro ao cadastrar Legenda. Já existe uma Legenda cadastrada com essa Descrição.");
            if (model.Descricao.Length > 250)
                throw new CustomException("Erro ao cadastrar Legenda. Campo Descrição tem mais de 250 caracteres.");
            return Add(model);
        }

        public void Atualizar(CalendarioLegenda model)
        {
            var existe = Get(e => string.Compare(e.Descricao.ToLower(), model.Descricao.ToLower(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0 && e.IdCalendario != model.IdCalendario);

            if (existe.Any())
                throw new CustomException("Erro ao editar Legenda. Já existe uma Legenda cadastrado com essa Descrição.");
            if (model.Descricao.Length > 250)
                throw new CustomException("Erro ao editar Legenda. Campo Descrição tem mais de 250 caracteres.");


            Update(model);
        }

        public void Apagar(int id)
        {
            CalendarioLegenda model = Get(a => a.IdCalendario == id, null, "CalendarioCivil").SingleOrDefault();

            if (model.CalendarioCivil.Any())
                throw new CustomException("Erro ao excluir Legenda. Esta Legenda já está associada a um Calendário Civil.");

            Delete(model);
        }
    }
}
