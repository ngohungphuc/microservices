using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingCart.ShoppingCart;

namespace ShoppingCart.Services
{
    public interface IProductCatalogueClient
    {
        Task<IEnumerable<ShoppingCartItem>> GetShoppingCartItems(int[] productCatalogueIds);
    }
}