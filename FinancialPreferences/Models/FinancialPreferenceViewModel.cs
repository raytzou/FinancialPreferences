using Common.Models;
using System.ComponentModel.DataAnnotations;

namespace FinancialPreferences.Models
{
    public class FinancialPreferenceViewModel
    {
        public List<PreferenceTableRowViewModel> Preferences { get; set; }
        public List<Product> Products { get; set; }
        public List<User> Users { get; set; }
        public PreferenceTableRowViewModel EditingPreference { get; set; } = new();
    }

    public class PreferenceTableRowViewModel
    {
        public Guid PreferenceId { get; set; }
        [Display(Name = "產品名稱")]
        public Guid ProductId { get; set; }
        [Display(Name = "扣款帳號")]
        public Guid UserId { get; set; }

        [Display(Name = "產品名稱")]
        public string ProductName { get; set; }
        [Display(Name = "產品價格")]
        public decimal ProductPrice { get; set; }
        [Display(Name = "手續費率 (%)")]
        public decimal FeeRate { get; set; }
        [Display(Name = "購買數量")]
        public int OrderQuantity { get; set; }
        [Display(Name = "預計總金額")]
        public decimal EstimatedTotalAmount { get; set; }
        [Display(Name = "總手續費")]
        public decimal TotalFee { get; set; }
        [Display(Name = "扣款帳號")]
        public string AccountNumber { get; set; }
        [Display(Name = "使用者姓名")]
        public string UserName { get; set; }
        [Display(Name = "聯絡 Email")]
        public string Email { get; set; }
    }
}
