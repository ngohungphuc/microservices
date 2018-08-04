using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nancy;
using Nancy.ModelBinding;

namespace ShoppingCart
{
    public class ShoppingCartModule: NancyModule
    {
        public ShoppingCartModule(
            IShoppingCartStore shoppingCartStore)
            : base("/shoppingcart")
        {
            Get("/{userid:int}", parameters =>
            {
                var userId = (int)parameters.userid;
                return shoppingCartStore.Get(userId);
            });

        }
    }

    public interface IShoppingCartStore
    {
        ShoppingCart Get(int userId);
        void Save(ShoppingCart shoppingCart);
    }

    public class ShoppingCart
    {
        public int UserId { get; }

        public ShoppingCart(int userId)
        {
            this.UserId = userId;
        }
    }
}
