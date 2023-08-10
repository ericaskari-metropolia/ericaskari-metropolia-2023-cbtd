using Infrastructure;

namespace CBTD.Pages.Manufacturer;

using DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Infrastructure.Models;

public class DeleteModel : PageModel
{
    private readonly UnitOfWork _unitOfWork;

    [BindProperty] //synchonizes form fields with values in code behind
    public Manufacturer? ObjCategory { get; set; }

    public DeleteModel(UnitOfWork unitOfWork) //dependency injection
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult OnGet(int? id)
    {
        ObjCategory = new Manufacturer();

        //edit mode
        if (id != 0) ObjCategory = _unitOfWork.Manufacturer.GetById(id);

        //  Nullable because Upsert is used.
        if (ObjCategory == null) return NotFound();

        //create new mode
        return Page();
    }

    public IActionResult OnPost()
    {
        //if this is a new category
        if (ObjCategory!.Id != 0)
        {
            _unitOfWork.Manufacturer.Delete(ObjCategory);
            TempData["success"] = "Manufacturer deleted Successfully";
        }

        _unitOfWork.Commit();

        return RedirectToPage("./Index");
    }
}