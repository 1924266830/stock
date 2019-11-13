using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevComponents.DotNetBar;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace 界面美化
{
    public partial class Login : Office2007Form
    {
        public Login()
        {
            InitializeComponent();
        }
        string str = "Server=LAPTOP-17RT7OKE;Database=库存管理系统;Trusted_Connection=Yes;Connect Timeout=90";

        public class User
        {
            public static String power = null;
            public static String name = null;
            public static String password = null;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            String sql = "select * from 用户信息表 where 姓名='"+textBox1 .Text .Trim()+"'";
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand mycom = new SqlCommand(sql, conn);
            SqlDataReader myread = mycom.ExecuteReader();
            if (myread.Read())
            {
                if (myread[2].ToString() == textBox2.Text.Trim())
                {
                    User.power = myread[1].ToString();
                    User.password = textBox2.Text.Trim();
                    User.name = textBox1.Text.Trim();
                    //更新系统日志
                    String logsql = "insert into 系统日志 values('" + Login.User.name + "','" + "登陆了" + "','" + DateTime.Now + "')";
                    SqlConnection log = new SqlConnection(str);
                    log.Open();
                    SqlCommand logcom = new SqlCommand(logsql, log);
                    logcom.ExecuteNonQuery();
                    Form_menu fm = new Form_menu();
                    fm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("登陆信息错误");
                }
            }
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
