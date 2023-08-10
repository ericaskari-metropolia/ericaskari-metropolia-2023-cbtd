namespace CBTD.Pages.Manufacturer;

using Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Infrastructure.Models;

public class IndexModel : PageModel
{
    private readonly UnitOfWork _unitOfWork;
    //our UI front end will support looping through and displaying Categories retrieved from the database and stored in a List
    public List<Manufacturer> ItemList; 

    public IndexModel(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        ItemList = new List<Manufacturer>();
    }

    public PageResult OnGet()
    {
        ItemList = _unitOfWork.Manufacturer.GetAll().ToList();
        return Page();
    }
}