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
    public partial class Form_adduser : Form
    {
        public Form_adduser()
        {
            InitializeComponent();
        }
        string str = "Server=LAPTOP-17RT7OKE;Database=库存管理系统;Trusted_Connection=Yes;Connect Timeout=90";

        private void Form_adduser_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            String sql = "select 权限 from 用户种类";
            SqlDataAdapter adapter = new SqlDataAdapter(sql,conn);
            DataSet ds = new DataSet();
            adapter.Fill(ds,"用户种类");
            comboBox1.DataSource = ds.Tables["用户种类"];
            comboBox1.DisplayMember = "权限";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            String sql = "insert into 用户信息表 values('"+textBox1.Text.Trim()+"','"+comboBox1.Text+"','"+textBox2.Text.Trim()+"')";
            SqlCommand mycom = new SqlCommand(sql,conn);
            mycom.ExecuteNonQuery();
            Form7_user f = new Form7_user();
            f = (Form7_user)this.Owner;
            f.refresh();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
