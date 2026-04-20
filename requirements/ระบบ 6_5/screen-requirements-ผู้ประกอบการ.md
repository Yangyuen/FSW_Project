# Screen Requirements: ผู้ประกอบการ (System 6_5)

## ขอบเขต
- เอกสารนี้กำหนดเฉพาะหน้าจอ (UI) เท่านั้น
- ไม่ทำ API และไม่ผูกฐานข้อมูลจริง
- ใช้ข้อมูล Mockup เท่านั้น
- ทุกหน้าจออยู่ภายใต้ Controller PSDeclaration
- อ้างอิงความต้องการจาก raw-requirements-ผู้ประกอบการ.txt (ข้อ 1.1-1.12)

## แนวทางข้อมูล Mockup
- ใช้ข้อมูลจำลองเป็นชุด เช่น โรงงาน, RMBS1, RMBS2, P1, P2, ใบรับรอง, %Yield, รายการเศษเหลือ, สถานะคำขอ
- สถานะหลักที่ต้องมีในทุกหน้าที่เกี่ยวข้อง: ร่าง, ส่งคำขอแล้ว, แจ้งแก้ไข, อนุมัติ, ไม่อนุมัติ, ยกเลิก
- หน้า list ทุกหน้ารองรับ sort, filter, pagination, export (Excel/PDF/CSV ตามบริบท)
- หน้า detail ทุกหน้ามีประวัติการดำเนินการแบบ mock timeline

## โครงสร้างเส้นทางหน้าจอ (ภายใต้ Controller PSDeclaration)
| Screen ID | Route ตัวอย่าง |
| --- | --- |
| OP-01 | /PSDeclaration/Operator/Dashboard |
| OP-02 | /PSDeclaration/Operator/RMBS1NoPS/Request |
| OP-03 | /PSDeclaration/Operator/RMBS1NoPS/List |
| OP-04 | /PSDeclaration/Operator/RMBS1PS/Request |
| OP-05 | /PSDeclaration/Operator/RMBS1PS/ChangeRequest |
| OP-06 | /PSDeclaration/Operator/RMBS1PS/Production |
| OP-07 | /PSDeclaration/Operator/RMBS2/List |
| OP-08 | /PSDeclaration/Operator/P1P2/Trade |
| OP-09 | /PSDeclaration/Operator/Yield/Request |
| OP-10 | /PSDeclaration/Operator/Certificate/Request |
| OP-11 | /PSDeclaration/Operator/Certificate/Track |
| OP-12 | /PSDeclaration/Operator/Certificate/Search |
| OP-13 | /PSDeclaration/Operator/Certificate/Amend |
| OP-14 | /PSDeclaration/Operator/Certificate/Reissue |
| OP-15 | /PSDeclaration/Operator/Certificate/Cancel |
| OP-16 | /PSDeclaration/Operator/ReturnWeight/Request |
| OP-17 | /PSDeclaration/Operator/ScrapTrade |
| OP-18 | /PSDeclaration/Operator/TraceabilityTree |

## รายการหน้าจอและ Component Mapping

### OP-01: หน้า Dashboard ผู้ประกอบการ
- ครอบคลุมข้อกำหนด: 1.3, 1.7, 1.8, 1.9, 1.10, 1.11, 1.12
- เป้าหมาย: สรุปงานค้าง, แจ้งเตือนใกล้วันส่งออก, ปุ่มลัดไปแต่ละกระบวนงาน
- Components จาก Views:
	- Views/Tables/DatatablesBasic.cshtml
	- Views/Ui/Badges.cshtml
	- Views/Ui/Alerts.cshtml
	- Views/Ui/Toasts.cshtml
	- Views/Ui/TabsPills.cshtml
	- Views/Icons/RiIcons.cshtml

### OP-02: หน้ายื่นคำขอพิจารณาวัตถุดิบตั้งต้น RMBS1 สถานะ NoPS
- ครอบคลุมข้อกำหนด: 1.1(1)-(5)
- เป้าหมาย: สร้าง/แก้ไขคำขอ, ตรวจสอบข้อมูล IMD/มาตรการ, แสดงผลคำนวณโควตา FCFS
- Components จาก Views:
	- Views/Forms/BasicInputs.cshtml
	- Views/Forms/Selects.cshtml
	- Views/Forms/Pickers.cshtml
	- Views/Forms/InputGroups.cshtml
	- Views/Forms/FileUpload.cshtml
	- Views/Forms/CustomOptions.cshtml
	- Views/Ui/Alerts.cshtml
	- Views/Modals/Examples.cshtml

