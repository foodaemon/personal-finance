using System;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Finance.Web.Infrastructure;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Finance.Core.Services;
using Finance.Common.Entities;

namespace Finance.Web.Controllers
{
    [Authorize]
    public class UtilityController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly ITransactionService _transactionService;
        
        public UtilityController(ITransactionService transactionService, 
            ICategoryService categoryService) 
        {
            _categoryService = categoryService;
            _transactionService = transactionService;
        }

        [HttpGet]
        public IActionResult ImportFile() 
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ImportFile(IFormFile csvfile) 
        {
            var uploadedFile = csvfile;
            var fileName = Guid.NewGuid().ToString();
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", $"{fileName}.csv");
            
            using (var stream = new FileStream(path, FileMode.Create)) 
            {
                await csvfile.CopyToAsync(stream);
            }
            ParseCsv(path);

            return Redirect("/transaction");
        }

        private void ParseCsv(string path, char delimeter = ',', char? quoteChar = null) 
        {
            string defaultCategory = "598a8a315782d948c6f85331";
            using(var streamReader = new StreamReader(path))
            {
                string headerLine = streamReader.ReadLine();
                string line;
                string[] row;
                while((line = streamReader.ReadLine()) != null) 
                {
                    row = line.Split(delimeter);
                    var transaction = new Transaction() 
                    {  
                        Date = DateTime.Parse(row[0]),
                        Description = row[2].Replace("\"", ""),
                        Amount = Math.Abs(Double.Parse(row[4])),
                        IsIncome = Double.Parse(row[4]) > 0 ? true: false,
                        CategoryId = defaultCategory,
                    };
                    _transactionService.InsertTransaction(transaction);
                }
            }
            System.IO.File.Delete(path);
        }
    }
}