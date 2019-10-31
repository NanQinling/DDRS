using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class LoginLog
    {
        public int LoginId { get; set; }
        public string SPName { get; set; }
        public string ServerName { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime ExitTime { get; set; }
        public string LoginAddr { get; set; }
        public string LoginDept { get; set; }
        //登录日志ID（扩展属性，用于登录退出的时候使用）
        public int LoginLogId { get; set; }


    }
}
