using Infrastructure;

namespace CBTD.Pages.Categories;

using DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Infrastructure.Models;

public class UpsertModel : PageModel
{
    private readonly UnitOfWork _unitOfWork;

    [BindProperty] //synchonizes form fields with values in code behind
    public Category? Item { get; set; }


    public UpsertModel(UnitOfWork unitOfWork) //dependency injection
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult OnGet(int? id)
    {
        Item = new Category();

        //edit mode
        if (id != 0) Item = _unitOfWork.Category.GetById(id);

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
            _unitOfWork.Category.Add(Item);
            TempData["success"] = "Category added Successfully";
        }
        //if category exists
        else
        {
            _unitOfWork.Category.Update(Item);
            TempData["success"] = "Category updated Successfully";
        }

        _unitOfWork.Commit();

        return RedirectToPage("./Index");
    }
}