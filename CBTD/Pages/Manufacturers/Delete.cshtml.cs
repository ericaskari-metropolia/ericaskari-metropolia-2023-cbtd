namespace CBTD.Pages.Manufacturer;

using DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Infrastructure.Models;

public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _db;

    [BindProperty] //synchonizes form fields with values in code behind
    public Manufacturer? ObjCategory { get; set; }

    public DeleteModel(ApplicationDbContext db) //dependency injection
    {
        _db = db;
    }

    public IActionResult OnGet(int? id)
    {
        ObjCategory = new Manufacturer();

        //edit mode
        if (id != 0) ObjCategory = _db.Manufacturer.Find(id);

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
            _db.Manufacturer.Remove(ObjCategory);
            TempData["success"] = "Manufacturer deleted Successfully";
        }

        _db.SaveChanges();

        return RedirectToPage("./Index");
    }
}