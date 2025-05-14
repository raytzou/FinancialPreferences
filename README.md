# 專案架構

本專案採用 **N-tier 分層架構**，共分為四層：展示層（Presentation）、業務邏輯層（Business）、資料存取層（Repository）、共用層（Entity/Common）。

```
FinancialPreferences.sln
|
|-- FinancialPreferences.FinancialPreferences (Presentation Layer)
|-- FinancialPreferences.Bussiness (Business Layer)
|-- FinancialPreferences.Repository (Data Access Layer)
|-- FinancialPreferences.Common (Entity / Shared Models)
|-- DB (SQL DDL & DML Scripts)
```


---

# 三層式部署架構（3-Tier Architecture）

- **Web Server**：Kestrel（開發階段使用輕量 Kestrel 執行，但亦可將 DLL 部署至 IIS）
- **Application Server**：無獨立站台，透過 Controller 處理商業邏輯（即 Web + App 合一）
- **Database Server**：SQL Server 2019 Developer（部署於獨立電腦，appsettings.json 中不使用 `localhost`，而是指定實體 LAN IP）

---

# 技術細節與實作說明

### 🔧 前端技術
- Razor View（使用 MVC，未使用 Razor Page 的 codebehind）
- 原生 JavaScript
- Bootstrap 5.3.3（透過 LibMan 引入）

### 🗄 後端資料處理
- 資料存取：ADO.NET 搭配 Stored Procedure
- DDL、DML 已整理至 `/DB` 資料夾
- 交易處理（Transaction）：雖未實作，已評估實作必要性，由於僅操作單一資料表（商品喜好），暫無交易需求

---

# 安全性實作

### 🛡 SQL Injection 防範
- **前端防範**：
  - 限制輸入：產品與帳號僅可選擇，不允許輸入任意文字
  - 數字欄位使用 `<input type="number">` 限制輸入格式
- **後端防範**：
  - 全部資料存取皆透過 Stored Procedure
  - ADO.NET 中使用參數化查詢（`command.Parameters.AddWithValue()`）防止拼接注入

### 🛡 XSS（跨站腳本攻擊）防範
- Razor View 中使用 `@Html.DisplayNameFor` 與 `asp-for` 屬性，避免原生輸出 HTML
- 所有輸出皆透過 Razor 自動 HTML 編碼機制

### 🛡 CSRF（跨站請求偽造）防範
- 所有 POST 表單皆搭配 `@Html.AntiForgeryToken()` 使用
- JavaScript 發送 POST 時透過 `getAntiForgeryToken()` 附帶驗證碼

---

📌 如需部署、建置或測試指引，請參見 `/DB/README.md` 或依需求擴充說明。
