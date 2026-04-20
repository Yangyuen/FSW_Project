namespace AspnetCoreMvcFull.Models;

public static class System65MockupFactory
{
  public static System65ScreenPageViewModel Build(
    System65ScreenDefinition current,
    IReadOnlyList<System65ScreenDefinition> roleScreens)
  {
    if (current.Role.Equals("Operator", StringComparison.OrdinalIgnoreCase))
    {
      return current.ScreenId switch
      {
        "OP-01" => BuildOperatorDashboard(current, roleScreens),
        "OP-02" => BuildRmbs1NoPsRequest(current, roleScreens),
        "OP-03" => BuildRmbs1NoPsList(current, roleScreens),
        "OP-04" => BuildRmbs1PsRequest(current, roleScreens),
        "OP-05" => BuildRmbs1PsChange(current, roleScreens),
        "OP-06" => BuildRmbs1PsProduction(current, roleScreens),
        "OP-07" => BuildRmbs2List(current, roleScreens),
        "OP-08" => BuildP1P2Trade(current, roleScreens),
        "OP-09" => BuildYieldRequest(current, roleScreens),
        "OP-10" => BuildCertificateRequest(current, roleScreens),
        "OP-11" => BuildCertificateTrack(current, roleScreens),
        "OP-12" => BuildCertificateSearch(current, roleScreens),
        "OP-13" => BuildCertificateAmend(current, roleScreens),
        "OP-14" => BuildCertificateReissue(current, roleScreens),
        "OP-15" => BuildCertificateCancel(current, roleScreens),
        "OP-16" => BuildReturnWeightRequest(current, roleScreens),
        "OP-17" => BuildScrapTrade(current, roleScreens),
        "OP-18" => BuildTraceabilityTree(current, roleScreens),
        _ => BuildFallbackScreen(current, roleScreens)
      };
    }

    return BuildFallbackScreen(current, roleScreens);
  }

  private static System65ScreenPageViewModel BuildOperatorDashboard(
    System65ScreenDefinition current,
    IReadOnlyList<System65ScreenDefinition> roleScreens)
  {
    return CreateModel(
      current,
      roleScreens,
      currentStatus: "พร้อมยื่นคำขอ 6 งาน",
      pageKicker: "ศูนย์ควบคุมงานผู้ประกอบการ",
      pageSummary: "สรุปงานค้างใกล้กำหนดส่งออก, แจ้งเตือนคำขอที่ถูกส่งกลับมาแก้ไข และลัดไปยังกระบวนงานหลักทั้งหมดของผู้ประกอบการ",
      kpiCards: new[]
      {
        Kpi("คำขอรอส่ง", "6 งาน", "รวม RMBS1, %Yield และใบรับรองที่ยังอยู่สถานะร่าง", "secondary", "ri-draft-line"),
        Kpi("รุ่นส่งออกใกล้กำหนด", "4 รุ่น", "มี 2 รุ่นที่ต้องยืนยันใบรับรองภายใน 48 ชั่วโมง", "warning", "ri-ship-line"),
        Kpi("น้ำหนักคงเหลือ RMBS", "128.4 ตัน", "คงเหลือพร้อมจัดสรรให้ผลิตหรือขายต่อ", "success", "ri-scales-3-line"),
        Kpi("รายการแจ้งแก้ไข", "3 รายการ", "ต้องตอบกลับเจ้าหน้าที่ก่อน 18 เม.ย. 2569", "danger", "ri-error-warning-line")
      },
      alerts: new[]
      {
        Alert("warning", "ใกล้กำหนดส่งออก", "รุ่นส่งออก RMBS2-2604-001 และ RMBS2-2604-003 ต้องออกใบรับรองภายใน 2 วัน"),
        Alert("info", "อัปเดตล่าสุด", "%Yield ปลาหมึกหอม Lot YD-24 ได้รับอนุมัติแล้ว สามารถนำไปใช้ในคำขอใหม่ได้")
      },
      workflowChips: new[]
      {
        Chip("ร่าง", "3", "secondary", true),
        Chip("ส่งคำขอแล้ว", "5", "info"),
        Chip("แจ้งแก้ไข", "2", "warning"),
        Chip("อนุมัติ", "18", "success"),
        Chip("ยกเลิก", "1", "danger")
      },
      primaryActions: new[]
      {
        Action("ยื่น RMBS1 NoPS", RouteOf(roleScreens, "OP-02"), "ri-file-add-line", "primary"),
        Action("ขอใบรับรอง", RouteOf(roleScreens, "OP-10"), "ri-award-line", "success"),
        Action("ซื้อขายเศษเหลือ", RouteOf(roleScreens, "OP-17"), "ri-recycle-line", "warning", true),
        Action("ตรวจเส้นทางซื้อขาย", RouteOf(roleScreens, "OP-18"), "ri-git-merge-line", "info", true)
      },
      detailPanels: new[]
      {
        Panel(
          "งานค้างตามกระบวนงาน",
          "สรุปจำนวนงานที่ต้องจัดการในวันนี้",
          "primary",
          new[]
          {
            Detail("RMBS1 NoPS", "2 ฉบับรอส่ง", "secondary", "หนึ่งฉบับขาดไฟล์ booking list"),
            Detail("RMBS1 PS", "1 ฉบับแจ้งแก้ไข", "warning", "เจ้าหน้าที่ให้แก้ไขประเทศปลายทาง"),
            Detail("ใบรับรอง PS/NMD", "2 ฉบับรอ e-Sign mock", "info", "ต้องตรวจข้อมูล HC ซ้ำก่อนพรีวิว"),
            Detail("คืนน้ำหนักส่งคืน", "1 ฉบับรอเลือกหลายใบรับรอง", "danger", "บางส่วนยังไม่ระบุวัตถุประสงค์")
          },
          new[] { "ใช้ badge สีเดียวกันทั้งระบบตาม requirement", "ทุก action ใน mockup นี้ยังไม่ผูก workflow จริง" },
          progressValue: 74,
          progressLabel: "ความพร้อมรวมของคำขอวันนี้"
        ),
        Panel(
          "ปฏิทินรุ่นส่งออก",
          "รายการรุ่นส่งออกที่ต้องจับตา",
          "info",
          new[]
          {
            Detail("17 เม.ย. 2569", "RMBS2-2604-001 / ญี่ปุ่น", "warning"),
            Detail("18 เม.ย. 2569", "CERT-PS-2604118 / สหรัฐอเมริกา", "danger"),
            Detail("22 เม.ย. 2569", "P2-LOT-APR-02 / เกาหลีใต้", "success")
          })
      },
      tableSections: new[]
      {
        Table(
          "งานค้างทั้งหมด",
          "รองรับการ sort ตามความเร่งด่วน, filter ตามสถานะ และ export เป็น Excel/CSV",
          new[] { "กระบวนงาน", "เลขอ้างอิง", "สถานะ", "ครบกำหนด", "การดำเนินการ" },
          new[]
          {
            Row(Cell("RMBS1 NoPS"), Cell("NP-260401"), Badge("ร่าง", "secondary"), Cell("16 เม.ย. 2569"), Cell("ตรวจ IMD และแนบ booking list")),
            Row(Cell("RMBS1 PS"), Cell("PS-260412"), Badge("แจ้งแก้ไข", "warning"), Cell("17 เม.ย. 2569"), Cell("แก้ประเทศปลายทางเป็น JP / KR")),
            Row(Cell("Certificate"), Cell("CERT-DRAFT-029"), Badge("ร่าง", "info"), Cell("18 เม.ย. 2569"), Cell("ตรวจ HC line 03 ซ้ำก่อนส่ง"))
          },
          filters: new[] { "สถานะทั้งหมด", "ใกล้ครบกำหนด", "เฉพาะงานแจ้งแก้ไข" },
          exportFormats: new[] { "Excel", "CSV" },
          paginationSummary: "แสดง 1-3 จาก 16 งาน"
        ),
        Table(
          "รุ่นส่งออกและใบรับรอง",
          "ตาราง mock สำหรับติดตามความพร้อมของรุ่นส่งออกและเลขอ้างอิงใบรับรอง",
          new[] { "รุ่นส่งออก", "สินค้าหลัก", "ใบรับรอง", "ประเทศ", "สถานะ" },
          new[]
          {
            Row(Cell("RMBS2-2604-001"), Cell("ปลาทูน่าแช่แข็ง"), Cell("CERT-PS-2604118"), Cell("ญี่ปุ่น"), Badge("ใกล้ครบกำหนด", "warning")),
            Row(Cell("P2-LOT-APR-02"), Cell("กุ้งแปรรูป"), Cell("CERT-NMD-2604104"), Cell("เกาหลีใต้"), Badge("พร้อมส่งออก", "success")),
            Row(Cell("SCR-2604-011"), Cell("เศษเหลือปลาหมึก"), Cell("BOOK-SCR-014"), Cell("ในประเทศ"), Badge("รอยืนยันรับซื้อ", "info"))
          },
          filters: new[] { "สัปดาห์นี้", "ต่างประเทศ", "ในประเทศ" },
          exportFormats: new[] { "Excel", "PDF" },
          paginationSummary: "แสดง 1-3 จาก 9 รุ่น"
        )
      },
      timeline: new[]
      {
        Event("สร้างคำขอ RMBS1 NoPS ใหม่", "เพิ่มวัตถุดิบปลาทูน่า 12,000 กก. สำหรับรอบนำเข้า FCFS เมษายน", "ผู้ประกอบการ", "วันนี้ 08:45", "primary", new[] { "NP-260401", "ร่าง" }),
        Event("เจ้าหน้าที่แจ้งแก้ไขคำขอ RMBS1 PS", "ให้ปรับประเทศปลายทางและแนบ invoice ล่าสุด", "ฝ่ายตรวจคำขอ", "เมื่อวาน 16:10", "warning", new[] { "PS-260412", "แจ้งแก้ไข" }),
        Event("อนุมัติ %Yield ใหม่", "ระบบปรับ benchmark ของปลาหมึกหอมเป็น 74.8%", "เจ้าหน้าที่ผู้อนุมัติ", "15 เม.ย. 2569 14:20", "success", new[] { "YD-24", "อนุมัติ" })
      },
      pageNotes: new[]
      {
        "Dashboard นี้ผูกกับ mock route ของผู้ประกอบการทั้งหมดใน Controller PSDeclaration",
        "ปุ่มลัดหลักตรงกับ requirement OP-01 และเน้นกลุ่มงานที่มี deadline ก่อน"
      });
  }

  private static System65ScreenPageViewModel BuildRmbs1NoPsRequest(
    System65ScreenDefinition current,
    IReadOnlyList<System65ScreenDefinition> roleScreens)
  {
    return CreateModel(
      current,
      roleScreens,
      currentStatus: "ร่าง",
      pageKicker: "คำขอพิจารณาวัตถุดิบตั้งต้น RMBS1 สถานะ NoPS",
      pageSummary: "ใช้สำหรับสร้างหรือแก้ไขคำขอ, ตรวจสอบข้อมูล IMD และมาตรการสุขอนามัย พร้อมคำนวณโควตา FCFS จากข้อมูล mockup",
      alerts: new[]
      {
        Alert("info", "FCFS quota mock", "ระบบ mock คำนวณคิว FCFS จากคำขอที่ยื่นก่อนหน้าและแสดงน้ำหนักคงเหลือแบบอ่านอย่างเดียว"),
        Alert("warning", "เอกสารหลักฐานยังไม่ครบ", "ต้องแนบ booking list และใบรับรอง sanitary measures อย่างน้อย 1 รายการก่อนกดส่งคำขอ")
      },
      workflowChips: new[]
      {
        Chip("ตรวจ IMD", "ผ่าน", "success", true),
        Chip("มาตรการสุขอนามัย", "1 รายการรอยืนยัน", "warning"),
        Chip("คำนวณ FCFS", "12.8 ตัน", "info"),
        Chip("แนบหลักฐาน", "2/3", "secondary")
      },
      primaryActions: new[]
      {
        Action("บันทึกร่าง", current.Route, "ri-save-line", "primary"),
        Action("พรีวิว PDF", current.Route, "ri-file-pdf-line", "danger", true),
        Action("ส่งคำขอ", current.Route, "ri-send-plane-line", "success"),
        Action("ไปหน้ารายการ", RouteOf(roleScreens, "OP-03"), "ri-list-check-3", "info", true)
      },
      formSections: new[]
      {
        Form(
          "ข้อมูลคำขอ",
          "กำหนดรอบคำขอและข้อมูลนำเข้าหลัก",
          new[]
          {
            Field("สถานประกอบการ", "โรงงานสมุทรสาคร 01", type: "select", helperText: "ดึงจาก master โรงงาน mock", required: true, options: new[] { "โรงงานสมุทรสาคร 01", "โรงงานสงขลา 02" }),
            Field("รหัสคำขอ", "NP-260401", type: "readonly", helperText: "เลขอ้างอิงสร้างอัตโนมัติใน mockup", columnSpan: 3),
            Field("วันที่ยื่น", "2026-04-16", type: "date", required: true, columnSpan: 3),
            Field("ประเทศต้นทาง", "ไต้หวัน", type: "select", required: true, options: new[] { "ไต้หวัน", "อินโดนีเซีย", "เวียดนาม" }),
            Field("เลขที่ IMD", "IMD-2026-0418", helperText: "ใช้ตรวจสอบเอกสารวัตถุดิบตั้งต้น", required: true),
            Field("รอบเรือ/เที่ยวเรือ", "Vessel Ocean Crest / Voyage 08", helperText: "ข้อมูล mock สำหรับผูก booking list", columnSpan: 12)
          }),
        Form(
          "รายละเอียดวัตถุดิบและโควตา",
          "ระบุน้ำหนักขอพิจารณาและข้อมูลสินค้าตั้งต้น",
          new[]
          {
            Field("ชนิดวัตถุดิบ", "ปลาทูน่าครีบเหลืองแช่แข็ง", type: "select", required: true, options: new[] { "ปลาทูน่าครีบเหลืองแช่แข็ง", "ปลาหมึกหอมแช่เย็น", "กุ้งขาวแช่แข็ง" }),
            Field("น้ำหนักขอพิจารณา", "12,800", type: "number", suffix: "กก.", required: true),
            Field("น้ำหนักคงเหลือ FCFS", "14,300", type: "readonly", suffix: "กก."),
            Field("คลังเก็บ", "คลังเย็น A-03", helperText: "แสดงเป็นข้อมูลอ้างอิงสำหรับเจ้าหน้าที่", columnSpan: 6),
            Field("หมายเหตุคำขอ", "ต้องการใช้สำหรับผลิตรุ่นส่งออกญี่ปุ่นสัปดาห์หน้า", type: "textarea", columnSpan: 12)
          }),
        Form(
          "หลักฐานและการยืนยัน",
          "แนบไฟล์และทำเครื่องหมายยืนยันข้อมูล",
          new[]
          {
            Field("ไฟล์แนบ", "booking-list-apr.pdf | sanitary-measures-jp.pdf", type: "file", helperText: "รองรับ mock upload หลายไฟล์", columnSpan: 12),
            Field("ยืนยันข้อมูล IMD ถูกต้อง", "true", type: "toggle", helperText: "ใช้ switch mock แทน custom option", columnSpan: 6),
            Field("ยืนยันมาตรการปลายทาง", "true", type: "toggle", helperText: "ข้อมูลนี้จะถูกใช้ในเอกสารพรีวิว", columnSpan: 6)
          })
      },
      detailPanels: new[]
      {
        Panel(
          "สรุปโควตา FCFS",
          "ภาพรวมคิวจองและน้ำหนักคงเหลือ",
          "info",
          new[]
          {
            Detail("คิวคำขอปัจจุบัน", "ลำดับที่ 3", "secondary"),
            Detail("น้ำหนักที่พร้อมจัดสรร", "12,800 กก.", "success"),
            Detail("น้ำหนักสำรองหลังคำขอนี้", "1,500 กก.", "warning"),
            Detail("รุ่นอ้างอิง", "RMBS1-NOPS-APR-08", "info")
          },
          new[] { "หากน้ำหนักขอเกิน 14,300 กก. ระบบ mock จะแสดงเตือน quota ไม่พอ", "ตัวเลขทั้งหมดเป็น mock data ไม่ผูกฐานข้อมูลจริง" },
          progressValue: 89,
          progressLabel: "สัดส่วนการใช้โควตาหลังส่งคำขอ"
        ),
        Panel(
          "ผลตรวจสอบ IMD และมาตรการ",
          "รายการตรวจ mock ก่อนส่งคำขอ",
          "success",
          new[]
          {
            Detail("IMD document", "ตรงตามเลขอ้างอิง", "success"),
            Detail("ประเทศปลายทาง", "มี sanitary measures ฉบับล่าสุด", "success"),
            Detail("ไฟล์ booking list", "แนบแล้ว", "success"),
            Detail("ข้อสังเกต", "ยังไม่แนบภาพฉลากคลังเย็น", "warning")
          })
      },
      tableSections: new[]
      {
        Table(
          "ทะเบียนไฟล์แนบ",
          "ใช้ตรวจรายการไฟล์ก่อนส่งคำขอ",
          new[] { "ไฟล์", "หมวดเอกสาร", "วันที่อัปโหลด", "สถานะ" },
          new[]
          {
            Row(Cell("booking-list-apr.pdf"), Cell("Booking List"), Cell("16 เม.ย. 2569 08:35"), Badge("พร้อมใช้", "success")),
            Row(Cell("sanitary-measures-jp.pdf"), Cell("Sanitary Measures"), Cell("16 เม.ย. 2569 08:38"), Badge("พร้อมใช้", "success")),
            Row(Cell("cold-storage-label.jpg"), Cell("รูปภาพประกอบ"), Cell("ยังไม่อัปโหลด"), Badge("รอแนบ", "warning"))
          },
          filters: new[] { "ทั้งหมด", "เอกสารบังคับ", "เอกสารเพิ่มเติม" },
          exportFormats: new[] { "Excel" },
          paginationSummary: "แสดง 1-3 จาก 3 ไฟล์"
        )
      },
      timeline: new[]
      {
        Event("เริ่มสร้างคำขอ", "คัดลอกข้อมูลโรงงานและเลข IMD จาก draft ก่อนหน้า", "ผู้ประกอบการ", "วันนี้ 08:21", "primary", new[] { "NP-260401", "ร่าง" }),
        Event("ระบบ mock ตรวจสอบ IMD", "พบเลข IMD และน้ำหนักนำเข้าในช่วงเวลาที่ใช้งานได้", "ระบบ", "วันนี้ 08:29", "info", new[] { "IMD-2026-0418", "ผ่าน" }),
        Event("แนบเอกสารมาตรการปลายทาง", "อัปโหลด sanitary measures สำหรับญี่ปุ่นฉบับอัปเดต", "ผู้ประกอบการ", "วันนี้ 08:38", "success", new[] { "ญี่ปุ่น", "แนบไฟล์" })
      },
      pageNotes: new[]
      {
        "หน้าจอนี้ครอบคลุมข้อกำหนด 1.1(1)-(5) ตาม requirement ผู้ประกอบการ",
        "ปุ่มส่งคำขอและพรีวิว PDF เป็น mock interaction เท่านั้น"
      });
  }

