using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class JiaBan
    {
        public int id { get; set; }
        public string 考勤年月 { get; set; }
        public string 人员编号 { get; set; }
        public string 姓名 { get; set; }
        public double 工作日加班次数 { get; set; }
        public double 休息日加班 { get; set; }
        public double 节假日加班 { get; set; }
        public double 正常调休 { get; set; }
        public double 夜间值班次数 { get; set; }
        public double 夜间值班调休次数 { get; set; }
        public double 金额 { get; set; }
        public string 备注 { get; set; }
        public string 部门 { get; set; }
        public string 更改者 { get; set; }
        public DateTime 更改日期 { get; set; }


    }
}
