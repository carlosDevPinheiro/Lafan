using LF.SysAdm.Data.Context;
using LF.SysAdm.Domain.Entity;
using LF.SysAdm.Domain.Querys.Address;
using LF.SysAdm.Domain.Querys.Customer;
using LF.SysAdm.Domain.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LF.SysAdm.Data.Repositorys.EF
{
    public class RepositoryCustomerEF : CRUDEF<Customer>, IRepositoryCustomer
    {
        private readonly DbContextEF _context;
        public RepositoryCustomerEF(IDbConnectionContext context) : base(context)
        {
            _context = (DbContextEF)context;
        }

        public CustomerQuery GetCustomer(Guid Id)
        {
            return _context.Set<Customer>().Where(c => c.ID.Equals(Id))
                .Select(c => new CustomerQuery
            {
                CustomerId = c.ID,
                DateBirthday = c.DateBirthday,
                DateOfChange = c.DateOfChange,
                DateRegister = c.DateRegister,
                Document = c.Document,
                Gender = c.Gender,
                Phone = c.Phone              
            }).FirstOrDefault();
        }

        public CustomerQuery GetCustomerCPF(string cpf)
        {
            return _context.Set<Customer>().Where(c => c.Document.Equals(cpf)).Select(c => new CustomerQuery
            {
                CustomerId = c.ID,
                DateBirthday = c.DateBirthday,
                DateOfChange = c.DateOfChange,
                DateRegister = c.DateRegister,
                Document = c.Document,
                Gender = c.Gender,
                Phone = c.Phone
            }).FirstOrDefault();
        }

        public CustomerWithAddressQuery GetCustomerWithAddress(Guid Id)
        {
            return (from c in _context.Set<Customer>()
                    join a in _context.Set<Address>() on c.ID equals a.Rel_Customer.ID
                    select new CustomerWithAddressQuery
                    {
                        CustomerId = c.ID,
                        DateBirthday = c.DateBirthday,
                        DateOfChange = c.DateOfChange,
                        DateRegister = c.DateRegister,
                        Document = c.Document,
                        Gender = c.Gender,
                        Phone = c.Phone,                       
                        ListAddress = new List<AddressQuery>
                                  {
                                      new AddressQuery
                                      {
                                           AddressId = a.ID,
                                            CEP = a.CEP,
                                             City = a.City,
                                              Complement = a.Complement,
                                               District = a.District,
                                                Number = a.Number,
                                                 State = a.State,
                                                  Street = a.Street
                                      }
                                  }
                    }).FirstOrDefault();
        }

        public CustomerWithUserQuery GetCutomerWithUser(Guid Id)
        {
            return (from c in _context.Set<Customer>()
                    join a in _context.Set<Users>() on c.UserId equals a.ID
                    select new CustomerWithUserQuery
                    {
                        CustomerId = c.ID,
                        UserId = c.UserId,
                        DateBirthday = c.DateBirthday,
                        DateOfChange = c.DateOfChange,
                        DateRegister = c.DateRegister,
                        Document = c.Document,
                        Email = a.Email,
                        Gender = c.Gender,
                        Name = a.Name,
                        Phone = c.Phone

                    }).FirstOrDefault();
        }
    }
}
