# Screen Requirements: ผู้ดูแลระบบ (System 6_5)

## ขอบเขต
- เอกสารนี้กำหนดเฉพาะหน้าจอ (UI) เท่านั้น
- ไม่ทำ API และไม่ผูกฐานข้อมูลจริง
- ใช้ข้อมูล Mockup เท่านั้น
- ทุกหน้าจออยู่ภายใต้ Controller System_6_5
- อ้างอิงความต้องการจาก raw-requirements-ผู้ดูแลระบบ.txt (ข้อ 3.1-3.7)

## แนวทางข้อมูล Mockup
- เตรียมข้อมูลจำลองสำหรับหน่วยงาน, พื้นที่รับผิดชอบ, เจ้าหน้าที่, สิทธิ์, รายชื่อผู้ลงนาม, DOF List, Traceability List, มาตรการสุขอนามัย, template ใบรับรอง
- รองรับสถานะข้อมูลหลัก: ใช้งาน, ระงับ, ยกเลิก
- หน้าจอ master data ทุกหน้ามีรายการประวัติการเปลี่ยนแปลงแบบ mock

## โครงสร้างเส้นทางหน้าจอ (ภายใต้ Controller System_6_5)
| Screen ID | Route ตัวอย่าง |
| --- | --- |
| AD-01 | /System_6_5/Admin/Organization |
| AD-02 | /System_6_5/Admin/OfficerAndPermissions |
| AD-03 | /System_6_5/Admin/Announcements |
| AD-04 | /System_6_5/Admin/DOFList |
| AD-05 | /System_6_5/Admin/TraceabilityList |
| AD-06 | /System_6_5/Admin/SanitaryMeasures |
| AD-07 | /System_6_5/Admin/CertificateDefaultConfig |
| AD-08 | /System_6_5/Admin/Reports |
| AD-09 | /System_6_5/Admin/CertificateFormatAndESign |

## รายการหน้าจอและ Component Mapping

### AD-01: หน้าจัดการองค์กรและพื้นที่รับผิดชอบ
- ครอบคลุมข้อกำหนด: 3.1(1), 3.1(3), 3.1(6), 3.1(7), 3.1(8), 3.1(11)
- เป้าหมาย: กำหนดโครงสร้างหน่วยงาน กตส./ศตส., mapping พื้นที่รับผิดชอบ, สิทธิ์เห็นข้อมูลข้ามหน่วย
- Components จาก Views:
	- Views/Forms/BasicInputs.cshtml
	- Views/Forms/Selects.cshtml
	- Views/Forms/CustomOptions.cshtml
	- Views/Tables/DatatablesAdvanced.cshtml
	- Views/Ui/Accordion.cshtml
	- Views/Ui/Alerts.cshtml

### AD-02: หน้าจัดการเจ้าหน้าที่ บทบาท และสิทธิ์
- ครอบคลุมข้อกำหนด: 3.1(2), 3.1(4), 3.1(9), 3.1(10)
- เป้าหมาย: ลงทะเบียนเจ้าหน้าที่, เปิด-ปิดสิทธิ์, กำหนดบทบาทผู้อนุมัติ/ผู้ลงนาม, สิทธิ์แก้ไข DOF/Traceability
- Components จาก Views:
	- Views/Forms/BasicInputs.cshtml
	- Views/Forms/Selects.cshtml
	- Views/Forms/Switches.cshtml
	- Views/Forms/FileUpload.cshtml
	- Views/Tables/DatatablesExtensions.cshtml
	- Views/Ui/Badges.cshtml
	- Views/Modals/Examples.cshtml

### AD-03: หน้าข่าวประชาสัมพันธ์
- ครอบคลุมข้อกำหนด: 3.1(5)
- เป้าหมาย: สร้าง/แก้ไข/เผยแพร่ข่าว พร้อมแนบลิงก์หรือไฟล์
- Components จาก Views:
	- Views/Forms/BasicInputs.cshtml
	- Views/Forms/Editors.cshtml
	- Views/Forms/FileUpload.cshtml
	- Views/Tables/DatatablesBasic.cshtml
	- Views/Ui/Alerts.cshtml
	- Views/Ui/Toasts.cshtml

### AD-04: หน้าฐานข้อมูล DOF List
- ครอบคลุมข้อกำหนด: 3.2(1)-(8)
- เป้าหมาย: จัดการข้อมูลโรงงานรับรองสุขอนามัย, import template หลายรายการ, export Excel, เก็บประวัติ
- Components จาก Views:
	- Views/Forms/BasicInputs.cshtml
	- Views/Forms/InputGroups.cshtml
	- Views/Forms/Selects.cshtml
	- Views/Forms/FileUpload.cshtml
	- Views/Tables/DatatablesExtensions.cshtml
	- Views/Ui/Badges.cshtml
	- Views/Modals/Examples.cshtml

