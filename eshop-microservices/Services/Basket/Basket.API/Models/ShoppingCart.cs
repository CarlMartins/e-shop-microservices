using Marten.Schema;

namespace Basket.API.Models;

public class ShoppingCart
{
    [Identity]
    public string UserName { get; set; } = string.Empty;
    public List<ShoppingCartItem> Items { get; set; } = [];
    public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);

    public ShoppingCart(string userName)
    {
        UserName = userName;
    }

    public ShoppingCart()
    { }
}