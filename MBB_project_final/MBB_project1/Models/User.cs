using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//Buradaki classı'da User tablosu ile ilişkili olucak şekilde yaptık.
namespace MBB_project1.Models
{
    [Table("user_table")]
    public class User
    {
        //burası database de otomatik artan olduğu için kullanıcak bu paramater
        public int ID { get; set; }
        [Required]
        public string USERNAME { get; set; }
        [Required]
        public string PASSWORD { get; set; }
        
        public string? EMAIL{ get; set; }    
        
    }
}
