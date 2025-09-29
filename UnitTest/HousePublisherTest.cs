using BusinessLogic.Services;
using Common.Models;
using Moq;
using Repository.Interfaces;

namespace UnitTest
{
    [TestClass]
    public sealed class HousePublisherTest
    {
        private HousePublisherService _service;
        private Mock<IHouseRepository> _mockRepository;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IHouseRepository>();
            _service = new HousePublisherService(_mockRepository.Object);
        }

        #region 房屋名稱驗證測試

        [TestMethod]
        public void Validate_HouseNameIsNull_ShouldReturnError()
        {
            // Arrange
            var house = CreateValidHouse();
            house.HouseName = null!;

            // Act
            var errors = _service.Validate(house);

            // Assert
            Assert.IsTrue(errors.Contains("房屋名稱為必填欄位"));
        }

        [TestMethod]
        public void Validate_HouseNameIsEmpty_ShouldReturnError()
        {
            // Arrange
            var house = CreateValidHouse();
            house.HouseName = "";

            // Act
            var errors = _service.Validate(house);

            // Assert
            Assert.IsTrue(errors.Contains("房屋名稱為必填欄位"));
        }

        [TestMethod]
        public void Validate_HouseNameIsWhitespace_ShouldReturnError()
        {
            // Arrange
            var house = CreateValidHouse();
            house.HouseName = "   ";

            // Act
            var errors = _service.Validate(house);

            // Assert
            Assert.IsTrue(errors.Contains("房屋名稱為必填欄位"));
        }

        [TestMethod]
        public void Validate_HouseNameExceeds200Characters_ShouldReturnError()
        {
            // Arrange
            var house = CreateValidHouse();
            house.HouseName = new string('A', 201);

            // Act
            var errors = _service.Validate(house);

            // Assert
            Assert.IsTrue(errors.Contains("房屋名稱長度不能超過 200 個字元"));
        }

        [TestMethod]
        public void Validate_HouseNameIs200Characters_ShouldNotReturnError()
        {
            // Arrange
            var house = CreateValidHouse();
            house.HouseName = new string('A', 200);

            // Act
            var errors = _service.Validate(house);

            // Assert
            Assert.IsFalse(errors.Any(e => e.Contains("房屋名稱")));
        }

        #endregion

        #region 地址驗證測試

        [TestMethod]
        public void Validate_AddressIsNull_ShouldReturnError()
        {
            // Arrange
            var house = CreateValidHouse();
            house.Address = null!;

            // Act
            var errors = _service.Validate(house);

            // Assert
            Assert.IsTrue(errors.Contains("地址為必填欄位"));
        }

        [TestMethod]
        public void Validate_AddressIsEmpty_ShouldReturnError()
        {
            // Arrange
            var house = CreateValidHouse();
            house.Address = "";

            // Act
            var errors = _service.Validate(house);

            // Assert
            Assert.IsTrue(errors.Contains("地址為必填欄位"));
        }

        [TestMethod]
        public void Validate_AddressExceeds500Characters_ShouldReturnError()
        {
            // Arrange
            var house = CreateValidHouse();
            house.Address = new string('B', 501);

            // Act
            var errors = _service.Validate(house);

            // Assert
            Assert.IsTrue(errors.Contains("地址長度不能超過 500 個字元"));
        }

        [TestMethod]
        public void Validate_AddressIs500Characters_ShouldNotReturnError()
        {
            // Arrange
            var house = CreateValidHouse();
            house.Address = new string('B', 500);

            // Act
            var errors = _service.Validate(house);

            // Assert
            Assert.IsFalse(errors.Any(e => e.Contains("地址")));
        }

        #endregion

        #region 總價格驗證測試

        [TestMethod]
        public void Validate_TotalPriceIsZero_ShouldReturnError()
        {
            // Arrange
            var house = CreateValidHouse();
            house.TotalPrice = 0;

            // Act
            var errors = _service.Validate(house);

            // Assert
            Assert.IsTrue(errors.Contains("總價格必須大於 0"));
        }

        [TestMethod]
        public void Validate_TotalPriceIsNegative_ShouldReturnError()
        {
            // Arrange
            var house = CreateValidHouse();
            house.TotalPrice = -100;

            // Act
            var errors = _service.Validate(house);

            // Assert
            Assert.IsTrue(errors.Contains("總價格必須大於 0"));
        }

