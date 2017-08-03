using LF.SysAdm.Domain.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LF.SysAdm.TEST.EntityTest
{
    [TestClass]
    public class CustomerTest
    {
        private Address _address;
        private Users _user;

        [TestInitialize]
        public void Instancia()
        {
            _address = new Address("Rua Teste",200,"Complemento","Bairro","Cidade","Estado","78005-210",null);
            _user = new Users("Teste User", "email@gmail.com", "123456");

        }
        [TestMethod]
        [TestCategory("Customer")]
        public void DeveRegistrarCustomer()
        {
            Customer _Customer = new Customer("285.568.118-92", new DateTime(1979, 09, 01), "(65)3052-6425", true, _user);
            _Customer.Rel_AdressList.Add(_address);

           Assert.AreEqual(true,_Customer.IsNotification());
        }

        [TestMethod]
        [TestCategory("Customer")]
        public void NaoDeveRegistrarCustomer()
        {
            Customer _Customer = new Customer("285.", new DateTime(1979, 09, 01), "(65)3052-6425", true, _user);
            _Customer.Rel_AdressList.Add(_address);

            Assert.AreEqual(false, _Customer.IsNotification());
        }

        [TestMethod]
        [TestCategory("Customer")]
        public void DeveAlterarCustomer()
        {
            Customer _Customer = new Customer("285.568.118-92", new DateTime(1979, 09, 01), "(65)3052-6425", true, _user);
            var temp = new
            {
                Document = _Customer.Document,
                DateBirthday = _Customer.DateBirthday,
                Phone = _Customer.Phone,
                DateOfChange = _Customer.DateOfChange,
                Gender = _Customer.Gender
            };
            _Customer.Rel_AdressList.Add(_address);
            _Customer.Edit("590.756.547-07", new DateTime(1980, 09, 10), "(11)5560-1730", false);
            
            Assert.AreEqual(true, _Customer.IsNotification());
            Assert.AreNotEqual(temp.Document, _Customer.Document);
            Assert.AreNotEqual(temp.DateBirthday, _Customer.DateBirthday);
            Assert.AreNotEqual(temp.Gender, _Customer.Gender);
            Assert.AreNotEqual(temp.Phone, _Customer.Phone);
            Assert.AreNotEqual(temp.DateOfChange, _Customer.DateOfChange);
        }

        [TestMethod]
        [TestCategory("Customer")]
        public void NaoDeveAlterarCustomer()
        {
            Customer _Customer = new Customer("285.568.118-92", new DateTime(1979, 09, 01), "(65)3052-6425", true, _user);
            var temp = new
            {
                Document = _Customer.Document,
                DateBirthday = _Customer.DateBirthday,
                Phone = _Customer.Phone,
                DateOfChange = _Customer.DateOfChange,
                Gender = _Customer.Gender
            };
            _Customer.Rel_AdressList.Add(_address);
            _Customer.Edit("590.", DateTime.Now, "(11)5560730", false);

            Assert.AreEqual(false, _Customer.IsNotification());
            Assert.AreNotEqual(temp.Document, _Customer.Document);
            Assert.AreNotEqual(temp.DateBirthday, _Customer.DateBirthday);
            Assert.AreNotEqual(temp.Gender, _Customer.Gender);
            Assert.AreNotEqual(temp.Phone, _Customer.Phone);
            Assert.AreNotEqual(temp.DateOfChange, _Customer.DateOfChange);
        }


    }
}
