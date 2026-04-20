using Microsoft.AspNetCore.Mvc;
using AspnetCoreMvcFull.Models;

namespace AspnetCoreMvcFull.Controllers;

public class ImportAdjustmentController : Controller
{
  private readonly IWebHostEnvironment _env;

  public ImportAdjustmentController(IWebHostEnvironment env)
  {
    _env = env;
  }

  public IActionResult Index()
  {
    return View();
  }

  public IActionResult CreateImportAdjustment()
  {
    return View();
  }
  
}
