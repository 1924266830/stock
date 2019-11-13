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
    public partial class Form_goodsstore : Form
    {
        public Form_goodsstore()
        {
            InitializeComponent();
        }
        string str = "Server=LAPTOP-17RT7OKE;Database=库存管理系统;Trusted_Connection=Yes;Connect Timeout=90";

        public class select {
            public static String goods = null;
        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form_goodsstore_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form_menu fm = new Form_menu();
            fm = (Form_menu)this.Owner;
            fm.Deletetabpage("商品库存");
        }
        public void refresh()
        {
            //商品资料的生成
            SqlConnection conn1 = new SqlConnection(str);
            conn1.Open();
            string sql1 = "select * from 商品库存";
            DataSet dataset1 = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(sql1, conn1);
            adapter.Fill(dataset1, "商品库存");
            dataGridView1.DataSource = dataset1.Tables["商品库存"];
            conn1.Close();
        }
        private void Form_goodsstore_Load(object sender, EventArgs e)
        {
            //树的生成
            TreeNode root = new TreeNode("所有商品");
            String sql = "select * from 商品种类 ";
            DataSet dataset = new DataSet();
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand mycom = new SqlCommand(sql, conn);
            SqlDataReader myread = mycom.ExecuteReader();
            while (myread.Read())
            {
                root.Nodes.Add(myread[0].ToString());
            }
            treeView1.Nodes.Add(root);
            conn.Close();
            treeView1.ExpandAll();
            //商品资料的生成
            SqlConnection conn1 = new SqlConnection(str);
            conn1.Open();
            string sql1 = "select * from 商品库存";
            DataSet dataset1 = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(sql1, conn1);
            adapter.Fill(dataset1, "商品库存");
            dataGridView1.DataSource = dataset1.Tables["商品库存"];
            conn1.Close();
        }

        private void treeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            String sql = "select 商品库存.* from 商品资料,商品库存 where 商品资料.类别='" + treeView1.SelectedNode.Text + "' and 商品资料.商品编号=商品库存.商品编号";
            if (treeView1.SelectedNode.Text == "所有商品")
            {
                sql = "select * from 商品库存";
            }
            DataSet dataset = new DataSet();
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            DataSet dataset1 = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            adapter.Fill(dataset, "商品库存");
            dataGridView1.DataSource = dataset.Tables["商品库存"];
            conn.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            select.goods = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            Form_goodsstore_change f = new Form_goodsstore_change();
            f.Owner = this;
            f.ShowDialog();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            SqlConnection conn1 = new SqlConnection(str);
            conn1.Open();
            string sql1 = "select * from 商品库存 order by 最后销售时间";
            DataSet dataset1 = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(sql1, conn1);
            adapter.Fill(dataset1, "商品库存");
            dataGridView1.DataSource = dataset1.Tables["商品库存"];
            conn1.Close();
        }
    }
}
