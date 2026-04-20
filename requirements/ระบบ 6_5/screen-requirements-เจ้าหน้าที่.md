# Screen Requirements: เจ้าหน้าที่ (System 6_5)

## ขอบเขต
- เอกสารนี้กำหนดเฉพาะหน้าจอ (UI) เท่านั้น
- ไม่ทำ API และไม่ผูกฐานข้อมูลจริง
- ใช้ข้อมูล Mockup เท่านั้น
- ทุกหน้าจออยู่ภายใต้ Controller PSDeclaration
- อ้างอิงความต้องการจาก raw-requirements-เจ้าหน้าที่.txt (ข้อ 2.1-2.15)

## แนวทางข้อมูล Mockup
- ต้องมีข้อมูลจำลองสำหรับคำขอ RMBS1:PS, %Yield, P1/P2, ใบรับรอง, ใบรับรองสุขอนามัย, ประวัติการแก้ไข, ประวัติการลงนาม
- สถานะคำขอหลัก: ร่าง, ส่งพิจารณา, แจ้งแก้ไข, รออนุมัติ, อนุมัติ, ไม่อนุมัติ, ยกเลิก
- หน้าจออนุมัติรองรับ Mockup แบบ single และ multi-signature
- หน้าจอที่เกี่ยวกับตรวจปล่อยต้องมีตัวอย่างวันที่โหลด, วันที่ส่งออก, วันที่ตรวจปล่อย, issue date

## โครงสร้างเส้นทางหน้าจอ (ภายใต้ Controller PSDeclaration)
| Screen ID | Route ตัวอย่าง |
| --- | --- |
| OF-01 | /PSDeclaration/Officer/Dashboard |
| OF-02 | /PSDeclaration/Officer/RMBS1PS/Review |
| OF-03 | /PSDeclaration/Officer/RMBS1PS/Approve |
| OF-04 | /PSDeclaration/Officer/RMBS1PS/ChangeOrCancel |
| OF-05 | /PSDeclaration/Officer/Yield/Review |
| OF-06 | /PSDeclaration/Officer/P1P2/Inspection |
| OF-07 | /PSDeclaration/Officer/TraceabilityTree |
| OF-08 | /PSDeclaration/Officer/Certificate/Register |
| OF-09 | /PSDeclaration/Officer/Certificate/Prepare |
| OF-10 | /PSDeclaration/Officer/Certificate/SignFlow |
| OF-11 | /PSDeclaration/Officer/Certificate/PostRelease |
| OF-12 | /PSDeclaration/Officer/Certificate/Search |
| OF-13 | /PSDeclaration/Officer/Certificate/AmendReview |
| OF-14 | /PSDeclaration/Officer/Certificate/ReissueReview |
| OF-15 | /PSDeclaration/Officer/Certificate/CancelReview |
| OF-16 | /PSDeclaration/Officer/ReturnWeight/Review |
| OF-17 | /PSDeclaration/Officer/Certificate/PermissionSearch |
| OF-18 | /PSDeclaration/Officer/SelfCertificate/Search |
| OF-19 | /PSDeclaration/Officer/Reports |
| OF-20 | /PSDeclaration/Officer/UserAudit/Search |
| OF-21 | /PSDeclaration/Officer/AccessAndPending |
| OF-22 | /PSDeclaration/Officer/RegistrationCompliance |
| OF-23 | /PSDeclaration/Officer/IntakeTemplateConfig |

## รายการหน้าจอและ Component Mapping

### OF-01: หน้า Dashboard เจ้าหน้าที่
- ครอบคลุมข้อกำหนด: 2.1, 2.2, 2.4, 2.13
- เป้าหมาย: คิวงานรอพิจารณา, แจ้งเตือนงานค้าง, ตัวชี้วัดรายวัน
- Components จาก Views:
	- Views/Tables/DatatablesBasic.cshtml
	- Views/Ui/Badges.cshtml
	- Views/Ui/Alerts.cshtml
	- Views/Ui/Toasts.cshtml
	- Views/Ui/TabsPills.cshtml
	- Views/Icons/RiIcons.cshtml

