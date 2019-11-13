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
    public partial class Form8_systemlog : Form
    {
        public Form8_systemlog()
   
        {
            InitializeComponent();
        }
        string str = "Server=LAPTOP-17RT7OKE;Database=库存管理系统;Trusted_Connection=Yes;Connect Timeout=90";

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form_ex_auditing_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form_menu fm = new Form_menu();
            fm = (Form_menu)this.Owner;
            fm.Deletetabpage("系统日志");
        }
        public void refresh()
        {
            dataGridView1.DataSource = null;
            String sql = "select * from 系统日志";
            DataSet dataset = new DataSet();
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            adapter.Fill(dataset, "系统日志");
            dataGridView1.DataSource = dataset.Tables["系统日志"];
        }
        private void Form_ex_auditing_Load(object sender, EventArgs e)
        {
            String sql = "select * from 系统日志";
            DataSet dataset = new DataSet();
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(sql,conn);
            adapter.Fill(dataset, "系统日志");
            dataGridView1.DataSource = dataset.Tables["系统日志"];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime dateTime1 = dateTimePicker2.Value.AddDays(-1);
            DateTime dateTime2 = dateTimePicker2.Value.AddDays(1);
            String sql = null;
            if (textBox3.Text.Trim()!=String.Empty)
                sql = "select * from 系统日志 where 用户='"+textBox3.Text.Trim()+"' and 时间 > '"+ dateTime1+ "' and 时间 < '"+dateTime2+"'" ;
            else
                sql = "select * from 系统日志 where 时间 > '" + dateTime1 + "' and 时间 < '" + dateTime2 + "'";
            DataSet dataset = new DataSet();
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            adapter.Fill(dataset, "系统日志");
            dataGridView1.DataSource = dataset.Tables["系统日志"];
        }
    }
}
