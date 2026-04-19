using Microsoft.AspNetCore.Mvc;
using AspnetCoreMvcFull.Models;

namespace AspnetCoreMvcFull.Controllers;

public class System_6_5Controller : Controller
{
  private readonly IWebHostEnvironment _env;

  public System_6_5Controller(IWebHostEnvironment env)
  {
    _env = env;
  }

  public IActionResult Index()
  {
    return View();
  }

  public IActionResult SelectType()
  {
    return View();
  }

  public IActionResult Create()
  {
    return View();
  }

  public IActionResult CreateProcessing()
  {
    return View();
  }

  public IActionResult CreateDeclaration()
  {
    return View();
  }

}