### OF-02: หน้าพิจารณาคำขอ RMBS1:PS (เจ้าหน้าที่)
- ครอบคลุมข้อกำหนด: 2.1(1)-(4), 2.1(7)
- เป้าหมาย: ตรวจคำขอ, ตรวจเอกสาร/HC, ใส่ผลพิจารณา, ส่งต่อผู้อนุมัติ
- Components จาก Views:
	- Views/Tables/DatatablesAdvanced.cshtml
	- Views/Forms/BasicInputs.cshtml
	- Views/Forms/Editors.cshtml
	- Views/Forms/FileUpload.cshtml
	- Views/Ui/Accordion.cshtml
	- Views/Ui/Alerts.cshtml
	- Views/Modals/Examples.cshtml

### OF-03: หน้าอนุมัติ RMBS1:PS (ผู้อนุมัติ + Multi-signature)
- ครอบคลุมข้อกำหนด: 2.1(5), 2.1(6), 2.1(8)
- เป้าหมาย: อนุมัติ/ไม่อนุมัติหลายคำขอ, แก้ผลพิจารณาจากเจ้าหน้าที่, ใส่หมายเหตุ
- Components จาก Views:
	- Views/Tables/DatatablesExtensions.cshtml
	- Views/Forms/BasicInputs.cshtml
	- Views/Forms/CustomOptions.cshtml
	- Views/Ui/Badges.cshtml
	- Views/Ui/Alerts.cshtml
	- Views/Ui/Toasts.cshtml
	- Views/Modals/Examples.cshtml

### OF-04: หน้าเปลี่ยนผล/ยกเลิกผล RMBS1:PS หลังอนุมัติ
- ครอบคลุมข้อกำหนด: 2.1(9), 2.1(10), 2.1(11), 2.1(12)
- เป้าหมาย: ดึงสถานะกลับ, ขอเปลี่ยนผล, ขอ/อนุมัติยกเลิก, เก็บประวัติย้อนหลัง
- Components จาก Views:
	- Views/Forms/BasicInputs.cshtml
	- Views/Forms/Editors.cshtml
	- Views/Forms/FileUpload.cshtml
	- Views/Tables/DatatablesAdvanced.cshtml
	- Views/Ui/ListGroups.cshtml
	- Views/Ui/Accordion.cshtml
	- Views/Ui/Alerts.cshtml
	- Views/Modals/Examples.cshtml

### OF-05: หน้าพิจารณาอนุมัติ %Yield
- ครอบคลุมข้อกำหนด: 2.2(1)-(8)
- เป้าหมาย: ตรวจคำขอ %Yield, แจ้งแก้ไข/ระงับ/ยกเลิก/อนุมัติ, ตรวจช่วงเวลาอนุมัติ
- Components จาก Views:
	- Views/Forms/FileUpload.cshtml
	- Views/Forms/BasicInputs.cshtml
	- Views/Forms/InputGroups.cshtml
	- Views/Forms/Selects.cshtml
	- Views/Tables/DatatablesBasic.cshtml
	- Views/Ui/Alerts.cshtml
	- Views/Ui/Badges.cshtml

### OF-06: หน้าตรวจสอบเอกสาร P1/P2
- ครอบคลุมข้อกำหนด: 2.3(1)
- เป้าหมาย: ตรวจเอกสาร P1/P2, พรีวิว PDF, export Excel
- Components จาก Views:
	- Views/Tables/DatatablesExtensions.cshtml
	- Views/Forms/BasicInputs.cshtml
	- Views/Ui/TabsPills.cshtml
	- Views/Ui/TooltipsPopovers.cshtml
	- Views/Ui/Badges.cshtml

### OF-07: หน้าตรวจสอบ Traceability Tree + มาตรการ
- ครอบคลุมข้อกำหนด: 2.3(2), 2.3(3), 2.3(4)
- เป้าหมาย: ดูเส้นทางซื้อขายทั้งสาย, ตรวจมาตรการประเทศคู่ค้า/สุขอนามัย, export ได้
- Components จาก Views:
	- Views/Ui/Accordion.cshtml
	- Views/Ui/Collapse.cshtml
	- Views/Ui/ListGroups.cshtml
	- Views/Tables/DatatablesExtensions.cshtml
	- Views/Ui/Alerts.cshtml

