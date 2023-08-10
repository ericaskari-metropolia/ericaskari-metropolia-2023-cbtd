using Infrastructure;

namespace CBTD.Pages.Categories;

using DataAccess.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Infrastructure.Models;

public class IndexModel : PageModel
{
    private readonly UnitOfWork _unitOfWork;
    //our UI front end will support looping through and displaying Categories retrieved from the database and stored in a List
    public List<Category> ItemList; 

    public IndexModel(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        ItemList = new List<Category>();
    }

    public PageResult OnGet()
    {
        //There are five major sets of IActionResult Types the can be returned
        //1. Server Status Code Results
        //2. Server Status Code and Object Results
        //3. Redirect (to another webpage) Results
        //4. File Results
        //5. Content Results (like another Razor Web Page)
        ItemList = _unitOfWork.Category.GetAll().ToList();
        return Page();
    }
}