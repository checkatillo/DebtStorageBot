using DebtStorageBot.DB.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebtStorageBot.DB.Services
{
    public class DebtsService : DatabaseService
    {
        private readonly DebtStorageContext _debtStorageContext;

        public DebtsService(DebtStorageContext debtStorageContext)
        {
            _debtStorageContext = debtStorageContext;
        }

        public void AddDebt(long creditor, long debtor, decimal amount, string creationDescription, IDbContextTransaction transaction = null)
        {
            RunInTransaction(_debtStorageContext, transaction, transaction =>
            {
                _debtStorageContext.DebtLogs.Add(new DebtLog()
                {
                    Amount = amount,
                    CreditorID = creditor,
                    DebtorID = debtor,
                    CreationDescription = creationDescription
                });

                var debtSum = _debtStorageContext.DebtSums.FirstOrDefault(t => t.DebtorID == debtor && t.CreditorID == creditor);
                if (debtSum == null)
                {
                    debtSum = new DebtSum()
                    {
                        DebtorID = debtor,
                        CreditorID = creditor,
                        Amount = 0.0M
                    };
                    _debtStorageContext.DebtSums.Add(debtSum);
                }
                debtSum.Amount += amount;

                _debtStorageContext.SaveChanges();

                return 0;
            });
        }

        public IEnumerable<DebtSum> GetDebtSumsAsCreditor(long creditor, int pageNum, int perPage)
        {
            return _debtStorageContext
                .Actors
                .FirstOrDefault(t => t.ActorID == creditor)
                ?.DebtSumAsCreditor
                .OrderByDescending(t => t.CreationDate)
                .Skip(perPage * pageNum)
                .Take(perPage)
                .ToList()
                ?? new List<DebtSum>();
        }

        public IEnumerable<DebtSum> GetDebtSumsAsDebtor(long debtor, int pageNum, int perPage)
        {
            return _debtStorageContext
                .Actors
                .FirstOrDefault(t => t.ActorID == debtor)
                ?.DebtSumAsDebtor
                .OrderByDescending(t => t.CreationDate)
                .Skip(perPage * pageNum)
                .Take(perPage)
                .ToList()
                ?? new List<DebtSum>();
        }

        public IEnumerable<DebtLog> GetDebtLogAsCreditor(long creditor, int pageNum, int perPage)
        {
            return _debtStorageContext
                .Actors
                .FirstOrDefault(t => t.ActorID == creditor)
                ?.DebtsAsCreditor
                .OrderByDescending(t => t.CreationDate)
                .Skip(perPage * pageNum)
                .Take(perPage)
                .ToList()
                ?? new List<DebtLog>();
        }

        public IEnumerable<DebtLog> GetDebtLogAsDebtor(long debtor, int pageNum, int perPage)
        {
            return _debtStorageContext
                .Actors
                .FirstOrDefault(t => t.ActorID == debtor)
                ?.DebtsAsDebtor
                .OrderByDescending(t => t.CreationDate)
                .Skip(perPage * pageNum)
                .Take(perPage)
                .ToList()
                ?? new List<DebtLog>();
        }
    }
}
