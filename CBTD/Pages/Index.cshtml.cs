using Infrastructure;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace CBTD.Pages;

using Microsoft.AspNetCore.Mvc.RazorPages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    public IEnumerable<Product> objProductList;
    public IEnumerable<Category> objCategoryList { get; set; }
    private readonly UnitOfWork _unitOfWork;

    public IndexModel(ILogger<IndexModel> logger, UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public IActionResult OnGet()
    {
        objCategoryList = _unitOfWork.Category.GetAll(null, c => c.DisplayOrder, null);
        objProductList = _unitOfWork.Product.GetAll(null, includes: "Category,Manufacturer");
        return Page();
    }
}