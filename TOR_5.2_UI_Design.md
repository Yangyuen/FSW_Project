# ![DPIM Logo](docs/images/logo-dpim.png) TOR 5.2 — Mining Account / SSO Registration UI Design

> **กรมอุตสาหกรรมพื้นฐานและการเหมืองแร่ (กพร.)**  
> **Department of Primary Industries and Mines (DPIM)**  
> 
> **วันที่จัดทำ:** 25 เมษายน 2569  
> **ขอบเขต:** ออกแบบ UI Wireframe-level สำหรับ TOR ข้อ 5.2 ระบบพิสูจน์และยืนยันตัวตน และบัญชีรายชื่อผู้ใช้งาน (Mining Account)  
> **Integration:** ใช้ Mock/Placeholder สำหรับ i-Industry API, ThaiID API, i-Industry Officer API  
> **Theme:** Mining for the Community (เหมืองแร่เพื่อชุมชน)  

---

## 📋 บทนำ (Introduction)

![Mining for the Community](docs/images/banner-mining-community.jpg)

ระบบ **กรมอุตสาหกรรมพื้นฐานและการเหมืองแร่ (กพร.) — Mining Portal (DPIM)** เป็นแพลตฟอร์มดิจิทัลที่รองรับการจัดการบัญชีผู้ใช้งานแบบสมบูรณ์สำหรับภาคเหมืองแร่ไทย โดยมีเป้าหมายเพื่อให้บริการข้อมูลและระบบงานอย่างปลอดภัย โปร่งใส และสะดวก ภายใต้หลักการ **"เหมืองแร่เพื่อชุมชน"** 

เอกสารนี้เป็นข้อกำหนดการออกแบบ UI ตามข้อ 5.2 ของ TOR ครอบคลุม:
- 🔐 **ระบบลงทะเบียนและยืนยันตัวตน** (Authentication & Registration)
- 🆔 **บัญชีรายชื่อผู้ใช้งาน** (User Account Management)
- 🔒 **ระบบ Single Sign-On (SSO)** พร้อม Integration ต่าง ๆ
- 👥 **บริหารจัดการผู้ใช้งาน** สำหรับผู้ดูแลระบบ

---

| กลุ่ม | ชื่อกลุ่ม | จำนวนหน้า | มีแล้ว / ขยาย | ต้องสร้างใหม่ |
|-------|-----------|-----------|----------------|---------------|
| A | Registration | 6 | 2 | 4 |
| B | Login / Authentication | 7 | 3 | 4 |
| C | Access Request | 3 | 0 | 3 |
| D | User Profile & Settings | 3 | 2 | 1 |
| E | Digital Certificate | 1 | 0 | 1 |
| F | Admin / SSO Management | 10 | 2 | 8 |
| **รวม** | | **30** | **9** | **21** |

---

## กลุ่ม A — Registration Pages
> อ้างอิง TOR: 5.2.1, 5.2.2, 5.2.3.4(9)

---

### A1 — หน้าเลือกช่องทางลงทะเบียน (Channel Selection)

- **Route:** `/Auth/RegisterChannel`
- **Controller:** `AuthController.RegisterChannel()`
- **Layout:** `_BlankLayout`
- **สถานะ:** ต้องสร้างใหม่

#### Layout / Sections

```
┌─────────────────────────────────────────┐
│    Logo กรมอุตสาหกรรมพื้นฐาน (กพร.)   │
│  "ลงทะเบียนเข้าใช้งาน DPIM Portal"     │
├─────────┬─────────────┬─────────────────┤
│  Card 1 │   Card 2    │     Card 3      │
│  [Icon] │   [Icon]    │    [Icon]       │
│  Self-  │   Walk-In   │    ThaiID       │
│ Service │             │  (โลโก้ ThaiID) │
│ คำอธิบาย│  คำอธิบาย  │   คำอธิบาย     │
│ [เลือก] │  [เลือก]   │   [เลือก]      │
└─────────┴─────────────┴─────────────────┘
│       มีบัญชีแล้ว? [เข้าสู่ระบบ]       │
└─────────────────────────────────────────┘
```

#### Components

| Component | รายละเอียด |
|-----------|-----------|
| Card — Self-Service | Icon: `bx-user-plus`, Label: "ลงทะเบียนด้วยตนเอง", คำอธิบาย: "สำหรับผู้ประกอบการที่เข้าสู่ระบบ DPIM", Link → A2 |
| Card — Walk-In | Icon: `bx-building`, Label: "ลงทะเบียนผ่านเจ้าหน้าที่", คำอธิบาย: "ติดต่อยื่นเอกสารที่กรมอุตสาหกรรมพื้นฐาน", Badge: "Walk-In" |
| Card — ThaiID | Icon: `bx-id-card`, Label: "ลงทะเบียนผ่าน ThaiID", คำอธิบาย: "ยืนยันตัวตนด้วย Application ThaiID", Link → A4 |
| Footer Link | "มีบัญชีแล้ว? เข้าสู่ระบบ" → `/Auth/LoginBasic` |

---

### A2 — ลงทะเบียน Self-Service ผู้ประกอบการ (Multi-Step)

- **Route:** `/Auth/RegisterMultiSteps`
- **Controller:** `AuthController.RegisterMultiSteps()`
- **Layout:** `_BlankLayout`
- **สถานะ:** ขยายจาก `Views/Auth/RegisterMultiSteps.cshtml` (มีอยู่แล้ว)
- **UI Reference:** 📐 ใช้ pattern จาก [Views/Wizards/CreateDeal.cshtml](Views/Wizards/CreateDeal.cshtml) — Vertical Stepper Layout ด้วย bs-stepper library

#### Wizard Steps

