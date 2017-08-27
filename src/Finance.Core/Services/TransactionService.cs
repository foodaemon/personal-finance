using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Finance.Common.Helpers;
using Finance.Data.Repositories;
using Finance.Common.Entities;

namespace Finance.Core.Services
{
    public class TransactionService : ITransactionService
    {
        readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public void DeleteTransaction(string id)
        {
            var transaction = GetTransactionById(id);
            _transactionRepository.Delete(id);
        }

        public IEnumerable<Transaction> GetLatestTransactions(int pageSize)
        {
            var results = _transactionRepository.GetAll().OrderByDescending(x => x.Date).Take(pageSize);
            return results;
        }

        public double GetTotalAmount(bool isIncome, IEnumerable<Transaction> transactions)
        {
            double totalAmount = 0.0;
            if (transactions.Any())
                totalAmount = transactions
                    .Where(x => x.IsIncome == isIncome)
                    .Select(x => x.Amount)
                    .Sum();
            return totalAmount;
        }

        public Dictionary<string, double> GetTotalExpensesByCategory(IEnumerable<Transaction> transactions)
        {
            var expensesByCategory = transactions.Where(x => x.IsIncome == false)
                .GroupBy(x => new { x.CategoryId, x.Category.Name })
                .Select(g => new { Category = g.Key.Name, Amount = g.Sum(t => t.Amount) })
                .ToDictionary(x => x.Category, x => x.Amount);

            return expensesByCategory;
        }

        public async Task<Transaction> GetTransactionById(string id)
        {
            var transaction = await _transactionRepository.GetById(id);
            return transaction;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionByMonthAndYear(int month, int year)
        {
            var startDate = DateTimeHelper.StartOfMonth(year: year, month: month);
            var endDate = DateTimeHelper.EndOfMonth(year: year, month: month);

            var transactions = await _transactionRepository.GetByDate(startDate, endDate);
            return transactions.OrderByDescending(x => x.Date);
        }

        public IEnumerable<Transaction> GetTransactionByYear(int year)
        {
            var startDate = DateTimeHelper.StartOfMonth(year: year, month: 1);
            var endDate = DateTimeHelper.EndOfMonth(year: year, month: 12);
            var transactions = _transactionRepository.GetAll()
                .Where(x => x.Date >= startDate && x.Date <= endDate)
                .OrderByDescending(x => x.Date);
            return transactions.ToList();
        }

        public Dictionary<int, double> GetYearlyTransactionsByMonth(bool isIncome = false)
        {
            var monthData = new Dictionary<int, double>();

            // initialize dictionary
            for (var i = 1; i <= 12; i++)
                monthData.Add(i, 0.0);

            var transactions = GetTransactionByYear(DateTime.Now.Year);
            var data = transactions.Where(x => x.IsIncome == isIncome)
                .GroupBy(x => new { x.Date.Year, x.Date.Month })
                .ToDictionary(d => d.Key.Month, v => v.Sum(x => x.Amount));

            foreach (var kv in data)
            {
                monthData[kv.Key] = kv.Value;
            }

            return monthData;
        }

        public void InsertTransaction(Transaction transaction)
        {
            transaction.CreatedAt = DateTime.Now;
            transaction.UpdatedAt = DateTime.Now;
            _transactionRepository.Insert(transaction);
        }

        public async Task UpdateTransaction(Transaction transaction)
        {
            var data = await GetTransactionById(transaction.Id);
            if (data == null)
                return;

            transaction.CreatedAt = data.CreatedAt;
            transaction.UpdatedAt = DateTime.UtcNow;
            
            _transactionRepository.Update(transaction);
        }
    } // class
} // namespace
