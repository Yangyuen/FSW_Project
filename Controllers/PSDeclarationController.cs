using Microsoft.AspNetCore.Mvc;
using AspnetCoreMvcFull.Models;

namespace AspnetCoreMvcFull.Controllers;

public class PSDeclarationController : Controller
{
  private readonly IWebHostEnvironment _env;

  public PSDeclarationController(IWebHostEnvironment env)
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

  public IActionResult CreateProcessing()
  {
    return View();
  }

  public IActionResult CreateDeclaration()
  {
    return View();
  }

}
