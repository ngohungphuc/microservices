using System;
using System.Collections.Generic;
using System.Text;
using Nancy;

namespace ProductCatalog
{
    public class ProductsModule : NancyModule
    {
        public ProductsModule(ProductStore productStore) : base("/products")
        {
            Get("", _ =>
            {
                string productIdsString = this.Request.Query.productIds;
                var productIds = ParseProductIdsFromQueryString(productIdsString);
                var products = productStore.GetProductsByIds(productIds);

                return this.Negotiate.WithModel(products).WithHeader("cache-control", "max-age:86400");
            });
        }

        private IEnumerable<int>
            ParseProductIdsFromQueryString(string productIdsString)
        {
            
        }
    }
    public interface ProductStore
    {
        IEnumerable<ProductCatalogProduct> GetProductsByIds(IEnumerable<int> productIds);
    }

    public class ProductCatalogProduct
    {
        public ProductCatalogProduct(int productId, string productName, string description, Money price)
        {
            this.ProductId = productId.ToString();
            this.ProductName = productName;
            this.ProductDescription = description;
            this.Price = price;
        }
        public string ProductId { get; private set; }
        public string ProductName { get; private set; }
        public string ProductDescription { get; private set; }
        public Money Price { get; private set; }
    }

    public class Money { }
}
