using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Finance.Core.Services;
using Finance.Common.Entities;
using Finance.Web.Infrastructure;
using Finance.Web.ViewModels;
using System.Threading.Tasks;

namespace Finance.Web.Controllers
{
    [Authorize]
    public class TransactionController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly ITransactionService _transactionService;

        public TransactionController(ICategoryService categoryService, 
            ITransactionService transactionService)
        {
            _categoryService = categoryService;
            _transactionService = transactionService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var categories = _categoryService.GetAll();
            var categorySelectList = categories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
            var viewModel = new TransactionViewModel();
            viewModel.CategorySelectList = categorySelectList.ToList();

            return View("Create", viewModel);
        }

        [HttpPost]
        public IActionResult Create(Transaction transaction)
        {
            transaction.CreatedBy = AccountId;
            transaction.GroupId = GroupId;
            _transactionService.InsertTransaction(transaction);
            return RedirectToAction("Index", new { year = transaction.Date.Year, month = transaction.Date.Month });
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            _transactionService.DeleteTransaction(id);
            return Redirect(Request.Headers["Referer"]);   // RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var transaction = await _transactionService.GetTransactionById(id: id);
            var categories = _categoryService.GetAll();

            var categorySelectList = categories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString(),
                Selected = (c.Id == transaction.CategoryId) ? true : false,
            });

            var viewModel = new TransactionViewModel()
            {
                Transaction = transaction,
                CategorySelectList = categorySelectList.ToList()
            };
            return View("Edit", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Transaction transaction)
        {
            _transactionService.UpdateTransaction(transaction);
            return RedirectToAction("Index", new { year = transaction.Date.Year, month = transaction.Date.Month });
        }

        [HttpGet]
        public async Task<IActionResult> ExpensesByCategory()
        {
            var month = DateTime.Now.Month;
            var year = DateTime.Now.Year;
            var transactions = await _transactionService.GetTransactionByMonthAndYear(month: month, year: year);
            var expenses = _transactionService.GetTotalExpensesByCategory(transactions);

            var jsonResult = new JsonResult(value: expenses);
            return jsonResult;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int year = 0, int month = 0)
        {
            if (year == 0)
                year = DateTime.Now.Year;
            if (month == 0)
                month = DateTime.Now.Month;
            var transactions = await _transactionService.GetTransactionByMonthAndYear(month, year);

            var viewModel = new TransactionListViewModel();
            viewModel.Transactions = transactions;
            viewModel.TotalIncome = _transactionService.GetTotalAmount(true, transactions);
            viewModel.TotalExpenses = _transactionService.GetTotalAmount(false, transactions);

            ViewBag.Date = month + "/" + year;
            return View("Index", viewModel);
        }

        [HttpGet]
        public IActionResult Latest()
        {
            var transactions = _transactionService.GetLatestTransactions(10);
            return new JsonResult(value: transactions);
        }

        [HttpGet]
        public IActionResult Yearly()
        {
            var yearlyIncome = _transactionService.GetYearlyTransactionsByMonth(isIncome: true);
            var yearlyExpense = _transactionService.GetYearlyTransactionsByMonth();
            var result = new { yearlyIncome = yearlyIncome, yearlyExpense = yearlyExpense };

            var jsonResult = new JsonResult(value: result);
            return jsonResult;
        }
    } // class
} // namespace
