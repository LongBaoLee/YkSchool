using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class Instructor
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("姓名")]
        public string RealName { get; set; }

        [DisplayName("联系电话")]
        public string ContantNumber { get; set; }

        [EmailAddress]
        [DisplayName("邮箱")]
        public string Email { get; set; }
    }
}