using System;
using System.Collections.Generic;

namespace Tien_C4_B1
{
    public interface IRepoCustomer<T>
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void RepairCard(Customer customer);
        void AddCustomerDetail(Customer customer, CustomerDetail customerDetail);
        T Get(string id);
        List<T> Gets();
    }
}
