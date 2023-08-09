using CBTD.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CBTD.Pages.Manufacturer;


public class IndexModel : PageModel
{
    ApplicationDbContext _db;
    public List<Models.Manufacturer> ObjCategoryList; //our UI front end will support looping through and displaying Categories retrieved from the database and stored in a List

    public  IndexModel(ApplicationDbContext db)
    {
        _db = db;
        ObjCategoryList = new List<Models.Manufacturer>();

    }
    public PageResult OnGet()
    {
        ObjCategoryList = _db.Manufacturer.ToList();
        return Page();
    }
}