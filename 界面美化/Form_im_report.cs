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
    public partial class Form_im_report : Form
    {
        public Form_im_report()
   
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
            fm.Deletetabpage("采购报表");
        }
        public void refresh()
        {
            dataGridView1.DataSource = null;
            String sql = "select * from 采购订单";
            DataSet dataset = new DataSet();
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            adapter.Fill(dataset, "采购订单");
            dataGridView1.DataSource = dataset.Tables["采购订单"];
        }
        private void Form_ex_auditing_Load(object sender, EventArgs e)
        {
            String sql = "select * from 采购订单";
            DataSet dataset = new DataSet();
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(sql,conn);
            adapter.Fill(dataset, "采购订单");
            dataGridView1.DataSource = dataset.Tables["采购订单"];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime dateTime1 = dateTimePicker2.Value.AddDays(-1);
            DateTime dateTime2 = dateTimePicker2.Value.AddDays(1);
            String sql = null;
            if (textBox3.Text.Trim()!=String.Empty)
                sql = "select * from 采购订单 where 供应商='"+textBox3.Text.Trim()+"' and 采购日期 > '"+ dateTime1+ "' and 采购日期 < '"+dateTime2+"'" ;
            else
                sql = "select * from 采购订单 where 采购日期 > '" + dateTime1 + "' and 采购日期 < '" + dateTime2 + "'";
            DataSet dataset = new DataSet();
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            adapter.Fill(dataset, "采购订单");
            dataGridView1.DataSource = dataset.Tables["采购订单"];
        }

 
    }
}
