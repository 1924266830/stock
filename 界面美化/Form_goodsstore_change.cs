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
    public partial class Form_goodsstore_change : Form
    {
        public Form_goodsstore_change()
        {
            InitializeComponent();
        }
        string str = "Server=LAPTOP-17RT7OKE;Database=库存管理系统;Trusted_Connection=Yes;Connect Timeout=90";

        private void Form_goodsstore_change_Load(object sender, EventArgs e)
        {
            String sql = "select * from 商品库存 where 商品编号='"+Form_goodsstore.select.goods+"'";
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand mycom = new SqlCommand(sql,conn);
            SqlDataReader myread = mycom.ExecuteReader();
            if (myread.Read())
            {
                label2.Text = myread[0].ToString();
                label4.Text = myread[1].ToString();
                label6.Text = myread[6].ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String sql = "update 商品库存 set 库存数量='"+textBox1.Text.Trim()+"' where 商品编号='" + Form_goodsstore.select.goods + "'";
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand mycom = new SqlCommand(sql, conn);
            int i = mycom.ExecuteNonQuery();
            if (i > 0)
            {
                Form_goodsstore f = new Form_goodsstore();
                f = (Form_goodsstore)this.Owner;
                f.refresh();
            }
            //更新系统日志
            String logsql = "insert into 系统日志 values('" + Login.User.name + "','" + "调整了商品库存" + "','" + DateTime.Now + "')";
            SqlConnection log = new SqlConnection(str);
            log.Open();
            SqlCommand logcom = new SqlCommand(logsql, log);
            logcom.ExecuteNonQuery();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
