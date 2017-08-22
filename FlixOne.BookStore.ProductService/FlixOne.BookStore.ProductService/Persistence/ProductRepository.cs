using FlixOne.BookStore.ProductService.Contexts;
using FlixOne.BookStore.ProductService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlixOne.BookStore.ProductService.Persistence
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _context;

        public ProductRepository(ProductContext context)
        {
            _context = context;
        }

        public void Add(Product Product)
        {
            _context.Add(Product);
            _context.SaveChanges();
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList();
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
