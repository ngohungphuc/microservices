using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace ShoppingCart.ShoppingCart
{
    public class ShoppingCartStore : IShoppingCartStore
    {
        private string connectionString =
            @"Data Source=TONYHUDSON\SQLEXPRESS;Initial Catalog=ShoppingCart;
            Integrated Security=True";

        private const string readItemsSql =
                @"select * from ShoppingCart, ShoppingCartItems
                where ShoppingCartItems.ShoppingCartId = ID
                and ShoppingCart.UserId=@UserId";

        public async Task<ShoppingCart> Get(int userId)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var items = await conn.QueryAsync<ShoppingCartItem>(readItemsSql, new {UserId = userId});
                return new ShoppingCart(userId, items);
            }
        }

        public Task Save(ShoppingCart shoppingCart)
        {
            throw new NotImplementedException();
        }
    }
}
