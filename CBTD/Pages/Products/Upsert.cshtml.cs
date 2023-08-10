using Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CBTD.Pages.Products;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Infrastructure.Models;

public class UpsertModel : PageModel
{
    private readonly UnitOfWork _unitOfWork;

    [BindProperty] //synchonizes form fields with values in code behind
    public Product? Item { get; set; }

    public IEnumerable<SelectListItem> CategoryList { get; set; }
    public IEnumerable<SelectListItem> ManufacturerList { get; set; }


    public UpsertModel(UnitOfWork unitOfWork) //dependency injection
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult OnGet(int? id)
    {
        Item = new Product();
        CategoryList = _unitOfWork.Category.GetAll().Select(Item => new SelectListItem
        {
            Text = Item.Name,
            Value = Item.Id.ToString()
        });
        
        ManufacturerList = _unitOfWork.Manufacturer.GetAll().Select(Item => new SelectListItem
        {
            Text = Item.Name,
            Value = Item.Id.ToString()
        });
        

        //edit mode
        if (id != 0) Item = _unitOfWork.Product.GetById(id);

        //  Nullable because Upsert is used.
        if (Item == null) return NotFound();

        //create new mode
        return Page();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid) return Page();

        //if this is a new category
        if (Item.Id == 0)
        {
            _unitOfWork.Product.Add(Item);
            TempData["success"] = "Product added Successfully";
        }
        //if category exists
        else
        {
            _unitOfWork.Product.Update(Item);
            TempData["success"] = "Product updated Successfully";
        }

        _unitOfWork.Commit();

        return RedirectToPage("./Index");
    }
}