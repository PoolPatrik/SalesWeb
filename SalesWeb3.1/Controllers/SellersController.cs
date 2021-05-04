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
        public async Task<IActionResult> Index()
        {
            var list = await _sellerService.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var departements = await _departmentervice.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departments = departements };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departements = await _departmentervice.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departements };
                return View(viewModel);
            }
            await _sellerService.InsertAsinc(seller);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id) //parametro opcional
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new {message = "Mensagem personalizada"});
            }
            var obj =await  _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new {message = "Mensagem personalizada"});
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id) //parametro opcional
        {
            try
            {
                await _sellerService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });

            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new {message = "Mensagem personalizada"});
            }
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new {message = "Mensagem personalizada"});
            }

            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new {message = "Mensagem personalizada"});
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new {message = "Mensagem personalizada"});
            }
            List<Department> departments =await  _departmentervice.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departements = await _departmentervice.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departements };
                return View(viewModel);
            }

            if (id != seller.Id)
            {

                return RedirectToAction(nameof(Error), new {message = "Mensagem personalizada BadRequest"});
            }

            try
            {
                await _sellerService.UpdateAsync(seller);
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