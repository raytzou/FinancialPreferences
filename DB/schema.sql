CREATE TABLE [dbo].[Users] (
    [UserId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [UserName] NVARCHAR(100) NOT NULL,
    [Email] NVARCHAR(255) NOT NULL,
    [AccountNumber] NVARCHAR(20) NOT NULL
);

CREATE TABLE [dbo].[Products] (
    [ProductId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [ProductName] NVARCHAR(100) NOT NULL,
    [Price] DECIMAL(18,2) NOT NULL,
    [FeeRate] DECIMAL(5,4) NOT NULL
);

CREATE TABLE [dbo].[UserPreferences] (
    [PreferenceId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [UserId] UNIQUEIDENTIFIER NOT NULL,
    [ProductId] UNIQUEIDENTIFIER NOT NULL,
    [OrderQuantity] INT NOT NULL,
    [AccountNumber] NVARCHAR(20) NOT NULL,
    [TotalAmount] DECIMAL(18,2) NOT NULL,
    [TotalFee] DECIMAL(18,2) NOT NULL,
    
    CONSTRAINT FK_UserPreferences_Users FOREIGN KEY ([UserId])
        REFERENCES [dbo].[Users]([UserId]),
    
    CONSTRAINT FK_UserPreferences_Products FOREIGN KEY ([ProductId])
        REFERENCES [dbo].[Products]([ProductId])
);