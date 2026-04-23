using Microsoft.AspNetCore.Mvc;
using AspnetCoreMvcFull.Models;

namespace AspnetCoreMvcFull.Controllers;

public class AdminController : Controller
{
  private readonly IWebHostEnvironment _env;

  public AdminController(IWebHostEnvironment env)
  {
    _env = env;
  }

  public IActionResult Organization()
  {
    return View();
  }

  public IActionResult DofList()
  {
    return View();
  }

  public IActionResult TraceabilityList()
  {
    return View();
  }

  public IActionResult NotificationReason()
  {
    return View();
  }

  public IActionResult IssueReason()
  {
    return View();
  }

  public IActionResult Reports()
  {
    return View();
  }

  public IActionResult CertificateTemplate()
  {
    return View();
  }
}