### OF-08: หน้าลงทะเบียนรับคำขอใบรับรอง PS/NMD
- ครอบคลุมข้อกำหนด: 2.4(5)-(10), 2.4(12), 2.4(14)
- เป้าหมาย: ตรวจร่างคำขอ, แจ้งแก้ไขก่อนรับคำขอ, ลงทะเบียนรับเรื่อง, ออกเลขรับคำขอ
- Components จาก Views:
	- Views/Tables/DatatablesAdvanced.cshtml
	- Views/Forms/BasicInputs.cshtml
	- Views/Forms/Pickers.cshtml
	- Views/Ui/Alerts.cshtml
	- Views/Ui/Badges.cshtml
	- Views/Modals/Examples.cshtml

### OF-09: หน้าจัดทำใบรับรอง PS/NMD
- ครอบคลุมข้อกำหนด: 2.4(1)-(4), 2.4(11), 2.4(13), 2.4(15)-(17), 2.4(29), 2.4(32), 2.4(34)
- เป้าหมาย: จัดทำข้อมูลใบรับรอง, validate HC ซ้ำ, ออกเลขใบรับรอง, พิมพ์ Original/Copy
- Components จาก Views:
	- Views/Forms/BasicInputs.cshtml
	- Views/Forms/Selects.cshtml
	- Views/Forms/Pickers.cshtml
	- Views/Forms/FileUpload.cshtml
	- Views/Forms/Editors.cshtml
	- Views/Ui/TabsPills.cshtml
	- Views/Ui/Alerts.cshtml
	- Views/Modals/Examples.cshtml

### OF-10: หน้าเสนอลงนาม/ลงนามใบรับรอง
- ครอบคลุมข้อกำหนด: 2.4(20)-(25)
- เป้าหมาย: เสนอลงนามหลายใบ, เปลี่ยนผู้ลงนาม, ลงนามอิเล็กทรอนิกส์, ผู้ลงนามแทน
- Components จาก Views:
	- Views/Tables/DatatablesExtensions.cshtml
	- Views/Forms/Selects.cshtml
	- Views/Forms/FileUpload.cshtml
	- Views/Forms/CustomOptions.cshtml
	- Views/Ui/Badges.cshtml
	- Views/Ui/Toasts.cshtml
	- Views/Modals/Examples.cshtml

### OF-11: หน้าติดตามหลังตรวจปล่อยและปรับน้ำหนัก
- ครอบคลุมข้อกำหนด: 2.4(18), 2.4(19), 2.4(30), 2.4(31)
- เป้าหมาย: ตรวจสถานะผ่านด่าน, ปรับน้ำหนักตามตรวจปล่อย, ตรวจ issue date ตามเงื่อนไข
- Components จาก Views:
	- Views/Tables/DatatablesAdvanced.cshtml
	- Views/Forms/InputGroups.cshtml
	- Views/Forms/Pickers.cshtml
	- Views/Ui/Alerts.cshtml
	- Views/Ui/Badges.cshtml
	- Views/Modals/Examples.cshtml

### OF-12: หน้าค้นหาใบรับรองและตรวจสอบ QR
- ครอบคลุมข้อกำหนด: 2.4(26), 2.4(27), 2.4(28), 2.4(33)
- เป้าหมาย: ค้นหาใบรับรองย้อนหลัง, ตรวจสอบจาก QR, export รายการและข้อมูลอ้างอิง
- Components จาก Views:
	- Views/Tables/DatatablesExtensions.cshtml
	- Views/Forms/BasicInputs.cshtml
	- Views/Forms/Selects.cshtml
	- Views/Ui/TooltipsPopovers.cshtml
	- Views/Icons/RiIcons.cshtml