```
┌──────────────────────────────────────────────────────────────┐
│              ลงทะเบียน Self-Service (DPIM)                  │
├────────────────────┬───────────────────────────────────────────┤
│ 01 ข้อมูลพื้นฐาน   │  Form: บุคคลธรรมดา/นิติบุคคล/วิสาหกิจ  │
│    ──────────────  │  ชื่อ, ประเภท, เลขบัตร, อีเมล, เบอร์   │
│ 02 แนบเอกสาร       │  Drag & Drop: อัปโหลดเอกสาร            │
│    ──────────────  │  (แบบ conditional ตามประเภท)          │
│ 03 ตรวจสอบ        │  Summary: ทบทวนข้อมูลและเอกสาร         │
│    ──────────────  │  Checkbox: ยอมรับ Privacy Policy       │
│ 04 สำเร็จ          │  Success: หมายเลขอ้างอิง + ขั้นตอนต่อ   │
│    ──────────────  │                                          │
└────────────────────┴───────────────────────────────────────────┘

Component: bs-stepper (Vertical Layout)
- ด้านซ้าย: Step indicator (numbered 01-04)
- ด้านขวา: Form / Content สำหรับแต่ละ step
- Navigation: [ย้อนกลับ] [ถัดไป] / [ส่ง]
```

##### Step 1 — ข้อมูลพื้นฐาน

| Field | ประเภท | Validation |
|-------|--------|-----------|
| ประเภทสมาชิก | Dropdown: บุคคลธรรมดา / นิติบุคคล / วิสาหกิจชุมชน | Required |
| คำนำหน้า | Dropdown: นาย / นาง / นางสาว | Required (บุคคลธรรมดา) |
| ชื่อ | Text | Required |
| นามสกุล | Text | Required |
| เลขบัตรประชาชน / เลขทะเบียนนิติบุคคล | Text + Mask | Required, 13 หลัก, ตรวจสอบซ้ำ |
| อีเมล | Email | Required, Format, ตรวจสอบซ้ำ |
| เบอร์โทรศัพท์ | Tel | Required, 10 หลัก |
| รหัสผ่าน | Password | Required, ≥8 ตัว, ตัวอักษร+ตัวเลข+สัญลักษณ์ |
| ยืนยันรหัสผ่าน | Password | Required, ต้องตรงกัน |

> **Duplicate Check:** หากเลขบัตรซ้ำ แสดง Alert: "หมายเลขบัตรประชาชน/ทะเบียนนี้มีในระบบแล้ว กรุณาติดต่อเจ้าหน้าที่"

##### Step 2 — แนบเอกสาร (Conditional ตามประเภทสมาชิก)

| ประเภทสมาชิก | เอกสารที่ต้องแนบ |
|-------------|----------------|
| บุคคลธรรมดา | สำเนาบัตรประชาชน, สำเนาทะเบียนบ้าน |
| นิติบุคคล | หนังสือรับรองบริษัท (ไม่เกิน 3 เดือน), สำเนาบัตรประชาชนผู้มีอำนาจ, หนังสือมอบอำนาจ (ถ้ามี) |
| วิสาหกิจชุมชน | หนังสือรับรองวิสาหกิจ, สำเนาบัตรประชาชนประธาน |

- Drag & Drop Upload Zone
- รองรับ: PDF, JPG, PNG ขนาดไม่เกิน 5 MB ต่อไฟล์
- แสดงรายการไฟล์ที่อัปโหลดพร้อมปุ่มลบ

##### Step 3 — ตรวจสอบข้อมูล

- แสดงข้อมูลทั้งหมดแบบ Read-only (2 Column)
- รายการเอกสารที่แนบ
- Checkbox: "ฉันได้อ่านและยอมรับ [นโยบายความเป็นส่วนตัว] และ [ข้อตกลงการใช้งาน]" (Required)
- ปุ่ม: `[ย้อนกลับ]` `[ยืนยันการลงทะเบียน]`

##### Step 4 — สำเร็จ

- Icon: `bx-check-circle` สีเขียว
- หมายเลขอ้างอิง: เช่น `REG-2569-00001` (กรมอุตสาหกรรมพื้นฐาน)
- ข้อความ: "ระบบ DPIM ได้ส่งลิงก์ยืนยันไปยัง [email] กรุณาตรวจสอบอีเมลเพื่อเปิดใช้งานบัญชี"
- ปุ่ม: `[กลับหน้าหลัก]`

---

### A3 — ลงทะเบียน Walk-In (เจ้าหน้าที่กรอกแทน)

- **Route:** `/Officer/RegisterUser`
- **Controller:** `OfficerController.RegisterUser()`
- **Layout:** `_ContentNavbarLayout`
- **สถานะ:** ต้องสร้างใหม่

#### Layout / Sections

```
┌──────────────────────────────────────────────┐
│  Breadcrumb: เจ้าหน้าที่ > ลงทะเบียนสมาชิก │
├──────────────────────────────────────────────┤
│  Card: ข้อมูลสมาชิก                          │
│  [Form Fields เหมือน A2 Step 1-2]            │
│  เพิ่ม: บันทึกเพิ่มเติม (Textarea)           │
│  เพิ่ม: สถานะเริ่มต้น (Active/Pending)       │
├──────────────────────────────────────────────┤
│  [บันทึก Draft]  [บันทึกและส่งอีเมลเชิญ]    │
└──────────────────────────────────────────────┘
```

#### ความแตกต่างจาก A2

- ไม่มีช่อง Password (ระบบสร้าง Temporary Password และส่งทาง Email)
- มีช่อง "บันทึกเพิ่มเติม (เจ้าหน้าที่)" — Textarea
- มีปุ่ม "บันทึก Draft" และ "บันทึกและส่งอีเมลเชิญ"
- สามารถกำหนด Activate Date ล่วงหน้าได้

---

### A4 — ลงทะเบียนผ่าน ThaiID

- **Route:** `/Auth/RegisterThaiID` (Redirect Page) + `/Auth/RegisterThaiIDCallback` (Callback)
- **Controller:** `AuthController.RegisterThaiID()`, `AuthController.RegisterThaiIDCallback()`
- **Layout:** `_BlankLayout`
- **สถานะ:** ต้องสร้างใหม่

#### Redirect Page

