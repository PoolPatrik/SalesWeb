﻿using SalesWeb31.Controllers;
using SalesWeb31.Data;
using SalesWeb31.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWeb31.Services
{
    public class SellerService
    {
        private readonly SalesWeb31Context _context;

        public SellerService(SalesWeb31Context context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        public void Insert(Seller obj)
        {
            obj.Department = _context.Department.First();
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Seller FindById(int id)
        {
            return _context.Seller.FirstOrDefault(obj => obj.Id == id);
        }

        public void Remove(int id)
        {
            var obj= _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();

        }
    }
}
