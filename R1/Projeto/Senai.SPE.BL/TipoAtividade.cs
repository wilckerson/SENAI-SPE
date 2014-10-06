using Common;
using Repository;
using Senai.SPE.Domain;
using SPERepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senai.SPE.BL
{
    public class TipoAtividadeBL : SPERepository<TipoAtividade>
    {
        public TipoAtividadeBL(TransactionRepository transaction = null) : base(transaction) { }

        public void Apagar(int id)
        {
            TipoAtividade model = Get(a => a.IdTipoAtividade == id, null, "AgendaDocente").SingleOrDefault();

            if (model.AgendaDocente.Any())
                throw new CustomException("Erro ao excluir Tipo de Atividade. Este Tipo de Atividade já encontra-se vinculada a um ou mais Agenda(s)Docente.");

            Delete(model);
        }
    }
}