### AD-05: หน้าฐานข้อมูล Traceability List
- ครอบคลุมข้อกำหนด: 3.3(1)-(8)
- เป้าหมาย: จัดการข้อมูลโรงงานตรวจสอบย้อนกลับ, import template, copy จาก DOF, export Excel
- Components จาก Views:
	- Views/Forms/BasicInputs.cshtml
	- Views/Forms/InputGroups.cshtml
	- Views/Forms/Selects.cshtml
	- Views/Forms/FileUpload.cshtml
	- Views/Tables/DatatablesExtensions.cshtml
	- Views/Ui/Alerts.cshtml
	- Views/Modals/Examples.cshtml

### AD-06: หน้าฐานข้อมูลมาตรการสุขอนามัยประเทศคู่ค้า
- ครอบคลุมข้อกำหนด: 3.4
- เป้าหมาย: ตั้งค่ามาตรการสุขอนามัยต่อประเทศ/สินค้า, ตรวจสอบรายละเอียด, export Excel
- Components จาก Views:
	- Views/Forms/BasicInputs.cshtml
	- Views/Forms/Selects.cshtml
	- Views/Forms/Pickers.cshtml
	- Views/Tables/DatatablesExtensions.cshtml
	- Views/Ui/Badges.cshtml
	- Views/Ui/TooltipsPopovers.cshtml

### AD-07: หน้าตั้งค่าคำเริ่มต้นการออกใบรับรอง PS/NMD
- ครอบคลุมข้อกำหนด: 3.5(1)-(3)
- เป้าหมาย: ปรับ template e-form ตามประเทศ, ตั้งค่าแจ้งเตือนใกล้ส่งออก, ตั้งค่าระงับใบรับรองตามประเทศ/โรงงาน
- Components จาก Views:
	- Views/Forms/BasicInputs.cshtml
	- Views/Forms/Selects.cshtml
	- Views/Forms/Pickers.cshtml
	- Views/Forms/Switches.cshtml
	- Views/Ui/TabsPills.cshtml
	- Views/Ui/Alerts.cshtml
	- Views/Modals/Examples.cshtml

### AD-08: หน้าศูนย์รายงาน Static/Dynamic
- ครอบคลุมข้อกำหนด: 3.6(1)-(12)
- เป้าหมาย: เรียกรายงานหลายประเภทตามเงื่อนไขค้นหาและ export data sheet
- Components จาก Views:
	- Views/Forms/BasicInputs.cshtml
	- Views/Forms/Selects.cshtml
	- Views/Forms/Pickers.cshtml
	- Views/Tables/DatatablesExtensions.cshtml
	- Views/Ui/TabsPills.cshtml
	- Views/Ui/Badges.cshtml

### AD-09: หน้ากำหนดรูปแบบการจัดทำใบรับรองและลายมือชื่ออิเล็กทรอนิกส์
- ครอบคลุมข้อกำหนด: 3.7(1)-(6)
- เป้าหมาย: ตั้งโหมดออกใบรับรอง (manual/auto), ช่วงวันที่ใช้งาน, จัดการไฟล์ลายเซ็น, ผู้ทำหน้าที่แทน
- Components จาก Views:
	- Views/Forms/BasicInputs.cshtml
	- Views/Forms/Selects.cshtml
	- Views/Forms/Pickers.cshtml
	- Views/Forms/FileUpload.cshtml
	- Views/Forms/Switches.cshtml
	- Views/Tables/DatatablesBasic.cshtml
	- Views/Ui/Alerts.cshtml
	- Views/Modals/Examples.cshtml

## Requirement Coverage Matrix
| หมวดข้อกำหนด | ครอบคลุมโดยหน้าจอ |
| --- | --- |
| 3.1 | AD-01, AD-02, AD-03 |
| 3.2 | AD-04 |
| 3.3 | AD-05 |
| 3.4 | AD-06 |
| 3.5 | AD-07 |
| 3.6 | AD-08 |
| 3.7 | AD-09 |

## หมายเหตุการพัฒนา Mockup
- หน้าจอ import/export ใช้ DatatablesExtensions เป็นมาตรฐานร่วม
- หน้าจอแก้ไขค่ากำกับระดับระบบให้มี confirm modal ทุกครั้งก่อนบันทึก
- หน้าจอที่มีสถานะระงับ/ยกเลิกให้แสดง badge สีชัดเจนเพื่อป้องกันความสับสน
