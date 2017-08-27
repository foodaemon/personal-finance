using System;
using System.Threading.Tasks;
using Finance.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Finance.Web.Controllers.Api
{
    [Route("api/v1/transactions")]
    public class TransactionApiController: Controller
    {
        readonly ITransactionService _transactionService;

        public TransactionApiController(ITransactionService transactionService) 
        {
            _transactionService = transactionService;
        }

        [HttpGet("year/{year}/month/{month}")]
        public async Task<IActionResult> GetByDate(int year = 0, int month = 0) 
        {
            if (year == 0)
                year = DateTime.Now.Year;
            if (month == 0)
                month = DateTime.Now.Month;
            var transactions = await _transactionService.GetTransactionByMonthAndYear(month, year);
            return Json(transactions);
        }
    } // class
} // namespace
