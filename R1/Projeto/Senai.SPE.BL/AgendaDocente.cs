using System.Data.Entity;
using System.Linq.Expressions;
using Repository;
using Senai.SPE.Domain;
using SPERepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BusinessLogic
{

    public class AgendaDocenteBL : SPERepository<AgendaDocente>
    {
        public AgendaDocenteBL(TransactionRepository transaction = null)
            : base(transaction)
        {

        }

        public List<AgendaDocente> GetDocentesAgendados(int IdModulo, int IdComponente, int idTurma)
        {
            return Context.AgendaDocente.Include("Docente").Where(a => a.IdModulo == IdModulo && a.IdComponente == IdComponente && a.IdTurma == idTurma).ToList();

        }

        public void AgendarDocente(AgendaDocente agenda)
        {
            this.Context.AgendaDocente.Add(agenda);
            this.Context.SaveChanges();
            this.Context.Dispose();
        }
    }
}