```
┌──────────────────────────────┐
│   [โลโก้ ThaiID]             │
│   กำลังเชื่อมต่อ ThaiID...  │
│   [Spinner Animation]        │
│                              │
│   [ยกเลิก]                  │
└──────────────────────────────┘
```

#### Callback Page (กรอกข้อมูลเพิ่มเติม)

| Field | ประเภท | หมายเหตุ |
|-------|--------|---------|
| ชื่อ-นามสกุล | Text (Read-only) | ดึงจาก ThaiID |
| เลขบัตรประชาชน | Text (Read-only) | ดึงจาก ThaiID |
| อีเมล | Email | กรอกเอง (Required) |
| เบอร์โทรศัพท์ | Tel | กรอกเอง (Required) |
| ประเภทสมาชิก | Dropdown | กรอกเอง (Required) |

---

### A5 — Verify Email / Activate Account

- **Route:** `/Auth/VerifyEmailBasic`
- **สถานะ:** มีแล้ว — ขยายเพิ่ม

#### เพิ่มเติมจากที่มีอยู่

- OTP 6 หลัก (6 Input boxes แยก)
- Countdown timer: "ส่งรหัสใหม่ได้ใน 02:30"
- ปุ่ม "ส่งรหัสอีกครั้ง" (ใช้งานได้เมื่อหมดเวลา)
- ลิงก์ "เปลี่ยนอีเมล"

---

### A6 — สรุปผลการลงทะเบียน

- **Route:** `/Auth/RegisterComplete`
- **Controller:** `AuthController.RegisterComplete()`
- **Layout:** `_BlankLayout`
- **สถานะ:** ต้องสร้างใหม่

#### Layout

```
┌──────────────────────────────────────┐
│   [Icon สำเร็จ / รอดำเนินการ]       │
│   "ลงทะเบียนสำเร็จ"                 │
│   หมายเลขอ้างอิง: REG-2569-00001    │
├──────────────────────────────────────┤
│   ขั้นตอนถัดไป (Stepper/Timeline):  │
│   ✓ ลงทะเบียนสำเร็จ                │
│   ○ ยืนยัน Email                    │
│   ○ รอการอนุมัติ (ถ้ามี)            │
│   ○ เข้าใช้งานระบบ                  │
├──────────────────────────────────────┤
│   [กลับหน้าหลัก]  [เข้าสู่ระบบ]    │
└──────────────────────────────────────┘
```

---

## กลุ่ม B — Login / Authentication Pages
> อ้างอิง TOR: 5.2.3

---

### B1 — Login หลัก

- **Route:** `/Auth/LoginBasic`
- **สถานะ:** มีแล้ว — ขยายเพิ่ม
- **UI Reference:** 📐 ใช้ pattern จาก [Views/Auth/RegisterCover.cshtml](Views/Auth/RegisterCover.cshtml) — Cover Layout พร้อม Background Illustration

#### เพิ่มเติมจากที่มีอยู่

```
┌────────────────────────────────┬─────────────────────────────────┐
│ Left Side (ซ่อน mobile,       │ Right Side: Login Form          │
│ แสดง desktop):                │ ┌─────────────────────────────┐ │
│ ┌──────────────────────────┐   │ │ Logo + ชื่อระบบ            │ │
│ │ Illustration:            │   │ │ "DPIM Portal"              │ │
│ │ Mining/Mining community  │   │ ├─────────────────────────────┤ │
│ │ theme                    │   │ │ [Email / Username]         │ │
│ │                          │   │ │ [Password]                 │ │
│ │                          │   │ │ [Remember Me]              │ │
│ │ + Background Mask Image  │   │ │ [เข้าสู่ระบบ]            │ │
│ └──────────────────────────┘   │ ├─────────────────────────────┤ │
│                                 │ ─── หรือเข้าสู่ระบบด้วย ─── │
│                                 │ [i-Industry]                 │
│                                 │ [i-Industry Officer]         │
│                                 │ [ThaiID]                     │
│                                 ├─────────────────────────────┤ │
│                                 │ ยังไม่มีบัญชี?              │
│                                 │ [ลงทะเบียน] / [ลืมรหัส]    │
│                                 └─────────────────────────────┘ │
└────────────────────────────────┴─────────────────────────────────┘

CSS Classes:
- Container: .authentication-wrapper.authentication-cover
- Left: col-lg-7 col-xl-8 (display: none on mobile)
- Right: col-lg-5 col-xl-4 .authentication-bg
- Illustration: .auth-cover-illustration (responsive data-app-* attrs)
```

---

### B2 — SSO Redirect — i-Industry (ผู้ประกอบการ)

- **Route:** `/Auth/SSORedirectIndustry`
- **Controller:** `AuthController.SSORedirectIndustry()`
- **Layout:** `_BlankLayout`
- **สถานะ:** ต้องสร้างใหม่

#### Layout

```
┌──────────────────────────────────┐
│   [โลโก้ i-Industry]             │
│   กำลังเชื่อมต่อ i-Industry...  │
│   [Spinner]                      │
│   (Mock: Redirect ไป placeholder)│
│   [ยกเลิก]                      │
└──────────────────────────────────┘
```

---

### B3 — SSO Redirect — i-Industry Officer (เจ้าหน้าที่)

- **Route:** `/Auth/SSORedirectOfficer`
- **Controller:** `AuthController.SSORedirectOfficer()`
- **Layout:** `_BlankLayout`
- **สถานะ:** ต้องสร้างใหม่
- Layout เหมือน B2 ต่างแค่ Label และโลโก้

---

### B4 — SSO Redirect — ThaiID

- **Route:** `/Auth/SSORedirectThaiID`
- **Controller:** `AuthController.SSORedirectThaiID()`
- **Layout:** `_BlankLayout`
- **สถานะ:** ต้องสร้างใหม่
- Layout เหมือน B2 ต่างแค่ Label และโลโก้ ThaiID

---

### B5 — MFA / Two-Factor Verification

- **Route:** `/Auth/TwoStepsBasic`
- **สถานะ:** มีแล้ว — ขยายเพิ่ม

#### เพิ่มเติมจากที่มีอยู่

