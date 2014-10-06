using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;

using System.Transactions;

namespace Repository
{
    public class TransactionRepository
    {

        private TransactionScope Scope { get; set; }
        private List<DbContext> Contexts { get; set; }

        public void AddContext(DbContext context)
        {
            Contexts.Add(context);
        }

        public bool HasTransaction
        {
            get
            {
                return Scope != null;
            }
        }

        public void BeginTransaction(System.Transactions.IsolationLevel isolationLevel = System.Transactions.IsolationLevel.ReadCommitted)
        {
            Contexts = new List<DbContext>();

            Scope = new TransactionScope(
                // Novas transações são sempre recriadas
                TransactionScopeOption.RequiresNew,
                // dados poderão ler os dados não submetidos
                new TransactionOptions()
                {
                    IsolationLevel = isolationLevel
                }
            );
        }

        public void BeginTransaction()
        {
            Contexts = new List<DbContext>();

            Scope = new TransactionScope(
                // Novas transações são sempre recriadas
                TransactionScopeOption.RequiresNew,
                // dados poderão ler os dados não submetidos
                new TransactionOptions()
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                }
            );
        }

        public bool CommitTransaction()
        {
            try
            {
                using (Scope)
                {
                    foreach (var Context in Contexts)
                    {
                        try
                        {
                            Context.SaveChanges();
                        }
                        catch (Exception)
                        {
                            Scope = null;
                            break;
                        }

                    }
                    Scope.Complete();
                    Scope = null;
                }

                return true;
            }
            catch (Exception ex)
            {
                Scope = null;
                return false;
            }
        }
    }
}
