using Infrastructure;

namespace CBTD.Pages.Manufacturer;

using DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Infrastructure.Models;

public class UpsertModel : PageModel
{
    private readonly UnitOfWork _unitOfWork;

    [BindProperty] //synchonizes form fields with values in code behind
    public Manufacturer? ObjCategory { get; set; }


    public UpsertModel(UnitOfWork unitOfWork) //dependency injection
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
        if (!ModelState.IsValid) return Page();

        //if this is a new category
        if (ObjCategory.Id == 0)
        {
            _unitOfWork.Manufacturer.Add(ObjCategory);
            TempData["success"] = "Manufacturer added Successfully";
        }
        //if category exists
        else
        {
            _unitOfWork.Manufacturer.Update(ObjCategory);
            TempData["success"] = "Manufacturer updated Successfully";
        }

        _unitOfWork.Commit();

        return RedirectToPage("./Index");
    }
}