### OP-03: หน้ารายการ RMBS1 NoPS และรายละเอียดเอกสาร
- ครอบคลุมข้อกำหนด: 1.2(1)
- เป้าหมาย: ค้นหา/ดูรายละเอียด/พรีวิว PDF RMBS1 NoPS ที่อนุมัติแล้ว
- Components จาก Views:
	- Views/Tables/DatatablesAdvanced.cshtml
	- Views/Tables/DatatablesExtensions.cshtml
	- Views/Ui/Badges.cshtml
	- Views/Ui/PaginationBreadcrumbs.cshtml
	- Views/Ui/TooltipsPopovers.cshtml

### OP-04: หน้าคำขอ RMBS1 สถานะ PS
- ครอบคลุมข้อกำหนด: 1.2(2)-(4), 1.3(1), 1.3(5)-(9)
- เป้าหมาย: สร้าง/บันทึก/แก้ไข/ยกเลิก/ลบ/แนบไฟล์, เลือกความประสงค์ขอใบรับรองและประเทศปลายทาง, ส่งคำขอ
- Components จาก Views:
	- Views/Forms/BasicInputs.cshtml
	- Views/Forms/Selects.cshtml
	- Views/Forms/Editors.cshtml
	- Views/Forms/FileUpload.cshtml
	- Views/Forms/Switches.cshtml
	- Views/Ui/TabsPills.cshtml
	- Views/Ui/Accordion.cshtml
	- Views/Ui/Alerts.cshtml
	- Views/Modals/Examples.cshtml

### OP-05: หน้าคำขอเปลี่ยนผลการพิจารณา RMBS1:PS
- ครอบคลุมข้อกำหนด: 1.3(2), 1.3(4)
- เป้าหมาย: ยื่นคำขอเปลี่ยนผล, ระบุเหตุผล, แนบหลักฐาน, ส่งให้เจ้าหน้าที่
- Components จาก Views:
	- Views/Forms/BasicInputs.cshtml
	- Views/Forms/Editors.cshtml
	- Views/Forms/FileUpload.cshtml
	- Views/Ui/Alerts.cshtml
	- Views/Ui/Toasts.cshtml
	- Views/Modals/Examples.cshtml

### OP-06: หน้าบันทึกข้อมูลในเอกสาร RMBS1:PS (การผลิต/ขายต่อ/ซื้อเพิ่ม)
- ครอบคลุมข้อกำหนด: 1.4(1)-(10), 1.4(15), 1.4(21)
- เป้าหมาย: บันทึก production batch, ตรวจ %Yield, balance stock, แจ้งเตือนโควตาไม่พอ
- Components จาก Views:
	- Views/Forms/BasicInputs.cshtml
	- Views/Forms/InputGroups.cshtml
	- Views/Forms/Selects.cshtml
	- Views/Forms/Pickers.cshtml
	- Views/Ui/Progress.cshtml
	- Views/Ui/Alerts.cshtml
	- Views/Ui/Badges.cshtml
	- Views/Tables/DatatablesAdvanced.cshtml
	- Views/Ui/TooltipsPopovers.cshtml

### OP-07: หน้ารายการ RMBS2 และรายละเอียดรุ่นส่งออก
- ครอบคลุมข้อกำหนด: 1.4(16)-(20)
- เป้าหมาย: แสดงรุ่นส่งออก, คงเหลือ, เลขอ้างอิงใบรับรอง/HC, export ข้อมูล
- Components จาก Views:
	- Views/Tables/DatatablesAdvanced.cshtml
	- Views/Tables/DatatablesExtensions.cshtml
	- Views/Ui/TabsPills.cshtml
	- Views/Ui/ListGroups.cshtml
	- Views/Ui/Badges.cshtml

### OP-08: หน้าซื้อขายผลิตภัณฑ์ P1/P2
- ครอบคลุมข้อกำหนด: 1.6(1)-(18), 1.6(20)
- เป้าหมาย: ซื้อ/ขายผลิตภัณฑ์, ติดตาม stock balance, ตรวจเงื่อนไข lock, เชื่อมโยง P1/P2 กับ RMBS/ใบรับรอง
- Components จาก Views:
	- Views/Forms/Selects.cshtml
	- Views/Forms/BasicInputs.cshtml
	- Views/Forms/InputGroups.cshtml
	- Views/Forms/CustomOptions.cshtml
	- Views/Tables/DatatablesAdvanced.cshtml
	- Views/Tables/DatatablesExtensions.cshtml
	- Views/Ui/Alerts.cshtml
	- Views/Ui/Badges.cshtml
	- Views/Modals/Examples.cshtml

