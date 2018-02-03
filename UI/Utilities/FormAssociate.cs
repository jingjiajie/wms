using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using WMS.DataAccess;
using System.Data.SqlClient;

namespace WMS.UI
{
    public partial class FormAssociate : Form
    {
        public enum MoveDirection
        {
            UP,DOWN
        }

        private class ListItem
        {
            public string Word;
            public string Hint;

            public override string ToString()
            {
                string str = string.Format("{0,-22}",Word);
                if (string.IsNullOrWhiteSpace(Hint) == false)
                {
                    str += " -" + Hint;
                }
                return str;
            }
        }

        List<string> sqls = new List<string>();
        WMSEntities globalWMSEntities = new WMSEntities();

        TextBox textBox = null; //附着的可编辑对象句柄
        private bool selected = false; //记录用户是否选择了项目，选择更新为true，文字更改恢复false
        private bool stayVisible = false; //强制保持显示

        public FormAssociate(TextBox textBox)
        {
            InitializeComponent();
            this.textBox = textBox;
            textBox.PreviewKeyDown += textBox_PreviewKeyDown;
            //textBox.VisibleChanged += textBox_VisibleChanged;
            textBox.TextChanged += textBox_TextChanged;
            this.FindParentForm(textBox).LocationChanged += textBoxBaseForm_LocationChanged;
            this.GotFocus += formAssociate_GotFocus;
            if (textBox.Visible == true)
            {
                this.Show();
            }
        }

        public void AddAssociationSQL(string sql)
        {
            this.sqls.Add(sql);
        }

        public void ClearAssociationSQL()
        {
            this.sqls.Clear();
        }

        private void textBoxBaseForm_LocationChanged(object sender, EventArgs e)
        {
            this.AdjustPosition();
        }

        //递归找父控件，直到找到Form
        private Form FindParentForm(Control c)
        {
            if (c is Form)
                return c as Form;
            else if (c.Parent != null)
                return FindParentForm(c.Parent);
            else
                return null;
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            this.Selected = false;
            this.RefreshAssociation();
        }

        private DateTime newestListBoxDataTime = DateTime.Now;
        public void RefreshAssociation()
        {
            if (string.IsNullOrWhiteSpace(textBox.Text) || this.sqls.Count == 0)
            {
                this.Hide();
                return;
            }
            new Thread(() =>
            {
                DateTime threadStartTime = DateTime.Now;
                this.newestListBoxDataTime = threadStartTime;
                try
                {
                    SqlConnection connection = (SqlConnection)globalWMSEntities.Database.Connection;
                    if(connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    SqlParameter parameter = new SqlParameter("@value", textBox.Text);
                    List<ListItem> data = new List<ListItem>(); //存储返回结果
                    foreach (string sql in this.sqls)
                    {
                        SqlCommand sqlCommand = new SqlCommand(sql, connection);
                        sqlCommand.Parameters.Add(parameter);
                        SqlDataReader dataReader = sqlCommand.ExecuteReader();
                        sqlCommand.Parameters.Clear();
                        while (data.Count < 30 && dataReader.Read()) //最多显示30条数据
                        {
                            ListItem newListItem = new ListItem();
                            if (dataReader.FieldCount >= 1)
                            {
                                newListItem.Word = dataReader.GetValue(0).ToString();
                            }
                            if(dataReader.FieldCount >= 2)
                            {
                                newListItem.Hint = dataReader.GetValue(1).ToString();
                            }
                            data.Add(newListItem);
                        }
                    }
                    if(this.newestListBoxDataTime > threadStartTime)
                    {
                        return;
                    }
                    this.Invoke(new Action(() =>{
                        this.listBox.Items.Clear();
                        this.listBox.Items.AddRange(data.ToArray());
                        if (data.Count == 0)
                        {
                            this.Hide();
                        }
                        else if (this.Visible == false && textBox.Visible == true)
                        {
                            this.Show();
                        }
                    }));
                }
                catch
                {
                    //网络连接失败就不联想了
                    return;
                }
            }).Start();
        }

        //volatile bool isWaitingShow = false;
        //private void textBox_VisibleChanged(object sender, EventArgs e)
        //{
        //    new Thread(()=>
        //    {
        //        if (isWaitingShow) return;
        //        isWaitingShow = true;
        //        Thread.Sleep(50); //考虑到ReoGrid太tm坑，必须先隐藏编辑框再显示回来。这里等50毫秒，放置显示隐藏时联想窗口跟着显示隐藏
        //        this.Invoke(new Action(()=>
        //        {
        //            //如果编辑框没有字，肯定不用显示
        //            if (string.IsNullOrWhiteSpace(textBox.Text))
        //            {
        //                this.Hide();
        //                isWaitingShow = false;
        //                return;
        //            }
        //            //编辑框有字的情况下，如果50毫秒过后，编辑框又重新显示出来了，并且有字
        //            //并且联想窗口不是选择了项之后自己隐藏的，则显示联想
        //            if (textBox.Visible && this.selected == false)
        //            {
        //                this.Show();
        //            }
        //            else
        //            {
        //                this.Hide();
        //            }
        //            isWaitingShow = false;
        //        }));
        //    }).Start();
        //}

        private void textBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                this.MoveSelection(MoveDirection.UP);
            }else if(e.KeyCode == Keys.Down)
            {
                this.MoveSelection(MoveDirection.DOWN);
            }else if(e.KeyCode == Keys.Enter)
            {
                this.SelectItem();
            }
        }

