using System.Text.Json;
using System.Text.Json.Serialization;
using Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CBTD.Pages.Products;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Infrastructure.Models;

public class UpsertModel : PageModel
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment;


    [BindProperty] //synchonizes form fields with values in code behind
    public Product? Item { get; set; }

    public IEnumerable<SelectListItem> CategoryList { get; set; }
    public IEnumerable<SelectListItem> ManufacturerList { get; set; }


    public UpsertModel(UnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment) //dependency injection
    {
        _unitOfWork = unitOfWork;
        _webHostEnvironment = webHostEnvironment;
    }

    public IActionResult OnGet(int? id)
    {
        FetchDropdowns();
        Console.WriteLine("Upsert Get");
        Item = new Product();
        CategoryList = _unitOfWork.Category.GetAll().Select(Item => new SelectListItem
        {
            Text = Item.Name,
            Value = Item.Id.ToString()
        });

        ManufacturerList = _unitOfWork.Manufacturer.GetAll().Select(Item => new SelectListItem
        {
            Text = Item.Name,
            Value = Item.Id.ToString()
        });


        //edit mode
        if (id != 0) Item = _unitOfWork.Product.GetById(id);

        //  Nullable because Upsert is used.
        if (Item == null) return NotFound();

        //create new mode
        return Page();
    }

    public IActionResult OnPost()
    {
        FetchDropdowns();

        Console.WriteLine("Product Upsert");
        string jsonString = JsonSerializer.Serialize(Item);
        Console.WriteLine(JsonSerializer.Serialize(ModelState.ErrorCount));
        Console.WriteLine(jsonString);
        Console.WriteLine(JsonSerializer.Serialize(ModelState));

        if (!ModelState.IsValid) return Page();

        string webRootPath = _webHostEnvironment.WebRootPath;
        var files = HttpContext.Request.Form.Files;

        if (Item.Id == 0)
        {
            this.CreateProduct();
        }
        else
        {
            UpdateProduct();
        }

        return RedirectToPage("./Index");
    }

    private void CreateProduct()
    {
        
        string webRootPath = _webHostEnvironment.WebRootPath;
        var files = HttpContext.Request.Form.Files;
        if (files.Count == 0)
        {
            TempData["error"] = "Image is required.";
            return;
        }

        //create a unique identifier for image name
        string fileName = Guid.NewGuid().ToString();
        //create variable to hold a path to images\products
        var uploads = Path.Combine(webRootPath, @"images/products/");
        Console.WriteLine($"uploads: {uploads}");
        var extension = Path.GetExtension(files[0].FileName);
        Console.WriteLine($"extension: {extension}");
        var fullPath = uploads + fileName + extension;
        Console.WriteLine($"fullPath: {fullPath}");
        using var fileStream = System.IO.File.Create(fullPath);
        files[0].CopyTo(fileStream);
        Item.ImageUrl = @"\images\products\" + fileName + extension;
        _unitOfWork.Product.Add(Item);
        _unitOfWork.Commit();
        TempData["success"] = "Product added Successfully";
    }

    private void UpdateProduct()
    {
        _unitOfWork.Product.Update(Item);
        _unitOfWork.Commit();
        TempData["success"] = "Product updated Successfully";
    }
    
    private void FetchDropdowns()
    {
        CategoryList = _unitOfWork.Category.GetAll().Select(Item => new SelectListItem
        {
            Text = Item.Name,
            Value = Item.Id.ToString()
        });

        ManufacturerList = _unitOfWork.Manufacturer.GetAll().Select(Item => new SelectListItem
        {
            Text = Item.Name,
            Value = Item.Id.ToString()
        });
    }
}