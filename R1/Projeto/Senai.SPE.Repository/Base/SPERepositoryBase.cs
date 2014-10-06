using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senai.SPE.Domain;
using Repository;
using System.Transactions;

namespace SPERepository
{
    public class SPERepository<TEntity> : EntityRepository<TEntity, SPEContext>
        where TEntity : class, new()
    {
        public SPERepository(TransactionRepository transactionScope = null)
        {
            if (transactionScope != null)
            {
                base.transactionScope = transactionScope;
            }
        }
    }
}
