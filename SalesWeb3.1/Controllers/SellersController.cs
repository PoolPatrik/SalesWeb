using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWeb31.Models;
using SalesWeb31.Models.ViewModels;
using SalesWeb31.Services;

namespace SalesWeb31.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly Departmentervice _departmentervice;

        public SellersController(SellerService sellerService, Departmentervice departmentervice)
        {
            _sellerService = sellerService;
            _departmentervice = departmentervice;
        }
        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {
            var departements = _departmentervice.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departements };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }
    }
}
