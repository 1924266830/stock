using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 界面美化
{
    public partial class Form_password : Form
    {
        public Form_password()
        {
            InitializeComponent();
        }
        string str = "Server=LAPTOP-17RT7OKE;Database=库存管理系统;Trusted_Connection=Yes;Connect Timeout=90";

        private void button1_Click(object sender, EventArgs e)
        {
            if (Login.User.password == textBox1.Text.Trim())
            {
                if (textBox2.Text.Trim() == textBox3.Text.Trim())
                {
                    String sql = "update 用户登录表 set 密码='" + textBox3.Text.Trim() + "' where 姓名='" + Login.User.name + "'";
                    SqlConnection conn = new SqlConnection(str);
                    conn.Open();
                    SqlCommand mycom = new SqlCommand(sql, conn);
                    int i = mycom.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("修改成功");
                    }
                    conn.Close();
                    this.Close();
                }
                else
                    MessageBox.Show("确认密码和新密码不一致");
            }
            else
            {
                MessageBox.Show("原密码错误");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
