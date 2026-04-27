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

  public IActionResult IndexTrackRequestStatus()
  {
    return View();
  }

  public IActionResult CreateTrackRequestStatus()
  {
    return View();
  }

  public IActionResult IndexManageCertificate()
  {
    return View();
  }

  public IActionResult CreateManageCertificatePS()
  {
    return View();
  }

  public IActionResult CreateManageCertificateNoPS()
  {
    return View();
  }

  public IActionResult IndexUpdateCertificate()
  {
    return View();
  }

  public IActionResult CreateUpdateCertificate()
  {
    return View();
  }

  public IActionResult IndexApproveReturnWeight()
  {
    return View();
  }

  public IActionResult CreateApproveReturnWeight()
  {
    return View();
  }

  public IActionResult IndexReturnCertificateWeight()
  {
    return View();
  }

  public IActionResult CreateReturnCertificateWeight()
  {
    return View();
  }

  public IActionResult IndexReplacementCertificate()
  {
    return View();
  }

  public IActionResult SelectTypeReplacement()
  {
    return View();
  }

  public IActionResult CreateReplacementDeclaration()
  {
    return View();
  }

  public IActionResult CreateReplacementProcessing()
  {
    return View();
  }

  public IActionResult ReviewDeclaration()
  {
    return View();
  }

  public IActionResult ReviewProcessing()
  {
    return View();
  }

}
