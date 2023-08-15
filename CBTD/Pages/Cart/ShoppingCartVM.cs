using System.ComponentModel.DataAnnotations;
using Infrastructure.Models;

namespace CBTD.Pages.Cart;

public class ShoppingCartVM
{
    public Product Product { get; set; }
    
    [Range(1, 1000, ErrorMessage = "Must be between 1 and 1000")]
    public int Count { get; set; }

    public IEnumerable<ShoppingCartItem> cartItems { get; set; }
    
    public double CartTotal {get; set;}
}