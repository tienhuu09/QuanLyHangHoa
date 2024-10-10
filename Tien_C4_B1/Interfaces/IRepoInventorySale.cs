using System;
using System.Collections.Generic;

namespace Tien_C4_B1
{
    public interface IRepoInventorySale<T>
    {
        void Add(T entity);
        void AddProduct(Product entity);
        void Update(T entity);
        void Delete(T entity);
        T Get(string id);
        List<T> Gets();
    }
}
