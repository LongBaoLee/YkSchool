using System;
using System.ComponentModel; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 

namespace Business.Models
{
    /// <summary>
    /// 用户
    /// </summary>
    public class User
    {
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("登录账号")]
        [Required]
        [Range(6,25,ErrorMessage = "请输入长度为6-12位的登录账号")]
        public  string Account { get; set; }

        [DisplayName("用户名称")]
        [Required]
        [StringLength(50,ErrorMessage = "请输入小于50个字符的用户名称")]
        public  string RealName { get; set; }

        [DisplayName("密码")]
        [Required]
        [Range(6, 25, ErrorMessage = "请输入长度为6-12位的密码")]
        public  string Password { get; set; }

        [DisplayName("最后登录时间")]
        public DateTime LastLoginDate { get; set; }

        [DisplayName("创建时间")]
        public  DateTime CreationTime { get; set; }
    }
}