  private static System65ScreenPageViewModel BuildRmbs1NoPsList(
    System65ScreenDefinition current,
    IReadOnlyList<System65ScreenDefinition> roleScreens)
  {
    return CreateModel(
      current,
      roleScreens,
      currentStatus: "อนุมัติ 24 ฉบับ",
      pageKicker: "รายการ RMBS1 NoPS และรายละเอียดเอกสาร",
      pageSummary: "ค้นหา, กรอง, sort และดูรายละเอียดเอกสาร RMBS1 NoPS ที่อนุมัติแล้ว พร้อม mock preview ของไฟล์ PDF",
      kpiCards: new[]
      {
        Kpi("เอกสารอนุมัติแล้ว", "24", "รวมเอกสารอนุมัติในไตรมาสปัจจุบัน", "success", "ri-file-check-line"),
        Kpi("รอพรีวิว", "3 ฉบับ", "มีไฟล์ PDF รุ่นล่าสุดพร้อมดาวน์โหลด mock", "info", "ri-file-search-line"),
        Kpi("น้ำหนักอนุมัติรวม", "286.2 ตัน", "สะสมตามรอบ FCFS ที่ได้รับอนุมัติ", "primary", "ri-scales-3-line")
      },
      primaryActions: new[]
      {
        Action("สร้างคำขอใหม่", RouteOf(roleScreens, "OP-02"), "ri-add-circle-line", "primary"),
        Action("Export รายการ", current.Route, "ri-download-2-line", "success", true)
      },
      formSections: new[]
      {
        Form(
          "ค้นหาและตัวกรอง",
          "ตัวกรองแบบ mock สำหรับรายการอนุมัติ",
          new[]
          {
            Field("เลขอ้างอิง", "NP-2603", placeholder: "ค้นหาด้วย Ref No.", helperText: "รองรับการค้นหา prefix ของคำขอ", columnSpan: 4),
            Field("สถานะ", "อนุมัติ", type: "select", columnSpan: 4, options: new[] { "ทั้งหมด", "อนุมัติ", "ยกเลิก" }),
            Field("ประเทศต้นทาง", "ทั้งหมด", type: "select", columnSpan: 4, options: new[] { "ทั้งหมด", "ไต้หวัน", "อินโดนีเซีย", "เวียดนาม" })
          })
      },
      detailPanels: new[]
      {
        Panel(
          "รายละเอียดเอกสารที่เลือก",
          "สรุปข้อมูลของเอกสารอนุมัติล่าสุด",
          "success",
          new[]
          {
            Detail("เลขอ้างอิง", "NP-260318", "info"),
            Detail("วันที่อนุมัติ", "31 มี.ค. 2569", "secondary"),
            Detail("น้ำหนักอนุมัติ", "18,500 กก.", "success"),
            Detail("PDF Preview", "RMBS1-NOPS-260318.pdf", "primary")
          },
          new[] { "Timeline ด้านล่างใช้เป็น mock history สำหรับหน้า detail", "ปุ่ม preview/download ยังไม่สร้างไฟล์ PDF จริง" })
      },
      tableSections: new[]
      {
        Table(
          "รายการเอกสาร RMBS1 NoPS",
          "รองรับ sort, filter, pagination และ export ตาม requirement OP-03",
          new[] { "Ref No.", "สถานประกอบการ", "ประเทศต้นทาง", "น้ำหนัก", "สถานะ", "อัปเดตล่าสุด" },
          new[]
          {
            Row(Cell("NP-260318"), Cell("โรงงานสมุทรสาคร 01"), Cell("ไต้หวัน"), Cell("18,500 กก."), Badge("อนุมัติ", "success"), Cell("31 มี.ค. 2569")),
            Row(Cell("NP-260322"), Cell("โรงงานสงขลา 02"), Cell("อินโดนีเซีย"), Cell("9,200 กก."), Badge("อนุมัติ", "success"), Cell("02 เม.ย. 2569")),
            Row(Cell("NP-260401"), Cell("โรงงานสมุทรสาคร 01"), Cell("เวียดนาม"), Cell("12,800 กก."), Badge("ร่าง", "secondary"), Cell("16 เม.ย. 2569"))
          },
          filters: new[] { "อนุมัติทั้งหมด", "อนุมัติเดือนนี้", "มี PDF พร้อมพรีวิว" },
          exportFormats: new[] { "Excel", "CSV", "PDF" },
          paginationSummary: "แสดง 1-3 จาก 24 รายการ"
        )
      },
      timeline: new[]
      {
        Event("อนุมัติเอกสาร", "คำขอ NP-260318 ได้รับอนุมัติและสร้าง mock PDF สำเร็จ", "เจ้าหน้าที่", "31 มี.ค. 2569 10:20", "success", new[] { "NP-260318", "อนุมัติ" }),
        Event("สร้างไฟล์พรีวิว", "ระบบสร้างชื่อเอกสาร RMBS1-NOPS-260318.pdf เพื่อให้ทดสอบหน้า preview", "ระบบ", "31 มี.ค. 2569 10:21", "info", new[] { "PDF Preview" }),
        Event("เปิดดูเอกสารล่าสุด", "ผู้ประกอบการเข้าดู mock detail ผ่านหน้า list", "ผู้ประกอบการ", "วันนี้ 09:15", "primary", new[] { "View Detail" })
      },
      pageNotes: new[] { "หน้าจอ list นี้มี control สำหรับ sort/filter/pagination/export ครบตาม requirement" });
  }

  private static System65ScreenPageViewModel BuildRmbs1PsRequest(
    System65ScreenDefinition current,
    IReadOnlyList<System65ScreenDefinition> roleScreens)
  {
    return CreateModel(
      current,
      roleScreens,
      currentStatus: "แจ้งแก้ไข",
      pageKicker: "คำขอ RMBS1 สถานะ PS",
      pageSummary: "สร้าง, บันทึก, แก้ไข, ยกเลิก และแนบไฟล์ของคำขอ RMBS1:PS พร้อมเลือกความประสงค์ขอใบรับรองและประเทศปลายทาง",
      alerts: new[]
      {
        Alert("warning", "รายการที่ต้องแก้ไข", "เจ้าหน้าที่ให้แก้ไขประเทศปลายทางให้ตรงกับเอกสาร sales contract และอัปเดตไฟล์ invoice ล่าสุด"),
        Alert("info", "การเชื่อมโยงเอกสาร", "คำขอนี้อ้างอิง NoPS เลขที่ NP-260318 และเชื่อมไปยังรุ่นผลิตใน OP-06 ได้ทันที")
      },
      workflowChips: new[]
      {
        Chip("บันทึกร่าง", "เสร็จแล้ว", "success"),
        Chip("เลือกใบรับรอง", "PS + HC", "info", true),
        Chip("แนบหลักฐาน", "ครบ 4 รายการ", "success"),
        Chip("ส่งคำขอ", "รอแก้ไข", "warning")
      },
      primaryActions: new[]
      {
        Action("บันทึกร่าง", current.Route, "ri-save-line", "primary"),
        Action("พรีวิว PDF", current.Route, "ri-file-pdf-2-line", "danger", true),
        Action("ส่งคำขอ", current.Route, "ri-send-plane-line", "success"),
        Action("ขอเปลี่ยนผล", RouteOf(roleScreens, "OP-05"), "ri-loop-left-line", "warning", true)
      },
      formSections: new[]
      {
        Form(
          "หัวคำขอและข้อมูลอ้างอิง",
          "เชื่อมโยงจาก RMBS1 NoPS และกำหนดรุ่นคำขอ PS",
          new[]
          {
            Field("เลขคำขอ PS", "PS-260412", type: "readonly", columnSpan: 4),
            Field("เอกสาร NoPS อ้างอิง", "NP-260318", type: "select", required: true, columnSpan: 4, options: new[] { "NP-260318", "NP-260322" }),
            Field("วันที่ยื่น", "2026-04-16", type: "date", required: true, columnSpan: 4),
            Field("รุ่นการผลิตเป้าหมาย", "RMBS1-PS-APR-04", helperText: "ใช้เป็น key เชื่อมกับหน้าบันทึกการผลิต", required: true, columnSpan: 6),
            Field("ความประสงค์คำขอ", "ออกใบรับรอง PS และ HC", type: "textarea", columnSpan: 6)
          }),
        Form(
          "ปลายทางและใบรับรอง",
          "เลือกประเภทใบรับรองและประเทศปลายทาง",
          new[]
          {
            Field("ประเภทใบรับรอง", "PS / HC", type: "select", required: true, columnSpan: 4, options: new[] { "PS", "PS / HC", "NMD / HC" }),
            Field("ประเทศปลายทางหลัก", "ญี่ปุ่น", type: "select", required: true, columnSpan: 4, options: new[] { "ญี่ปุ่น", "เกาหลีใต้", "สหรัฐอเมริกา" }),
            Field("ประเทศปลายทางรอง", "เกาหลีใต้", type: "select", columnSpan: 4, options: new[] { "ไม่มี", "ญี่ปุ่น", "เกาหลีใต้", "สหรัฐอเมริกา" }),
            Field("แนบไฟล์หลักฐาน", "invoice-ps-04.pdf | sales-contract-jp.pdf | packing-list-04.xlsx", type: "file", columnSpan: 12),
            Field("รายละเอียดการแก้ไขล่าสุด", "อัปเดตประเทศปลายทางให้ตรงกับ sales contract และแนบ invoice rev.2", type: "editor", columnSpan: 12)
          })
      },
      detailPanels: new[]
      {
        Panel(
          "สรุปคำขอและกฎการดำเนินการ",
          "ปุ่มการทำงานที่ requirement ระบุไว้สำหรับหน้าคำขอนี้",
          "primary",
          new[]
          {
            Detail("บันทึก/แก้ไข", "รองรับ", "success"),
            Detail("ยกเลิก/ลบ", "ใช้ modal mock ยืนยัน", "warning"),
            Detail("แนบไฟล์", "หลายไฟล์", "info"),
            Detail("สถานะปัจจุบัน", "แจ้งแก้ไข", "danger")
          },
          new[] { "หากไม่ต้องการดำเนินการต่อ ให้ใช้ปุ่มยกเลิกใน action bar ด้านบน", "เลขอ้างอิงทั้งหมดเป็น mock เท่านั้น" }),
        Panel(
          "เชื่อมโยงเอกสารต้นทาง",
          "ข้อมูลจาก NoPS และ quota ที่พร้อมใช้งาน",
          "success",
          new[]
          {
            Detail("NoPS reference", "NP-260318", "info"),
            Detail("น้ำหนักที่อนุมัติ", "18,500 กก.", "success"),
            Detail("จัดสรรให้คำขอนี้", "11,400 กก.", "primary"),
            Detail("น้ำหนักคงเหลือ", "7,100 กก.", "warning")
          },
          progressValue: 62,
          progressLabel: "สัดส่วนการจัดสรรของเอกสารต้นทาง")
      },
      tableSections: new[]
      {
        Table(
          "รายการสินค้าในคำขอ",
          "รายละเอียด line item ที่จะออกใบรับรอง",
          new[] { "สินค้า", "น้ำหนักสุทธิ", "ปลายทาง", "ชนิดใบรับรอง", "สถานะ" },
          new[]
          {
            Row(Cell("ปลาทูน่าครีบเหลืองแช่แข็ง"), Cell("6,800 กก."), Cell("ญี่ปุ่น"), Cell("PS / HC"), Badge("พร้อมส่ง", "success")),
            Row(Cell("ปลาทูน่าหั่นชิ้น"), Cell("4,600 กก."), Cell("เกาหลีใต้"), Cell("PS"), Badge("รอแก้ไขปลายทาง", "warning"))
          },
          filters: new[] { "ทั้งหมด", "ปลายทางญี่ปุ่น", "PS / HC" },
          exportFormats: new[] { "Excel", "PDF" },
          paginationSummary: "แสดง 1-2 จาก 2 รายการ"
        )
      },
      timeline: new[]
      {
        Event("สร้างคำขอ PS", "คัดลอกข้อมูลจาก NP-260318 เพื่อเริ่มทำคำขอ PS", "ผู้ประกอบการ", "14 เม.ย. 2569 09:10", "primary", new[] { "PS-260412", "ร่าง" }),
        Event("ส่งคำขอครั้งแรก", "ระบบ mock บันทึกประเทศปลายทางญี่ปุ่นและเกาหลีใต้", "ผู้ประกอบการ", "15 เม.ย. 2569 13:05", "info", new[] { "ส่งคำขอแล้ว" }),
        Event("เจ้าหน้าที่แจ้งแก้ไข", "ให้ปรับประเทศปลายทางและแนบ invoice rev.2", "เจ้าหน้าที่", "15 เม.ย. 2569 16:42", "warning", new[] { "แจ้งแก้ไข" })
      },
      pageNotes: new[] { "หน้าจอนี้ครอบคลุม requirement 1.2(2)-(4) และ 1.3 บางส่วนสำหรับฝั่งผู้ประกอบการ" });
  }

