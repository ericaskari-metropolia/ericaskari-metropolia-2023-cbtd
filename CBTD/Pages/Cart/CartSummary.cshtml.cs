using System.Security.Claims;
using CBTD.Pages.Cart;
using Infrastructure;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Utility;

namespace CBTD.Pages.CartSummary;

public class CartSummary : PageModel
{
    private readonly UnitOfWork _unitOfWork;
    
    public ShoppingCartVM? ShoppingCartVm { get; set; }

    public CartSummary(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public IActionResult OnGet()
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

        ShoppingCartVm = new ShoppingCartVM()
        {
            cartItems = _unitOfWork.ShoppingCartItem.GetAll(u => u.ApplicationUserId == claim.Value,
                includes: "Product"),
            OrderHeader = new OrderHeader()
        };
        ShoppingCartVm.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.Get(
            u => u.Id == claim.Value);

        ShoppingCartVm.OrderHeader.Name = ShoppingCartVm.OrderHeader.ApplicationUser.FullName;
        ShoppingCartVm.OrderHeader.PhoneNumber = ShoppingCartVm.OrderHeader.ApplicationUser.PhoneNumber;
        ShoppingCartVm.OrderHeader.StreetAddress = ShoppingCartVm.OrderHeader.ApplicationUser.StreetAddress;
        ShoppingCartVm.OrderHeader.City = ShoppingCartVm.OrderHeader.ApplicationUser.City;
        ShoppingCartVm.OrderHeader.State = ShoppingCartVm.OrderHeader.ApplicationUser.State;
        ShoppingCartVm.OrderHeader.PostalCode = ShoppingCartVm.OrderHeader.ApplicationUser.PostalCode;

        foreach (var cart in ShoppingCartVm.cartItems)
        {
            cart.CartPrice = Pages.Cart.IndexModel.GetPriceBasedOnQuantity(cart.Count, cart.Product.UnitPrice,
                cart.Product.HalfDozenPrice, cart.Product.DozenPrice);
            ShoppingCartVm.OrderHeader.OrderTotal += cart.CartPrice * cart.Count;

        }
        return Page();
    }


    public IActionResult OnPost()
    {
        var claimsIdentity = User.Identity as ClaimsIdentity;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

        //The Order is actually the shopping cart items, so we might as well use it

        if (ShoppingCartVm is null)
        {
            ShoppingCartVm = new ShoppingCartVM();
        }
        
        ShoppingCartVm.cartItems = _unitOfWork.ShoppingCartItem.GetAll(u => u.ApplicationUserId == claim.Value,
            includes: "Product");

        ShoppingCartVm.OrderHeader.OrderDate = System.DateTime.Now;
        ShoppingCartVm.OrderHeader.ApplicationUserId = claim.Value;

        foreach (var cart in ShoppingCartVm.cartItems)
        {
            cart.CartPrice = Pages.Cart.IndexModel.GetPriceBasedOnQuantity(cart.Count, cart.Product.UnitPrice,
                cart.Product.HalfDozenPrice, cart.Product.DozenPrice);
            ShoppingCartVm.OrderHeader.OrderTotal += cart.CartPrice * cart.Count;
        }

        ApplicationUser applicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == claim.Value);

        ShoppingCartVm.OrderHeader.PaymentStatus = OrderStatus.PaymentStatusPending;
        ShoppingCartVm.OrderHeader.OrderStatus = OrderStatus.StatusInProcess;

        _unitOfWork.OrderHeader.Add(ShoppingCartVm.OrderHeader);
        _unitOfWork.Commit();

        //Once the Order Header is created, we can start cycling through our Order Details

        foreach (var cart in ShoppingCartVm.cartItems)
        {
            OrderDetails orderDetail = new()
            {
                ProductId = cart.ProductId,
                OrderId = ShoppingCartVm.OrderHeader.Id,
                Price = cart.CartPrice,
                Count = cart.Count
            };
            _unitOfWork.OrderDetails.Add(orderDetail);
            _unitOfWork.Commit();
        }

        //don't forget to clear the physical shopping cart entries

        _unitOfWork.ShoppingCartItem.Delete(ShoppingCartVm.cartItems);
        _unitOfWork.Commit();
        return RedirectToPage("OrderConfirmation");
    }
}