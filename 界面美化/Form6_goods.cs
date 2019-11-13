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
    public partial class Form6_goods : Form
    {
        public Form6_goods()
        {
            InitializeComponent();
        }

        string str = "Server=LAPTOP-17RT7OKE;Database=库存管理系统;Trusted_Connection=Yes;Connect Timeout=90";
        public  void refresh()
        {
            //树的生成
            treeView1.Nodes[0].Remove();
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
            string sql1 = "select * from 商品资料";
            DataSet dataset1 = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(sql1, conn1);
            adapter.Fill(dataset1, "商品资料");
            dataGridView1.DataSource = dataset1.Tables["商品资料"];
            conn1.Close();
        }
        public class select {
            public static String cname;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form_goodstype f = new Form_goodstype();
            f.Owner = this;
            f.ShowDialog();
        }

        private void Form4_customer_Load(object sender, EventArgs e)
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
            string sql1 = "select * from 商品资料";
            DataSet dataset1 = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(sql1, conn1);
            adapter.Fill(dataset1, "商品资料");
            dataGridView1.DataSource = dataset1.Tables["商品资料"];
            conn1.Close();
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == -1 && e.RowIndex > -1)
            {

                StringFormat sf = new StringFormat(StringFormat.GenericDefault);

                sf.Alignment = StringAlignment.Center;

                e.PaintBackground(e.CellBounds, true);

                e.Graphics.DrawString((e.RowIndex + 1).ToString(), this.Font,

                    new SolidBrush(Color.Black), e.CellBounds, sf);

                e.Handled = true;
            }
        }
        //搜索按钮
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() != String.Empty)
            {
                String sql = "select * from 商品资料 where 商品名称 like '" + textBox1.Text.Trim() + "%' or 拼音码 like '" + textBox1.Text.Trim() + "%' or 商品编号 like '"+ textBox1.Text.Trim() + "%'";
                SqlConnection conn = new SqlConnection(str);
                conn.Open();
                SqlCommand mycom = new SqlCommand(sql,conn);
                mycom.ExecuteNonQuery();
                DataSet dataset = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(dataset, "商品资料");
                dataGridView1.DataSource = dataset.Tables["商品资料"];
            }
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            String sql = "select * from 商品资料 where 类别='" + treeView1.SelectedNode.Text + "'";
            if (treeView1.SelectedNode.Text == "所有商品")
            {
                sql = "select * from 商品资料";
            }
            DataSet dataset = new DataSet();
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            DataSet dataset1 = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            adapter.Fill(dataset, "商品资料");
            dataGridView1.DataSource = dataset.Tables["商品资料"];
            conn.Close();
        }
        //添加
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Form_addgoods f = new Form_addgoods();
            f.Owner = this;
            f.ShowDialog();
        }

        private void Form4_customer_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form_menu fm = new Form_menu();
            fm = (Form_menu)this.Owner;
            fm.Deletetabpage("商品资料");
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Form_change.owner.text = "商品名称：";
            Form4_customer.select.cname = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            Form_change f = new Form_change();
            f.Owner = this;
            f.ShowDialog();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            String sql = "delete  from 商品资料 where 商品编号='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'" ;
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand mycom = new SqlCommand(sql, conn);
            mycom.ExecuteNonQuery();        
            conn.Close();
            refresh();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
