INSERT INTO [dbo].[Users] (
    UserId,
    UserName,
    Email,
    AccountNumber
)
VALUES
(NEWID(), N'王大明', 'user1@example.com', '1234567890'),
(NEWID(), N'陳小華', 'user2@example.com', '0987654321');

INSERT INTO [dbo].[Products] (
    ProductId,
    ProductName,
    Price,
    FeeRate
)
VALUES
(NEWID(), N'ETF 0050', 120.00, 0.0100),   -- 1%
(NEWID(), N'台積電股票', 600.00, 0.0150), -- 1.5%
(NEWID(), N'金融債券A', 1000.00, 0.0050); -- 0.5%