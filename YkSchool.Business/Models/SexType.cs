using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public enum SexType
    {
        /// <summary>
        /// 男
        /// </summary>
        [Description("男")]
        [Display(Name = "男")]
        Male = 0,
        /// <summary>
        /// 女
        /// </summary>
        [Display(Name = "女")]
        [Description("女")]
        Female = 1
    }
}