  private static System65ScreenPageViewModel BuildRmbs1PsChange(
    System65ScreenDefinition current,
    IReadOnlyList<System65ScreenDefinition> roleScreens)
  {
    return CreateModel(
      current,
      roleScreens,
      currentStatus: "ร่าง",
      pageKicker: "คำขอเปลี่ยนผลการพิจารณา RMBS1:PS",
      pageSummary: "ใช้ยื่นคำขอเปลี่ยนผลพร้อมระบุเหตุผลและแนบหลักฐานประกอบก่อนส่งให้เจ้าหน้าที่",
      alerts: new[]
      {
        Alert("warning", "คำขอเดิมถูกไม่อนุมัติ", "เหตุผลเดิมคือข้อมูลปลายทางไม่สอดคล้องกับเอกสาร shipping instruction"),
        Alert("info", "แนบหลักฐานใหม่", "สามารถแนบใบแก้ไข invoice, sales contract และคำชี้แจงเพิ่มเติมใน mock editor ได้")
      },
      primaryActions: new[]
      {
        Action("บันทึกร่าง", current.Route, "ri-save-line", "primary"),
        Action("ส่งคำขอ", current.Route, "ri-send-plane-line", "success"),
        Action("กลับหน้าคำขอ PS", RouteOf(roleScreens, "OP-04"), "ri-arrow-left-line", "secondary", true)
      },
      formSections: new[]
      {
        Form(
          "รายละเอียดคำขอเปลี่ยนผล",
          "อธิบายเหตุผลและแนบหลักฐานเพิ่มเติม",
          new[]
          {
            Field("เลขคำขออ้างอิง", "PS-260412", type: "readonly", columnSpan: 4),
            Field("ผลพิจารณาเดิม", "ไม่อนุมัติ", type: "readonly", columnSpan: 4),
            Field("วันที่ยื่นคำขอเปลี่ยนผล", "2026-04-16", type: "date", columnSpan: 4, required: true),
            Field("เหตุผลการยื่นใหม่", "แก้ไขปลายทางให้ตรงกับ sales contract rev.2 และแนบ invoice ฉบับล่าสุดแล้ว", type: "editor", columnSpan: 12, required: true),
            Field("หลักฐานประกอบ", "invoice-rev2.pdf | sales-contract-rev2.pdf | explanation-letter.docx", type: "file", columnSpan: 12)
          })
      },
      detailPanels: new[]
      {
        Panel(
          "สรุปผลพิจารณาเดิม",
          "ข้อมูลที่ถูกใช้เป็นฐานก่อนยื่นคำขอเปลี่ยนผล",
          "danger",
          new[]
          {
            Detail("เลขผลพิจารณา", "REV-PS-260412", "info"),
            Detail("ผลเดิม", "ไม่อนุมัติ", "danger"),
            Detail("ประเด็นหลัก", "ประเทศปลายทางไม่ตรงเอกสาร", "warning"),
            Detail("ผู้แจ้งผล", "เจ้าหน้าที่กลุ่ม RMBS", "secondary")
          }),
        Panel(
          "Checklist ก่อนส่ง",
          "รายการตรวจสำหรับ mock workflow",
          "success",
          new[]
          {
            Detail("ชี้แจงเหตุผลครบ", "พร้อมส่ง", "success"),
            Detail("แนบเอกสารใหม่", "ครบ 3 ไฟล์", "success"),
            Detail("เชื่อมกับคำขอเดิม", "PS-260412", "info")
          },
          progressValue: 100,
          progressLabel: "ความครบถ้วนของคำขอเปลี่ยนผล")
      },
      tableSections: new[]
      {
        Table(
          "ทะเบียนหลักฐานประกอบ",
          "สรุปรายการเอกสารที่แนบมากับคำขอเปลี่ยนผล",
          new[] { "ไฟล์", "หมวด", "Rev", "สถานะ" },
          new[]
          {
            Row(Cell("invoice-rev2.pdf"), Cell("Invoice"), Cell("Rev.2"), Badge("พร้อมใช้", "success")),
            Row(Cell("sales-contract-rev2.pdf"), Cell("Sales Contract"), Cell("Rev.2"), Badge("พร้อมใช้", "success")),
            Row(Cell("explanation-letter.docx"), Cell("คำชี้แจง"), Cell("New"), Badge("พร้อมใช้", "success"))
          },
          filters: new[] { "ทั้งหมด" },
          exportFormats: new[] { "Excel" },
          paginationSummary: "แสดง 1-3 จาก 3 รายการ"
        )
      },
      timeline: new[]
      {
        Event("รับผลไม่อนุมัติ", "ระบบ mock บันทึกผลไม่อนุมัติของคำขอ PS-260412", "เจ้าหน้าที่", "15 เม.ย. 2569 16:42", "danger", new[] { "ไม่อนุมัติ" }),
        Event("เตรียมเอกสารชุดใหม่", "อัปโหลด invoice และ sales contract rev.2", "ผู้ประกอบการ", "16 เม.ย. 2569 09:03", "info", new[] { "แนบหลักฐาน" }),
        Event("ร่างคำขอเปลี่ยนผล", "กรอกเหตุผลและเลือกส่งให้เจ้าหน้าที่ในรอบถัดไป", "ผู้ประกอบการ", "16 เม.ย. 2569 09:20", "primary", new[] { "ร่าง" })
      });
  }

  private static System65ScreenPageViewModel BuildRmbs1PsProduction(
    System65ScreenDefinition current,
    IReadOnlyList<System65ScreenDefinition> roleScreens)
  {
    return CreateModel(
      current,
      roleScreens,
      currentStatus: "ใช้งานอยู่",
      pageKicker: "บันทึกข้อมูลเอกสาร RMBS1:PS",
      pageSummary: "บันทึก production batch, การขายต่อ, การซื้อเพิ่ม และตรวจ %Yield กับ stock balance ก่อนออก RMBS2",
      kpiCards: new[]
      {
        Kpi("วัตถุดิบจัดสรร", "11,400 กก.", "ผูกกับคำขอ PS-260412", "primary", "ri-stack-line"),
        Kpi("ผลิตสะสม", "8,240 กก.", "รวม 3 production batch ล่าสุด", "success", "ri-box-3-line"),
        Kpi("%Yield ล่าสุด", "72.3%", "ต่ำกว่า benchmark 74.8% เล็กน้อย", "warning", "ri-percent-line"),
        Kpi("คงเหลือพร้อมใช้", "3,160 กก.", "สามารถขายต่อหรือซื้อเพิ่มเพื่อปิด lot ได้", "info", "ri-scales-2-line")
      },
      alerts: new[]
      {
        Alert("warning", "Yield ต่ำกว่า benchmark", "batch PROD-APR-03 ได้ yield 71.8% ควรระบุเหตุผลประกอบก่อนส่งต่อเจ้าหน้าที่"),
        Alert("info", "balance stock", "หากต้องการซื้อเพิ่ม สามารถผูกกับ P1/P2 ผ่านหน้าซื้อขาย OP-08 ได้")
      },
      workflowChips: new[]
      {
        Chip("Production batch", "3 รายการ", "success", true),
        Chip("Sell forward", "1 รายการ", "info"),
        Chip("Buy additional", "รอเพิ่ม", "secondary"),
        Chip("Stock lock", "ไม่ติดเงื่อนไข", "success")
      },
      primaryActions: new[]
      {
        Action("เพิ่ม batch ผลิต", current.Route, "ri-add-box-line", "primary"),
        Action("Export balance", current.Route, "ri-download-2-line", "success", true),
        Action("เปิด RMBS2", RouteOf(roleScreens, "OP-07"), "ri-archive-stack-line", "info", true)
      },
      formSections: new[]
      {
        Form(
          "บันทึก production batch",
          "ข้อมูลสำหรับ batch ใหม่หรือแก้ไข batch เดิม",
          new[]
          {
            Field("เลข batch", "PROD-APR-04", helperText: "mock running number", columnSpan: 4, required: true),
            Field("วันที่ผลิต", "2026-04-16", type: "date", columnSpan: 4, required: true),
            Field("รูปแบบดำเนินการ", "ผลิต", type: "select", columnSpan: 4, options: new[] { "ผลิต", "ขายต่อ", "ซื้อเพิ่ม" }),
            Field("น้ำหนักวัตถุดิบเข้า", "3,200", type: "number", suffix: "กก.", columnSpan: 4),
            Field("น้ำหนักผลิตออก", "2,304", type: "number", suffix: "กก.", columnSpan: 4),
            Field("Yield ที่ได้", "72.0", type: "number", suffix: "%", columnSpan: 4),
            Field("หมายเหตุ batch", "ผลิตเพื่อส่งออกญี่ปุ่น lot สัปดาห์ที่ 3 ของเดือนเมษายน", type: "textarea", columnSpan: 12)
          }),
        Form(
          "ซื้อเพิ่มหรือขายต่อ",
          "บันทึกกรณีมีการเคลื่อนย้าย stock",
          new[]
          {
            Field("คู่ค้า", "บริษัท ปลายทางซีฟู้ด จำกัด", type: "select", columnSpan: 6, options: new[] { "บริษัท ปลายทางซีฟู้ด จำกัด", "โรงงานสมุทรสาคร 01", "ผู้ประกอบการนอกระบบ" }),
            Field("ปริมาณ", "960", type: "number", suffix: "กก.", columnSpan: 3),
            Field("ประเภท", "ขายต่อ", type: "select", columnSpan: 3, options: new[] { "ขายต่อ", "ซื้อเพิ่ม" }),
            Field("เหตุผล", "ปรับสมดุล stock เพื่อปิด lot ส่งออกญี่ปุ่น", type: "textarea", columnSpan: 12)
          })
      },
      detailPanels: new[]
      {
        Panel(
          "Stock balance",
          "สรุปคงเหลือหลังหัก production และ movement ทั้งหมด",
          "success",
          new[]
          {
            Detail("คงเหลือต้นงวด", "11,400 กก.", "primary"),
            Detail("ใช้ในการผลิต", "8,240 กก.", "info"),
            Detail("ขายต่อ", "960 กก.", "warning"),
            Detail("คงเหลือปลายงวด", "2,200 กก.", "success")
          },
          progressValue: 81,
          progressLabel: "สัดส่วนการใช้วัตถุดิบในเอกสารนี้"),
        Panel(
          "กฎแจ้งเตือน",
          "เงื่อนไข mock ที่จะแสดง alert เมื่อข้อมูลผิดปกติ",
          "warning",
          new[]
          {
            Detail("Yield ต่ำกว่าอนุมัติ", "ต้องกรอกเหตุผล", "warning"),
            Detail("quota ไม่พอ", "ล็อกการบันทึกเพิ่มเติม", "danger"),
            Detail("ขายต่อเกินคงเหลือ", "ไม่อนุญาต", "danger")
          })
      },
      tableSections: new[]
      {
        Table(
          "รายการ production batch",
          "รองรับ sort และ export เพื่อใช้ตรวจสอบย้อนหลัง",
          new[] { "Batch", "วันที่", "วัตถุดิบเข้า", "ผลิตออก", "%Yield", "สถานะ" },
          new[]
          {
            Row(Cell("PROD-APR-01"), Cell("10 เม.ย. 2569"), Cell("3,600 กก."), Cell("2,650 กก."), Cell("73.6%"), Badge("บันทึกแล้ว", "success")),
            Row(Cell("PROD-APR-02"), Cell("12 เม.ย. 2569"), Cell("2,800 กก."), Cell("2,032 กก."), Cell("72.6%"), Badge("บันทึกแล้ว", "success")),
            Row(Cell("PROD-APR-03"), Cell("15 เม.ย. 2569"), Cell("3,000 กก."), Cell("2,154 กก."), Cell("71.8%"), Badge("ตรวจสอบเหตุผล", "warning"))
          },
          filters: new[] { "ทั้งหมด", "Yield ต่ำกว่า benchmark", "สัปดาห์นี้" },
          exportFormats: new[] { "Excel", "CSV" },
          paginationSummary: "แสดง 1-3 จาก 3 batch"
        ),
        Table(
          "รายการ movement ที่เกี่ยวข้อง",
          "ทั้งขายต่อและซื้อเพิ่มจากเอกสารเดียวกัน",
          new[] { "Ref", "ประเภท", "คู่ค้า", "ปริมาณ", "สถานะ" },
          new[]
          {
            Row(Cell("MV-APR-02"), Cell("ขายต่อ"), Cell("บริษัท ปลายทางซีฟู้ด จำกัด"), Cell("960 กก."), Badge("ยืนยันแล้ว", "info")),
            Row(Cell("MV-APR-03"), Cell("ซื้อเพิ่ม"), Cell("รอระบุคู่ค้า"), Cell("0 กก."), Badge("ร่าง", "secondary"))
          },
          filters: new[] { "ทั้งหมด", "ขายต่อ", "ซื้อเพิ่ม" },
          exportFormats: new[] { "Excel" },
          paginationSummary: "แสดง 1-2 จาก 2 รายการ"
        )
      },
      timeline: new[]
      {
        Event("บันทึก batch แรก", "สร้าง PROD-APR-01 จากวัตถุดิบ 3,600 กก.", "ผู้ประกอบการ", "10 เม.ย. 2569 10:30", "primary", new[] { "Production" }),
        Event("ระบบแจ้งเตือน Yield ต่ำ", "batch PROD-APR-03 ต่ำกว่า benchmark ที่อนุมัติไว้", "ระบบ", "15 เม.ย. 2569 15:20", "warning", new[] { "72%", "Alert" }),
        Event("บันทึกขายต่อ", "ขายต่อ 960 กก. ให้คู่ค้าภายในระบบ", "ผู้ประกอบการ", "16 เม.ย. 2569 08:55", "info", new[] { "Sell forward" })
      });
  }

