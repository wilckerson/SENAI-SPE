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

    public class AgendaAmbienteBL : SPERepository<AgendaAmbiente>
    {
        public AgendaAmbienteBL(TransactionRepository transaction = null)
            : base(transaction)
        {
           
        }

        public List<AgendaAmbiente> GetAmbientesAgendados(int IdModulo, int IdComponente, int idTurma)
        {
            return this.Context.AgendaAmbiente.Include("Ambiente").Include("Ambiente.TipoAmbiente").Where(a => a.IdModulo == IdModulo && a.IdComponente == IdComponente && a.IdTurma == idTurma).ToList();
        }

            public void AgendarAmbiente(AgendaAmbiente agenda)
            {
                this.Context.AgendaAmbiente.Add(agenda);
                this.Context.SaveChanges();
                this.Context.Dispose();
            }

    }
}
