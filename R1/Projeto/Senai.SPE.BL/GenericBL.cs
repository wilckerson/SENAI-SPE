using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senai.SPE.Domain;
using System.Linq.Expressions;
using Repository;
using System.Transactions;
using SPERepository;


namespace BusinessLogic
{
    public class GenericBL<TEntity>
        where TEntity : class, new()
    {

        private TransactionRepository transactionScope;

        public void SetTransactionScope(TransactionRepository transaction)
        {
            transactionScope = transaction;
            _generic = new SPERepository<TEntity>(transactionScope);
        }

        #region Properties

        private SPERepository<TEntity> _generic;
        public SPERepository<TEntity> Generic
        {
            get
            {
                return _generic ?? (_generic = new SPERepository<TEntity>(transactionScope));
            }
        }

        #endregion
    }
}