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
    public partial class Form_ex_auditing : Form
    {
        public Form_ex_auditing()
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
            fm.Deletetabpage("销售订单审核");
        }
        public void refresh()
        {
            dataGridView1.DataSource = null;
            String sql = "select * from 待审核销售订单";
            DataSet dataset = new DataSet();
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            adapter.Fill(dataset, "待审核销售订单");
            dataGridView1.DataSource = dataset.Tables["待审核销售订单"];
        }
        private void Form_ex_auditing_Load(object sender, EventArgs e)
        {
            String sql = "select * from 待审核销售订单";
            DataSet dataset = new DataSet();
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(sql,conn);
            adapter.Fill(dataset, "待审核销售订单");
            dataGridView1.DataSource = dataset.Tables["待审核销售订单"];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String sql = "select * from 待审核销售订单 where 销售单号='" + textBox1.Text.Trim() + "' or 商品编号='" + textBox1.Text.Trim()
                            + "' or 商品名称='" + textBox1.Text.Trim() + "'";
            DataSet dataset = new DataSet();
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            adapter.Fill(dataset, "待审核销售订单");
            dataGridView1.DataSource = dataset.Tables["待审核销售订单"];
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox2.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            int num = Convert.ToInt32(textBox4.Text.Trim());
            String sql = "select 库存数量 from 商品库存 where 商品编号='"+dataGridView1 .CurrentRow.Cells[1].Value.ToString()+"'";
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand mycom = new SqlCommand(sql, conn);
            SqlDataReader myread = mycom.ExecuteReader();
            int amount = 0;
            if(myread.Read())
                amount = Convert.ToInt32(myread[0].ToString());
            textBox5.Text = amount.ToString();
            if (amount < num)
            {
                button2.Enabled = false;
            }
            else {
                button2.Enabled = true;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //更新商品库存表的库存
            int amount = Convert.ToInt32(textBox5.Text.Trim());
            int num = Convert.ToInt32(textBox4.Text.Trim());
            amount = amount - num;
            textBox5.Text = amount.ToString();
            String sql = "update 商品库存 set 库存数量='" + textBox5.Text.Trim() + "' where 商品编号='" + dataGridView1.CurrentRow.Cells[1].Value.ToString() + "'";
            SqlConnection conn5 = new SqlConnection(str);
            conn5.Open();
            SqlCommand mycom = new SqlCommand(sql, conn5);
            int i = mycom.ExecuteNonQuery();
            conn5.Close();
            //跟新客户信息表的收款日期，欠款
            int money = 0;
            String sql4 = "select 欠款 from 客户信息表 where 客户名称='" + textBox3.Text.Trim() + "'";
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand mycom4 = new SqlCommand(sql4, conn);
            SqlDataReader myread = mycom4.ExecuteReader();
            if (myread.Read())
            {
                if (!myread.IsDBNull(0))
                    money = Convert.ToInt32(myread[0].ToString()) + Convert.ToInt32(dataGridView1.CurrentRow.Cells[5].Value.ToString())* Convert.ToInt32(dataGridView1.CurrentRow.Cells[3].Value.ToString());
                else
                    money = Convert.ToInt32(dataGridView1.CurrentRow.Cells[5].Value.ToString())* Convert.ToInt32(dataGridView1.CurrentRow.Cells[3].Value.ToString());
            }
            conn.Close();
            String sql3 = "update 客户信息表 set 收款日期='" + dateTimePicker1.Value + "',欠款='" + money.ToString() + "' where 客户名称='" + textBox3.Text.Trim() + "'";
            conn.Open();
            SqlCommand mycom3 = new SqlCommand(sql3, conn);
            mycom3.ExecuteNonQuery();
            conn.Close();

            //在销售订单表中插入数据
            String sql1 = "insert into 销售订单 values('" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "','" + dataGridView1.CurrentRow.Cells[1].Value.ToString() + "','" +
                dataGridView1.CurrentRow.Cells[2].Value.ToString() + "','" + dataGridView1.CurrentRow.Cells[3].Value.ToString() + "','" + dataGridView1.CurrentRow.Cells[4].Value.ToString() + "','" +
                dataGridView1.CurrentRow.Cells[5].Value.ToString() + "','" + dataGridView1.CurrentRow.Cells[6].Value.ToString() + "','" + dataGridView1.CurrentRow.Cells[7].Value.ToString() + "','" + 
                dateTimePicker1.Value + "','" + dateTimePicker2.Value + "')";
            SqlConnection conn1 = new SqlConnection(str);
            conn1.Open();
            SqlCommand mycom1 = new SqlCommand(sql1, conn1);
           mycom1.ExecuteNonQuery();
            conn1.Close();
            //在带销售表中删去
            string sql2 = "delete from 待审核销售订单 where 销售单号='" + textBox2.Text.Trim() + "'";
            SqlConnection conn2 = new SqlConnection(str);
            conn2.Open();
            SqlCommand mycom2 = new SqlCommand(sql2, conn2);
             mycom2.ExecuteNonQuery();
            conn2.Close();
            refresh();
            //更新系统日志
            String logsql = "insert into 系统日志 values('" + Login.User.name + "','" + "对销售订单进行了审核" + "','" + DateTime.Now + "')";
            SqlConnection log = new SqlConnection(str);
            log.Open();
            SqlCommand logcom = new SqlCommand(logsql, log);
            logcom.ExecuteNonQuery();
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

    }
}
