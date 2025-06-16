# 用戶角色權限系統資料庫架構

## 資料表結構

### Users 表 - 用戶
```sql
CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    UserName NVARCHAR(100) NOT NULL
);
```
- **UserId**: 用戶ID (主鍵，自動遞增)
- **UserName**: 用戶名稱

### Roles 表 - 角色
```sql
CREATE TABLE Roles (
    RoleId INT IDENTITY(1,1) PRIMARY KEY,
    RoleName NVARCHAR(100) NOT NULL
);
```
- **RoleId**: 角色ID (主鍵，自動遞增)
- **RoleName**: 角色名稱

### UserRoles 表 - 用戶角色關聯
```sql
CREATE TABLE UserRoles (
    UserId INT NOT NULL,
    RoleId INT NOT NULL,
    PRIMARY KEY (UserId, RoleId),
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    FOREIGN KEY (RoleId) REFERENCES Roles(RoleId)
);
```
- **UserId**: 用戶ID (外鍵)
- **RoleId**: 角色ID (外鍵)
- 複合主鍵設計，用於多對多關係

### Functions 表 - 功能模組
```sql
CREATE TABLE Functions (
    FunctionId INT IDENTITY(1,1) PRIMARY KEY,
    FunctionName NVARCHAR(100) NOT NULL
);
```
- **FunctionId**: 功能ID (主鍵，自動遞增)
- **FunctionName**: 功能名稱

### RoleFunctionPermissions 表 - 角色功能權限
```sql
CREATE TABLE RoleFunctionPermissions (
    RoleId INT NOT NULL,
    FunctionId INT NOT NULL,
    CanCreate BIT NOT NULL DEFAULT 0,
    CanRead BIT NOT NULL DEFAULT 0,
    CanUpdate BIT NOT NULL DEFAULT 0,
    CanDelete BIT NOT NULL DEFAULT 0,
    PRIMARY KEY (RoleId, FunctionId),
    FOREIGN KEY (RoleId) REFERENCES Roles(RoleId),
    FOREIGN KEY (FunctionId) REFERENCES Functions(FunctionId)
);
```
- **RoleId**: 角色ID (外鍵)
- **FunctionId**: 功能ID (外鍵)
- **CanCreate**: 新增權限 (布林值，預設為0)
- **CanRead**: 讀取權限 (布林值，預設為0)
- **CanUpdate**: 更新權限 (布林值，預設為0)
- **CanDelete**: 刪除權限 (布林值，預設為0)

## 範例資料

### 用戶資料
| UserId | UserName |
|--------|----------|
| 1      | Tom      |
| 2      | Mary     |

### 角色資料
| RoleId | RoleName   |
|--------|------------|
| 1      | 系統管理員 |
| 2      | 一般使用者 |

### 用戶角色關聯
| UserId | RoleId | 說明                    |
|--------|--------|-------------------------|
| 1      | 1      | Tom 是系統管理員        |
| 1      | 2      | Tom 同時也是一般使用者  |
| 2      | 2      | Mary 是一般使用者       |

### 功能模組
| FunctionId | FunctionName |
|------------|--------------|
| 1          | 用戶管理     |
| 2          | 報表查詢     |
| 3          | 系統設定     |

### 角色功能權限配置
| 角色       | 功能     | 新增 | 讀取 | 更新 | 刪除 |
|------------|----------|------|------|------|------|
| 系統管理員 | 用戶管理 | ✓    | ✓    | ✓    | ✓    |
| 系統管理員 | 報表查詢 | ✓    | ✓    | ✓    | ✓    |
| 系統管理員 | 系統設定 | ✓    | ✓    | ✓    | ✓    |
| 一般使用者 | 報表查詢 | ✗    | ✓    | ✗    | ✗    |

## 系統架構特點

1. **多對多關係設計**: 用戶可以擁有多個角色，角色可以分配給多個用戶
2. **細粒度權限控制**: 每個角色對每個功能都有獨立的CRUD權限設定
3. **可擴展性**: 可以輕鬆新增新的用戶、角色和功能模組
4. **權限繼承**: 用戶通過角色獲得權限，便於統一管理

## 使用情境

- Tom 作為系統管理員，對所有功能都有完整的CRUD權限
- Mary 作為一般使用者，只能查詢報表，無法進行其他操作
- 系統可以靈活地為不同角色配置不同的功能權限