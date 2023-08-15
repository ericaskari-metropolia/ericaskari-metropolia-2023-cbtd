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

    public ShoppingCart objCart { get; set; }

    
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
    
}