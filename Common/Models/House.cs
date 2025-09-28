using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Models
{
    public class House
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(200)]
        [Display(Name = "房屋名稱")]
        public string HouseName { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        [Display(Name = "地址")]
        public string Address { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "總價格")]
        public decimal TotalPrice { get; set; }

        [Required]
        [Column(TypeName = "decimal(8,2)")]
        [Display(Name = "坪數")]
        public decimal FloorArea { get; set; }

        [MaxLength(2000)]
        [Display(Name = "描述")]
        public string? Description { get; set; }

        [Display(Name = "建立時間")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [Display(Name = "更新時間")]
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
    }
}
