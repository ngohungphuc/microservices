using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingCart.ShoppingCart;

namespace ShoppingCart
{
    public interface IProductCatalogueClient
    {
        Task<IEnumerable<ShoppingCartItem>> GetShoppingCartItems(int[] productCatalogueIds);
    }
}