using BusinessLogic.Services.Interfaces;
using Common.Models;
using Repository.Interfaces;

namespace BusinessLogic.Services
{
    public class HousePublisherService : IHousePublisherService
    {
        private readonly IHouseRepository _repository;

        public HousePublisherService(IHouseRepository repository)
        {
            _repository = repository;
        }

        public List<House> GetAllHouses() => _repository.GetAll().ToList();

        public void Create(string houseName, string address, decimal totalPrice, decimal floorArea, string description)
        {
            _repository.Create(new House()
            {
                Id = Guid.NewGuid(),
                HouseName = houseName,
                Address = address,
                TotalPrice = totalPrice,
                FloorArea = floorArea,
                Description = description,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
            });

            _repository.CommitChanges();
        }

        public void Delete(string id)
        {
            _repository.Delete(Guid.Parse(id));
            _repository.CommitChanges();
        }

        public void Update(House content)
        {
            _repository.Update(content);
            _repository.CommitChanges();
        }

        public List<string> Validate(House content)
        {
            var errors = new List<string>();

            // 驗證房屋名稱
            if (string.IsNullOrWhiteSpace(content.HouseName))
            {
                errors.Add("房屋名稱為必填欄位");
            }
            else if (content.HouseName.Length > 200)
            {
                errors.Add("房屋名稱長度不能超過 200 個字元");
            }

            // 驗證地址
            if (string.IsNullOrWhiteSpace(content.Address))
            {
                errors.Add("地址為必填欄位");
            }
            else if (content.Address.Length > 500)
            {
                errors.Add("地址長度不能超過 500 個字元");
            }

            // 驗證總價格
            if (content.TotalPrice <= 0)
            {
                errors.Add("總價格必須大於 0");
            }
            else if (content.TotalPrice > 999999999999999.99m) // decimal(18,2) 的最大值
            {
                errors.Add("總價格超過系統限制");
            }

            // 驗證坪數
            if (content.FloorArea <= 0)
            {
                errors.Add("坪數必須大於 0");
            }
            else if (content.FloorArea > 999999.99m) // decimal(8,2) 的最大值
            {
                errors.Add("坪數超過系統限制");
            }

            // 驗證描述（選填欄位）
            if (!string.IsNullOrEmpty(content.Description) && content.Description.Length > 2000)
            {
                errors.Add("描述長度不能超過 2000 個字元");
            }

            // 業務邏輯驗證
            // 驗證坪單價是否合理（可選的業務邏輯）
            if (content.TotalPrice > 0 && content.FloorArea > 0)
            {
                var pricePerPing = content.TotalPrice / content.FloorArea;
                if (pricePerPing > 5000000) // 每坪超過 500 萬可能不合理
                {
                    errors.Add("每坪單價過高，請檢查總價格或坪數是否正確");
                }
                else if (pricePerPing < 10000) // 每坪低於 1 萬可能不合理
                {
                    errors.Add("每坪單價過低，請檢查總價格或坪數是否正確");
                }
            }

            return errors;
        }
    }
}
