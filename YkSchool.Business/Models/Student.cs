using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    /// <summary>
    /// 学生
    /// </summary>
    public class Student
    {
        public  int Id { get; set; }

        public Student()
        {
            CreationTime = DateTime.Now;
        }
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "请输入学生姓名")]
        [MaxLength(50)]
        [DisplayName("学生名称")]
        public string RealName { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        [DisplayName("年龄")]
        [Range(0, 150, ErrorMessage = "请输入0到150的数字")]
        public int Age { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [DisplayName("性别")]
        public SexType Sex { get; set; }

        /// <summary>
        /// 所在学校
        /// </summary>
        [MaxLength(200)]
        [DisplayName("所在学校")]
        public string CurrentSchool { get; set; }
        /// <summary>
        /// 年级
        /// </summary>
        /// 
        [MaxLength(50)]
        [DisplayName("年级")]
        public string Grade { get; set; }
        /// <summary>
        /// 班级
        /// </summary>
        [MaxLength(50)]
        [DisplayName("班级")]
        public string Class { get; set; }
        /// <summary>
        /// 家长姓名
        /// </summary>
        [Required(ErrorMessage = "请输入学生家长姓名")]
        [MaxLength(50)]
        [DisplayName("家长姓名")]
        public string ParentsName { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        [Required(ErrorMessage = "请输入学生家长联系电话")]
        [MaxLength(50)]
        [DisplayName("联系电话")]
        public string ContantNumber { get; set; }

        /// <summary>
        /// 家庭住址
        /// </summary>
        [MaxLength(250)]
        public string Address { get; set; }

        /// <summary>
        /// 所在舞蹈班级
        /// </summary>
        [DisplayName("所在舞蹈年级")]
        public int? DanceGradeId { get; set; }
         
        /// <summary>
        /// 总缴费金额
        /// </summary>
        [DisplayName("总缴费金额")]
        public decimal TotalFeeAmount { get; set; }


        /// <summary>
        /// 总缴费金额
        /// </summary>
        [DisplayName("总缴费金额")]
        public decimal LastFeeAoumnt { get; set; }

        /// <summary>
        /// 最后缴费时间
        /// </summary>
        [DisplayName("最后缴费时间"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? LastFeeDate { get; set; }
        /// <summary>
        /// 学费到期时间
        /// </summary>
        [DisplayName("学费到期时间"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ExpireDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(2000)]
        [DisplayName("备注")]
        public string Description { get; set; }

        [DisplayName("创建时间")]
        public DateTime CreationTime { get; set; }

        [DisplayName("是否删除")]
        public bool IsDeleted { get; set; }
    }
}