using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Authentication.Models;
using Project.Clients.Models;
using Project.Helpers;
using Project.SoftwareSystems.Controllers;
using Project.SoftwareSystems.Models;
using Xunit;

namespace Project.Tests
{
    public class UnitTest1 : IDisposable
    {
        private readonly RevenueContext _context;

        public UnitTest1()
        {
            var options = new DbContextOptionsBuilder<RevenueContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            
            _context = new RevenueContext(options: options);
            
            _context.Individuals.Add(entity: new Individual
            {
                Id = 1,
                FirstName = "A",
                LastName = "K",
                Pesel = "12345678910",
                Address = " ",
                Email = "user@gmail.com",
                PhoneNumber = "1234567890"
            });
            _context.Companies.Add(entity: new Company
            {
                Id = 1,
                CompanyName = "A",
                Krs = "1234556789",
                Address = " ",
                Email = "company@gmail.com",
                PhoneNumber = "1234567890"
            });
            _context.SoftwareSystems.Add(entity: new SoftwareSystem
            {
                Id = 1,
                Name = null,
                Description = null,
                Version = null,
                Category = null
            });
            _context.Contracts.Add(entity: new Contract
            {
                Id = 1,
                SoftwareSystemId = 1,
                ClientId = 1,
                IsSigned = true,
                SoftwareVersion = null
            });
            _context.Discounts.Add(entity: new Discount { Id = 1 });
            _context.Payments.Add(entity: new Payment { Id = 1 });
            _context.Roles.Add(entity: new Role
            {
                Id = 1,
                Name = null
            });
            _context.Users.Add(entity: new User
            {
                Id = 1,
                Username = null,
                Password = null
            });

            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

       /* [Fact]
        public async Task HasActiveContract_NoActiveContract_ReturnsFalse()
        {
            var controller = new ContractController(_context);
            var result = controller.HasActiveContract(2, 2); // Non-existing contract
            
            Assert.False(result);
        }


        [Fact]
        public void ValidDuration_ValidDates()
        {
            var startDate = DateTime.Now;
            var endDate = startDate.AddDays(5);
            
            var result = ContractController.ValidDuration(startDate, endDate);
            
            Assert.True(result);
        }

        [Fact]
        public void ValidDuration_InvalidDates()
        {
            var startDate = DateTime.Now;
            var endDate = startDate.AddDays(1);
            
            var result = ContractController.ValidDuration(startDate, endDate);
            
            Assert.False(result);
        }

        [Fact]
        public void GetTotalPrice_CalculatesTotalPriceCorrectly()
        {
            var contract = new Contract
            {
                SoftwareSystemId = 1,
                ClientId = 1,
                SupportYears = 2,
                Price = 1000,
                SoftwareVersion = null 
            };
            
          //  var result = ContractController.GetTotalPrice(contract);
            
           // Assert.Equal(3000, result); 
        }
        */
    }
}

