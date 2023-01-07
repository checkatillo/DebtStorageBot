using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebtStorageBot.DB.Services
{
    public class DatabaseService
    {
        public T RunInTransaction<T>(DbContext context, IDbContextTransaction transaction, Func<IDbContextTransaction, T> action)
        {
            bool transactionToClose = transaction == null;
            
            if (transactionToClose)
                transaction = context.Database.BeginTransaction();

            action(transaction);

            if (transactionToClose)
            {
                transaction.Commit();
                transaction.Dispose();
            }
        }
    }
}
