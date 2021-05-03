using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWeb31.Models
{
    public class Seller
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "{0} obrigatorio")]
        [StringLength(60, MinimumLength = 3, ErrorMessage ="{0} deve conter mais que {2} caracteres e menos que {1}")]
         public string Nome { get; set; }

        [Required(ErrorMessage = "{0} obrigatorio")]
        [EmailAddress(ErrorMessage = "Diegite um e-mail valido")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} obrigatorio")]
        [Display(Name = "Birth Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "{0} obrigatorio")]
        [Range(100.0, 50000.0, ErrorMessage = "{0} deve ser menor que {2} e maior que {1}")]
        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString ="{0:F2}")]
        public double BaseSalary { get; set; }
        public Department Department { get; set; }
        public int DepartmentId{ get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller()
        {

        }

        public Seller( string nome, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Nome = nome;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }

        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }
    }
}
