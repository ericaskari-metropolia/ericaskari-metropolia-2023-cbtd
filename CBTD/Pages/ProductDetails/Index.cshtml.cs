using System.Security.Claims;
using Infrastructure;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CBTD.Pages.ProductDetails;

public class IndexModel : PageModel
{
    private readonly UnitOfWork _unitOfWork;
    [BindProperty] public int txtCount { get; set; }

    public Product objProduct { get; set; }

    public ShoppingCartItem ObjCartItem { get; set; }


    public IndexModel(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult OnGet(int? productId)
    {
        //check to see if user logged on
        var claimsIdentity = User.Identity as ClaimsIdentity;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        TempData["UserLoggedIn"] = claim;
        objProduct = _unitOfWork.Product.Get(p => p.Id == productId, includes: "Category,Manufacturer");
        return Page();
    }

    public IActionResult OnPost(Product objProduct)
    {
        //check to see if we have a shopping cart and this item already for the user

        var claimsIdentity = User.Identity as ClaimsIdentity;

        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

        ShoppingCartItem cartItemFromDb = _unitOfWork.ShoppingCartItem.Get(
            u => u.ApplicationUserId == claim.Value && u.ProductId == objProduct.Id);


        if (cartItemFromDb == null)
        {
            ObjCartItem = new ShoppingCartItem();
            ObjCartItem.ApplicationUserId = claim.Value;
            ObjCartItem.ProductId = objProduct.Id;
            ObjCartItem.Count = txtCount;
            _unitOfWork.ShoppingCartItem.Add(ObjCartItem);
            _unitOfWork.Commit();
        }

        else
        {
            _unitOfWork.ShoppingCartItem.IncrementCount(cartItemFromDb, txtCount);
            _unitOfWork.ShoppingCartItem.Update(cartItemFromDb);
            _unitOfWork.Commit();
        }
        return Page();

        return RedirectToPage("Index");
    }
}