        [TestMethod]
        public void Validate_TotalPriceExceedsSystemLimit_ShouldReturnError()
        {
            // Arrange
            var house = CreateValidHouse();
            house.TotalPrice = 1000000000000000m; // 超過 decimal(18,2) 限制

            // Act
            var errors = _service.Validate(house);

            // Assert
            Assert.IsTrue(errors.Contains("總價格超過系統限制"));
        }

        [TestMethod]
        public void Validate_TotalPriceIsAtSystemLimit_ShouldNotReturnError()
        {
            // Arrange
            var house = CreateValidHouse();
            house.TotalPrice = 999999999999999.99m; // 在限制內

            // Act
            var errors = _service.Validate(house);

            // Assert
            Assert.IsFalse(errors.Any(e => e.Contains("總價格超過系統限制")));
        }

        #endregion

        #region 坪數驗證測試

        [TestMethod]
        public void Validate_FloorAreaIsZero_ShouldReturnError()
        {
            // Arrange
            var house = CreateValidHouse();
            house.FloorArea = 0;

            // Act
            var errors = _service.Validate(house);

            // Assert
            Assert.IsTrue(errors.Contains("坪數必須大於 0"));
        }

        [TestMethod]
        public void Validate_FloorAreaIsNegative_ShouldReturnError()
        {
            // Arrange
            var house = CreateValidHouse();
            house.FloorArea = -10;

            // Act
            var errors = _service.Validate(house);

            // Assert
            Assert.IsTrue(errors.Contains("坪數必須大於 0"));
        }

        [TestMethod]
        public void Validate_FloorAreaExceedsSystemLimit_ShouldReturnError()
        {
            // Arrange
            var house = CreateValidHouse();
            house.FloorArea = 1000000m; // 超過 decimal(8,2) 限制

            // Act
            var errors = _service.Validate(house);

            // Assert
            Assert.IsTrue(errors.Contains("坪數超過系統限制"));
        }

        [TestMethod]
        public void Validate_FloorAreaIsAtSystemLimit_ShouldNotReturnError()
        {
            // Arrange
            var house = CreateValidHouse();
            house.FloorArea = 999999.99m; // 在限制內

            // Act
            var errors = _service.Validate(house);

            // Assert
            Assert.IsFalse(errors.Any(e => e.Contains("坪數超過系統限制")));
        }

        #endregion

        #region 描述驗證測試

        [TestMethod]
        public void Validate_DescriptionIsNull_ShouldNotReturnError()
        {
            // Arrange
            var house = CreateValidHouse();
            house.Description = null;

            // Act
            var errors = _service.Validate(house);

            // Assert
            Assert.IsFalse(errors.Any(e => e.Contains("描述")));
        }

        [TestMethod]
        public void Validate_DescriptionIsEmpty_ShouldNotReturnError()
        {
            // Arrange
            var house = CreateValidHouse();
            house.Description = "";

            // Act
            var errors = _service.Validate(house);

            // Assert
            Assert.IsFalse(errors.Any(e => e.Contains("描述")));
        }

        [TestMethod]
        public void Validate_DescriptionExceeds2000Characters_ShouldReturnError()
        {
            // Arrange
            var house = CreateValidHouse();
            house.Description = new string('C', 2001);

            // Act
            var errors = _service.Validate(house);

            // Assert
            Assert.IsTrue(errors.Contains("描述長度不能超過 2000 個字元"));
        }

        [TestMethod]
        public void Validate_DescriptionIs2000Characters_ShouldNotReturnError()
        {
            // Arrange
            var house = CreateValidHouse();
            house.Description = new string('C', 2000);

            // Act
            var errors = _service.Validate(house);

            // Assert
            Assert.IsFalse(errors.Any(e => e.Contains("描述長度不能超過")));
        }

        #endregion

        #region 坪單價業務邏輯驗證測試

        [TestMethod]
        public void Validate_PricePerPingTooHigh_ShouldReturnError()
        {
            // Arrange
            var house = CreateValidHouse();
            house.TotalPrice = 10000000m; // 1000萬
            house.FloorArea = 1m; // 1坪
            // 每坪1000萬，超過500萬限制

            // Act
            var errors = _service.Validate(house);

            // Assert
            Assert.IsTrue(errors.Contains("每坪單價過高，請檢查總價格或坪數是否正確"));
        }

        [TestMethod]
        public void Validate_PricePerPingTooLow_ShouldReturnError()
        {
            // Arrange
            var house = CreateValidHouse();
            house.TotalPrice = 5000m; // 5千
            house.FloorArea = 1m; // 1坪
            // 每坪5千，低於1萬限制

            // Act
            var errors = _service.Validate(house);

            // Assert
            Assert.IsTrue(errors.Contains("每坪單價過低，請檢查總價格或坪數是否正確"));
        }

