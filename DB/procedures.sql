CREATE PROCEDURE [dbo].[sp_AddUserPreference]
    @PreferenceId UNIQUEIDENTIFIER,
    @UserId UNIQUEIDENTIFIER,
    @ProductId UNIQUEIDENTIFIER,
    @OrderQuantity INT,
    @AccountNumber NVARCHAR(20),
    @TotalAmount DECIMAL(18,2),
    @TotalFee DECIMAL(18,2)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[UserPreferences] (
        PreferenceId, UserId, ProductId, OrderQuantity, AccountNumber, TotalAmount, TotalFee
    )
    VALUES (
        @PreferenceId, @UserId, @ProductId, @OrderQuantity, @AccountNumber, @TotalAmount, @TotalFee
    );
END

CREATE PROCEDURE [dbo].[sp_GetAllProducts]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        ProductId,
        ProductName,
        Price,
        FeeRate
    FROM 
        [dbo].[Products];
END

CREATE PROCEDURE [dbo].[sp_GetAllUsers]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        UserId,
        UserName,
        Email,
        AccountNumber
    FROM 
        [dbo].[Users];
END

CREATE PROCEDURE [dbo].[sp_GetAllUserPreferences]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        PreferenceId,
        UserId,
        ProductId,
        OrderQuantity,
        AccountNumber,
        TotalAmount,
        TotalFee
    FROM 
        [dbo].UserPreferences;
END

CREATE PROCEDURE [dbo].[sp_DeleteUserPreference]
    @PreferenceId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM [dbo].[UserPreferences]
    WHERE PreferenceId = @PreferenceId;
END

CREATE PROCEDURE sp_UpdateUserPreference
    @PreferenceId UNIQUEIDENTIFIER,
    @ProductId UNIQUEIDENTIFIER,
    @UserId UNIQUEIDENTIFIER,
    @OrderQuantity INT,
    @AccountNumber NVARCHAR(50),
    @TotalAmount DECIMAL(18, 2),
    @TotalFee DECIMAL(18, 2)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE UserPreference
    SET
        ProductId = @ProductId,
        UserId = @UserId,
        OrderQuantity = @OrderQuantity,
        AccountNumber = @AccountNumber,
        TotalAmount = @TotalAmount,
        TotalFee = @TotalFee
    WHERE PreferenceId = @PreferenceId;
END