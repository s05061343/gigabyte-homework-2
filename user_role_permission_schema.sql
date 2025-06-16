
-- 建立資料表

CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    UserName NVARCHAR(100) NOT NULL
);

CREATE TABLE Roles (
    RoleId INT IDENTITY(1,1) PRIMARY KEY,
    RoleName NVARCHAR(100) NOT NULL
);

CREATE TABLE UserRoles (
    UserId INT NOT NULL,
    RoleId INT NOT NULL,
    PRIMARY KEY (UserId, RoleId),
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    FOREIGN KEY (RoleId) REFERENCES Roles(RoleId)
);

CREATE TABLE Functions (
    FunctionId INT IDENTITY(1,1) PRIMARY KEY,
    FunctionName NVARCHAR(100) NOT NULL
);

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

-- 插入範例資料

-- Users
INSERT INTO Users (UserName) VALUES ('Tom');
INSERT INTO Users (UserName) VALUES ('Mary');

-- Roles
INSERT INTO Roles (RoleName) VALUES ('系統管理員');
INSERT INTO Roles (RoleName) VALUES ('一般使用者');

-- UserRoles
INSERT INTO UserRoles (UserId, RoleId) VALUES (1, 1);
INSERT INTO UserRoles (UserId, RoleId) VALUES (1, 2);
INSERT INTO UserRoles (UserId, RoleId) VALUES (2, 2);

-- Functions
INSERT INTO Functions (FunctionName) VALUES ('用戶管理');
INSERT INTO Functions (FunctionName) VALUES ('報表查詢');
INSERT INTO Functions (FunctionName) VALUES ('系統設定');

-- RoleFunctionPermissions
INSERT INTO RoleFunctionPermissions (RoleId, FunctionId, CanCreate, CanRead, CanUpdate, CanDelete)
VALUES 
(1, 1, 1, 1, 1, 1),
(1, 2, 1, 1, 1, 1),
(1, 3, 1, 1, 1, 1);

INSERT INTO RoleFunctionPermissions (RoleId, FunctionId, CanCreate, CanRead, CanUpdate, CanDelete)
VALUES 
(2, 2, 0, 1, 0, 0);
