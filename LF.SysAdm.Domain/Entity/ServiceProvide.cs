using LF.SysAdm.Domain.Entity.Base;
using LF.SysAdm.Shared.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF.SysAdm.Domain.Entity
{
    public class ServiceProvide : BaseEntity
    {
        protected ServiceProvide(){ }

        public ServiceProvide(int tempo, decimal price, string description,Employee func)
        {
            Tempo = tempo;
            Price = price;
            DateRegister = DateTime.Now;
            Canceled = false;
            EmployeeId = func.ID;
            Rel_Employee = func;
            Description = description;
        }
        
        public int Tempo { get; private set; }
        public decimal Price { get; private set; }
        public DateTime DateRegister { get; private set; }
        public DateTime DateOfChanged { get; private set; }
        public bool Canceled { get; private set; }
        public string Description { get; private set; }

        public Guid EmployeeId { get; private set; } 
        public Employee Rel_Employee { get; private set; }
       

        public override void Register()
        {
            new ValidationContract<ServiceProvide>(this)
                .IsGreaterOrEqualsThan(x => x.Tempo, 5, "Tempo minimo para um Servico é de 5 min")
                .IsGreaterOrEqualsThan(x => x.Price, 0.0m, "Valor não pode ser negativ ")
                .HasMaxLenght(x => x.Description, 400, "Tamnho Maximo para campo descrção é de 400 char");
        }

        public void CancelState(bool status)
        {
            Canceled = status;
            DateOfChanged = DateTime.Now;
        }

    }
}
