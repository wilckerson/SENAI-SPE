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

    public class AgendamentoBL : SPERepository<Agendamento>
    {
        public AgendamentoBL(TransactionRepository transaction = null) : base(transaction) { }


        public bool Deletar(int id)
        {
            bool retorno = this.Delete(id);

            return retorno;
        }
    }
}
