using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class KaoPing
    {
        // ID, 考评年月, 部门, 人员编号, 姓名, 德, 能, 勤, 绩, 考评得分, 考评意见, 具体解释说明, 备注, 更改者, 更改日期;

        public int id { get; set; }
        public string 考评年月 { get; set; }
        public string 部门 { get; set; }
        public string 人员编号 { get; set; }
        public string 姓名 { get; set; }
        public double 德 { get; set; }
        public double 能 { get; set; }
        public double 勤 { get; set; }
        public double 绩 { get; set; }
        public double 考评得分 { get; set; }
        public string 考评意见 { get; set; }
        public string 具体解释说明 { get; set; }
        public string 备注 { get; set; }
        public string 更改者 { get; set; }
        public DateTime 更改日期 { get; set; }
        public bool IsSubmit { get; set; }
        public int 排序 { get; set; }





    }
}
