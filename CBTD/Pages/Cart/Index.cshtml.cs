using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using Infrastructure;
using Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CBTD.Pages.Cart;

[Authorize]
public class IndexModel : PageModel
{
    private readonly UnitOfWork _unitOfWork;

    public ShoppingCartVM ShoppingCartVm { get; set; }

    public IndexModel(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult OnGet()
    {
        var claimsIdentity = User.Identity as ClaimsIdentity;

        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

        ShoppingCartVm = new ShoppingCartVM()
        {
            cartItems = _unitOfWork.ShoppingCartItem.GetAll(u => u.ApplicationUserId == claim.Value, u => u.ProductId, "Product"),
        };
        foreach (var item in ShoppingCartVm.cartItems)
        {
            item.CartPrice = GetPriceBasedOnQuantity(item);
            ShoppingCartVm.CartTotal += (item.CartPrice * item.Count);
        }

        return Page();
    }

    private double GetPriceBasedOnQuantity(double quantity, double unitPrice, double priceHalfDozen, double priceDozen)
    {
        if (quantity <= 5)
        {
            return unitPrice;
        }

        if (quantity <= 11)
        {
            return priceHalfDozen;
        }

        return priceDozen;
    }

    private double GetPriceBasedOnQuantity(ShoppingCartItem item)
    {
        return GetPriceBasedOnQuantity(item.Count, item.Product.UnitPrice, item.Product.HalfDozenPrice, item.Product.DozenPrice);
    }
    
    public IActionResult OnPostMinus(int id)
    {
        var cart = _unitOfWork.ShoppingCartItem.Get(c => c.Id == id);
        if (cart.Count == 1)
        {
            _unitOfWork.ShoppingCartItem.Delete(cart);
        }


        else
        {
            cart.Count -= 1;


            _unitOfWork.ShoppingCartItem.Update(cart);
        }
        _unitOfWork.Commit();


        var cnt = _unitOfWork.ShoppingCartItem.GetAll(u => u.ApplicationUserId == cart.ApplicationUserId).Count();
        //HttpContext.Session.SetInt32(SD.ShoppingCart, cnt);
        return RedirectToPage();
    }
    
    public IActionResult OnPostPlus(int id)
    {
        var cart = _unitOfWork.ShoppingCartItem.Get(c => c.Id == id);
        cart.Count += 1;

        _unitOfWork.ShoppingCartItem.Update(cart);

        _unitOfWork.Commit();

        return RedirectToPage();
    }
    
    public IActionResult OnPostRemove(int id)
    {
        var cart = _unitOfWork.ShoppingCartItem.Get(c => c.Id == id);
        _unitOfWork.ShoppingCartItem.Delete(cart);
        _unitOfWork.Commit();
        var cnt = _unitOfWork.ShoppingCartItem.GetAll(u => u.ApplicationUserId == cart.ApplicationUserId).Count();
        //HttpContext.Session.SetInt32(SD.ShoppingCart, cnt);
        return RedirectToPage();


    }
}