- **Tabs เลือกวิธี MFA:**
  - Email OTP (6 หลัก)
  - Google Authenticator / Microsoft Authenticator (TOTP 6 หลัก)
- **Trusted Device Checkbox:** "จดจำอุปกรณ์นี้ (30 วัน)" ไม่ต้อง MFA อีกครั้ง
- แสดงข้อมูล: "กำลังยืนยันสำหรับบัญชี: [email]"

---

### B6 — Forgot Password

- **Route:** `/Auth/ForgotPasswordBasic`
- **สถานะ:** มีแล้ว — ขยายเพิ่ม

#### เพิ่มเติม

- เพิ่ม Tab: "รับ OTP ผ่าน SMS" (เบอร์โทรที่ลงทะเบียนไว้)

---

### B7 — Reset Password

- **Route:** `/Auth/ResetPasswordBasic`
- **สถานะ:** มีแล้ว — ครบถ้วน
- Password Strength Indicator
- Validation: ≥8 ตัว, มีตัวอักษร+ตัวเลข+สัญลักษณ์

---

## กลุ่ม C — Access Request Pages
> อ้างอิง TOR: 5.2.3.1(2), 5.2.3.4(9)

---

### C1 — ยื่นคำขอเปิดสิทธิ์ระบบ (ผู้ประกอบการ)

- **Route:** `/Auth/AccessRequest`
- **Controller:** `AuthController.AccessRequest()`
- **Layout:** `_ContentNavbarLayout`
- **สถานะ:** ต้องสร้างใหม่

#### Layout / Fields

```
┌──────────────────────────────────────────┐
│  "ยื่นคำขอสิทธิ์การเข้าใช้งาน"          │
├──────────────────────────────────────────┤
│  Card: เลือกระบบงานที่ต้องการ           │
│  [Checkbox] ระบบตรวจสอบ RMBS1           │
│  [Checkbox] ระบบประกาศ PS/NoPS           │
│  [Checkbox] ระบบนำเข้า/ส่งออกแร่        │
├──────────────────────────────────────────┤
│  Card: ข้อมูลผู้ยื่นคำขอ                │
│  ชื่อ-นามสกุล (Auto-fill Read-only)     │
│  ตำแหน่ง: [Text]                         │
│  หน่วยงาน: [Text]                        │
│  เหตุผลการขอ: [Textarea]                 │
├──────────────────────────────────────────┤
│  Card: แนบเอกสาร                         │
│  [Drag & Drop Upload Zone]               │
├──────────────────────────────────────────┤
│  [ยกเลิก]            [ส่งคำขอ]          │
└──────────────────────────────────────────┘
```

---

### C2 — รายการคำขอสิทธิ์ (เจ้าหน้าที่รีวิว)

- **Route:** `/Officer/AccessRequestList`
- **Controller:** `OfficerController.AccessRequestList()`
- **Layout:** `_ContentNavbarLayout`
- **สถานะ:** ต้องสร้างใหม่

#### DataTable Columns

| Column | รายละเอียด |
|--------|-----------|
| หมายเลขอ้างอิง | REG-2569-xxxxx |
| ชื่อผู้ขอ | ชื่อ-นามสกุล |
| ระบบงานที่ขอ | Badge list |
| วันที่ยื่น | DD/MM/YYYY |
| สถานะ | Badge: รอพิจารณา / อนุมัติ / ปฏิเสธ |
| Actions | [ดูรายละเอียด] [อนุมัติ] [ปฏิเสธ] |

#### Filter Bar

- Dropdown สถานะ
- Date Range Picker (วันที่ยื่น)
- Search (ชื่อ / หมายเลขอ้างอิง)

---

### C3 — อนุมัติ / ปฏิเสธคำขอสิทธิ์

- **Route:** `/Officer/ReviewAccessRequest/{id}`
- **Controller:** `OfficerController.ReviewAccessRequest(int id)`
- **Layout:** `_ContentNavbarLayout`
- **สถานะ:** ต้องสร้างใหม่

#### Layout / Sections

```
┌──────────────────────────────────────────┐
│  Breadcrumb > รายการคำขอ > รีวิวคำขอ    │
├───────────────────┬──────────────────────┤
│  ข้อมูลผู้ขอ      │  เอกสารแนบ           │
│  (Read-only card)│  (File preview)      │
├───────────────────┴──────────────────────┤
│  ผลการพิจารณา:                           │
│  ● อนุมัติ  ○ ปฏิเสธ                   │
│  เหตุผล: [Textarea] (Required ถ้าปฏิเสธ)│
├──────────────────────────────────────────┤
│  [ย้อนกลับ]    [บันทึกผลการพิจารณา]    │
└──────────────────────────────────────────┘
```

> **หมายเหตุ:** เมื่อบันทึก ระบบส่ง Email แจ้งผลการพิจารณาพร้อมเหตุผลไปยังผู้ยื่นอัตโนมัติ

---

## กลุ่ม D — User Profile & Settings
> อ้างอิง TOR: 5.2.5, 5.2.6

---

### D1 — User Profile (แก้ไขข้อมูลส่วนตัว)

- **Route:** `/Users/ViewAccount`
- **สถานะ:** มีแล้ว — ขยายเพิ่ม

#### เพิ่มเติมจากที่มีอยู่

- แยกส่วนข้อมูล เป็น 2 กลุ่ม:

| กลุ่ม | Fields | เงื่อนไขการแก้ไข |
|-------|--------|-----------------|
| ข้อมูลทั่วไป | ชื่อ, นามสกุล, เบอร์โทร, หน่วยงาน, อีเมลสำรอง | แก้ไขได้ปกติ |
| ข้อมูลสำคัญ | เลขบัตรประชาชน, วันเกิด, อีเมลหลัก | ต้องยืนยันตัวตนใหม่ก่อนแก้ไข (Modal Verify Password / OTP) |

---

### D2 — Address Book (จัดการหลายที่อยู่)

- **Route:** `/Users/ViewAddresses`
- **Controller:** `UsersController.ViewAddresses()`
- **Layout:** `_ContentNavbarLayout`
- **สถานะ:** ต้องสร้างใหม่