  private static System65ScreenPageViewModel BuildRmbs2List(
    System65ScreenDefinition current,
    IReadOnlyList<System65ScreenDefinition> roleScreens)
  {
    return CreateModel(
      current,
      roleScreens,
      currentStatus: "พร้อมส่งออก",
      pageKicker: "รายการ RMBS2 และรายละเอียดรุ่นส่งออก",
      pageSummary: "แสดงรุ่นส่งออก, คงเหลือ, เลขอ้างอิงใบรับรองและข้อมูล export พร้อม export รายการเป็น Excel หรือ PDF",
      kpiCards: new[]
      {
        Kpi("รุ่นส่งออกเปิดอยู่", "7 รุ่น", "เป็นรุ่นที่ยังไม่ปิดงานส่งออก", "primary", "ri-archive-drawer-line"),
        Kpi("เชื่อมใบรับรองแล้ว", "5 รุ่น", "มีเลขอ้างอิง PS หรือ HC ครบ", "success", "ri-award-line"),
        Kpi("คงเหลือพร้อมจัดส่ง", "22.6 ตัน", "ยอดรวมของทุก lot ที่ยัง active", "info", "ri-ship-2-line")
      },
      primaryActions: new[]
      {
        Action("ออกใบรับรองใหม่", RouteOf(roleScreens, "OP-10"), "ri-file-edit-line", "primary"),
        Action("Export รายการ", current.Route, "ri-download-2-line", "success", true)
      },
      formSections: new[]
      {
        Form(
          "ตัวกรองรุ่นส่งออก",
          "ค้นหารายการ lot ที่เกี่ยวข้องกับใบรับรองหรือประเทศปลายทาง",
          new[]
          {
            Field("เลขรุ่นส่งออก", "RMBS2-2604", placeholder: "พิมพ์บางส่วนของเลข lot", columnSpan: 4),
            Field("ประเทศ", "ทั้งหมด", type: "select", columnSpan: 4, options: new[] { "ทั้งหมด", "ญี่ปุ่น", "เกาหลีใต้", "สหรัฐอเมริกา" }),
            Field("สถานะ lot", "พร้อมส่งออก", type: "select", columnSpan: 4, options: new[] { "ทั้งหมด", "พร้อมส่งออก", "รอใบรับรอง", "ปิดงาน" })
          })
      },
      detailPanels: new[]
      {
        Panel(
          "lot ที่เลือก",
          "สรุปข้อมูล lot RMBS2 ล่าสุด",
          "success",
          new[]
          {
            Detail("เลข lot", "RMBS2-2604-001", "info"),
            Detail("น้ำหนักคงเหลือ", "6,800 กก.", "success"),
            Detail("ใบรับรองอ้างอิง", "CERT-PS-2604118", "primary"),
            Detail("ประเทศปลายทาง", "ญี่ปุ่น", "secondary")
          }),
        Panel(
          "โครงสร้างคงเหลือ",
          "กระจายน้ำหนักตามสถานะ lot",
          "info",
          new[]
          {
            Detail("พร้อมส่งออก", "15.4 ตัน", "success"),
            Detail("รอออกใบรับรอง", "4.9 ตัน", "warning"),
            Detail("สำรองหลังตรวจปล่อย", "2.3 ตัน", "info")
          },
          progressValue: 68,
          progressLabel: "สัดส่วน lot ที่พร้อมออกใบรับรอง")
      },
      tableSections: new[]
      {
        Table(
          "รายการ RMBS2",
          "List รองรับ sort, filter, pagination และ export",
          new[] { "เลข lot", "สินค้า", "คงเหลือ", "ประเทศ", "สถานะ", "อัปเดต" },
          new[]
          {
            Row(Cell("RMBS2-2604-001"), Cell("ปลาทูน่าแช่แข็ง"), Cell("6,800 กก."), Cell("ญี่ปุ่น"), Badge("พร้อมส่งออก", "success"), Cell("16 เม.ย. 2569")),
            Row(Cell("RMBS2-2604-002"), Cell("ปลาทูน่าหั่นชิ้น"), Cell("4,200 กก."), Cell("เกาหลีใต้"), Badge("รอใบรับรอง", "warning"), Cell("15 เม.ย. 2569")),
            Row(Cell("RMBS2-2604-003"), Cell("กุ้งปรุงสุก"), Cell("3,500 กก."), Cell("สหรัฐอเมริกา"), Badge("พร้อมส่งออก", "success"), Cell("15 เม.ย. 2569"))
          },
          filters: new[] { "พร้อมส่งออก", "รอใบรับรอง", "ญี่ปุ่น" },
          exportFormats: new[] { "Excel", "PDF" },
          paginationSummary: "แสดง 1-3 จาก 7 lot"
        ),
        Table(
          "เลขอ้างอิงใบรับรอง / HC",
          "ใช้ดูการเชื่อมโยง lot กับเอกสารปลายทาง",
          new[] { "เลข lot", "เลขใบรับรอง", "HC", "สถานะ" },
          new[]
          {
            Row(Cell("RMBS2-2604-001"), Cell("CERT-PS-2604118"), Cell("HC-2604108"), Badge("จับคู่แล้ว", "success")),
            Row(Cell("RMBS2-2604-002"), Cell("CERT-DRAFT-029"), Cell("-"), Badge("รออนุมัติ", "warning"))
          },
          filters: new[] { "ทั้งหมด", "มี HC", "รออนุมัติ" },
          exportFormats: new[] { "Excel" },
          paginationSummary: "แสดง 1-2 จาก 5 รายการ"
        )
      },
      timeline: new[]
      {
        Event("สร้าง lot RMBS2", "ระบบสร้าง RMBS2-2604-001 จากข้อมูลการผลิตล่าสุด", "ระบบ", "14 เม.ย. 2569 10:05", "primary", new[] { "RMBS2" }),
        Event("จับคู่ใบรับรอง", "ผูก CERT-PS-2604118 กับ lot ส่งออกญี่ปุ่น", "ผู้ประกอบการ", "15 เม.ย. 2569 11:40", "success", new[] { "Certificate" }),
        Event("ส่งออกข้อมูล", "ผู้ใช้กด export รายการ lot เพื่อตรวจสอบกับทีม shipping", "ผู้ประกอบการ", "วันนี้ 09:42", "info", new[] { "Export" })
      });
  }

  private static System65ScreenPageViewModel BuildP1P2Trade(
    System65ScreenDefinition current,
    IReadOnlyList<System65ScreenDefinition> roleScreens)
  {
    return CreateModel(
      current,
      roleScreens,
      currentStatus: "มี 2 รายการรอยืนยัน",
      pageKicker: "ซื้อขายผลิตภัณฑ์ P1/P2",
      pageSummary: "ซื้อหรือขายผลิตภัณฑ์, ตรวจ stock balance และเงื่อนไข lock พร้อมเชื่อมโยง P1/P2 กับ RMBS และใบรับรอง",
      kpiCards: new[]
      {
        Kpi("P1 คงเหลือ", "7.8 ตัน", "พร้อมเปิดคำสั่งขายในระบบ", "success", "ri-shopping-basket-line"),
        Kpi("P2 คงเหลือ", "5.1 ตัน", "มี 1 lot ที่ถูก lock เพราะอยู่ระหว่างออกใบรับรอง", "warning", "ri-box-1-line"),
        Kpi("คำสั่งซื้อขายเปิดอยู่", "6 รายการ", "รวมซื้อและขายภายในระบบ", "primary", "ri-exchange-dollar-line")
      },
      alerts: new[]
      {
        Alert("warning", "เงื่อนไข lock", "Lot P2-APR-02 ถูก lock ไว้เพราะเชื่อมกับคำขอใบรับรอง CERT-DRAFT-029"),
        Alert("info", "Traceability", "ทุกคำสั่งซื้อขาย mock จะมีเลขอ้างอิงย้อนกลับไปยัง RMBS/Certificate เพื่อใช้ใน OP-18")
      },
      primaryActions: new[]
      {
        Action("สร้างคำสั่งซื้อขาย", current.Route, "ri-add-circle-line", "primary"),
        Action("ดู traceability", RouteOf(roleScreens, "OP-18"), "ri-git-merge-line", "info", true),
        Action("ขอใบรับรอง", RouteOf(roleScreens, "OP-10"), "ri-award-line", "success", true)
      },
      formSections: new[]
      {
        Form(
          "ข้อมูลคำสั่งซื้อขาย",
          "กรอกประเภทสินค้าและคู่ค้า",
          new[]
          {
            Field("ประเภทคำสั่ง", "ขาย", type: "select", columnSpan: 4, options: new[] { "ซื้อ", "ขาย" }),
            Field("สินค้า", "P2 ปลาทูน่าหั่นชิ้น", type: "select", columnSpan: 4, options: new[] { "P1 ปลาทูน่า whole round", "P2 ปลาทูน่าหั่นชิ้น", "P2 กุ้งปรุงสุก" }),
            Field("คู่ค้า", "บริษัท ซากุระเทรดดิ้ง", type: "select", columnSpan: 4, options: new[] { "บริษัท ซากุระเทรดดิ้ง", "บริษัท Ocean Hub", "ผู้ประกอบการในระบบ 6_5" }),
            Field("ปริมาณ", "1,250", type: "number", suffix: "กก.", columnSpan: 4),
            Field("Lot อ้างอิง", "P2-APR-02", type: "select", columnSpan: 4, options: new[] { "P2-APR-01", "P2-APR-02", "P1-APR-03" }),
            Field("เงื่อนไขการล็อก", "ตรวจแล้วไม่มีการล็อกเพิ่ม", type: "readonly", columnSpan: 4),
            Field("รายละเอียดเพิ่มเติม", "ขายให้คู่ค้าเพื่อใช้ใน lot ส่งออกเกาหลีใต้ พร้อมแนบเลข certificate อ้างอิง", type: "textarea", columnSpan: 12)
          })
      },
      detailPanels: new[]
      {
        Panel(
          "Stock balance",
          "คงเหลือก่อนและหลังสร้างคำสั่งซื้อขาย",
          "primary",
          new[]
          {
            Detail("ก่อนทำรายการ", "5,100 กก.", "info"),
            Detail("จองขายครั้งนี้", "1,250 กก.", "warning"),
            Detail("คงเหลือหลังทำรายการ", "3,850 กก.", "success"),
            Detail("เงื่อนไข lock", "1 lot ติด lock", "danger")
          },
          progressValue: 75,
          progressLabel: "สัดส่วนสินค้า P2 ที่ยังพร้อมขาย"),
        Panel(
          "การเชื่อมโยงอ้างอิง",
          "แสดงเส้นทางย้อนกลับไปยัง RMBS และใบรับรอง",
          "info",
          new[]
          {
            Detail("RMBS ต้นทาง", "RMBS1-PS-APR-04", "primary"),
            Detail("Certificate", "CERT-DRAFT-029", "warning"),
            Detail("ผู้ขายในระบบ", "โรงงานสมุทรสาคร 01", "secondary")
          })
      },
      tableSections: new[]
      {
        Table(
          "ทะเบียนซื้อขาย P1/P2",
          "รองรับ list, sort, filter, pagination และ export",
          new[] { "Ref", "ประเภท", "สินค้า", "คู่ค้า", "ปริมาณ", "สถานะ" },
          new[]
          {
            Row(Cell("TRD-APR-014"), Cell("ขาย"), Cell("P2 ปลาทูน่าหั่นชิ้น"), Cell("บริษัท ซากุระเทรดดิ้ง"), Cell("1,250 กก."), Badge("รอยืนยัน", "info")),
            Row(Cell("TRD-APR-011"), Cell("ซื้อ"), Cell("P1 ปลาทูน่า whole round"), Cell("บริษัท Ocean Hub"), Cell("2,000 กก."), Badge("อนุมัติ", "success")),
            Row(Cell("TRD-APR-010"), Cell("ขาย"), Cell("P2 กุ้งปรุงสุก"), Cell("ผู้ประกอบการในระบบ 6_5"), Cell("900 กก."), Badge("ติด lock", "warning"))
          },
          filters: new[] { "ทั้งหมด", "รอยืนยัน", "ติด lock" },
          exportFormats: new[] { "Excel", "CSV", "PDF" },
          paginationSummary: "แสดง 1-3 จาก 6 รายการ"
        ),
        Table(
          "Stock balance by lot",
          "คงเหลือสินค้าแต่ละ lot หลังหักคำสั่งซื้อขาย",
          new[] { "Lot", "คงเหลือ", "Lock", "Certificate Ref", "สถานะ" },
          new[]
          {
            Row(Cell("P2-APR-01"), Cell("2,600 กก."), Cell("ไม่มี"), Cell("CERT-PS-2604118"), Badge("พร้อมขาย", "success")),
            Row(Cell("P2-APR-02"), Cell("2,500 กก."), Cell("มี"), Cell("CERT-DRAFT-029"), Badge("ติด lock", "warning"))
          },
          filters: new[] { "พร้อมขาย", "ติด lock" },
          exportFormats: new[] { "Excel" },
          paginationSummary: "แสดง 1-2 จาก 2 lot"
        )
      },
      timeline: new[]
      {
        Event("เปิดคำสั่งซื้อขายใหม่", "สร้าง TRD-APR-014 สำหรับ lot P2-APR-02", "ผู้ประกอบการ", "วันนี้ 09:12", "primary", new[] { "TRD-APR-014" }),
        Event("ระบบตรวจเงื่อนไข lock", "พบว่า lot อ้างอิงเชื่อมกับคำขอใบรับรอง draft", "ระบบ", "วันนี้ 09:13", "warning", new[] { "Lock" }),
        Event("ยืนยันข้อมูลคู่ค้า", "อัปเดตข้อมูลผู้รับซื้อและปริมาณที่ต้องการขาย", "ผู้ประกอบการ", "วันนี้ 09:20", "info", new[] { "คู่ค้า" })
      });
  }

  private static System65ScreenPageViewModel BuildYieldRequest(
    System65ScreenDefinition current,
    IReadOnlyList<System65ScreenDefinition> roleScreens)
  {
    return CreateModel(
      current,
      roleScreens,
      currentStatus: "ส่งคำขอแล้ว",
      pageKicker: "คำขอพิจารณาอนุมัติ %Yield",
      pageSummary: "ยื่นคำขอ %Yield, ขอแก้ไขผลอนุมัติ และแนบไฟล์หลักฐานหรือนำเข้า template สำหรับรายการวัตถุดิบ/ผลผลิต",
      kpiCards: new[]
      {
        Kpi("Yield ที่ขออนุมัติ", "74.8%", "สำหรับปลาหมึกหอม lot YD-24", "primary", "ri-percent-line"),
        Kpi("รายการนำเข้าจาก template", "12 แถว", "ผ่านการตรวจ mock 11 แถว", "info", "ri-file-excel-2-line"),
        Kpi("ไฟล์หลักฐาน", "4 ไฟล์", "ภาพผลิตจริงและรายงาน QC", "success", "ri-attachment-2")
      },
      alerts: new[]
      {
        Alert("info", "Template import", "ระบบ mock นำเข้าข้อมูลจากไฟล์ yield-template-apr.xlsx แล้วสร้างตาราง preview ด้านล่าง"),
        Alert("warning", "ข้อมูลแถวที่ 11", "พบค่า yield สูงกว่าขอบเขต benchmark ควรใส่คำอธิบายเพิ่มเติมก่อนส่งคำขอแก้ไข")
      },
      primaryActions: new[]
      {
        Action("Import template", current.Route, "ri-upload-cloud-2-line", "primary"),
        Action("บันทึกร่าง", current.Route, "ri-save-line", "secondary", true),
        Action("กลับหน้าการผลิต", RouteOf(roleScreens, "OP-06"), "ri-arrow-left-line", "info", true)
      },
      formSections: new[]
      {
        Form(
          "ข้อมูลคำขอ %Yield",
          "ระบุสินค้า, lot และเอกสารประกอบ",
          new[]
          {
            Field("เลขคำขอ", "YD-24", type: "readonly", columnSpan: 4),
            Field("สินค้า", "ปลาหมึกหอมแช่แข็ง", type: "select", columnSpan: 4, options: new[] { "ปลาหมึกหอมแช่แข็ง", "ปลาทูน่าครีบเหลือง", "กุ้งขาวปรุงสุก" }),
            Field("lot อ้างอิง", "PROD-APR-03", type: "select", columnSpan: 4, options: new[] { "PROD-APR-03", "PROD-APR-04" }),
            Field("Yield ที่ขอ", "74.8", type: "number", suffix: "%", columnSpan: 4),
            Field("ไฟล์ template", "yield-template-apr.xlsx", type: "file", columnSpan: 4),
            Field("หลักฐานเพิ่มเติม", "qc-report.pdf | production-photo-1.jpg | production-photo-2.jpg", type: "file", columnSpan: 4),
            Field("คำชี้แจง", "ค่า yield สูงขึ้นจากการปรับ cutting standard และลดเศษเสียในไลน์ผลิตช่วงสัปดาห์นี้", type: "editor", columnSpan: 12)
          })
      },
      detailPanels: new[]
      {
        Panel(
          "ผลอนุมัติล่าสุด",
          "ใช้เทียบกับคำขอที่กำลังยื่น",
          "success",
          new[]
          {
            Detail("benchmark เดิม", "72.5%", "warning"),
            Detail("คำขอใหม่", "74.8%", "primary"),
            Detail("ส่วนต่าง", "+2.3%", "success"),
            Detail("สถานะ", "ส่งคำขอแล้ว", "info")
          }),
        Panel(
          "หมายเหตุจากการตรวจ mock",
          "จุดที่ต้องตรวจเพิ่มก่อนอนุมัติ",
          "warning",
          new[]
          {
            Detail("แถวผิดปกติ", "1 แถว", "warning"),
            Detail("รูปภาพไลน์ผลิต", "ครบ 2 รูป", "success"),
            Detail("QC report", "แนบแล้ว", "success")
          })
      },
      tableSections: new[]
      {
        Table(
          "Preview รายการจาก template",
          "แสดงแถวข้อมูลที่ import เข้ามาเพื่อตรวจสอบก่อนส่งคำขอ",
          new[] { "แถว", "วัตถุดิบเข้า", "ผลผลิต", "%Yield", "สถานะ" },
          new[]
          {
            Row(Cell("1"), Cell("420 กก."), Cell("314 กก."), Cell("74.8%"), Badge("ผ่าน", "success")),
            Row(Cell("2"), Cell("415 กก."), Cell("309 กก."), Cell("74.5%"), Badge("ผ่าน", "success")),
            Row(Cell("11"), Cell("430 กก."), Cell("332 กก."), Cell("77.2%"), Badge("ตรวจเพิ่ม", "warning"))
          },
          filters: new[] { "ทั้งหมด", "ผ่าน", "ตรวจเพิ่ม" },
          exportFormats: new[] { "Excel" },
          paginationSummary: "แสดง 1-3 จาก 12 แถว"
        )
      },
      timeline: new[]
      {
        Event("นำเข้า template", "อัปโหลด yield-template-apr.xlsx และสร้าง preview data", "ผู้ประกอบการ", "14 เม.ย. 2569 11:12", "primary", new[] { "Import" }),
        Event("แนบหลักฐาน QC", "เพิ่มรายงาน QC และภาพการผลิตจริง", "ผู้ประกอบการ", "14 เม.ย. 2569 11:28", "info", new[] { "QC report" }),
        Event("ส่งคำขอ %Yield", "ระบบบันทึกคำขอ YD-24 เพื่อรอเจ้าหน้าที่พิจารณา", "ผู้ประกอบการ", "14 เม.ย. 2569 12:05", "success", new[] { "ส่งคำขอแล้ว" })
      });
  }

