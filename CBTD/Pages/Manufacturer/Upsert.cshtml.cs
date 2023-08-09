using CBTD.Data;
using CBTD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CBTD.Pages.Manufacturer;

public class UpsertModel : PageModel
{
    private readonly ApplicationDbContext _db;

    [BindProperty] //synchonizes form fields with values in code behind
    public Models.Manufacturer? ObjCategory { get; set; }


    public UpsertModel(ApplicationDbContext db) //dependency injection
    {
        _db = db;
    }

    public IActionResult OnGet(int? id)
    {
        ObjCategory = new Models.Manufacturer();

        //edit mode
        if (id != 0)
        {
            ObjCategory = _db.Manufacturer.Find(id);
        }

        //  Nullable because Upsert is used.
        if (ObjCategory == null)
        {
            return NotFound();
        }

        //create new mode
        return Page();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        //if this is a new category
        if (ObjCategory.Id == 0)
        {
            _db.Manufacturer.Add(ObjCategory);
            TempData["success"] = "Manufacturer added Successfully";
        }
        //if category exists
        else
        {
            _db.Manufacturer.Update(ObjCategory);
            TempData["success"] = "Manufacturer updated Successfully";
        }

        _db.SaveChanges();

        return RedirectToPage("./Index");
    }
}