#### Layout / Sections

```
┌─────────────────────────────────────────────┐
│  "บัญชีที่อยู่"              [+ เพิ่มที่อยู่]│
├────────────────┬────────────────┬────────────┤
│ Card: ที่อยู่ 1│ Card: ที่อยู่ 2│  Card: +   │
│ [Badge: หลัก] │                │  เพิ่มใหม่ │
│ บ้านเลขที่...  │ บ้านเลขที่... │            │
│ [แก้ไข][ลบ]  │ [แก้ไข][ลบ]  │            │
│ [ตั้งเป็นหลัก]│ [ตั้งเป็นหลัก]│            │
└────────────────┴────────────────┴────────────┘
```

#### Modal Form — เพิ่ม/แก้ไขที่อยู่

| Field | ประเภท | Validation |
|-------|--------|-----------|
| ชื่อที่อยู่ (Label) | Text | Required เช่น "ที่อยู่บ้าน", "ที่อยู่สำนักงาน" |
| บ้านเลขที่ | Text | Required |
| หมู่ที่ / ซอย / ถนน | Text | Optional |
| ตำบล/แขวง | Text/Select | Required |
| อำเภอ/เขต | Text/Select | Required |
| จังหวัด | Select | Required |
| รหัสไปรษณีย์ | Text | Required, 5 หลัก |
| ประเภทที่อยู่ | Select: ที่อยู่จัดส่ง / ที่อยู่ออกใบกำกับ / ที่อยู่ติดต่อ | Required |

---

### D3 — Security Settings

- **Route:** `/Users/ViewSecurity`
- **สถานะ:** มีแล้ว — ขยายเพิ่ม

#### เพิ่มเติม

**Section: MFA Setup**

| วิธี | Toggle | รายละเอียด |
|------|--------|-----------|
| Email OTP | On/Off | แสดง Email ที่ใช้ |
| Authenticator App | On/Off | QR Code สำหรับ Setup + Backup Codes |

**Section: Trusted Devices**

- DataTable: ชื่ออุปกรณ์, Browser, วันที่เพิ่ม, วันหมดอายุ
- ปุ่ม [ลบ] แต่ละรายการ
- ปุ่ม [ลบทั้งหมด]

---

## กลุ่ม E — Digital Certificate
> อ้างอิง TOR: 5.2.4

---

### E1 — ใบรับรองอิเล็กทรอนิกส์ Self-Sign

- **Route:** `/Users/Certificate`
- **Controller:** `UsersController.Certificate()`
- **Layout:** `_ContentNavbarLayout`
- **สถานะ:** ต้องสร้างใหม่
- **ผู้ใช้:** ทั้งผู้ประกอบการและเจ้าหน้าที่

#### Layout / Sections

```
┌──────────────────────────────────────────┐
│  "ใบรับรองอิเล็กทรอนิกส์ของฉัน"        │
├──────────────────────────────────────────┤
│  Card: สถานะใบรับรอง                    │
│  [Badge: Active / หมดอายุ / ยังไม่มี]  │
│  ชื่อ: [ชื่อผู้ใช้]                     │
│  Serial: [Serial Number]                 │
│  วันที่ออก: DD/MM/YYYY                  │
│  วันหมดอายุ: DD/MM/YYYY                 │
├──────────────────────────────────────────┤
│  [ขอออกใบรับรอง] [ดาวน์โหลด] [ยกเลิก]  │
└──────────────────────────────────────────┘
```

---

## กลุ่ม F — Admin / SSO Management
> อ้างอิง TOR: 5.2.3.4(7)(18)-(30)

---

### F1 — User Management (CRUD + กลุ่ม + สิทธิ์)

- **Route:** `/Users/List`
- **สถานะ:** มีแล้ว — ขยายเพิ่ม

#### เพิ่มเติม Columns

| Column เพิ่มเติม | รายละเอียด |
|----------------|-----------|
| กลุ่ม/บทบาท | Badge list |
| วันที่เปิดใช้งาน (Activate Date) | DD/MM/YYYY |
| สถานะ | Active / Suspended / Pending |
| Actions เพิ่ม | [กำหนดกลุ่ม] [เพิกถอนสิทธิ์] |

#### Modal: กำหนดกลุ่มและบทบาท

- Multi-select Checkbox: กลุ่มผู้ใช้
- Dropdown: บทบาทหลัก
- Date Picker: วันที่เริ่มใช้งาน (Activate Date)

---

### F2 — Privacy Policy & Notice Management

- **Route:** `/Admin/PrivacyPolicy`
- **Controller:** `AdminController.PrivacyPolicy()`
- **Layout:** `_ContentNavbarLayout`
- **สถานะ:** ต้องสร้างใหม่

#### Sections

| Section | รายละเอียด |
|---------|-----------|
| รายการ Version | DataTable: Version, วันที่บังคับใช้, สถานะ (Draft/Published), Actions |
| Form แก้ไข | Rich Text Editor (Quill/TinyMCE), ชื่อเอกสาร, วันที่บังคับใช้, ปุ่ม Save Draft / Publish |
| ประวัติ Consent | DataTable: ผู้ใช้, วันที่ยินยอม, Version ที่ยินยอม |

---

### F3 — Cookie Consent Banner Management

- **Route:** `/Admin/CookieConsent`
- **Controller:** `AdminController.CookieConsent()`
- **Layout:** `_ContentNavbarLayout`
- **สถานะ:** ต้องสร้างใหม่

#### Form Fields

| Field | ประเภท |
|-------|--------|
| ข้อความ Banner | Textarea |
| Label ปุ่ม Accept | Text |
| Label ปุ่ม Reject | Text |
| ลิงก์ Privacy Policy | URL Input |
| Position | Select: Bottom / Top / Bottom-left / Bottom-right |
| Preview | Live Preview Banner ด้านล่าง |

---

### F4 — MFA Policy Management

