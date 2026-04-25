using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreMvcFull.Controllers;

public class ScrapTradingController : Controller
{
  private readonly IWebHostEnvironment _env;

  public ScrapTradingController(IWebHostEnvironment env)
  {
    _env = env;
  }

  // รายการการซื้อขายเศษเหลือ (ผู้ขาย + ผู้ซื้อ)
  public IActionResult Index()
  {
    return View();
  }

  // ผู้ขายบันทึกการขาย / แก้ไข / ยกเลิก
  public IActionResult CreateSeller()
  {
    return View();
  }

  // ผู้ซื้อยืนยันรับซื้อ / บันทึกขายต่อ
  public IActionResult CreateBuyer()
  {
    return View();
  }
}
