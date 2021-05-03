using SalesWeb31.Data;
using SalesWeb31.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWeb31.Services
{
    public class Departmentervice
    {
        private readonly SalesWeb31Context _context;

        public Departmentervice(SalesWeb31Context context)
        {
            _context = context;
        }

        public async Task<List<Department>> FindAllAsync()
        {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
        }
    }
}