- **Route:** `/Admin/MFAPolicy`
- **Controller:** `AdminController.MFAPolicy()`
- **Layout:** `_ContentNavbarLayout`
- **สถานะ:** ต้องสร้างใหม่

#### Form Fields

| Policy | Toggle | Config |
|--------|--------|--------|
| Risk-Based MFA | On/Off | กำหนด Risk Score threshold |
| Location-Based MFA | On/Off | กำหนด Allowed countries/IP ranges |
| Trusted Device | On/Off | กำหนด Expiry (วัน) |
| MFA Required For | Checkbox: ผู้ประกอบการ / เจ้าหน้าที่ / Admin |

---

### F5 — Audit Log Viewer

- **Route:** `/Admin/AuditLog`
- **Controller:** `AdminController.AuditLog()`
- **Layout:** `_ContentNavbarLayout`
- **สถานะ:** ต้องสร้างใหม่

#### DataTable Columns

| Column | รายละเอียด |
|--------|-----------|
| Timestamp | DD/MM/YYYY HH:mm:ss |
| ผู้ใช้ | Username / Email |
| Action | Login / Logout / Register / ChangePassword / etc. |
| IP Address | xxx.xxx.xxx.xxx |
| Resource | ระบบงาน/หน้าที่เข้าถึง |
| ผลลัพธ์ | Badge: Success / Failed |

#### Filter Bar

- Date Range: Default 10 วันล่าสุด (ย้อนหลังได้ ≥ 10 วัน)
- Dropdown: Action Type, ผู้ใช้, ผลลัพธ์
- ปุ่ม Export: PDF / Excel

---

### F6 — Reports

- **Route:** `/Officer/IndexReports` (ขยายจากที่มีอยู่)
- **สถานะ:** มีแล้ว — ขยายเพิ่ม

#### Report Templates เพิ่มเติม (ตาม TOR)

| Report | Filter | Format |
|--------|--------|--------|
| สรุปการออกรหัสผู้ใช้งานใหม่ | ช่วงเวลา | PDF / Excel / Word |
| รายการบัญชีผู้ใช้งานที่ Active | ณ วันที่ | PDF / Excel / Word |
| รายการบัญชีผู้ใช้งานที่ถูกปิด | ช่วงเวลา | PDF / Excel / Word |
| รายชื่อผู้ใช้แยกตามกลุ่ม | กลุ่มผู้ใช้, ระบบงาน | PDF / Excel / Word |
| สรุปกลุ่มผู้ใช้ในแต่ละระบบงาน | ระบบงาน | PDF / Excel / Word |

- ทุก Report มี **Preview** ก่อน Export
- รองรับ Export เป็น PDF, Microsoft Excel, Microsoft Word

---

### F7 — FAQ Management (Admin)

- **Route:** `/Admin/FAQ`
- **Controller:** `AdminController.FAQ()`
- **Layout:** `_ContentNavbarLayout`
- **สถานะ:** ต้องสร้างใหม่

#### Sections

| Section | รายละเอียด |
|---------|-----------|
| รายการ Q&A | DataTable: ลำดับ, หมวดหมู่, คำถาม, สถานะ, Actions (แก้ไข/ลบ/เรียงลำดับ) |
| Modal: เพิ่ม/แก้ไข | หมวดหมู่ (Select), คำถาม (Text), คำตอบ (Rich Text Editor), สถานะ (Active/Inactive) |

---

### F8 — Data Analyzer Dashboard

- **Route:** `/Admin/DataAnalyzer`
- **Controller:** `AdminController.DataAnalyzer()`
- **Layout:** `_ContentNavbarLayout`
- **สถานะ:** ต้องสร้างใหม่

#### Charts / Widgets (Mock Data)

| Widget | ประเภท Chart |
|--------|------------|
| ผู้ใช้ใหม่รายเดือน | Bar Chart (ApexCharts) |
| Login Activity (7 วันล่าสุด) | Line Chart |
| MFA Adoption Rate | Donut Chart |
| สถานะบัญชีผู้ใช้ทั้งหมด | Summary Cards (Total / Active / Suspended / Pending) |
| Top Active Users | DataTable |

---

### F9 — Todo List (หลัง Login)

- **สถานะ:** ต้องสร้างใหม่ — Component ใน Dashboard
- แสดงอัตโนมัติหลัง Login หากมีรายการที่รอดำเนินการ

#### รายการ Todo ตัวอย่าง

| รายการ | เงื่อนไขแสดง | Link |
|--------|-------------|------|
| มีคำขอสิทธิ์รออนุมัติ N รายการ | เฉพาะเจ้าหน้าที่ | C2 |
| มีคำขอลงทะเบียนรอตรวจสอบ N รายการ | เฉพาะ Admin | F1 |
| ใบรับรองดิจิทัลหมดอายุในอีก 30 วัน | ทุกบทบาท | E1 |
| กรุณายืนยัน Privacy Policy ใหม่ | ทุกบทบาท (เมื่อมีการ Update) | — |

---

### F10 — Favorite Menu Management

- **สถานะ:** ต้องสร้างใหม่ — Component ใน User Settings
- **Route:** `/Users/ViewAccount#favorite-menu`

#### Layout

- Checkbox list ของเมนูทั้งหมดในระบบ
- ผู้ใช้เลือกเมนูที่ชื่นชอบ (Multi-select)
- Sidebar แสดงหัวข้อ "เมนูโปรด" พร้อมรายการที่เลือก

---

## สรุปไฟล์ที่ต้องดำเนินการ

### ไฟล์ที่มีอยู่แล้ว — ต้องขยาย

