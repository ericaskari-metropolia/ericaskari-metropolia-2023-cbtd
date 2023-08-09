namespace CBTD.Pages.Manufacturer;

using DataAccess.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Infrastructure.Models;

public class IndexModel : PageModel
{
    ApplicationDbContext _db;
    public List<Manufacturer> ObjCategoryList; //our UI front end will support looping through and displaying Categories retrieved from the database and stored in a List

    public  IndexModel(ApplicationDbContext db)
    {
        _db = db;
        ObjCategoryList = new List<Manufacturer>();

    }
    public PageResult OnGet()
    {
        ObjCategoryList = _db.Manufacturer.ToList();
        return Page();
    }
}