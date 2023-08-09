using DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CBTD.Pages.Categories;
using Infrastructure.Models;

public class UpsertModel : PageModel
{
    private readonly ApplicationDbContext _db;

    [BindProperty] //synchonizes form fields with values in code behind
    public Category? ObjCategory { get; set; }


    public UpsertModel(ApplicationDbContext db) //dependency injection
    {
        _db = db;
    }

    public IActionResult OnGet(int? id)
    {
        ObjCategory = new Category();

        //edit mode
        if (id != 0)
        {
            ObjCategory = _db.Category.Find(id);
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
            _db.Category.Add(ObjCategory);
            TempData["success"] = "Category added Successfully";
        }
        //if category exists
        else
        {
            _db.Category.Update(ObjCategory);
            TempData["success"] = "Category updated Successfully";
        }

        _db.SaveChanges();

        return RedirectToPage("./Index");
    }
}