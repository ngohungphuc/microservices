using System;
using System.Linq;
using Nancy;
using Nancy.ModelBinding;
using ShoppingCart.EventFeed;
using ShoppingCart.Services;
using ShoppingCart.ShoppingCart;

namespace ShoppingCart
{
    public class ShoppingCartModule : NancyModule
    {
        public ShoppingCartModule(
            IShoppingCartStore shoppingCartStore,
            IProductCatalogueClient productCatalogue,
            IEventStore eventStore)
            : base("/shoppingcart")
        {
            Get("/{userid:int}", parameters =>
            {
                var userId = (int)parameters.userid;
                return shoppingCartStore.Get(userId);
            });


            Post("/{userid:int}/items", async (parameters, _) =>
            {
                var productcatalogIds = this.Bind<int[]>();
                var userId = (int)parameters.userid;

                var shoppingCart = await shoppingCartStore.Get(userId);
                var shoppingCartItems =
                    await productCatalogue.GetShoppingCartItems(productcatalogIds).ConfigureAwait(false);
                shoppingCart.AddItems(shoppingCartItems, eventStore);
                await shoppingCartStore.Save(shoppingCart);
                return shoppingCart;
            });

            Delete("/{userid:int}/items", async (parameters, _) =>
            {
                var productCatalogIds = this.Bind<int[]>();
                var userId = (int)parameters.userid;
                var shoppingCart = await shoppingCartStore.Get(userId);
                shoppingCart.RemoveItems(productCatalogIds, eventStore);
                await shoppingCartStore.Save(shoppingCart);
                return shoppingCart;
            });
        }
    }
}