  private static System65ScreenPageViewModel BuildCertificateRequest(
    System65ScreenDefinition current,
    IReadOnlyList<System65ScreenDefinition> roleScreens)
  {
    return CreateModel(
      current,
      roleScreens,
      currentStatus: "ร่าง",
      pageKicker: "ขอใบรับรอง PS/NMD",
      pageSummary: "เขียนคำขอ, Save as, ลงนามอิเล็กทรอนิกส์แบบ mock, กรอก HC, ตรวจข้อมูลซ้ำ และสร้าง draft เพื่อส่งต่อเจ้าหน้าที่",
      kpiCards: new[]
      {
        Kpi("Draft ใบรับรอง", "3 ฉบับ", "รวมคำขอที่ยังไม่ส่งเจ้าหน้าที่", "primary", "ri-file-text-line"),
        Kpi("HC line", "5 รายการ", "มี 1 รายการซ้ำกับคำขอเดิม", "warning", "ri-shield-cross-line"),
        Kpi("ปลายทาง", "2 ประเทศ", "ญี่ปุ่นและเกาหลีใต้", "info", "ri-global-line")
      },
      alerts: new[]
      {
        Alert("info", "e-Sign mock", "การลงนามในหน้าจอนี้เป็น mock interaction เท่านั้น ไม่มีการสร้างลายเซ็นจริง"),
        Alert("warning", "ตรวจข้อมูลซ้ำ", "HC line 03 ตรงกับคำขอ CERT-PS-2604101 ควรตรวจว่าต้องการ re-use หรือแก้ไขข้อมูล")
      },
      workflowChips: new[]
      {
        Chip("Draft", "กำลังจัดทำ", "secondary", true),
        Chip("Save As", "พร้อมใช้", "info"),
        Chip("e-Sign", "mock", "warning"),
        Chip("Officer review", "รอส่ง", "secondary")
      },
      primaryActions: new[]
      {
        Action("Save as Draft", current.Route, "ri-save-3-line", "primary"),
        Action("พรีวิว PDF", current.Route, "ri-file-pdf-line", "danger", true),
        Action("ติดตามคำขอ", RouteOf(roleScreens, "OP-11"), "ri-route-line", "info", true),
        Action("ค้นหาใบรับรอง", RouteOf(roleScreens, "OP-12"), "ri-search-eye-line", "success", true)
      },
      formSections: new[]
      {
        Form(
          "ข้อมูลหัวคำขอ",
          "ระบุเลข draft, วันที่ยื่น และประเภทใบรับรอง",
          new[]
          {
            Field("เลข draft", "CERT-DRAFT-029", type: "readonly", columnSpan: 4),
            Field("ประเภทใบรับรอง", "PS / NMD + HC", type: "select", columnSpan: 4, required: true, options: new[] { "PS", "PS / NMD + HC", "HC" }),
            Field("วันที่ยื่น", "2026-04-16", type: "date", columnSpan: 4, required: true),
            Field("ผู้รับสินค้า (Consignee)", "Sakura Marine Foods Co., Ltd.", columnSpan: 6, required: true),
            Field("ท่าเรือปลายทาง", "Tokyo Port", columnSpan: 3),
            Field("ประเทศปลายทาง", "ญี่ปุ่น", type: "select", columnSpan: 3, options: new[] { "ญี่ปุ่น", "เกาหลีใต้", "สหรัฐอเมริกา" })
          }),
        Form(
          "HC และ e-Sign",
          "ควบคุมข้อมูลสุขอนามัยและ mock signing",
          new[]
          {
            Field("HC line 01", "Frozen yellowfin tuna loin", columnSpan: 6),
            Field("HC line 02", "Frozen yellowfin tuna steak", columnSpan: 6),
            Field("HC line 03", "Frozen yellowfin tuna chunk", columnSpan: 6, helperText: "แถวนี้ถูกแจ้งว่าซ้ำกับคำขอเดิม"),
            Field("ไฟล์แนบ", "packing-list-ps.xlsx | invoice-ps-04.pdf | label-photo.zip", type: "file", columnSpan: 6),
            Field("ลงนามอิเล็กทรอนิกส์ (mock)", "true", type: "toggle", columnSpan: 4),
            Field("บันทึกประกอบ", "ขอให้เจ้าหน้าที่ใช้ template ญี่ปุ่น revision 2026-Q2", type: "editor", columnSpan: 8)
          })
      },
      detailPanels: new[]
      {
        Panel(
          "ผลตรวจข้อมูลซ้ำ",
          "แสดงจุดเสี่ยงก่อนส่งคำขอ",
          "warning",
          new[]
          {
            Detail("HC line 03", "ซ้ำกับ CERT-PS-2604101", "warning"),
            Detail("เลข invoice", "ไม่ซ้ำ", "success"),
            Detail("ประเทศปลายทาง", "ตรงกับเอกสาร sales contract", "success"),
            Detail("สถานะ draft", "พร้อมส่งหลังแก้ 1 จุด", "info")
          }),
        Panel(
          "ทางเลือกใบรับรอง",
          "สรุปประเภทเอกสารที่จะออกจากคำขอนี้",
          "info",
          new[]
          {
            Detail("PS", "ออกทันทีหลังอนุมัติ", "primary"),
            Detail("NMD", "ใช้กรณีตลาดกำหนดเพิ่ม", "secondary"),
            Detail("HC", "แนบ 5 line item", "success")
          },
          progressValue: 80,
          progressLabel: "ความพร้อมของคำขอใบรับรอง")
      },
      tableSections: new[]
      {
        Table(
          "รายการสินค้าในใบรับรอง",
          "ใช้ตรวจข้อมูลสินค้าและน้ำหนักสุทธิก่อนสร้าง draft",
          new[] { "สินค้า", "น้ำหนัก", "lot อ้างอิง", "ปลายทาง", "สถานะ" },
          new[]
          {
            Row(Cell("Frozen yellowfin tuna loin"), Cell("3,400 กก."), Cell("RMBS2-2604-001"), Cell("ญี่ปุ่น"), Badge("พร้อมออกใบรับรอง", "success")),
            Row(Cell("Frozen yellowfin tuna steak"), Cell("2,100 กก."), Cell("RMBS2-2604-002"), Cell("เกาหลีใต้"), Badge("รอตรวจ HC", "warning"))
          },
          filters: new[] { "ทั้งหมด", "พร้อมออกใบรับรอง", "รอตรวจ HC" },
          exportFormats: new[] { "Excel", "PDF" },
          paginationSummary: "แสดง 1-2 จาก 2 รายการ"
        )
      },
      timeline: new[]
      {
        Event("สร้าง draft ใบรับรอง", "ระบบ mock สร้างเลข CERT-DRAFT-029", "ผู้ประกอบการ", "วันนี้ 08:10", "primary", new[] { "Draft" }),
        Event("เพิ่ม HC line", "กรอกข้อมูลสุขอนามัยครบ 5 line item", "ผู้ประกอบการ", "วันนี้ 08:24", "info", new[] { "HC" }),
        Event("ตรวจข้อมูลซ้ำ", "พบ HC line 03 ซ้ำกับคำขอเดิมในระบบ", "ระบบ", "วันนี้ 08:26", "warning", new[] { "Duplicate Check" })
      },
      pageNotes: new[] { "ครอบคลุม requirement 1.7(1)-(23) ในรูปแบบ mock UI เท่านั้น" });
  }

  private static System65ScreenPageViewModel BuildCertificateTrack(
    System65ScreenDefinition current,
    IReadOnlyList<System65ScreenDefinition> roleScreens)
  {
    return CreateModel(
      current,
      roleScreens,
      currentStatus: "แจ้งแก้ไข",
      pageKicker: "ติดตามคำขอใบรับรองและหลังตรวจปล่อย",
      pageSummary: "ติดตามสถานะคำขอ, ปรับน้ำหนักหลังตรวจปล่อย, แก้ไขตามที่เจ้าหน้าที่แจ้ง และส่งกลับเพื่อพิจารณาได้จากหน้าจอเดียว",
      kpiCards: new[]
      {
        Kpi("คำขอเปิดอยู่", "5 ฉบับ", "อยู่ระหว่างรอเจ้าหน้าที่หรือรอแก้ไข", "primary", "ri-file-list-3-line"),
        Kpi("ต้องแก้ไข", "2 ฉบับ", "มี comment จากเจ้าหน้าที่แนบใน timeline", "warning", "ri-error-warning-line"),
        Kpi("หลังตรวจปล่อย", "1 ฉบับ", "ต้องปรับน้ำหนักให้ตรง inspection result", "info", "ri-scales-line")
      },
      alerts: new[]
      {
        Alert("warning", "ตรวจปล่อยล่าสุด", "CERT-DRAFT-029 ต้องปรับน้ำหนักสุทธิจาก 5,500 กก. เหลือ 5,430 กก. ก่อนส่งกลับ"),
        Alert("info", "โหมดติดตามคำขอ", "เลือกคำขอที่สนใจจากตารางด้านล่างเพื่ออ่าน timeline และรายการแก้ไขจำลอง" )
      },
      primaryActions: new[]
      {
        Action("ส่งกลับเพื่อพิจารณา", current.Route, "ri-send-plane-line", "success"),
        Action("ค้นหาใบรับรอง", RouteOf(roleScreens, "OP-12"), "ri-search-eye-line", "info", true),
        Action("ขอแก้ไขใบรับรอง", RouteOf(roleScreens, "OP-13"), "ri-edit-2-line", "warning", true)
      },
      formSections: new[]
      {
        Form(
          "ปรับน้ำหนักหลังตรวจปล่อย",
          "ใช้เฉพาะกรณีมีผลตรวจจริงต่างจากข้อมูล draft",
          new[]
          {
            Field("เลขคำขอ", "CERT-DRAFT-029", type: "select", columnSpan: 4, options: new[] { "CERT-DRAFT-029", "CERT-PS-2604118", "CERT-NMD-2604104" }),
            Field("น้ำหนักเดิม", "5,500", type: "number", suffix: "กก.", columnSpan: 4),
            Field("น้ำหนักหลังตรวจปล่อย", "5,430", type: "number", suffix: "กก.", columnSpan: 4),
            Field("สาเหตุการปรับ", "น้ำหนักหลังชั่งจริงลดลงตาม inspection report", type: "textarea", columnSpan: 12)
          })
      },
      detailPanels: new[]
      {
        Panel(
          "สถานะคำขอที่เลือก",
          "แสดงสรุปสถานะและ comment ล่าสุดจากเจ้าหน้าที่",
          "warning",
          new[]
          {
            Detail("คำขอ", "CERT-DRAFT-029", "info"),
            Detail("สถานะ", "แจ้งแก้ไข", "warning"),
            Detail("เหตุผลล่าสุด", "ต้องปรับน้ำหนักสุทธิหลังตรวจปล่อย", "danger"),
            Detail("กำหนดส่งกลับ", "17 เม.ย. 2569", "secondary")
          }),
        Panel(
          "ประเด็นตรวจปล่อย",
          "สรุปรายการที่ต้องแก้ไขก่อนส่งกลับ",
          "info",
          new[]
          {
            Detail("น้ำหนักสุทธิ", "ปรับจาก 5,500 เป็น 5,430 กก.", "warning"),
            Detail("รูปแบบ HC", "คงเดิม", "success"),
            Detail("ไฟล์แนบ", "inspection-report.pdf", "primary")
          })
      },
      tableSections: new[]
      {
        Table(
          "รายการคำขอใบรับรอง",
          "List พร้อม filter และ export สำหรับติดตามภาพรวม",
          new[] { "เลขคำขอ", "ปลายทาง", "ประเภท", "สถานะ", "อัปเดตล่าสุด" },
          new[]
          {
            Row(Cell("CERT-DRAFT-029"), Cell("ญี่ปุ่น"), Cell("PS / HC"), Badge("แจ้งแก้ไข", "warning"), Cell("16 เม.ย. 2569 08:46")),
            Row(Cell("CERT-PS-2604118"), Cell("ญี่ปุ่น"), Cell("PS"), Badge("อนุมัติ", "success"), Cell("15 เม.ย. 2569 11:40")),
            Row(Cell("CERT-NMD-2604104"), Cell("เกาหลีใต้"), Cell("NMD / HC"), Badge("ส่งคำขอแล้ว", "info"), Cell("14 เม.ย. 2569 17:10"))
          },
          filters: new[] { "ทั้งหมด", "แจ้งแก้ไข", "หลังตรวจปล่อย" },
          exportFormats: new[] { "Excel", "CSV" },
          paginationSummary: "แสดง 1-3 จาก 5 รายการ"
        )
      },
      timeline: new[]
      {
        Event("ส่งคำขอใบรับรอง", "ส่ง draft CERT-DRAFT-029 ให้เจ้าหน้าที่ตรวจ", "ผู้ประกอบการ", "15 เม.ย. 2569 09:30", "primary", new[] { "ส่งคำขอแล้ว" }),
        Event("ผลตรวจปล่อย", "inspection report แจ้งให้ปรับน้ำหนักสุทธิ", "เจ้าหน้าที่", "16 เม.ย. 2569 08:40", "warning", new[] { "หลังตรวจปล่อย" }),
        Event("รอส่งกลับ", "ผู้ประกอบการเปิดหน้าติดตามเพื่อแก้ไขข้อมูลและส่งกลับ", "ผู้ประกอบการ", "วันนี้ 09:10", "info", new[] { "แก้ไข" })
      });
  }