        private void SelectItem()
        {
            //没显示联想，按回车。显然不应该把联想内容填上去
            if (this.Visible == false)
            {
                return;
            }
            if (this.listBox.SelectedItem != null)
            {
                this.stayVisible = true;
                textBox.TextChanged -= this.textBox_TextChanged; //修改文字不触发事件
                textBox.Text = (this.listBox.SelectedItem as ListItem).Word;
                textBox.TextChanged += this.textBox_TextChanged;
                this.Selected = true;
                this.stayVisible = false;
                this.Hide();
            }
        }

        private void formAssociate_GotFocus(object sender, EventArgs e)
        {
            this.GiveBackFocus();
        }

        public void MoveSelection(MoveDirection direction)
        {
            if(direction == MoveDirection.UP)
            {
                if(this.listBox.SelectedIndex > 0)
                {
                    this.listBox.SelectedIndex--;
                }
            }else if(direction == MoveDirection.DOWN)
            {
                if (this.listBox.SelectedIndex < this.listBox.Items.Count - 1)
                {
                    this.listBox.SelectedIndex++;
                }
            }
        }

        private void AdjustPosition()
        {
            Point textBoxScreenPosition = textBox.PointToScreen(new Point(0, 0));
            this.SetPosition(textBoxScreenPosition.X, textBoxScreenPosition.Y + 20); //把联想窗口调整到输入地方下面一些
            this.GiveBackFocus();
        }

        private void GiveBackFocus()
        {
            this.textBox.Focus();
            if (textBox.SelectionLength > 0)
            {
                textBox.SelectionLength = 0;
                textBox.SelectionStart = textBox.Text.Length;
            }
        }

        public new void Show()
        {
            if (this.Visible == false)
            {
                base.Show();
            }
            this.AdjustPosition();
        }

        private void SetPosition(int x,int y)
        {
            this.Location = new Point(x, y);
        }

        private void FormAssociate_Load(object sender, EventArgs e)
        {

        }

        const int WS_EX_NOACTIVATE = 0x08000000;
        //重载Form的CreateParams属性，添加不获取焦点属性值。  
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= WS_EX_NOACTIVATE;
                return cp;
            }
        }

        public bool StayVisible { get => stayVisible; set => stayVisible = value; }
        public bool Selected { get => selected; set => selected = value; }

        public new void Hide()
        {
            if (this.stayVisible)
            {
                return;
            }
            else
            {
                base.Hide();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox_DoubleClick(object sender, EventArgs e)
        {
            this.SelectItem();
        }

        private void listBox_Click(object sender, EventArgs e)
        {
            GiveBackFocus();
        }
    }
}