### OF-13: หน้าพิจารณาการขอเปลี่ยนแปลงใบรับรอง
- ครอบคลุมข้อกำหนด: 2.5(1)-(7)
- เป้าหมาย: พิจารณาคำขอแก้ไขใบรับรองและ workflow เทียบเท่าการออกใบรับรอง
- Components จาก Views:
	- Views/Forms/BasicInputs.cshtml
	- Views/Forms/Editors.cshtml
	- Views/Forms/FileUpload.cshtml
	- Views/Tables/DatatablesBasic.cshtml
	- Views/Ui/Alerts.cshtml
	- Views/Modals/Examples.cshtml

### OF-14: หน้าพิจารณาออกใบรับรองทดแทนฉบับเก่า
- ครอบคลุมข้อกำหนด: 2.6(1)-(8)
- เป้าหมาย: พิจารณาใบแทน, ยกเลิกฉบับเดิม, สร้างฉบับใหม่พร้อมสถานะใบแทน
- Components จาก Views:
	- Views/Forms/BasicInputs.cshtml
	- Views/Forms/FileUpload.cshtml
	- Views/Tables/DatatablesBasic.cshtml
	- Views/Ui/Alerts.cshtml
	- Views/Ui/Badges.cshtml
	- Views/Modals/Examples.cshtml

### OF-15: หน้าพิจารณายกเลิกใบรับรอง
- ครอบคลุมข้อกำหนด: 2.7(1)-(9)
- เป้าหมาย: พิจารณายกเลิกใบรับรอง, คืนน้ำหนัก/โควตาอัตโนมัติ, ติดตามคำขอยกเลิก
- Components จาก Views:
	- Views/Forms/BasicInputs.cshtml
	- Views/Forms/Editors.cshtml
	- Views/Tables/DatatablesBasic.cshtml
	- Views/Ui/Alerts.cshtml
	- Views/Ui/Toasts.cshtml
	- Views/Modals/Examples.cshtml

### OF-16: หน้าพิจารณาคืนน้ำหนักสินค้าส่งคืน
- ครอบคลุมข้อกำหนด: 2.8(1)-(16)
- เป้าหมาย: พิจารณาคำขอคืนน้ำหนัก, ตรวจหลายใบรับรอง/หลายรุ่น, ระบุผลอนุมัติ
- Components จาก Views:
	- Views/Forms/BasicInputs.cshtml
	- Views/Forms/Selects.cshtml
	- Views/Forms/FileUpload.cshtml
	- Views/Tables/DatatablesAdvanced.cshtml
	- Views/Ui/Accordion.cshtml
	- Views/Ui/Alerts.cshtml
	- Views/Modals/Examples.cshtml

### OF-17: หน้าค้นหาใบรับรอง PS/NMD ตามสิทธิ์หน่วยงาน
- ครอบคลุมข้อกำหนด: 2.9(1)-(2)
- เป้าหมาย: ให้เจ้าหน้าที่ที่มีสิทธิ์ค้นหาข้อมูลและดูรายละเอียดตามข้อจำกัดสิทธิ์
- Components จาก Views:
	- Views/Tables/DatatablesAdvanced.cshtml
	- Views/Forms/BasicInputs.cshtml
	- Views/Forms/Selects.cshtml
	- Views/Ui/Badges.cshtml
	- Views/Ui/TooltipsPopovers.cshtml

### OF-18: หน้าค้นหาหนังสือรับรองตนเอง
- ครอบคลุมข้อกำหนด: 2.10(1)-(4)
- เป้าหมาย: ค้นหาเอกสารรับรองตนเอง, เปิดดูลิงก์ RMBS/P1/P2, export Excel
- Components จาก Views:
	- Views/Tables/DatatablesExtensions.cshtml
	- Views/Forms/BasicInputs.cshtml
	- Views/Forms/Selects.cshtml
	- Views/Ui/TabsPills.cshtml
	- Views/Ui/Badges.cshtml