  private static System65ScreenPageViewModel BuildCertificateSearch(
    System65ScreenDefinition current,
    IReadOnlyList<System65ScreenDefinition> roleScreens)
  {
    return CreateModel(
      current,
      roleScreens,
      currentStatus: "ค้นหาได้ 3 ฉบับ",
      pageKicker: "ค้นหาและตรวจสอบใบรับรอง",
      pageSummary: "ค้นหาใบรับรอง, แสดงผลจาก QR verification, ดูข้อมูลอ้างอิง RMBS/P1/P2 และ export ผลลัพธ์เป็น Excel",
      kpiCards: new[]
      {
        Kpi("ใบรับรอง active", "48 ฉบับ", "ค้นหาได้จากเลขที่ใบรับรองหรือ QR mock", "success", "ri-qr-scan-2-line"),
        Kpi("QR verified", "12 ครั้ง", "สะสมในสัปดาห์ปัจจุบัน", "info", "ri-qr-code-line"),
        Kpi("พบสถานะไม่พร้อมใช้", "2 ฉบับ", "เป็นใบรับรองที่ถูกยกเลิกหรือหมดอายุ", "warning", "ri-shield-check-line")
      },
      primaryActions: new[]
      {
        Action("Export Excel", current.Route, "ri-file-excel-2-line", "success"),
        Action("คำขอเปลี่ยนแปลง", RouteOf(roleScreens, "OP-13"), "ri-edit-box-line", "warning", true),
        Action("ขอใบแทน", RouteOf(roleScreens, "OP-14"), "ri-file-copy-line", "info", true)
      },
      formSections: new[]
      {
        Form(
          "ค้นหาใบรับรอง / QR Verification",
          "ค้นหาได้จากเลขใบรับรอง, ref lot, ประเทศ หรือผลจาก QR mock",
          new[]
          {
            Field("เลขใบรับรอง", "CERT-PS-2604118", columnSpan: 4),
            Field("QR payload", "QR:CERT-PS-2604118|RMBS2-2604-001", columnSpan: 4),
            Field("สถานะ", "ทั้งหมด", type: "select", columnSpan: 4, options: new[] { "ทั้งหมด", "อนุมัติ", "ไม่อนุมัติ", "ยกเลิก" }),
            Field("ประเทศ", "ญี่ปุ่น", type: "select", columnSpan: 4, options: new[] { "ทั้งหมด", "ญี่ปุ่น", "เกาหลีใต้", "สหรัฐอเมริกา" }),
            Field("RMBS/P1/P2 Ref", "RMBS2-2604-001", columnSpan: 4),
            Field("ช่วงเวลา", "2026-04-01 ถึง 2026-04-16", type: "readonly", columnSpan: 4)
          })
      },
      detailPanels: new[]
      {
        Panel(
          "ผล QR Verification",
          "ผลตรวจจาก QR mock ของใบรับรองที่เลือก",
          "success",
          new[]
          {
            Detail("เลขใบรับรอง", "CERT-PS-2604118", "primary"),
            Detail("ผลตรวจ", "Valid", "success"),
            Detail("lot อ้างอิง", "RMBS2-2604-001", "info"),
            Detail("สถานะล่าสุด", "อนุมัติ", "success")
          }),
        Panel(
          "ข้อมูลอ้างอิงย้อนหลัง",
          "เชื่อมโยงเอกสารต้นทางเพื่อใช้ตรวจสอบย้อนกลับ",
          "info",
          new[]
          {
            Detail("RMBS1", "PS-260412", "primary"),
            Detail("P1/P2", "P2-APR-02", "secondary"),
            Detail("ผู้ประกอบการ", "โรงงานสมุทรสาคร 01", "secondary")
          })
      },
      tableSections: new[]
      {
        Table(
          "ผลการค้นหาใบรับรอง",
          "รองรับ sort, filter, pagination และ export",
          new[] { "เลขใบรับรอง", "lot อ้างอิง", "ปลายทาง", "ประเภท", "สถานะ" },
          new[]
          {
            Row(Cell("CERT-PS-2604118"), Cell("RMBS2-2604-001"), Cell("ญี่ปุ่น"), Cell("PS"), Badge("อนุมัติ", "success")),
            Row(Cell("CERT-NMD-2604104"), Cell("P2-APR-02"), Cell("เกาหลีใต้"), Cell("NMD / HC"), Badge("ส่งคำขอแล้ว", "info")),
            Row(Cell("CERT-PS-2604097"), Cell("RMBS2-2603-004"), Cell("สหรัฐอเมริกา"), Cell("PS"), Badge("ยกเลิก", "danger"))
          },
          filters: new[] { "ทั้งหมด", "อนุมัติ", "ยกเลิก" },
          exportFormats: new[] { "Excel", "CSV" },
          paginationSummary: "แสดง 1-3 จาก 48 รายการ"
        )
      },
      timeline: new[]
      {
        Event("ค้นหาจากเลขใบรับรอง", "ผู้ใช้ค้นหา CERT-PS-2604118 เพื่อดูสถานะล่าสุด", "ผู้ประกอบการ", "วันนี้ 10:02", "primary", new[] { "Search" }),
        Event("อ่าน QR mock", "ระบบ parse payload และตรวจผลกับรายการใน mock catalog", "ระบบ", "วันนี้ 10:03", "info", new[] { "QR Verification" }),
        Event("เตรียม export", "ผู้ใช้เตรียม export ผลลัพธ์ค้นหาเป็น Excel", "ผู้ประกอบการ", "วันนี้ 10:05", "success", new[] { "Export" })
      });
  }

  private static System65ScreenPageViewModel BuildCertificateAmend(
    System65ScreenDefinition current,
    IReadOnlyList<System65ScreenDefinition> roleScreens)
  {
    return CreateModel(
      current,
      roleScreens,
      currentStatus: "ส่งคำขอแล้ว",
      pageKicker: "คำขอเปลี่ยนแปลงใบรับรอง",
      pageSummary: "สร้างคำขอแก้ไขใบรับรอง, แนบหลักฐาน, ติดตามสถานะ และพรีวิวเอกสารฉบับแก้ไขแบบ mock",
      primaryActions: new[]
      {
        Action("พรีวิวฉบับแก้ไข", current.Route, "ri-file-pdf-line", "danger", true),
        Action("ติดตามคำขอ", RouteOf(roleScreens, "OP-11"), "ri-route-line", "info", true),
        Action("ค้นหาใบรับรอง", RouteOf(roleScreens, "OP-12"), "ri-search-line", "success", true)
      },
      formSections: new[]
      {
        Form(
          "รายละเอียดคำขอแก้ไข",
          "ระบุใบรับรองเดิม เหตุผล และเอกสารประกอบ",
          new[]
          {
            Field("ใบรับรองเดิม", "CERT-PS-2604118", type: "select", columnSpan: 4, options: new[] { "CERT-PS-2604118", "CERT-NMD-2604104" }),
            Field("หมวดการแก้ไข", "แก้ consignee", type: "select", columnSpan: 4, options: new[] { "แก้ consignee", "แก้น้ำหนัก", "แก้ lot อ้างอิง" }),
            Field("วันที่ยื่น", "2026-04-16", type: "date", columnSpan: 4),
            Field("เหตุผลการแก้ไข", "ผู้รับสินค้าแจ้งแก้ชื่อบริษัทให้ตรงกับ registration ล่าสุด", type: "editor", columnSpan: 12),
            Field("หลักฐาน", "new-registration.pdf | amendment-request-letter.docx", type: "file", columnSpan: 12)
          })
      },
      detailPanels: new[]
      {
        Panel(
          "Before / After",
          "สรุปความแตกต่างของใบรับรองเดิมและฉบับที่ขอแก้ไข",
          "info",
          new[]
          {
            Detail("Consignee เดิม", "Sakura Marine Food Co., Ltd.", "secondary"),
            Detail("Consignee ใหม่", "Sakura Marine Foods Co., Ltd.", "primary"),
            Detail("เอกสารที่เปลี่ยน", "PS ฉบับหลัก", "warning")
          }),
        Panel(
          "สถานะล่าสุด",
          "ใช้ติดตามคำขอแก้ไขจนกว่าจะออกเอกสารฉบับใหม่",
          "success",
          new[]
          {
            Detail("คำขอแก้ไข", "AMD-260416-01", "info"),
            Detail("สถานะ", "ส่งคำขอแล้ว", "success"),
            Detail("เอกสารแนบ", "2 ไฟล์", "secondary")
          })
      },
      tableSections: new[]
      {
        Table(
          "ประวัติคำขอแก้ไข",
          "รายการย้อนหลังของใบรับรองฉบับเดียวกัน",
          new[] { "Ref", "ใบรับรอง", "ประเด็น", "สถานะ", "อัปเดต" },
          new[]
          {
            Row(Cell("AMD-260416-01"), Cell("CERT-PS-2604118"), Cell("แก้ consignee"), Badge("ส่งคำขอแล้ว", "info"), Cell("16 เม.ย. 2569")),
            Row(Cell("AMD-260401-02"), Cell("CERT-PS-2604012"), Cell("แก้ lot อ้างอิง"), Badge("อนุมัติ", "success"), Cell("02 เม.ย. 2569"))
          },
          filters: new[] { "ทั้งหมด", "ส่งคำขอแล้ว", "อนุมัติ" },
          exportFormats: new[] { "Excel" },
          paginationSummary: "แสดง 1-2 จาก 2 รายการ"
        )
      },
      timeline: new[]
      {
        Event("เริ่มคำขอแก้ไข", "สร้าง AMD-260416-01 จากใบรับรอง CERT-PS-2604118", "ผู้ประกอบการ", "วันนี้ 10:20", "primary", new[] { "Amend" }),
        Event("แนบหลักฐาน", "อัปโหลด registration ใหม่และหนังสือคำขอ", "ผู้ประกอบการ", "วันนี้ 10:24", "info", new[] { "หลักฐาน" }),
        Event("ส่งให้เจ้าหน้าที่", "คำขออยู่ในสถานะส่งคำขอแล้วเพื่อรอพิจารณา", "ผู้ประกอบการ", "วันนี้ 10:31", "success", new[] { "ส่งคำขอแล้ว" })
      });
  }

  private static System65ScreenPageViewModel BuildCertificateReissue(
    System65ScreenDefinition current,
    IReadOnlyList<System65ScreenDefinition> roleScreens)
  {
    return CreateModel(
      current,
      roleScreens,
      currentStatus: "ร่าง",
      pageKicker: "คำขอใบรับรองทดแทนฉบับเก่า",
      pageSummary: "ยื่นคำขอออกใบแทนฉบับเดิม, ระบุเหตุผล, แนบหลักฐาน และติดตามผลได้จาก mock screen นี้",
      primaryActions: new[]
      {
        Action("บันทึกร่าง", current.Route, "ri-save-line", "primary"),
        Action("ติดตามคำขอ", RouteOf(roleScreens, "OP-11"), "ri-route-line", "info", true),
        Action("ค้นหาใบรับรอง", RouteOf(roleScreens, "OP-12"), "ri-search-line", "success", true)
      },
      formSections: new[]
      {
        Form(
          "รายละเอียดใบแทน",
          "กำหนดเหตุผลและเอกสารประกอบ",
          new[]
          {
            Field("ใบรับรองเดิม", "CERT-PS-2604012", type: "select", columnSpan: 4, options: new[] { "CERT-PS-2604012", "CERT-PS-2604118" }),
            Field("เหตุผล", "สูญหายระหว่างขนส่งเอกสาร", type: "select", columnSpan: 4, options: new[] { "สูญหายระหว่างขนส่งเอกสาร", "ชำรุด", "ข้อมูลพิมพ์ผิด" }),
            Field("วันที่ยื่น", "2026-04-16", type: "date", columnSpan: 4),
            Field("หลักฐาน", "incident-report.pdf | carrier-confirmation.pdf", type: "file", columnSpan: 12),
            Field("หมายเหตุเพิ่มเติม", "ขอให้ระบุว่าเป็นใบแทนฉบับเดิมและคงข้อมูล shipment เดิมทุกประการ", type: "textarea", columnSpan: 12)
          })
      },
      detailPanels: new[]
      {
        Panel(
          "ใบรับรองที่ถูกแทน",
          "สรุปข้อมูลเอกสารฉบับเดิม",
          "info",
          new[]
          {
            Detail("เลขใบรับรองเดิม", "CERT-PS-2604012", "primary"),
            Detail("ออกเมื่อ", "1 เม.ย. 2569", "secondary"),
            Detail("สถานะปัจจุบัน", "ใช้งานอยู่", "success"),
            Detail("เหตุผลขอใบแทน", "สูญหาย", "warning")
          }),
        Panel(
          "กฎการออกใบแทน",
          "ข้อกำหนด mock ที่ผู้ใช้ต้องรับทราบ",
          "warning",
          new[]
          {
            Detail("เลข shipment", "ต้องคงเดิม", "success"),
            Detail("หลักฐานเหตุสูญหาย", "ต้องแนบ", "warning"),
            Detail("tracking status", "ติดตามผ่าน OP-11", "info")
          })
      },
      tableSections: new[]
      {
        Table(
          "ประวัติการขอใบแทน",
          "ดูย้อนหลังคำขอใบแทนของบริษัทเดียวกัน",
          new[] { "Ref", "ใบรับรองเดิม", "เหตุผล", "สถานะ" },
          new[]
          {
            Row(Cell("REI-260416-01"), Cell("CERT-PS-2604012"), Cell("สูญหายระหว่างขนส่งเอกสาร"), Badge("ร่าง", "secondary")),
            Row(Cell("REI-260312-02"), Cell("CERT-PS-2603120"), Cell("ชำรุด"), Badge("อนุมัติ", "success"))
          },
          filters: new[] { "ทั้งหมด", "ร่าง", "อนุมัติ" },
          exportFormats: new[] { "Excel" },
          paginationSummary: "แสดง 1-2 จาก 2 รายการ"
        )
      },
      timeline: new[]
      {
        Event("เลือกใบรับรองเดิม", "ผูกคำขอใบแทนกับ CERT-PS-2604012", "ผู้ประกอบการ", "วันนี้ 10:44", "primary", new[] { "Reissue" }),
        Event("แนบหลักฐานเหตุสูญหาย", "อัปโหลด incident report และเอกสารรับรองจากขนส่ง", "ผู้ประกอบการ", "วันนี้ 10:48", "info", new[] { "หลักฐาน" }),
        Event("บันทึกร่าง", "คำขอถูกเก็บเป็นร่างเพื่อรอส่งคำขอ", "ผู้ประกอบการ", "วันนี้ 10:52", "secondary", new[] { "ร่าง" })
      });
  }

