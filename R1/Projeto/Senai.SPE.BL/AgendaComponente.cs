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
   

    public class AgendaComponenteBL : SPERepository<AgendaComponente>
    {
        public AgendaComponenteBL(TransactionRepository transaction)
            : base(transaction)
        {

        }

        public List<AgendaComponente> GetComponenteAgendados(int IdModulo, int IdComponente, int idTurma)
        {
            List<AgendaComponente> item = new List<AgendaComponente>();

            var saf = Context.AgendaComponente.Include("Turma").Include("Turma.Turno").Where(a => a.IdModulo == IdModulo && a.IdComponente == IdComponente && a.IdTurma == idTurma);
            if (saf.Count() != 0)
            {
                return saf.ToList();
            }
            else
            {
                return item;
            }
        }

        public void AgendarComponente(AgendaComponente agenda)
        {
            this.Context.AgendaComponente.Add(agenda);
            this.Context.SaveChanges();
            this.Context.Dispose();
        }
    }
}
