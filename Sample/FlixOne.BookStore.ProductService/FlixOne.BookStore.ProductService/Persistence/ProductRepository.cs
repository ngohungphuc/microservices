using FlixOne.BookStore.ProductService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlixOne.BookStore.ProductService.Persistence
{
    public class ProductRepository : IProductRepository
    {
        public void Add(Product Product)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Product> GetAll()
        {
            throw new NotImplementedException();
        }
        public Product GetBy(Guid id)
        {
            throw new NotImplementedException();
        }
        public bool Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(Product Product)
        {
            throw new NotImplementedException();
        }
    }
}
