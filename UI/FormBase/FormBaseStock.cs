using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using unvell.ReoGrid;

namespace WMS.UI
{
    public partial class FormBaseStock : Form
    {
        public FormBaseStock()
        {
            InitializeComponent();
        }

        private void FormBaseStock_Load(object sender, EventArgs e)
        {
            InitWorksheet();
        }

        private void InitWorksheet()
        {
            var worksheet = this.reoGridControlMain.Worksheets[0];
            worksheet.SelectionMode = WorksheetSelectionMode.Row;
            string[] titles = { "ID", "零件ID", "收货单ID", "存货日期", "生产日期", "失效日期", "库区", "目标库位", "包装单位", "收货区 库位", "溢库区 库位", "出库区 库位", "收货区数量", "溢库区数量", "出货区数量", "翻包器具数量", "回收箱体", "无订单区", "不合格品区", "规划拖位", "规划翻包器具数量", "托盘号", "批次", "库存状态", "厂商批号", "项目信息", "项目阶段信息", "物权属性", "车型", "箱号" };
            for(int i=0;i<titles.Length;i++)
            {
                worksheet.ColumnHeaders[i].Text = titles[i];
            }
            worksheet.Columns = titles.Length;
        }
    }
}
