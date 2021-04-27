using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWeb31.Models;
using SalesWeb31.Models.ViewModels;
using SalesWeb31.Services;
using SalesWeb31.Services.exception;

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

        public IActionResult Delete(int? id) //parametro opcional
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new {message = "Mensagem personalizada"});
            }
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new {message = "Mensagem personalizada"});
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id) //parametro opcional
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new {message = "Mensagem personalizada"});
            }
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new {message = "Mensagem personalizada"});
            }

            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new {message = "Mensagem personalizada"});
            }

            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new {message = "Mensagem personalizada"});
            }
            List<Department> departments = _departmentervice.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if (id != seller.Id)
            {

                return RedirectToAction(nameof(Error), new {message = "Mensagem personalizada BadRequest"});
            }

            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new {message = e.Message});
            }
            catch(DbConcurrencyException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}