### OP-09: หน้าคำขอพิจารณาอนุมัติ %Yield
- ครอบคลุมข้อกำหนด: 1.5(1)-(9)
- เป้าหมาย: ยื่นคำขอ %Yield, ขอแก้ไขผลอนุมัติ, import template, แนบไฟล์หลักฐาน
- Components จาก Views:
	- Views/Forms/FileUpload.cshtml
	- Views/Forms/BasicInputs.cshtml
	- Views/Forms/InputGroups.cshtml
	- Views/Forms/Selects.cshtml
	- Views/Tables/DatatablesBasic.cshtml
	- Views/Ui/Alerts.cshtml
	- Views/Ui/Toasts.cshtml

### OP-10: หน้าขอใบรับรอง PS/NMD (เขียนคำขอ)
- ครอบคลุมข้อกำหนด: 1.7(1)-(23)
- เป้าหมาย: เขียนคำขอ, Save as, ลงนามอิเล็กทรอนิกส์ (mock), กรอก HC, ตรวจข้อมูลซ้ำ, สร้าง draft
- Components จาก Views:
	- Views/Forms/BasicInputs.cshtml
	- Views/Forms/Selects.cshtml
	- Views/Forms/Pickers.cshtml
	- Views/Forms/FileUpload.cshtml
	- Views/Forms/Editors.cshtml
	- Views/Forms/CustomOptions.cshtml
	- Views/Ui/TabsPills.cshtml
	- Views/Ui/Alerts.cshtml
	- Views/Modals/Examples.cshtml

### OP-11: หน้าติดตามคำขอใบรับรองและหลังตรวจปล่อย
- ครอบคลุมข้อกำหนด: 1.7(24)-(27), 1.7(31), 1.7(32)
- เป้าหมาย: ติดตามสถานะ, ปรับน้ำหนักหลังตรวจปล่อย, แก้ไขตามที่เจ้าหน้าที่แจ้ง, ส่งกลับเพื่อพิจารณา
- Components จาก Views:
	- Views/Tables/DatatablesAdvanced.cshtml
	- Views/Forms/InputGroups.cshtml
	- Views/Forms/BasicInputs.cshtml
	- Views/Ui/ListGroups.cshtml
	- Views/Ui/Accordion.cshtml
	- Views/Ui/Alerts.cshtml
	- Views/Ui/Badges.cshtml
	- Views/Modals/Examples.cshtml

### OP-12: หน้าค้นหาและตรวจสอบใบรับรอง (รวม QR Verification)
- ครอบคลุมข้อกำหนด: 1.7(28)-(30), 1.7(33), 1.7(34)
- เป้าหมาย: ค้นหาใบรับรอง, แสดงผลจาก QR, ดูข้อมูลอ้างอิง RMBS/P1/P2, export Excel
- Components จาก Views:
	- Views/Tables/DatatablesExtensions.cshtml
	- Views/Forms/BasicInputs.cshtml
	- Views/Forms/Selects.cshtml
	- Views/Ui/Badges.cshtml
	- Views/Ui/TooltipsPopovers.cshtml
	- Views/Icons/RiIcons.cshtml

### OP-13: หน้าคำขอเปลี่ยนแปลงใบรับรอง
- ครอบคลุมข้อกำหนด: 1.8(1)-(8)
- เป้าหมาย: สร้างคำขอแก้ไขใบรับรอง, แนบหลักฐาน, ติดตามสถานะ, พรีวิวเอกสารฉบับแก้ไข
- Components จาก Views:
	- Views/Forms/BasicInputs.cshtml
	- Views/Forms/Editors.cshtml
	- Views/Forms/FileUpload.cshtml
	- Views/Tables/DatatablesBasic.cshtml
	- Views/Ui/Alerts.cshtml
	- Views/Modals/Examples.cshtml

### OP-14: หน้าคำขอใบรับรองทดแทนฉบับเก่า
- ครอบคลุมข้อกำหนด: 1.9(1)-(9)
- เป้าหมาย: ยื่นคำขอใบแทน, ระบุเหตุผล, แนบหลักฐาน, ติดตามสถานะ
- Components จาก Views:
	- Views/Forms/BasicInputs.cshtml
	- Views/Forms/Pickers.cshtml
	- Views/Forms/FileUpload.cshtml
	- Views/Tables/DatatablesBasic.cshtml
	- Views/Ui/Alerts.cshtml
	- Views/Ui/Badges.cshtml
	- Views/Modals/Examples.cshtml

