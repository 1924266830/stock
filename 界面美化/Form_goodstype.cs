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
    public partial class Form_goodstype : Form
    {
        public Form_goodstype()

        {
            InitializeComponent();
        }
        string str = "Server=LAPTOP-17RT7OKE;Database=库存管理系统;Trusted_Connection=Yes;Connect Timeout=90";
        String select = null;
        TreeNode root = new TreeNode("所有商品");
        public void refresh(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            for (int i = 0; i < root.Nodes.Count; i++)
            {
                dataGridView1.Rows.Add(root.Nodes[i].Text, root.Text);
            }
        }
        public Boolean Iswrite(object sender, EventArgs e)
        {
            for (int i = 0; i < root.Nodes.Count; i++)
            {
                if (root.Nodes[i].Text == "请输入新的类别名称")
                {
                    MessageBox.Show("请先输入新的类别名称，再在进行操作");
                    return false;
                }
                    
            }
            return true;
        }

        private void Form_customertype_Load(object sender, EventArgs e)
        {
            
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
            treeView1.ExpandAll();
            refresh(sender,e);
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Iswrite(sender, e))
            {
                root.Nodes.Add("请输入新的类别名称");
                treeView1.LabelEdit = true;
                treeView1.ExpandAll();
                root.Nodes[(root.Nodes.Count-1)].BeginEdit();
                refresh(sender, e);
            }
        }

        private void treeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            refresh(sender,e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
                for (int i = 0; i < root.Nodes.Count; i++)
                {
                    if (select == root.Nodes[i].Text)
                    {
                        root.Nodes[i].Remove();
                    }
                }
                refresh(sender, e);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            select = e.Node.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
                for (int i = 0; i < root.Nodes.Count; i++)
                {
                    if (select == root.Nodes[i].Text)
                    {
                        treeView1.LabelEdit = true;
                        root.Nodes[i].BeginEdit();
                    }

                }
                refresh(sender, e);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
              refresh(sender, e);
              String sql = "delete from 商品种类";
              SqlConnection conn = new SqlConnection(str);
              conn.Open();
              SqlCommand mycom = new SqlCommand(sql, conn);
              mycom.ExecuteNonQuery();
              conn.Close();
              String sql1 = "insert into 商品种类 values('";
              for (int i = 0; i < dataGridView1.RowCount-1; i++)
              {
                if (i > 0)
                    sql1 += ",('";
                 sql1 += dataGridView1.Rows[i].Cells[0].Value.ToString() + "','所有商品')";  
              }
              SqlConnection conn1 = new SqlConnection(str);
              conn1.Open();
              SqlCommand mycom1 = new SqlCommand(sql1, conn1);
              mycom1.ExecuteNonQuery();
              conn1.Close();
              Form6_goods fs;
              fs =(Form6_goods)this.Owner;
              fs.refresh();
            //更新系统日志
            String logsql = "insert into 系统日志 values('" + Login.User.name + "','" + "对商品种类进行操作" + "','" + DateTime.Now + "')";
            SqlConnection log = new SqlConnection(str);
            log.Open();
            SqlCommand logcom = new SqlCommand(logsql, log);
            logcom.ExecuteNonQuery();
            this.Close();
        }
        private void treeView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            refresh(sender, e);
        }
        private void treeView1_MouseClick(object sender, MouseEventArgs e)
        {
            dataGridView1.Rows.Clear();
            for (int i = 0; i < root.Nodes.Count; i++)
            {
                dataGridView1.Rows.Add(root.Nodes[i].Text, root.Text);
            }
            refresh(sender, e);
            dataGridView1.Focus();
            treeView1.LabelEdit = false;
        }
    }
}
