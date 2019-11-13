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
    public partial class Form_change : Form
    {
        public Form_change()
        {
            InitializeComponent();
        }

        string str = "Server=LAPTOP-17RT7OKE;Database=库存管理系统;Trusted_Connection=Yes;Connect Timeout=90";
        public class owner
        {
            public static String text=null;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (owner.text == "客户名称：")
            {
                //修改信息
                SqlConnection conn1 = new SqlConnection(str);
                conn1.Open();
                string sql1 = "update 客户信息表 set 拼音码='" + textBox2.Text.Trim() + "',公司名称='" + textBox3.Text.Trim() + "',联系人='" +
                    textBox6.Text.Trim() + "',类别='" + comboBox1.Text + "',区域='" + textBox4.Text.Trim() + "',地址='" + textBox5.Text.Trim() + "',电话='" + textBox7.Text.Trim() +
                    "',开户银行='" + textBox8.Text.Trim() + "',银行账户='" + textBox9.Text.Trim() + "',欠款='" + textBox10.Text.Trim() + "',收款期限='" + textBox11.Text.Trim() + "' where 客户名称='" + textBox1.Text.ToString() + "'";
                SqlCommand mycom = new SqlCommand(sql1,conn1);
                mycom.ExecuteNonQuery();
                conn1.Close();
                //刷新客户资料窗口
                Form4_customer f4;
                f4 = (Form4_customer)this.Owner;
                f4.refresh();
                //更新系统日志
                String logsql = "insert into 系统日志 values('" + Login.User.name + "','" + "修改客户信息" + "','" + DateTime.Now + "')";
                SqlConnection log = new SqlConnection(str);
                log.Open();
                SqlCommand logcom = new SqlCommand(logsql, log);
                logcom.ExecuteNonQuery();
                this.Close();
            }
            else
            {
                //修改信息
                SqlConnection conn1 = new SqlConnection(str);
                conn1.Open();
                string sql1 = "update 供应商信息表 set 拼音码='"  + textBox2.Text.Trim() + "',公司名称='" + textBox3.Text.Trim() + "',联系人='" +
                    textBox6.Text.Trim() + "',类别='" + comboBox1.Text + "',区域='" + textBox4.Text.Trim() + "',地址='" + textBox5.Text.Trim() + "',电话='" + textBox7.Text.Trim() +
                    "',开户银行='" + textBox8.Text.Trim() + "',银行账户='" + textBox9.Text.Trim() + "',欠款='" + textBox10.Text.Trim() + "',收款期限='" + textBox11.Text.Trim() + "' where 供应商名称='"+textBox1.Text.ToString()+"'";
                SqlCommand mycom = new SqlCommand(sql1, conn1);
                mycom.ExecuteNonQuery();
                conn1.Close();
                this.Close();
                //刷新客户资料窗口
                Form5_supplier f;
                f = (Form5_supplier)this.Owner;
                f.refresh();
                //更新系统日志
                String logsql = "insert into 系统日志 values('" + Login.User.name + "','" + "修改供应商信息" + "','" + DateTime.Now + "')";
                SqlConnection log = new SqlConnection(str);
                log.Open();
                SqlCommand logcom = new SqlCommand(logsql, log);
                logcom.ExecuteNonQuery();
                this.Close();
            }
        }

        private void Form_add_Load(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            if (owner.text == "客户名称：")
            {
                label1.Text = "客户名称：";
                SqlConnection conn1 = new SqlConnection(str);
                conn1.Open();
                string sql1 = "select 客户类别 from 客户种类";
                DataSet dataset1 = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(sql1, conn1);
                adapter.Fill(dataset1, "客户种类");
                comboBox1.DataSource = dataset1.Tables["客户种类"];
                comboBox1.DisplayMember = "客户类别";
                conn1.Close();
                //加载客户信息
                String sql = "select * from 客户信息表 where 客户名称='"+Form4_customer.select.cname+"'";
                SqlConnection conn = new SqlConnection(str);
                conn.Open();
                SqlCommand mycom = new SqlCommand(sql, conn);
                SqlDataReader myread = mycom.ExecuteReader();
                if (myread.Read())
                {
                    if (!myread.IsDBNull(0))
                        textBox1.Text = myread[0].ToString();
                    if (!myread.IsDBNull(1))
                        textBox2.Text = myread[1].ToString();
                    if (!myread.IsDBNull(2))
                        textBox3.Text = myread[2].ToString();
                    if (!myread.IsDBNull(3))
                        textBox6.Text = myread[3].ToString();
                    if (!myread.IsDBNull(4))
                        comboBox1.Text = myread[4].ToString();
                    if (!myread.IsDBNull(5))
                        textBox4.Text = myread[5].ToString();
                    if (!myread.IsDBNull(6))
                        textBox5.Text = myread[6].ToString();
                    if (!myread.IsDBNull(7))
                        textBox7.Text = myread[7].ToString();
                    if (!myread.IsDBNull(8))
                        textBox8.Text = myread[8].ToString();
                    if (!myread.IsDBNull(9))
                        textBox9.Text = myread[9].ToString();
                    if (!myread.IsDBNull(10))
                        textBox10.Text = myread[10].ToString();
                    if (!myread.IsDBNull(11))
                        textBox11.Text = myread[11].ToString();
                }
            }
            else
            {
                label1.Text = "供应商名称：";
                SqlConnection conn1 = new SqlConnection(str);
                conn1.Open();
                string sql1 = "select 供应商类别 from 供应商种类";
                DataSet dataset1 = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(sql1, conn1);
                adapter.Fill(dataset1, "供应商种类");
                comboBox1.DataSource = dataset1.Tables["供应商种类"];
                comboBox1.DisplayMember = "供应商类别";
                conn1.Close();
                //加载供应商信息
                String sql = "select * from 供应商信息表 where 供应商名称='" + Form4_customer.select.cname + "'";
                SqlConnection conn = new SqlConnection(str);
                conn.Open();
                SqlCommand mycom = new SqlCommand(sql, conn);
                SqlDataReader myread = mycom.ExecuteReader();
                if (myread.Read())
                {
                    if (!myread.IsDBNull(0))
                        textBox1.Text = myread[0].ToString();
                    if (!myread.IsDBNull(1))
                        textBox2.Text = myread[1].ToString();
                    if (!myread.IsDBNull(2))
                        textBox3.Text = myread[2].ToString();
                    if (!myread.IsDBNull(3))
                        textBox6.Text = myread[3].ToString();
                    if (!myread.IsDBNull(4))
                        comboBox1.Text = myread[4].ToString();
                    if (!myread.IsDBNull(5))
                        textBox4.Text = myread[5].ToString();
                    if (!myread.IsDBNull(6))
                        textBox5.Text = myread[6].ToString();
                    if (!myread.IsDBNull(7))
                        textBox7.Text = myread[7].ToString();
                    if (!myread.IsDBNull(8))
                        textBox8.Text = myread[8].ToString();
                    if (!myread.IsDBNull(9))
                        textBox9.Text = myread[9].ToString();
                    if (!myread.IsDBNull(10))
                        textBox10.Text = myread[10].ToString();
                    if (!myread.IsDBNull(11))
                        textBox11.Text = myread[11].ToString();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