### OP-15: หน้าคำขอยกเลิกใบรับรอง
- ครอบคลุมข้อกำหนด: 1.10(1)-(12)
- เป้าหมาย: ยื่นยกเลิกใบรับรอง, ระบุเหตุผล, แนบหลักฐาน, ติดตามผลคืนน้ำหนักโควตา
- Components จาก Views:
	- Views/Forms/BasicInputs.cshtml
	- Views/Forms/Editors.cshtml
	- Views/Forms/FileUpload.cshtml
	- Views/Tables/DatatablesBasic.cshtml
	- Views/Ui/Alerts.cshtml
	- Views/Ui/Toasts.cshtml
	- Views/Modals/Examples.cshtml

### OP-16: หน้าคำขอคืนน้ำหนักสินค้าส่งคืน
- ครอบคลุมข้อกำหนด: 1.11(1)-(16)
- เป้าหมาย: สร้างคำขอคืนน้ำหนักแบบบางส่วน/ทั้งหมด, ระบุวัตถุประสงค์, แนบหลักฐาน, ตรวจรุ่นที่เกี่ยวข้องหลายใบรับรอง
- Components จาก Views:
	- Views/Forms/BasicInputs.cshtml
	- Views/Forms/Selects.cshtml
	- Views/Forms/InputGroups.cshtml
	- Views/Forms/FileUpload.cshtml
	- Views/Tables/DatatablesAdvanced.cshtml
	- Views/Ui/Accordion.cshtml
	- Views/Ui/Alerts.cshtml
	- Views/Modals/Examples.cshtml

### OP-17: หน้าซื้อขายเศษเหลือ
- ครอบคลุมข้อกำหนด: 1.12(1)-(8), 1.4(22), 1.6(19), 1.11(14)
- เป้าหมาย: สร้างหนังสือกำกับซื้อขายเศษเหลือ, บันทึกซื้อขายใน/นอกระบบ, ยืนยันรับซื้อ, พรีวิว PDF
- Components จาก Views:
	- Views/Forms/BasicInputs.cshtml
	- Views/Forms/Selects.cshtml
	- Views/Forms/InputGroups.cshtml
	- Views/Tables/DatatablesExtensions.cshtml
	- Views/Ui/Alerts.cshtml
	- Views/Ui/Badges.cshtml
	- Views/Modals/Examples.cshtml

### OP-18: หน้าตรวจสอบเส้นทางการซื้อขาย (Tree Diagram)
- ครอบคลุมข้อกำหนด: 1.4(12), 1.6(14)
- เป้าหมาย: ตรวจสอบ trace ย้อนกลับจาก RMBS/P1/P2 ไปยังต้นทาง, export Excel
- Components จาก Views:
	- Views/Ui/Accordion.cshtml
	- Views/Ui/Collapse.cshtml
	- Views/Ui/ListGroups.cshtml
	- Views/Tables/DatatablesExtensions.cshtml
	- Views/Ui/TooltipsPopovers.cshtml

## Requirement Coverage Matrix
| หมวดข้อกำหนด | ครอบคลุมโดยหน้าจอ |
| --- | --- |
| 1.1 | OP-02 |
| 1.2 | OP-03, OP-04 |
| 1.3 | OP-04, OP-05 |
| 1.4 | OP-06, OP-07, OP-17, OP-18 |
| 1.5 | OP-09 |
| 1.6 | OP-08, OP-17, OP-18 |
| 1.7 | OP-10, OP-11, OP-12 |
| 1.8 | OP-13 |
| 1.9 | OP-14 |
| 1.10 | OP-15 |
| 1.11 | OP-16, OP-17 |
| 1.12 | OP-17 |

## หมายเหตุการพัฒนา Mockup
- ปุ่มที่ต้องมีในทุกฟอร์มสำคัญ: บันทึก, ส่งคำขอ, ยกเลิก, ลบ, พรีวิว PDF, Export Excel
- หน้าจอที่มีอนุมัติ/ไม่อนุมัติให้ใช้ modal ยืนยันจาก Views/Modals/Examples.cshtml
- รายการสถานะให้ใช้ badge สีมาตรฐานเดียวกันทั้งระบบ
