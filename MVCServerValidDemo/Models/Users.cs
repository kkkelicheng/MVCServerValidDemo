using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MVCServerValidDemo.Models
{
    /// <summary>
    /// 业务模型 不是和数据库字段等同的
    /// 验证的时候需要使用到类库 ComponentModel;
    /// 早期是没有的，现在在MVC中才有，早期用的if判断，winform是没有的
    /// （标识）5个常见的特性：
    /// 1. DisplayName 显示名
    /// 2. Required 非空
    /// 3. StringLength 长度验证
    /// 4. Range 数字范围
    /// 5. RegularExpression 正则验证
    /// </summary>
    public class Users
    {
        public int UserId { get; set; }

        //Required 是DataAnnotations中的，指明一个数据字段是必要的，不能为空
        //[Required(ErrorMessage ="账号不能为空")] 
        //[StringLength(12,MinimumLength =6,ErrorMessage = "账号只能是12位")]
        //假如上面的 账号 两个想改成 用户名，修改起来挺麻烦的，可以用占位符
        [DisplayName("账号")] //显示名
        [Required(ErrorMessage = "{0}不能为空")] //Required 是DataAnnotations中的，指明一个数据字段是必要的，不能为空
        [StringLength(12,MinimumLength =6,ErrorMessage = "{0}长度是{2}到{1}位")] //账号长度是6到12位。
        public string Account { get; set; }

        public string Password1 { get; set; }

        [DisplayName("密码")]
        [Required(ErrorMessage = "{0}不能为空")]
        [Compare("Password1", ErrorMessage = "两次{0}不一致")]
        public string Password2 { get; set; }

        [DisplayName("年纪")]
        [Range(16,50,ErrorMessage ="{0}只能在{1}到{2}之间")]
        public int Age { get; set; }

        [DisplayName("邮箱")]
        [RegularExpression(@"\w+@\w+.\w+",ErrorMessage ="邮箱地址不合法")]
        public string Email { get; set; }   
    }
}