  private static System65ScreenPageViewModel BuildCertificateCancel(
    System65ScreenDefinition current,
    IReadOnlyList<System65ScreenDefinition> roleScreens)
  {
    return CreateModel(
      current,
      roleScreens,
      currentStatus: "รอพิจารณา",
      pageKicker: "คำขอยกเลิกใบรับรอง",
      pageSummary: "ยื่นยกเลิกใบรับรอง, ระบุเหตุผล, แนบหลักฐาน และติดตามผลคืนน้ำหนักโควตาในรูปแบบ mock workflow",
      alerts: new[]
      {
        Alert("warning", "ผลกระทบโควตา", "เมื่อยกเลิกใบรับรอง CERT-PS-2604097 จะมีน้ำหนัก 3,200 กก. ถูกคืนกลับเข้าสิทธิ์ต้นทางหลังอนุมัติ"),
        Alert("info", "หลักฐานแนะนำ", "แนะนำให้แนบ cancellation notice จากคู่ค้าและเอกสาร shipping void" )
      },
      primaryActions: new[]
      {
        Action("พรีวิวหนังสือยกเลิก", current.Route, "ri-file-pdf-line", "danger", true),
        Action("ติดตามคำขอ", RouteOf(roleScreens, "OP-11"), "ri-route-line", "info", true),
        Action("คืนน้ำหนักส่งคืน", RouteOf(roleScreens, "OP-16"), "ri-reply-all-line", "success", true)
      },
      formSections: new[]
      {
        Form(
          "รายละเอียดคำขอยกเลิก",
          "ระบุใบรับรอง, เหตุผล และหลักฐาน",
          new[]
          {
            Field("ใบรับรองที่ยกเลิก", "CERT-PS-2604097", type: "select", columnSpan: 4, options: new[] { "CERT-PS-2604097", "CERT-NMD-2604104" }),
            Field("เหตุผลการยกเลิก", "คู่ค้าปลายทางยกเลิกคำสั่งซื้อ", type: "editor", columnSpan: 8),
            Field("วันที่ยื่น", "2026-04-16", type: "date", columnSpan: 4),
            Field("หลักฐาน", "buyer-cancel-note.pdf | shipping-void.pdf", type: "file", columnSpan: 8),
            Field("ยืนยันการคืนน้ำหนัก", "true", type: "toggle", columnSpan: 4)
          })
      },
      detailPanels: new[]
      {
        Panel(
          "ผลกระทบโควตา",
          "น้ำหนักที่จะคืนกลับหลังอนุมัติยกเลิก",
          "warning",
          new[]
          {
            Detail("น้ำหนักตามใบรับรอง", "3,200 กก.", "info"),
            Detail("สิทธิ์ต้นทาง", "PS-260404", "primary"),
            Detail("สถานะคืนโควตา", "รอพิจารณา", "warning")
          },
          progressValue: 56,
          progressLabel: "สถานะการคืนสิทธิ์น้ำหนัก"),
        Panel(
          "ข้อสังเกตจากเจ้าหน้าที่",
          "ช่องสำหรับ mock note เมื่อมีการตอบกลับ",
          "info",
          new[]
          {
            Detail("comment ล่าสุด", "รอแนบ cancellation notice จาก buyer", "warning"),
            Detail("deadline ตอบกลับ", "18 เม.ย. 2569", "secondary")
          })
      },
      tableSections: new[]
      {
        Table(
          "lot ที่ได้รับผลกระทบ",
          "แสดง lot และน้ำหนักที่จะคืนกลับเมื่อยกเลิกสำเร็จ",
          new[] { "lot", "ใบรับรอง", "น้ำหนัก", "ผลกระทบ" },
          new[]
          {
            Row(Cell("RMBS2-2603-004"), Cell("CERT-PS-2604097"), Cell("3,200 กก."), Badge("คืนสิทธิ์", "info")),
            Row(Cell("P2-APR-01"), Cell("CERT-PS-2604097"), Cell("1,150 กก."), Badge("ปลด lock", "success"))
          },
          filters: new[] { "ทั้งหมด" },
          exportFormats: new[] { "Excel" },
          paginationSummary: "แสดง 1-2 จาก 2 รายการ"
        )
      },
      timeline: new[]
      {
        Event("สร้างคำขอยกเลิก", "อ้างอิงจาก CERT-PS-2604097", "ผู้ประกอบการ", "วันนี้ 11:10", "primary", new[] { "Cancel" }),
        Event("แนบหลักฐานจาก buyer", "อัปโหลด buyer-cancel-note.pdf", "ผู้ประกอบการ", "วันนี้ 11:14", "info", new[] { "Evidence" }),
        Event("คำขอรอพิจารณา", "ระบบบันทึกสถานะรอพิจารณาและเตรียมคืนโควตา", "ระบบ", "วันนี้ 11:20", "warning", new[] { "รอพิจารณา" })
      });
  }

  private static System65ScreenPageViewModel BuildReturnWeightRequest(
    System65ScreenDefinition current,
    IReadOnlyList<System65ScreenDefinition> roleScreens)
  {
    return CreateModel(
      current,
      roleScreens,
      currentStatus: "ร่าง",
      pageKicker: "คำขอคืนน้ำหนักสินค้าส่งคืน",
      pageSummary: "สร้างคำขอคืนน้ำหนักแบบบางส่วนหรือทั้งหมด, ระบุวัตถุประสงค์, แนบหลักฐาน และตรวจรุ่นที่เกี่ยวข้องหลายใบรับรองได้",
      primaryActions: new[]
      {
        Action("บันทึกร่าง", current.Route, "ri-save-line", "primary"),
        Action("ส่งคำขอ", current.Route, "ri-send-plane-line", "success"),
        Action("ค้นหาใบรับรอง", RouteOf(roleScreens, "OP-12"), "ri-search-eye-line", "info", true)
      },
      formSections: new[]
      {
        Form(
          "ประเภทการคืนน้ำหนัก",
          "เลือกบางส่วนหรือทั้งหมด พร้อมระบุวัตถุประสงค์",
          new[]
          {
            Field("ประเภทคำขอ", "บางส่วน", type: "select", columnSpan: 4, options: new[] { "บางส่วน", "ทั้งหมด" }),
            Field("วัตถุประสงค์", "สินค้าถูกส่งคืนจากผู้นำเข้าเพื่อตรวจคุณภาพใหม่", type: "editor", columnSpan: 8),
            Field("ใบรับรองที่เกี่ยวข้อง", "CERT-PS-2604118", type: "select", columnSpan: 4, options: new[] { "CERT-PS-2604118", "CERT-NMD-2604104", "CERT-PS-2604097" }),
            Field("น้ำหนักขอคืน", "1,850", type: "number", suffix: "กก.", columnSpan: 4),
            Field("จำนวนใบรับรองที่เกี่ยวข้อง", "2", type: "readonly", columnSpan: 4),
            Field("หลักฐานการส่งคืน", "return-note.pdf | warehouse-receipt.pdf", type: "file", columnSpan: 12)
          })
      },
      detailPanels: new[]
      {
        Panel(
          "สรุปทางเลือกการคืนน้ำหนัก",
          "เปรียบเทียบบางส่วนและทั้งหมด",
          "info",
          new[]
          {
            Detail("บางส่วน", "คืนเฉพาะน้ำหนักที่รับกลับจริง", "success"),
            Detail("ทั้งหมด", "คืนทั้ง lot ที่เกี่ยวข้อง", "warning"),
            Detail("หลายใบรับรอง", "รองรับ", "primary")
          }),
        Panel(
          "Validation",
          "สิ่งที่ระบบ mock จะตรวจ",
          "warning",
          new[]
          {
            Detail("น้ำหนักรวม", "ต้องไม่เกินยอดส่งออกเดิม", "warning"),
            Detail("หลักฐานรับคืน", "ต้องแนบอย่างน้อย 1 ไฟล์", "success"),
            Detail("วัตถุประสงค์", "ห้ามเว้นว่าง", "danger")
          })
      },
      tableSections: new[]
      {
        Table(
          "ใบรับรองและรุ่นที่เกี่ยวข้อง",
          "แสดงหลายใบรับรองที่ใช้คำนวณน้ำหนักคืน",
          new[] { "ใบรับรอง", "lot", "น้ำหนักส่งออก", "น้ำหนักขอคืน", "สถานะ" },
          new[]
          {
            Row(Cell("CERT-PS-2604118"), Cell("RMBS2-2604-001"), Cell("5,430 กก."), Cell("1,200 กก."), Badge("เลือกแล้ว", "info")),
            Row(Cell("CERT-NMD-2604104"), Cell("P2-APR-02"), Cell("2,300 กก."), Cell("650 กก."), Badge("เลือกแล้ว", "info")),
            Row(Cell("CERT-PS-2604097"), Cell("RMBS2-2603-004"), Cell("3,200 กก."), Cell("0 กก."), Badge("ยังไม่เลือก", "secondary"))
          },
          filters: new[] { "เลือกแล้ว", "ยังไม่เลือก" },
          exportFormats: new[] { "Excel", "PDF" },
          paginationSummary: "แสดง 1-3 จาก 3 รายการ"
        )
      },
      timeline: new[]
      {
        Event("เริ่มคำขอคืนน้ำหนัก", "สร้าง draft สำหรับสินค้าส่งคืนจากญี่ปุ่น", "ผู้ประกอบการ", "วันนี้ 11:33", "primary", new[] { "Return Weight" }),
        Event("เลือกหลายใบรับรอง", "ผูกน้ำหนักคืนกับ CERT-PS-2604118 และ CERT-NMD-2604104", "ผู้ประกอบการ", "วันนี้ 11:40", "info", new[] { "Multi-Cert" }),
        Event("แนบหลักฐานรับคืน", "อัปโหลด return-note.pdf และ warehouse receipt", "ผู้ประกอบการ", "วันนี้ 11:45", "success", new[] { "Evidence" })
      });
  }

  private static System65ScreenPageViewModel BuildScrapTrade(
    System65ScreenDefinition current,
    IReadOnlyList<System65ScreenDefinition> roleScreens)
  {
    return CreateModel(
      current,
      roleScreens,
      currentStatus: "มี 1 รายการรอยืนยัน",
      pageKicker: "ซื้อขายเศษเหลือ",
      pageSummary: "สร้างหนังสือกำกับซื้อขายเศษเหลือ, บันทึกซื้อขายใน/นอกระบบ, ยืนยันรับซื้อ และพรีวิว PDF แบบ mock",
      kpiCards: new[]
      {
        Kpi("เศษเหลือพร้อมขาย", "1,420 กก.", "รวมเศษปลาทูน่าและเศษปลาหมึก", "success", "ri-recycle-line"),
        Kpi("รายการในระบบ", "4 รายการ", "ซื้อขายกับผู้ประกอบการในระบบ 6_5", "info", "ri-links-line"),
        Kpi("รอยืนยันรับซื้อ", "1 รายการ", "ฝั่งผู้รับซื้อยังไม่กดยืนยัน mock", "warning", "ri-user-received-line")
      },
      primaryActions: new[]
      {
        Action("สร้างหนังสือกำกับ", current.Route, "ri-file-text-line", "primary"),
        Action("พรีวิว PDF", current.Route, "ri-file-pdf-line", "danger", true),
        Action("ตรวจเส้นทางซื้อขาย", RouteOf(roleScreens, "OP-18"), "ri-git-merge-line", "info", true)
      },
      formSections: new[]
      {
        Form(
          "คำสั่งซื้อขายเศษเหลือ",
          "รองรับทั้งซื้อขายในระบบและนอกระบบ",
          new[]
          {
            Field("ประเภทคำสั่ง", "ขาย", type: "select", columnSpan: 4, options: new[] { "ซื้อ", "ขาย" }),
            Field("รูปแบบคู่ค้า", "ในระบบ", type: "select", columnSpan: 4, options: new[] { "ในระบบ", "นอกระบบ" }),
            Field("คู่ค้า", "โรงงานสงขลา 02", type: "select", columnSpan: 4, options: new[] { "โรงงานสงขลา 02", "ผู้รับซื้อภายนอก", "บริษัท Ocean Hub" }),
            Field("ชนิดเศษเหลือ", "เศษปลาทูน่า", type: "select", columnSpan: 4, options: new[] { "เศษปลาทูน่า", "เศษปลาหมึก", "เศษกุ้ง" }),
            Field("ปริมาณ", "540", type: "number", suffix: "กก.", columnSpan: 4),
            Field("เลขหนังสือกำกับ", "SCR-BOOK-014", type: "readonly", columnSpan: 4),
            Field("รายละเอียด", "ขายเศษเหลือจาก line ผลิตปลาทูน่าชุด APR-03 เพื่อเข้าแปรรูปต่อ", type: "textarea", columnSpan: 12)
          })
      },
      detailPanels: new[]
      {
        Panel(
          "สถานะการยืนยันรับซื้อ",
          "ใช้ติดตามคู่ค้าที่ต้องยืนยันรายการ",
          "warning",
          new[]
          {
            Detail("หนังสือกำกับ", "SCR-BOOK-014", "primary"),
            Detail("คู่ค้า", "โรงงานสงขลา 02", "secondary"),
            Detail("สถานะ", "รอยืนยันรับซื้อ", "warning")
          }),
        Panel(
          "เอกสารพรีวิว",
          "รายการข้อมูลที่จะไปอยู่ใน PDF mock",
          "info",
          new[]
          {
            Detail("เลขหนังสือกำกับ", "SCR-BOOK-014", "primary"),
            Detail("ปริมาณรวม", "540 กก.", "success"),
            Detail("ผู้ออกเอกสาร", "โรงงานสมุทรสาคร 01", "secondary")
          })
      },
      tableSections: new[]
      {
        Table(
          "ทะเบียนซื้อขายเศษเหลือ",
          "รองรับ list, filter, pagination และ export",
          new[] { "Ref", "ชนิดเศษเหลือ", "คู่ค้า", "ปริมาณ", "สถานะ" },
          new[]
          {
            Row(Cell("SCR-BOOK-014"), Cell("เศษปลาทูน่า"), Cell("โรงงานสงขลา 02"), Cell("540 กก."), Badge("รอยืนยันรับซื้อ", "warning")),
            Row(Cell("SCR-BOOK-011"), Cell("เศษปลาหมึก"), Cell("ผู้รับซื้อภายนอก"), Cell("320 กก."), Badge("ยืนยันแล้ว", "success")),
            Row(Cell("SCR-BOOK-010"), Cell("เศษกุ้ง"), Cell("บริษัท Ocean Hub"), Cell("190 กก."), Badge("บันทึกในระบบ", "info"))
          },
          filters: new[] { "ทั้งหมด", "ในระบบ", "นอกระบบ", "รอยืนยัน" },
          exportFormats: new[] { "Excel", "PDF", "CSV" },
          paginationSummary: "แสดง 1-3 จาก 4 รายการ"
        )
      },
      timeline: new[]
      {
        Event("สร้างหนังสือกำกับใหม่", "ระบบ mock สร้าง SCR-BOOK-014", "ผู้ประกอบการ", "วันนี้ 12:02", "primary", new[] { "Scrap Trade" }),
        Event("กำหนดคู่ค้าในระบบ", "เลือกโรงงานสงขลา 02 เป็นผู้รับซื้อ", "ผู้ประกอบการ", "วันนี้ 12:05", "info", new[] { "In-system" }),
        Event("รอยืนยันรับซื้อ", "รายการถูกส่งไปที่ฝั่งคู่ค้าเพื่อกดยืนยันใน mock workflow", "ระบบ", "วันนี้ 12:06", "warning", new[] { "Pending confirmation" })
      });
  }

