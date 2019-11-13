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
    public partial class Form_im_auditing : Form
    {
        public Form_im_auditing()
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
            fm.Deletetabpage("采购订单审核");
        }
        public void refresh()
        {
            dataGridView1.DataSource = null;
            String sql = "select * from 待审核采购订单";
            DataSet dataset = new DataSet();
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            adapter.Fill(dataset, "待审核采购订单");
            dataGridView1.DataSource = dataset.Tables["待审核采购订单"];
        }
        private void Form_ex_auditing_Load(object sender, EventArgs e)
        {
            String sql = "select * from 待审核采购订单";
            DataSet dataset = new DataSet();
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(sql,conn);
            adapter.Fill(dataset, "待审核采购订单");
            dataGridView1.DataSource = dataset.Tables["待审核采购订单"];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String sql = "select * from 待审核采购订单 where 采购单号='" + textBox1.Text.Trim() + "' or 商品编号='" + textBox1.Text.Trim()
                            + "' or 商品名称='" + textBox1.Text.Trim() + "'";
            DataSet dataset = new DataSet();
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            adapter.Fill(dataset, "待审核采购订单");
            dataGridView1.DataSource = dataset.Tables["待审核采购订单"];
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox2.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            int num = Convert.ToInt32(textBox4.Text.Trim());
            String sql = "select 库存数量 from 商品库存 where 商品编号='"+dataGridView1 .CurrentRow.Cells[1].Value.ToString()+"'";
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand mycom = new SqlCommand(sql, conn);
            SqlDataReader myread = mycom.ExecuteReader();
            int amount = 0;
            if(myread.Read())
                amount = Convert.ToInt32(myread[0].ToString());
            textBox5.Text = myread[0].ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //更新商品库存表的库存
            int amount = Convert.ToInt32(textBox5.Text.Trim());
            int num = Convert.ToInt32(textBox4.Text.Trim());
            amount += num;
            textBox5.Text = amount.ToString();
            String sql = "update 商品库存 set 库存数量='" + amount.ToString() + "' where 商品编号='" + dataGridView1.CurrentRow.Cells[1].Value.ToString()+"'";
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand mycom = new SqlCommand(sql,conn);
            mycom.ExecuteNonQuery();
            conn.Close();
            //更新该供应商的还款日期,以及欠款
            int money = 0;
            String sql4 = "select 欠款 from 供应商信息表 where 供应商名称='" + textBox3.Text.Trim() + "'";
            conn.Open();
            SqlCommand mycom4 = new SqlCommand(sql4, conn);
            SqlDataReader myread = mycom4.ExecuteReader();
            if (myread.Read())
            {
                if (!myread.IsDBNull(0))
                    money += Convert.ToInt32(myread[0].ToString()) ;
                money += Convert.ToInt32(dataGridView1.CurrentRow.Cells[5].Value.ToString());
            }
            conn.Close();
            String sql3 = "update 供应商信息表 set 收款日期='" + dateTimePicker1.Value + "',欠款='" + money.ToString() + "' where 供应商名称='" + textBox3.Text.Trim() + "'";
            conn.Open();
            SqlCommand mycom3 = new SqlCommand(sql3, conn);
            mycom3.ExecuteNonQuery();
            conn.Close();

            //在采购订单表中插入数据
            String sql1 = "insert into 采购订单 values('" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "','" + dataGridView1.CurrentRow.Cells[1].Value.ToString() + "','" +
                dataGridView1.CurrentRow.Cells[2].Value.ToString() + "','" + dataGridView1.CurrentRow.Cells[3].Value.ToString() + "','" + dataGridView1.CurrentRow.Cells[4].Value.ToString() + "','" +
                dataGridView1.CurrentRow.Cells[5].Value.ToString() + "','" + dataGridView1.CurrentRow.Cells[6].Value.ToString() + "','" + 
                dateTimePicker1.Value + "','" + dateTimePicker2.Value + "')";
            conn.Open();
            SqlCommand mycom1 = new SqlCommand(sql1, conn);
           mycom1.ExecuteNonQuery();
            conn.Close();
            //在带采购表中删去
            string sql2 = "delete from 待审核采购订单 where 采购单号='" + textBox2.Text.Trim() + "'";
            conn.Open();
            SqlCommand mycom2 = new SqlCommand(sql2, conn);
             mycom2.ExecuteNonQuery();
            conn.Close();
            refresh();
            //更新系统日志
            String logsql = "insert into 系统日志 values('" + Login.User.name + "','" + "对采购订单进行了审核" + "','" + DateTime.Now + "')";
            SqlConnection log = new SqlConnection(str);
            log.Open();
            SqlCommand logcom = new SqlCommand(logsql, log);
            logcom.ExecuteNonQuery();

        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }
}