        [TestMethod]
        public void Validate_PricePerPingAtUpperLimit_ShouldNotReturnError()
        {
            // Arrange
            var house = CreateValidHouse();
            house.TotalPrice = 5000000m; // 500萬
            house.FloorArea = 1m; // 1坪
            // 每坪500萬，在限制內

            // Act
            var errors = _service.Validate(house);

            // Assert
            Assert.IsFalse(errors.Any(e => e.Contains("每坪單價")));
        }

        [TestMethod]
        public void Validate_PricePerPingAtLowerLimit_ShouldNotReturnError()
        {
            // Arrange
            var house = CreateValidHouse();
            house.TotalPrice = 10000m; // 1萬
            house.FloorArea = 1m; // 1坪
            // 每坪1萬，在限制內

            // Act
            var errors = _service.Validate(house);

            // Assert
            Assert.IsFalse(errors.Any(e => e.Contains("每坪單價")));
        }

        [TestMethod]
        public void Validate_PricePerPingInValidRange_ShouldNotReturnError()
        {
            // Arrange
            var house = CreateValidHouse();
            house.TotalPrice = 1000000m; // 100萬
            house.FloorArea = 30m; // 30坪
            // 每坪約33,333，在合理範圍內

            // Act
            var errors = _service.Validate(house);

            // Assert
            Assert.IsFalse(errors.Any(e => e.Contains("每坪單價")));
        }

        #endregion

        #region 組合驗證測試

        [TestMethod]
        public void Validate_ValidHouse_ShouldReturnNoErrors()
        {
            // Arrange
            var house = CreateValidHouse();

            // Act
            var errors = _service.Validate(house);

            // Assert
            Assert.AreEqual(0, errors.Count, $"預期沒有錯誤，但收到：{string.Join(", ", errors)}");
        }

        [TestMethod]
        public void Validate_MultipleErrors_ShouldReturnAllErrors()
        {
            // Arrange
            var house = new House
            {
                Id = Guid.NewGuid(),
                HouseName = "", // 錯誤：空白
                Address = "", // 錯誤：空白
                TotalPrice = -100, // 錯誤：負數
                FloorArea = 0, // 錯誤：零
                Description = new string('D', 2001), // 錯誤：超過長度
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            // Act
            var errors = _service.Validate(house);

            // Assert
            Assert.IsTrue(errors.Count >= 5, $"預期至少5個錯誤，實際收到 {errors.Count} 個：{string.Join(", ", errors)}");
            Assert.IsTrue(errors.Contains("房屋名稱為必填欄位"));
            Assert.IsTrue(errors.Contains("地址為必填欄位"));
            Assert.IsTrue(errors.Contains("總價格必須大於 0"));
            Assert.IsTrue(errors.Contains("坪數必須大於 0"));
            Assert.IsTrue(errors.Contains("描述長度不能超過 2000 個字元"));
        }

        #endregion

        #region 邊界值測試

        [TestMethod]
        public void Validate_BoundaryValues_ShouldHandleCorrectly()
        {
            // Arrange
            var house = new House
            {
                Id = Guid.NewGuid(),
                HouseName = new string('A', 200), // 剛好200字元
                Address = new string('B', 500), // 剛好500字元
                TotalPrice = 999999999999999.99m, // 最大允許價格
                FloorArea = 999999.99m, // 最大允許坪數
                Description = new string('C', 2000), // 剛好2000字元
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            // Act
            var errors = _service.Validate(house);

            // Assert
            // 這個測試應該會因為坪單價過高而失敗，但其他欄位驗證應該通過
            var nonPriceErrors = errors.Where(e => !e.Contains("每坪單價")).ToList();
            Assert.AreEqual(0, nonPriceErrors.Count, $"邊界值測試失敗：{string.Join(", ", nonPriceErrors)}");
        }

        #endregion

        /// <summary>
        /// 建立一個有效的房屋物件用於測試
        /// </summary>
        private static House CreateValidHouse()
        {
            return new House
            {
                Id = Guid.NewGuid(),
                HouseName = "測試房屋",
                Address = "台北市大安區測試路123號",
                TotalPrice = 1000000m, // 100萬
                FloorArea = 30m, // 30坪
                Description = "這是一個測試用的房屋描述",
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };
        }
    }
}
