using System.Collections.Generic;
using System.Threading.Tasks;
using Finance.Common.Entities;

namespace Finance.Core.Services
{
    public interface ITransactionService
    {
        /// <summary>
        /// Deletes the transaction.
        /// </summary>
        /// <param name="id">Identifier.</param>
        void DeleteTransaction(string id);

        IEnumerable<Transaction> GetLatestTransactions(int pageSize);
        
        /// <summary>
        /// Gets the transaction by identifier.
        /// </summary>
        /// <returns>The transaction by identifier.</returns>
        /// <param name="id">Identifier.</param>
        Task<Transaction> GetTransactionById(string id);
        
        /// <summary>
        /// Gets the transaction by month and year.
        /// </summary>
        /// <returns>The transaction by month and year.</returns>
        /// <param name="month">Month.</param>
        /// <param name="year">Year.</param>
        Task<IEnumerable<Transaction>> GetTransactionByMonthAndYear(int month, int year);
        
        /// <summary>
        /// Gets the transaction by year.
        /// </summary>
        /// <returns>The transaction by year.</returns>
        /// <param name="year">Year.</param>
        IEnumerable<Transaction> GetTransactionByYear(int year);
        
        /// <summary>
        /// Inserts the transaction.
        /// </summary>
        /// <param name="transaction">Transaction.</param>
        void InsertTransaction(Transaction transaction);
        
        /// <summary>
        /// Gets the total amount.
        /// </summary>
        /// <returns>The total amount.</returns>
        /// <param name="isIncome">If set to <c>true</c> is income.</param>
        /// <param name="transactions">Transactions.</param>
        double GetTotalAmount(bool isIncome, IEnumerable<Transaction> transactions);
        
        /// <summary>
        /// Gets the total expenses by category.
        /// </summary>
        /// <returns>The total expenses by category.</returns>
        /// <param name="transactions">Transactions.</param>
        Dictionary<string, double> GetTotalExpensesByCategory(IEnumerable<Transaction> transactions);
        
        Dictionary<int, double> GetYearlyTransactionsByMonth(bool isIncome = false);
        
        /// <summary>
        /// Updates the transaction.
        /// </summary>
        /// <param name="transaction">Transaction.</param>
        Task UpdateTransaction(Transaction transaction);
    } // class
}
