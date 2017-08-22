using FlixOne.BookStore.ProductService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlixOne.BookStore.ProductService.Persistence
{
    public interface IProductRepository
    {
        void Add(Product Product);
        IEnumerable<Product> GetAll();
        Product GetBy(Guid id);
        bool Remove(Guid id);
        void Update(Product Product);
    }
}
