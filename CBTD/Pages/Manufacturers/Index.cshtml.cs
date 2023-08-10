namespace CBTD.Pages.Manufacturer;

using Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Infrastructure.Models;

public class IndexModel : PageModel
{
    private readonly UnitOfWork _unitOfWork;
    //our UI front end will support looping through and displaying Categories retrieved from the database and stored in a List
    public List<Manufacturer> ObjCategoryList; 

    public IndexModel(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        ObjCategoryList = new List<Manufacturer>();
    }

    public PageResult OnGet()
    {
        ObjCategoryList = _unitOfWork.Manufacturer.GetAll().ToList();
        return Page();
    }
}