### OF-19: หน้ารายงาน Static/Dynamic
- ครอบคลุมข้อกำหนด: 2.11(1)-(12)
- เป้าหมาย: กรองรายงานตามเงื่อนไขกรมประมง, แสดง data sheet, export Excel
- Components จาก Views:
	- Views/Forms/BasicInputs.cshtml
	- Views/Forms/Selects.cshtml
	- Views/Forms/Pickers.cshtml
	- Views/Tables/DatatablesExtensions.cshtml
	- Views/Ui/TabsPills.cshtml
	- Views/Ui/Badges.cshtml

### OF-20: หน้าค้นหาข้อมูลผู้เข้าใช้งาน
- ครอบคลุมข้อกำหนด: 2.12(1)
- เป้าหมาย: สืบค้นข้อมูลผู้ใช้งานระบบและดูรายละเอียดการใช้งาน
- Components จาก Views:
	- Views/Tables/DatatablesBasic.cshtml
	- Views/Forms/BasicInputs.cshtml
	- Views/Forms/Selects.cshtml
	- Views/Ui/Badges.cshtml

### OF-21: หน้าสิทธิ์การใช้งานและรายการงานค้าง
- ครอบคลุมข้อกำหนด: 2.13(1)-(2)
- เป้าหมาย: ตรวจสิทธิ์ตามหน่วยงาน/บทบาท และแสดงรายการคำขอค้างพร้อมลิงก์เข้ารายละเอียด
- Components จาก Views:
	- Views/Tables/DatatablesAdvanced.cshtml
	- Views/Forms/Switches.cshtml
	- Views/Forms/Selects.cshtml
	- Views/Ui/Alerts.cshtml
	- Views/Ui/Badges.cshtml

### OF-22: หน้าตรวจสอบข้อมูลลงทะเบียนผู้ใช้งานสำหรับใบรับรอง
- ครอบคลุมข้อกำหนด: 2.14(1)-(4)
- เป้าหมาย: ตรวจข้อมูลผู้ประกอบการ TH/EN, ตรวจความสอดคล้อง DOF List, แจ้งเตือน mismatch
- Components จาก Views:
	- Views/Forms/BasicInputs.cshtml
	- Views/Forms/InputGroups.cshtml
	- Views/Forms/Selects.cshtml
	- Views/Tables/DatatablesBasic.cshtml
	- Views/Ui/Alerts.cshtml

### OF-23: หน้ากำหนดต้นแบบการลงทะเบียนรับเรื่องและผู้ทำหน้าที่แทน
- ครอบคลุมข้อกำหนด: 2.15(1)
- เป้าหมาย: กำหนด template ขั้นตอนรับเรื่องและ mapping ผู้แทนในหน่วยงาน
- Components จาก Views:
	- Views/Forms/BasicInputs.cshtml
	- Views/Forms/Selects.cshtml
	- Views/Forms/Switches.cshtml
	- Views/Forms/Pickers.cshtml
	- Views/Tables/DatatablesBasic.cshtml
	- Views/Ui/Alerts.cshtml

## Requirement Coverage Matrix
| หมวดข้อกำหนด | ครอบคลุมโดยหน้าจอ |
| --- | --- |
| 2.1 | OF-02, OF-03, OF-04 |
| 2.2 | OF-05 |
| 2.3 | OF-06, OF-07 |
| 2.4 | OF-08, OF-09, OF-10, OF-11, OF-12 |
| 2.5 | OF-13 |
| 2.6 | OF-14 |
| 2.7 | OF-15 |
| 2.8 | OF-16 |
| 2.9 | OF-17 |
| 2.10 | OF-18 |
| 2.11 | OF-19 |
| 2.12 | OF-20 |
| 2.13 | OF-21 |
| 2.14 | OF-22 |
| 2.15 | OF-23 |

## หมายเหตุการพัฒนา Mockup
- ทุกหน้าพิจารณา/อนุมัติใช้ modal ยืนยันจาก Views/Modals/Examples.cshtml
- ทุกหน้ารายการที่ต้อง export ใช้ DatatablesExtensions เพื่อรองรับ Excel/PDF/CSV
- สถานะเอกสารและคำขอใช้ badge สีเดียวกันทั้งระบบเพื่อให้ตีความตรงกัน
