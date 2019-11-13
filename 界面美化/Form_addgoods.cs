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
    public partial class Form_addgoods : Form
    {
        public Form_addgoods()
        {
            InitializeComponent();
        }
        string str = "Server=LAPTOP-17RT7OKE;Database=库存管理系统;Trusted_Connection=Yes;Connect Timeout=90";


        private void Form_addgoods_Load(object sender, EventArgs e)
        {
            label14.Text = "商品名称,进/售价,库存上/下限不能为空\n且进/售价、库存上/下限必须为数字";
            //自动分配编号
            String sql = "select 商品编号 from 商品资料";
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand mycom = new SqlCommand(sql,conn);
            SqlDataReader myread = mycom.ExecuteReader();
            String num = null;
            while (myread.Read())
            {
                num = myread[0].ToString();
            }
            conn.Close();
            textBox1.Text = (Convert.ToInt32(num) + 1).ToString();
            textBox1.Enabled = false;
            //供应商
            String sql1 = "select 供应商名称 from 供应商信息表";
            SqlConnection conn1 = new SqlConnection(str);
            conn1.Open();
            DataSet dataset1 = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(sql1, conn1);
            adapter.Fill(dataset1, "供应商信息表");
            comboBox1.DataSource = dataset1.Tables["供应商信息表"];
            comboBox1.DisplayMember = "供应商名称";
            conn1.Close();
            //类别
            String sql2 = "select 商品类别 from 商品种类";
            SqlConnection conn2 = new SqlConnection(str);
            conn2.Open();
            DataSet dataset2 = new DataSet();
            SqlDataAdapter adapter2 = new SqlDataAdapter(sql2, conn2);
            adapter2.Fill(dataset2, "商品种类");
            comboBox2.DataSource = dataset2.Tables["商品种类"];
            comboBox2.DisplayMember = "商品类别";
            conn2.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ( textBox2.Text.Trim() != String.Empty && textBox8.Text.Trim() != String.Empty && textBox9.Text.Trim() != String.Empty && textBox10.Text.Trim() != String.Empty && textBox11.Text.Trim() != String.Empty)
            {
                SqlConnection conn1 = new SqlConnection(str);
                conn1.Open();
                string sql1 = "insert into 商品资料 values('" + textBox1.Text.Trim() + "','" + textBox2.Text.Trim() + "','" + textBox3.Text.Trim() + "','" +
                    textBox4.Text.Trim() + "','" + textBox5.Text.Trim() + "','" + textBox6.Text.Trim() + "','" + textBox7.Text.Trim() + "','" + comboBox1.Text.Trim() +
                    "','" + comboBox2.Text.Trim() + "','" + textBox8.Text.Trim() + "','" + textBox9.Text.Trim() + "','" + textBox10.Text.Trim() + "','" + textBox11.Text.Trim() + "')";
                SqlCommand mycom = new SqlCommand(sql1, conn1);
                mycom.ExecuteNonQuery();
                conn1.Close();
                Form6_goods f;
                f = (Form6_goods)this.Owner;
                f.refresh();
                //在商品库存加入该商品
                 String sql2 = "insert into 商品库存 values('" + textBox1.Text.Trim() + "','" + textBox2.Text.Trim() + "','" + textBox4.Text.Trim() + "','" +
                   textBox5.Text.Trim() + "','" + textBox6.Text.Trim() + "','" + textBox7.Text.Trim() + "','0','" + textBox8.Text.Trim() + "','"+ DateTime.Now +"')";
                SqlConnection conn2 = new SqlConnection(str);
                conn2.Open();
                SqlCommand mycom1 = new SqlCommand(sql2, conn2);
                mycom1.ExecuteNonQuery();
                conn2.Close();
                
                //更新系统日志
                String logsql = "insert into 系统日志 values('" + Login.User.name + "','" + "添加商品资料" + "','" + DateTime.Now + "')";
                SqlConnection log = new SqlConnection(str);
                log.Open();
                SqlCommand logcom = new SqlCommand(logsql, log);
                logcom.ExecuteNonQuery();
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox3.Text = GetPY(textBox2.Text.Trim());
        }
        public string ConvertToPY(string str)
        {

            if (str.CompareTo("吖") < 0) return str;
            if (str.CompareTo("八") < 0) return "A";
            if (str.CompareTo("嚓") < 0) return "B";
            if (str.CompareTo("咑") < 0) return "C";
            if (str.CompareTo("妸") < 0) return "D";
            if (str.CompareTo("发") < 0) return "E";
            if (str.CompareTo("旮") < 0) return "F";
            if (str.CompareTo("铪") < 0) return "G";
            if (str.CompareTo("讥") < 0) return "H";
            if (str.CompareTo("咔") < 0) return "J";
            if (str.CompareTo("垃") < 0) return "K";
            if (str.CompareTo("嘸") < 0) return "L";
            if (str.CompareTo("拏") < 0) return "M";
            if (str.CompareTo("噢") < 0) return "N";
            if (str.CompareTo("妑") < 0) return "O";
            if (str.CompareTo("七") < 0) return "P";
            if (str.CompareTo("亽") < 0) return "Q";
            if (str.CompareTo("仨") < 0) return "R";
            if (str.CompareTo("他") < 0) return "S";
            if (str.CompareTo("哇") < 0) return "T";
            if (str.CompareTo("夕") < 0) return "W";
            if (str.CompareTo("丫") < 0) return "X";
            if (str.CompareTo("帀") < 0) return "Y";
            if (str.CompareTo("咗") < 0) return "Z";
            return str;


        }
        public string GetPY(string str)
        {
            int i = str.Trim().Length;
            string st = "";
            for (int t = 0; t < i; t++)
            {
                st += ConvertToPY(str.Trim().Substring(t, 1));
            }
            return st;
        }

    }
}
