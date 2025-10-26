/* =====================================================
   Project: Recycling Management System
   Author: Kamo Phiri
   Description: SQL Server schema for RecyclingSystem
   Created: 2025-10-26
   ===================================================== */

CREATE DATABASE RecyclingManagement;
GO
USE RecyclingManagement;
GO

-- =========================
-- TABLE: Users
-- =========================
CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(256) NOT NULL,
    Email NVARCHAR(150) NOT NULL,
    Role NVARCHAR(50) NOT NULL CHECK (Role IN ('Admin', 'User')),
    CreatedAt DATETIME DEFAULT GETDATE()
);
GO

-- =========================
-- TABLE: RecyclableItems
-- =========================
CREATE TABLE RecyclableItems (
    ItemID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT NOT NULL,
    ItemType NVARCHAR(50) NOT NULL CHECK (ItemType IN ('Plastic', 'Paper', 'Glass', 'Metal')),
    Weight DECIMAL(10,2) NOT NULL,
    Condition NVARCHAR(20) NOT NULL CHECK (Condition IN ('Good', 'Okay', 'Damaged')),
    ImagePath NVARCHAR(255),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME NULL,
    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE
);
GO

CREATE TABLE InventoryActions (
    ActionID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT NOT NULL,
    ItemID INT NOT NULL,
    ActionType NVARCHAR(50) CHECK (ActionType IN ('Create', 'Update', 'Delete')),
    ActionDate DATETIME DEFAULT GETDATE(),

    CONSTRAINT FK_InventoryActions_Users FOREIGN KEY (UserID)
        REFERENCES Users(UserID)
        ON DELETE CASCADE,

    CONSTRAINT FK_InventoryActions_RecyclableItems FOREIGN KEY (ItemID)
        REFERENCES RecyclableItems(ItemID)
        ON DELETE CASCADE
);
GO

-- =========================
-- TABLE: InventoryActions
-- =========================
CREATE TABLE InventoryActions (
    ActionID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT NOT NULL,
    ItemID INT NOT NULL,
    ActionType NVARCHAR(50) NOT NULL CHECK (ActionType IN ('Create', 'Update', 'Delete')),
    ActionTime DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE,
    FOREIGN KEY (ItemID) REFERENCES RecyclableItems(ItemID) ON DELETE CASCADE
);
GO

-- =========================
-- VIEW: vw_UserInventory
-- =========================
CREATE VIEW vw_UserInventory AS
SELECT 
    u.Username,
    ri.ItemID,
    ri.ItemType,
    ri.Weight,
    ri.Condition,
    ri.CreatedAt,
    ri.UpdatedAt
FROM RecyclableItems ri
INNER JOIN Users u ON ri.UserID = u.UserID;
GO

-- =========================
-- INDEXES
-- =========================
CREATE INDEX IX_RecyclableItems_UserID ON RecyclableItems(UserID);
CREATE INDEX IX_InventoryActions_UserID ON InventoryActions(UserID);
CREATE INDEX IX_InventoryActions_ItemID ON InventoryActions(ItemID);
GO