  private static System65ScreenPageViewModel BuildTraceabilityTree(
    System65ScreenDefinition current,
    IReadOnlyList<System65ScreenDefinition> roleScreens)
  {
    return CreateModel(
      current,
      roleScreens,
      currentStatus: "พร้อมตรวจสอบ",
      pageKicker: "ตรวจสอบเส้นทางการซื้อขาย",
      pageSummary: "ตรวจสอบ trace ย้อนกลับจาก RMBS/P1/P2 ไปยังต้นทาง, แสดง tree diagram แบบ mock และ export Excel ได้",
      kpiCards: new[]
      {
        Kpi("ระดับ trace", "4 ชั้น", "จากใบรับรองย้อนกลับถึงวัตถุดิบตั้งต้น", "primary", "ri-node-tree"),
        Kpi("เอกสารเชื่อมโยง", "7 ฉบับ", "ประกอบด้วย RMBS1, RMBS2, P1/P2 และ Certificate", "info", "ri-git-branch-line"),
        Kpi("ปลายทาง export", "2 ประเทศ", "ญี่ปุ่นและเกาหลีใต้", "success", "ri-earth-line")
      },
      primaryActions: new[]
      {
        Action("Export Excel", current.Route, "ri-file-excel-2-line", "success"),
        Action("ค้นหาใบรับรอง", RouteOf(roleScreens, "OP-12"), "ri-search-eye-line", "info", true),
        Action("กลับหน้า P1/P2", RouteOf(roleScreens, "OP-08"), "ri-arrow-left-line", "secondary", true)
      },
      formSections: new[]
      {
        Form(
          "เงื่อนไขการ trace",
          "เลือกจุดเริ่มต้นของการตรวจสอบย้อนกลับ",
          new[]
          {
            Field("ประเภทเอกสารต้นทาง", "ใบรับรอง", type: "select", columnSpan: 4, options: new[] { "ใบรับรอง", "RMBS2", "P1/P2" }),
            Field("เลขอ้างอิง", "CERT-PS-2604118", columnSpan: 4),
            Field("รูปแบบการ export", "Excel", type: "select", columnSpan: 4, options: new[] { "Excel", "CSV" })
          })
      },
      detailPanels: new[]
      {
        Panel(
          "สรุปเส้นทางที่พบ",
          "เส้นทางหลักของ lot ที่เลือก",
          "success",
          new[]
          {
            Detail("ปลายทาง", "ญี่ปุ่น", "primary"),
            Detail("ใบรับรอง", "CERT-PS-2604118", "info"),
            Detail("lot ส่งออก", "RMBS2-2604-001", "secondary"),
            Detail("วัตถุดิบต้นทาง", "NP-260318", "success")
          }),
        Panel(
          "ข้อสังเกตการ trace",
          "ข้อมูลที่ผู้ใช้ควรเห็นก่อน export",
          "info",
          new[]
          {
            Detail("เส้นทางย่อย", "2 เส้นทาง", "secondary"),
            Detail("จุดที่มีการซื้อขาย P1/P2", "1 จุด", "warning"),
            Detail("เชื่อมเศษเหลือ", "มี", "info")
          })
      },
      tableSections: new[]
      {
        Table(
          "ทะเบียนอ้างอิงตามลำดับ trace",
          "ใช้ดูเอกสารทุกชั้นและ export เป็น Excel",
          new[] { "ลำดับ", "ประเภท", "เลขอ้างอิง", "บทบาท", "สถานะ" },
          new[]
          {
            Row(Cell("1"), Cell("Certificate"), Cell("CERT-PS-2604118"), Cell("ปลายทาง"), Badge("อนุมัติ", "success")),
            Row(Cell("2"), Cell("RMBS2"), Cell("RMBS2-2604-001"), Cell("lot ส่งออก"), Badge("พร้อมส่งออก", "info")),
            Row(Cell("3"), Cell("P2"), Cell("P2-APR-02"), Cell("ซื้อขายระหว่างทาง"), Badge("จับคู่แล้ว", "secondary")),
            Row(Cell("4"), Cell("RMBS1 NoPS"), Cell("NP-260318"), Cell("ต้นทางวัตถุดิบ"), Badge("อนุมัติ", "success"))
          },
          filters: new[] { "ทุกชั้น", "เฉพาะ P1/P2", "เฉพาะ RMBS" },
          exportFormats: new[] { "Excel", "CSV" },
          paginationSummary: "แสดง 1-4 จาก 7 รายการ"
        )
      },
      traceNodes: new[]
      {
        Node(
          "CERT-PS-2604118",
          "ใบรับรองส่งออกญี่ปุ่น",
          "success",
          new[]
          {
            Node(
              "RMBS2-2604-001",
              "lot ส่งออก 6,800 กก.",
              "info",
              new[]
              {
                Node(
                  "P2-APR-02",
                  "ซื้อขายระหว่างโรงงาน 1,250 กก.",
                  "warning",
                  new[]
                  {
                    Node("PS-260412", "คำขอ RMBS1:PS", "primary"),
                    Node("SCR-BOOK-014", "เศษเหลือที่เชื่อมโยง 540 กก.", "secondary")
                  }),
                Node("NP-260318", "ต้นทาง RMBS1 NoPS อนุมัติ 18,500 กก.", "success")
              })
          })
      },
      timeline: new[]
      {
        Event("เริ่ม trace จากใบรับรอง", "เลือก CERT-PS-2604118 เป็น root node", "ผู้ประกอบการ", "วันนี้ 12:22", "primary", new[] { "Root" }),
        Event("ระบบสร้าง tree mock", "เชื่อม RMBS2, P2 และ RMBS1 ตามข้อมูลอ้างอิงที่เตรียมไว้", "ระบบ", "วันนี้ 12:23", "info", new[] { "Tree Diagram" }),
        Event("เตรียม export", "ผู้ใช้พร้อม export ลำดับ trace เป็น Excel", "ผู้ประกอบการ", "วันนี้ 12:25", "success", new[] { "Export" })
      });
  }

  private static System65ScreenPageViewModel BuildFallbackScreen(
    System65ScreenDefinition current,
    IReadOnlyList<System65ScreenDefinition> roleScreens)
  {
    var componentRows = current.Components.Count > 0
      ? current.Components
        .Select((component, index) => Row(
          Cell((index + 1).ToString()),
          Cell(component),
          Badge("อ้างอิง component", "info")))
        .ToArray()
      : new[]
      {
        Row(Cell("1"), Cell("ยังไม่ระบุ component จาก requirement"), Badge("mock scaffold", "secondary"))
      };

    var requirementChips = current.RequirementCoverage
      .Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
      .Select((coverage, index) => Chip(coverage, string.Empty, index == 0 ? "primary" : "secondary", index == 0))
      .ToArray();

    return CreateModel(
      current,
      roleScreens,
      currentStatus: "Mock scaffold",
      pageKicker: $"{current.RoleLabel} preview",
      pageSummary: current.Goal,
      isFallback: true,
      kpiCards: new[]
      {
        Kpi("จำนวนหน้าของ role", roleScreens.Count.ToString(), "จาก requirement file ปัจจุบัน", "primary", "ri-layout-grid-line"),
        Kpi("component mapping", current.Components.Count.ToString(), "จำนวน view ตัวอย่างที่อ้างอิงในเอกสาร", "info", "ri-puzzle-line"),
        Kpi("สถานะหน้าจอ", "Mock scaffold", "ยังไม่ทำ customization แบบ Operator", "warning", "ri-hammer-line")
      },
      alerts: new[]
      {
        Alert("info", "Fallback screen", "หน้าจอนี้ใช้ scaffold ทั่วไปจาก requirement เพื่อไม่ให้ route ของ role อื่นพัง"),
        Alert("warning", "Mock only", "ยังไม่มี form flow เฉพาะหน้าจอเหมือนฝั่งผู้ประกอบการ")
      },
      workflowChips: requirementChips,
      primaryActions: new[]
      {
        Action("กลับหน้า index", "/PSDeclaration", "ri-home-4-line", "primary"),
        Action("เปิด role dashboard", roleScreens.FirstOrDefault()?.Route ?? "/PSDeclaration", "ri-dashboard-line", "info", true)
      },
      detailPanels: new[]
      {
        Panel(
          "ขอบเขต requirement",
          "สรุปจากเอกสาร requirement เดิม",
          "primary",
          new[]
          {
            Detail("Screen ID", current.ScreenId, "info"),
            Detail("Role", current.RoleLabel, "secondary"),
            Detail("Coverage", current.RequirementCoverage, "success"),
            Detail("Route", current.Route, "primary")
          },
          new[] { "Controller ใช้ requirement file เดียวกันในการสร้าง catalog และ route", "สามารถต่อยอดเป็นหน้าเฉพาะ role ภายหลังได้" })
      },
      tableSections: new[]
      {
        Table(
          "Component mapping",
          "รายการหน้าอ้างอิงที่ requirement ระบุไว้",
          new[] { "ลำดับ", "Views ที่อ้างอิง", "ชนิด" },
          componentRows,
          filters: new[] { "ทั้งหมด" },
          exportFormats: new[] { "Excel" },
          paginationSummary: $"แสดง 1-{componentRows.Length} จาก {componentRows.Length} รายการ",
          supportsPagination: false)
      },
      timeline: new[]
      {
        Event("โหลด requirement", "System65ScreenCatalog อ่านไฟล์ requirement ของ role นี้สำเร็จ", "ระบบ", "ขณะเปิดหน้า", "primary", new[] { current.ScreenId }),
        Event("สร้าง scaffold", "ระบบสร้าง mock scaffold จาก metadata ใน requirement", "ระบบ", "ขณะเปิดหน้า", "info", new[] { "Fallback" }),
        Event("พร้อมต่อยอด", "สามารถเพิ่มหน้าเฉพาะ role นี้ได้โดยไม่เปลี่ยน route scheme", "ระบบ", "ถัดไป", "success", new[] { "Extensible" })
      },
      pageNotes: new[] { "หน้าจอนี้ตั้งใจให้ทำงานได้สำหรับ role อื่นที่ยังไม่ได้ customize รายละเอียด" });
  }

  private static System65ScreenPageViewModel CreateModel(
    System65ScreenDefinition current,
    IReadOnlyList<System65ScreenDefinition> roleScreens,
    string currentStatus,
    string pageKicker,
    string pageSummary,
    bool isFallback = false,
    IReadOnlyList<System65KpiCard>? kpiCards = null,
    IReadOnlyList<System65AlertMessage>? alerts = null,
    IReadOnlyList<System65Chip>? workflowChips = null,
    IReadOnlyList<System65ActionItem>? primaryActions = null,
    IReadOnlyList<System65FormSection>? formSections = null,
    IReadOnlyList<System65DetailPanel>? detailPanels = null,
    IReadOnlyList<System65TableSection>? tableSections = null,
    IReadOnlyList<System65TimelineEvent>? timeline = null,
    IReadOnlyList<System65TraceNode>? traceNodes = null,
    IReadOnlyList<string>? pageNotes = null)
  {
    return new System65ScreenPageViewModel
    {
      Current = current,
      RoleScreens = roleScreens,
      CurrentStatus = currentStatus,
      PageKicker = pageKicker,
      PageSummary = pageSummary,
      IsFallback = isFallback,
      KpiCards = kpiCards ?? Array.Empty<System65KpiCard>(),
      Alerts = alerts ?? Array.Empty<System65AlertMessage>(),
      WorkflowChips = workflowChips ?? Array.Empty<System65Chip>(),
      PrimaryActions = primaryActions ?? Array.Empty<System65ActionItem>(),
      FormSections = formSections ?? Array.Empty<System65FormSection>(),
      DetailPanels = detailPanels ?? Array.Empty<System65DetailPanel>(),
      TableSections = tableSections ?? Array.Empty<System65TableSection>(),
      Timeline = timeline ?? Array.Empty<System65TimelineEvent>(),
      TraceNodes = traceNodes ?? Array.Empty<System65TraceNode>(),
      PageNotes = pageNotes ?? Array.Empty<string>()
    };
  }

  private static string RouteOf(IReadOnlyList<System65ScreenDefinition> screens, string screenId)
  {
    return screens.FirstOrDefault(screen => screen.ScreenId.Equals(screenId, StringComparison.OrdinalIgnoreCase))?.Route ?? "#";
  }

  private static System65KpiCard Kpi(string title, string value, string caption, string tone, string icon)
  {
    return new System65KpiCard
    {
      Title = title,
      Value = value,
      Caption = caption,
      Tone = tone,
      Icon = icon
    };
  }

  private static System65ActionItem Action(string label, string url, string icon, string tone, bool isOutline = false)
  {
    return new System65ActionItem
    {
      Label = label,
      Url = url,
      Icon = icon,
      Tone = tone,
      IsOutline = isOutline
    };
  }

  private static System65AlertMessage Alert(string tone, string title, string message)
  {
    return new System65AlertMessage
    {
      Tone = tone,
      Title = title,
      Message = message
    };
  }

  private static System65Chip Chip(string label, string value, string tone, bool isActive = false)
  {
    return new System65Chip
    {
      Label = label,
      Value = value,
      Tone = tone,
      IsActive = isActive
    };
  }

  private static System65FormSection Form(string title, string description, IReadOnlyList<System65FormField> fields)
  {
    return new System65FormSection
    {
      Title = title,
      Description = description,
      Fields = fields
    };
  }

  private static System65FormField Field(
    string label,
    string value,
    string type = "text",
    string helperText = "",
    bool required = false,
    int columnSpan = 6,
    string placeholder = "",
    string prefix = "",
    string suffix = "",
    IReadOnlyList<string>? options = null)
  {
    return new System65FormField
    {
      Label = label,
      Value = value,
      Type = type,
      HelperText = helperText,
      Required = required,
      ColumnSpan = columnSpan,
      Placeholder = placeholder,
      Prefix = prefix,
      Suffix = suffix,
      Options = options ?? Array.Empty<string>()
    };
  }

  private static System65DetailPanel Panel(
    string title,
    string description,
    string tone,
    IReadOnlyList<System65DetailItem> items,
    IReadOnlyList<string>? notes = null,
    int? progressValue = null,
    string progressLabel = "")
  {
    return new System65DetailPanel
    {
      Title = title,
      Description = description,
      Tone = tone,
      Items = items,
      Notes = notes ?? Array.Empty<string>(),
      ProgressValue = progressValue,
      ProgressLabel = progressLabel
    };
  }

  private static System65DetailItem Detail(string label, string value, string tone, string helpText = "")
  {
    return new System65DetailItem
    {
      Label = label,
      Value = value,
      Tone = tone,
      HelpText = helpText
    };
  }

  private static System65TableSection Table(
    string title,
    string description,
    IReadOnlyList<string> headers,
    IReadOnlyList<System65TableRow> rows,
    IReadOnlyList<string>? filters = null,
    IReadOnlyList<string>? exportFormats = null,
    string paginationSummary = "",
    bool supportsSort = true,
    bool supportsFilter = true,
    bool supportsSearch = true,
    bool supportsPagination = true)
  {
    return new System65TableSection
    {
      Title = title,
      Description = description,
      Headers = headers,
      Rows = rows,
      Filters = filters ?? Array.Empty<string>(),
      ExportFormats = exportFormats ?? Array.Empty<string>(),
      PaginationSummary = paginationSummary,
      SupportsSort = supportsSort,
      SupportsFilter = supportsFilter,
      SupportsSearch = supportsSearch,
      SupportsPagination = supportsPagination
    };
  }

  private static System65TableRow Row(params System65TableCell[] cells)
  {
    return new System65TableRow
    {
      Cells = cells
    };
  }

  private static System65TableCell Cell(string text, string tone = "secondary", string note = "", string icon = "")
  {
    return new System65TableCell
    {
      Text = text,
      Tone = tone,
      Note = note,
      Icon = icon
    };
  }

  private static System65TableCell Badge(string text, string tone, string note = "")
  {
    return new System65TableCell
    {
      Text = text,
      Tone = tone,
      Note = note,
      IsBadge = true
    };
  }

  private static System65TimelineEvent Event(
    string title,
    string description,
    string actor,
    string time,
    string tone,
    IReadOnlyList<string> tags)
  {
    return new System65TimelineEvent
    {
      Title = title,
      Description = description,
      Actor = actor,
      Time = time,
      Tone = tone,
      Tags = tags
    };
  }

  private static System65TraceNode Node(
    string title,
    string subtitle,
    string tone,
    IReadOnlyList<System65TraceNode>? children = null)
  {
    return new System65TraceNode
    {
      Title = title,
      Subtitle = subtitle,
      Tone = tone,
      Children = children ?? Array.Empty<System65TraceNode>()
    };
  }
}