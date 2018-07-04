using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    /// <summary>
    /// 舞蹈年级
    /// </summary>
    public class DanceGrande
    {
        public  int Id { get; set; } 
         
        [Required]
        [MaxLength(50)]
        [DisplayName("年级名称")]
        public string Title { get; set; }

        [DisplayName("班主任")]
        public int? InstructorId { get; set; } 
         
        [DisplayName("备注")]
        public int Description { get; set; }

    }
}