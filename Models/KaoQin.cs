using System;

namespace Models
{
    public class KaoQin
    {
        public int id { get; set; }
        public string 考勤年月 { get; set; }
        public string 人员编号 { get; set; }
        public string 姓名 { get; set; }
        public double 应出勤 { get; set; }
        public double 实际出勤 { get; set; }
        public double 出差 { get; set; }
        public double 旷工 { get; set; }
        public double 年假 { get; set; }
        public double 事假 { get; set; }
        public double 病假 { get; set; }
        public double 正常调休 { get; set; }
        public double 产假 { get; set; }
        public double 陪产假 { get; set; }
        public double 婚假 { get; set; }
        public double 丧假 { get; set; }
        public double 迟到早退次数 { get; set; }
        public double 缺卡次数 { get; set; }
        public double 工作日加班次数 { get; set; }
        public double 休息日加班 { get; set; }
        public double 节假日加班 { get; set; }
        public double 休息日出差 { get; set; }
        public double 夜间值班次数 { get; set; }
        public double 夜间值班调休次数 { get; set; }
        public double 打卡签到次数 { get; set; }
        public double 工作时长 { get; set; }
        public string 备注 { get; set; }
        public string 部门 { get; set; }
        public string 班组 { get; set; }
        public string 更改者 { get; set; }
        public DateTime 更改日期 { get; set; }

    }
}
