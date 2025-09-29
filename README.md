# 專案架構

本專案採用 **N-tier 分層架構**，共分為四層：展示層（Presentation）、業務邏輯層（Business）、資料存取層（Repository）、共用層（Entity/Common）。

```
FinancialPreferences.sln
|
|-- FinancialPreferences.FinancialPreferences (Presentation Layer)
|-- FinancialPreferences.Business (Business Layer)
|-- FinancialPreferences.Repository (Data Access Layer)
|-- FinancialPreferences.Common (Entity / Shared Models)
|-- UnitTest (Unit Test Project)
|-- DB (SQL DDL & DML Scripts)
```

---

# 系統功能說明

本專案包含兩套完整的 CRUD 系統，用於技術架構和最佳實務的展示：

## 📋 金融商品喜好清單系統
- **技術架構**：傳統 MVC 模式
- **前端技術**：原生 Razor View + Bootstrap
- **資料存取**：ADO.NET + Stored Procedure
- **用途**：展示傳統 ASP.NET MVC 開發模式

## 🏠 房屋刊登系統
- **技術架構**：現代化前後端分離
- **前端技術**：Alpine.js + Bootstrap + Fetch API
- **後端 API**：RESTful API 設計
- **資料存取**：Entity Framework Core + Repository Pattern
- **用途**：展示現代化 Web 應用程式開發模式

---

# 三層式部署架構（3-Tier Architecture）

- **Web Server**：Kestrel（開發階段使用輕量 Kestrel 執行，但亦可將 DLL 部署至 IIS）
- **Application Server**：無獨立站台，透過 Controller 處理商業邏輯（即 Web + App 合一）
- **Database Server**：SQL Server 2019 Developer（部署於獨立電腦，appsettings.json 中不使用 `localhost`，而是指定實體 LAN IP）

---

# 技術細節與實作說明

## 🔧 前端技術

### 金融商品喜好清單
- **技術棧**：Razor View（傳統 MVC 模式）
- **UI 框架**：Bootstrap 5.3.3（透過 LibMan 引入）
- **JavaScript**：原生 JavaScript + jQuery
- **資料處理**：Form 提交 + 頁面重新載入

### 房屋刊登系統
- **技術棧**：Alpine.js + RESTful API
- **UI 框架**：Bootstrap 5.3.3
- **資料綁定**：Alpine.js 雙向綁定
- **API 通訊**：Fetch API（原生 JavaScript）
- **用戶體驗**：SPA 式操作，無頁面重新載入

## 🗄 後端資料處理

### 資料存取技術
- **ADO.NET**：用於金融商品喜好清單系統
  - Stored Procedure 搭配參數化查詢
  - 完全掌控 SQL 執行效能
- **Entity Framework Core**：用於房屋刊登系統
  - Code First 模式 (不使用 Migration 而是手工寫 DDL script)
  - Repository Pattern 實作
  - DbContext 管理資料庫連線

### 資料庫設計
- **DDL、DML**：已整理至 `/DB` 資料夾
- **交易處理（Transaction）**：雖未實作，已評估實作必要性，由於僅操作單一資料表，暫無交易需求

## 🚀 API 設計

### RESTful API (房屋刊登系統)
```
GET    /api/houses       - 取得所有房屋
POST   /api/houses       - 建立新房屋
PUT    /api/houses/{id}  - 更新指定房屋
DELETE /api/houses/{id}  - 刪除指定房屋
```

### API 特色
- **標準 HTTP 動詞**：遵循 RESTful 設計原則
- **統一回應格式**：成功/錯誤訊息格式一致
- **Service 層驗證**：不依賴 ModelState，使用自訂驗證邏輯
- **例外處理**：完整的錯誤處理機制

## 🧪 測試策略

### 單元測試
- **測試框架**：MSTest
- **模擬框架**：Moq
- **測試範圍**：Service 層業務邏輯驗證
- **測試類型**：
  - 邊界值測試
  - 負面測試
  - 業務規則驗證
  - 組合驗證測試

### 測試涵蓋
- ✅ 房屋資料驗證邏輯
- ✅ 業務規則驗證（坪單價合理性）
- ✅ 邊界條件測試
- ✅ 錯誤處理測試

---

# 安全性實作

## 🛡 SQL Injection 防範

### 前端防範
- **限制輸入**：產品與帳號僅可選擇，不允許輸入任意文字
- **數字欄位**：使用 `<input type="number">` 限制輸入格式
- **前端驗證**：JavaScript 輸入格式驗證

### 後端防範
- **ADO.NET 系統**：全部資料存取皆透過 Stored Procedure + 參數化查詢
- **Entity Framework 系統**：ORM 自動防止 SQL Injection
- **Service 層驗證**：雙重驗證機制

## 🛡 XSS（跨站腳本攻擊）防範
- **Razor View**：使用 `@Html.DisplayNameFor` 與 `asp-for` 屬性
- **自動編碼**：所有輸出皆透過 Razor 自動 HTML 編碼機制
- **Alpine.js**：自動 HTML 轉義處理
- **JavaScript**：使用 `escapeHtml()` 函數處理動態內容

## 🛡 CSRF（跨站請求偽造）防範
- **傳統表單**：所有 POST 表單皆搭配 `@Html.AntiForgeryToken()` 使用
- **JavaScript 發送**：透過 `getAntiForgeryToken()` 附帶驗證碼（適用於金融商品系統）
- **API 系統**：房屋刊登系統使用 RESTful API，可考慮實作 JWT 或其他認證機制

---

# 架構優勢與設計模式

## 🏗 設計模式
- **Repository Pattern**：資料存取層抽象化
- **Dependency Injection**：依賴注入降低耦合
- **Service Layer Pattern**：業務邏輯集中管理
- **MVC Pattern**：關注點分離

## 📦 分層責任
- **Presentation Layer**：UI 展示、用戶互動、輸入驗證
- **Business Layer**：業務邏輯、資料驗證、流程控制
- **Data Access Layer**：資料持久化、資料庫操作
- **Common Layer**：共用模型、常數定義、工具類別

## 🔄 資料流程
1. **用戶操作** → Presentation Layer
2. **請求處理** → Controller
3. **業務邏輯** → Service Layer
4. **資料驗證** → Service.Validate()
5. **資料操作** → Repository Layer
6. **資料庫存取** → ADO.NET/Entity Framework
7. **回應處理** → Controller → View/API Response

---

# 開發環境與部署

## 💻 開發技術棧
- **.NET 8.0**：最新 LTS 版本
- **C# 12.0**：最新語言特性
- **SQL Server 2019 Developer**：企業級資料庫
- **Visual Studio 2022**：IDE 開發環境

## 📚 前端資源管理
- **LibMan**：前端套件管理
- **Bootstrap 5.3.3**：UI 框架
- **Alpine.js**：輕量級前端框架
- **Font Awesome**：圖示字體