| ไฟล์ | รายละเอียดการขยาย |
|------|-----------------|
| `Views/Auth/RegisterMultiSteps.cshtml` | ขยายเป็น 4-Step Wizard ตาม A2 |
| `Views/Auth/LoginBasic.cshtml` | เพิ่มปุ่ม SSO ตาม B1 |
| `Views/Auth/VerifyEmailBasic.cshtml` | เพิ่ม OTP 6 หลัก + Resend Timer ตาม A5 |
| `Views/Auth/TwoStepsBasic.cshtml` | เพิ่ม MFA Tabs + Trusted Device ตาม B5 |
| `Views/Auth/ForgotPasswordBasic.cshtml` | เพิ่มช่องทาง SMS ตาม B6 |
| `Views/Users/ViewAccount.cshtml` | แยกส่วนข้อมูลสำคัญ + Verify Modal ตาม D1 |
| `Views/Users/ViewSecurity.cshtml` | เพิ่ม MFA Setup + Trusted Devices ตาม D3 |
| `Views/Users/List.cshtml` | เพิ่ม Columns + Modal ตาม F1 |
| `Views/Officer/IndexReports.cshtml` | เพิ่ม Report Templates ตาม F6 |

### ไฟล์ที่ต้องสร้างใหม่ — Views

| ไฟล์ | กลุ่ม |
|------|-------|
| `Views/Auth/RegisterChannel.cshtml` | A1 |
| `Views/Auth/RegisterThaiID.cshtml` | A4 |
| `Views/Auth/RegisterThaiIDCallback.cshtml` | A4 |
| `Views/Auth/RegisterComplete.cshtml` | A6 |
| `Views/Auth/SSORedirectIndustry.cshtml` | B2 |
| `Views/Auth/SSORedirectOfficer.cshtml` | B3 |
| `Views/Auth/SSORedirectThaiID.cshtml` | B4 |
| `Views/Auth/AccessRequest.cshtml` | C1 |
| `Views/Officer/AccessRequestList.cshtml` | C2 |
| `Views/Officer/ReviewAccessRequest.cshtml` | C3 |
| `Views/Officer/RegisterUser.cshtml` | A3 |
| `Views/Users/ViewAddresses.cshtml` | D2 |
| `Views/Users/Certificate.cshtml` | E1 |
| `Views/Admin/PrivacyPolicy.cshtml` | F2 |
| `Views/Admin/CookieConsent.cshtml` | F3 |
| `Views/Admin/MFAPolicy.cshtml` | F4 |
| `Views/Admin/AuditLog.cshtml` | F5 |
| `Views/Admin/FAQ.cshtml` | F7 |
| `Views/Admin/DataAnalyzer.cshtml` | F8 |

### ไฟล์ที่ต้องสร้างใหม่ — Controllers

| ไฟล์ | Actions ที่เพิ่ม |
|------|----------------|
| `Controllers/AuthController.cs` | `RegisterChannel`, `RegisterThaiID`, `RegisterThaiIDCallback`, `RegisterComplete`, `SSORedirectIndustry`, `SSORedirectOfficer`, `SSORedirectThaiID`, `AccessRequest` |
| `Controllers/OfficerController.cs` | `RegisterUser`, `AccessRequestList`, `ReviewAccessRequest` |
| `Controllers/UsersController.cs` | `ViewAddresses`, `Certificate` |
| `Controllers/AdminController.cs` | `PrivacyPolicy`, `CookieConsent`, `MFAPolicy`, `AuditLog`, `FAQ`, `DataAnalyzer` |

### ไฟล์ที่ต้องอัปเดต — Menu

| ไฟล์ | รายละเอียด |
|------|-----------|
| `Views/Shared/Sections/Menu/_VerticalMenu.cshtml` | เพิ่ม Menu entries สำหรับหน้าใหม่ทั้งหมด |

---

---

## 📐 UI Pattern Reference Guide

### Wizard / Multi-Step Form Pattern
**ใช้สำหรับ:** A2 (Registration Self-Service), A3 (Registration Walk-In), และ Forms ที่มีหลายขั้นตอน

**Reference File:** [Views/Wizards/CreateDeal.cshtml](Views/Wizards/CreateDeal.cshtml)  
**Library:** `bs-stepper` (Bootstrap Stepper)  
**Layout:** Vertical Stepper + Form Content (2 columns)

**Features:**
- Step indicator (Numbered: 01, 02, 03, 04)
- Title + Subtitle สำหรับแต่ละ step
- Progress line connecting steps
- Form validation built-in
- Navigation buttons (Back, Next, Submit)

**Vendor Styles/Scripts:**
```html
<link rel="stylesheet" href="~/vendor/libs/bs-stepper/bs-stepper.css" />
<link rel="stylesheet" href="~/vendor/libs/select2/select2.css" />
<link rel="stylesheet" href="~/vendor/libs/flatpickr/flatpickr.css" />
<link rel="stylesheet" href="~/vendor/libs/@form-validation/form-validation.css" />

<script src="~/vendor/libs/bs-stepper/bs-stepper.js"></script>
<script src="~/vendor/libs/select2/select2.js"></script>
<script src="~/vendor/libs/flatpickr/flatpickr.js"></script>
<script src="~/vendor/libs/@form-validation/popular.js"></script>
<script src="~/vendor/libs/@form-validation/bootstrap5.js"></script>
```

---

### Cover Layout Pattern (Auth Pages)
**ใช้สำหรับ:** B1 (Login), B2-B4 (SSO Redirects), และ Auth pages อื่น ๆ

**Reference File:** [Views/Auth/RegisterCover.cshtml](Views/Auth/RegisterCover.cshtml)  
**Layout:** `_BlankLayout`  
**Pattern:** Side-by-side (Left: Illustration, Right: Form)

**Features:**
- Logo + Brand name at top
- Left side: Responsive illustration (hidden on mobile, visible on desktop)
- Right side: Form container with authentication fields
- Background mask image (data-app-light-img / data-app-dark-img support)
- Already have account / Sign up links

