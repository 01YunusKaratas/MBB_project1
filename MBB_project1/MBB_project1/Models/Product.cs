using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MBB_project1.Models
{
    [Table("product_table")]
    public class Product
    {
        
        [Key] // bu ise database de otomatik artmasından dolayı 
        public int PRODUCT_ID { get; set; }
        [Required] //bu alanlar boş bırakılamaz
        public string PRODUCT_NAME { get; set; }
        [Required]
        public int PRODUCT_COUNT{  get; set; }
        [Required]
        public string PRODUCT_DEPARTMENT { get; set; }
    }
}
