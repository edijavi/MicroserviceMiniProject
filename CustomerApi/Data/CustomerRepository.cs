using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using SharedModels;
using System;

namespace CustomerApi.Data
{
    public class CustomerRepository : IRepository<Customer>
    {
        private readonly CustomerApiContext db;

        public CustomerRepository(CustomerApiContext context)
        {
            db = context;
        }

        Order IRepository<Customer>.Add(Customer entity)
        {
            if (entity.Date == null)
                entity.Date = DateTime.Now;
            
            var newCustomer = db.Customer.Add(entity).Entity;
            db.SaveChanges();
            return newCustomer;
        }

        void IRepository<Customer>.Edit(Customer entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }

        Order IRepository<Customer>.Get(int id)
        {
            return db.Customers.FirstOrDefault(o => o.Id == id);
        }

        IEnumerable<Customer> IRepository<Customer>.GetAll()
        {
            return db.Customers.ToList();
        }

        void IRepository<Customer>.Remove(int id)
        {
            var customer = db.Customers.FirstOrDefault(p => p.Id == id);
            db.Customers.Remove(customer);
            db.SaveChanges();
        }
    }
}