**HTML Structure:**
```html
<div class="authentication-wrapper authentication-cover">
  <!-- Logo -->
  <a href="/" class="auth-cover-brand d-flex align-items-center gap-2">
    <span class="app-brand-logo demo">@await Html.PartialAsync("..._Macros")</span>
    <span class="app-brand-text demo text-heading fw-semibold">@appName</span>
  </a>
  
  <div class="authentication-inner row m-0">
    <!-- Left Side: Illustration -->
    <div class="d-none d-lg-flex col-lg-7 col-xl-8 align-items-center justify-content-center p-12 pb-2">
      <img src="~/img/illustrations/auth-register-illustration-light.png" class="auth-cover-illustration w-100" 
           data-app-light-img="illustrations/auth-register-illustration-light.png" 
           data-app-dark-img="illustrations/auth-register-illustration-dark.png" />
      <img src="~/img/illustrations/auth-cover-register-mask-light.png" class="authentication-image"
           data-app-light-img="illustrations/auth-cover-register-mask-light.png"
           data-app-dark-img="illustrations/auth-cover-register-mask-dark.png" />
    </div>
    
    <!-- Right Side: Form -->
    <div class="d-flex col-12 col-lg-5 col-xl-4 align-items-center authentication-bg position-relative py-sm-12 px-12 py-6">
      <div class="w-px-400 mx-auto pt-12 pt-lg-0">
        <form id="formAuthentication" class="mb-5">
          <!-- Form fields here -->
        </form>
      </div>
    </div>
  </div>
</div>
```

**CSS Classes:**
- Container: `.authentication-wrapper.authentication-cover`
- Left Column: `.d-none.d-lg-flex.col-lg-7.col-xl-8`
- Right Column: `.d-flex.col-12.col-lg-5.col-xl-4.authentication-bg`
- Content Wrapper: `.w-px-400.mx-auto`

**Vendor Styles:**
```html
<link rel="stylesheet" href="~/vendor/libs/@form-validation/form-validation.css" />
<link rel="stylesheet" href="~/vendor/css/pages/page-auth.css" />
```

---
- **Integration:** SSO (i-Industry, ThaiID) ใช้ Mock/Placeholder — ยังไม่ implement จริง
- **Email:** แจ้ง OTP Activation, ผลอนุมัติ/ปฏิเสธ, Temporary Password → ใช้ Mock ในช่วงพัฒนา
- **File Upload:** รองรับ PDF, JPG, PNG ≤ 5 MB ต่อไฟล์
- **Password Policy:** ≥8 ตัว, มีตัวอักษร + ตัวเลข + สัญลักษณ์, เข้ารหัสแบบ irreversible
- **Session Timeout:** กำหนดได้ผ่าน Admin (F4)
- **Export Reports:** PDF, Microsoft Excel, Microsoft Word พร้อม Preview

---

## 🏛️ Brand Information & Design System

### โลโก้และสีแบรนด์ (Logo & Colors)

**Logo:** ![DPIM Logo](docs/images/logo-dpim.png) *กรมอุตสาหกรรมพื้นฐานและการเหมืองแร่*

**สีหลัก:**
- 🔵 **Primary Blue:** `#003d82` (สำหรับปุ่ม Action หลัก, Headings)
- 🟢 **Secondary Green:** `#1ba845` (สำหรับ Accent, Success states)
- ⚫ **Dark Text:** `#333333`
- ⚪ **Background:** `#ffffff`, `#f5f5f5`

**Typography:**
- Heading: **TH Sarabun New** / **Roboto** (700 weight)
- Body: **TH Sarabun New** / **Roboto** (400 weight)
- Monospace (Code): **Courier New** / **Consolas**

### ข้อความแบรนด์ (Brand Messaging)

| ตัวอักษร | ภาษาอังกฤษ | การใช้งาน |
|---------|-----------|----------|
| **DPIM Portal** | Mining Permits & Information System | ชื่อระบบหลัก |
| **Mining for the Community** | เหมืองแร่เพื่อชุมชน | Tagline / Slogan |
| **กรมอุตสาหกรรมพื้นฐานและการเหมืองแร่ (กพร.)** | Department of Primary Industries and Mines | ชื่อหน่วยงานเต็ม |

### Component Library & Assets

- **Icon Set:** [BoxIcons](https://boxicons.com/) - `bx-*` class prefix
- **Chart Library:** [ApexCharts](https://apexcharts.com/)
- **Rich Text Editor:** Quill หรือ TinyMCE
- **Date Picker:** Litepicker หรือ Flatpickr
- **Data Table:** DataTables.net (Bootstrap 5 integration)
- **Alert / Modal:** SweetAlert2
- **Form Validation:** Parsley.js หรือ Built-in HTML5 validation
- **File Upload:** Dropzone.js

### ความสะดวกการใช้งาน (Accessibility & UX)

- ✅ WCAG 2.1 Level AA compliance
- ✅ Thai language full support (UTF-8)
- ✅ Responsive Design (Mobile, Tablet, Desktop)
- ✅ Dark mode toggle (optional)
- ✅ High contrast mode option
- ✅ Keyboard navigation support
- ✅ Screen reader friendly (ARIA labels)

---

## 📱 Device Support & Responsive Breakpoints

| Device | Width | Support |
|--------|-------|---------|
| Mobile | ≤ 576px | Full support |
| Tablet | 577 - 992px | Full support |
| Desktop | ≥ 993px | Full support |
| Large Desktop | ≥ 1400px | Full support |

---

## 📄 Document Version & Change Log

| Version | Date | Changes |
|---------|------|---------|
| 1.0 | 25 Apr 2569 | Initial design specification — 30 UI pages, 6 groups, Full TOR 5.2 coverage |
| 1.1 | (TBD) | Include branding assets, logo, colors, design system |
| 2.0 | (TBD) | High-fidelity mockups, interaction flows, animation specs |

---

## 📞 Contact & Support

**ดูแลระบบโดย:** กรมอุตสาหกรรมพื้นฐานและการเหมืองแร่ (กพร.)  
**Email:** dpim-admin@industry.go.th  
**Website:** https://www.industry.go.th/  
**Portal:** https://mining.industry.go.th/ (DPIM Portal)

---

*เอกสารนี้เป็นลักษณะเอกสารตั้งแต่ยาง Design Specification ที่มีจุดมุ่งหมายเพื่อให้เป็นแนวทางในการพัฒนาระบบ DPIM Portal ตามข้อกำหนด TOR ข้อ 5.2*

**© 2569 Department of Primary Industries and Mines — All Rights Reserved**
