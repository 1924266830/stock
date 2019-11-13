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
using DevComponents.DotNetBar;
using static 界面美化.Form_menu;

namespace 界面美化
{
    public partial class Form_menu : Office2007Form

    {
        public Form_menu()
        {
            InitializeComponent();
        }
        string str = "Server=LAPTOP-17RT7OKE;Database=库存管理系统;Trusted_Connection=Yes;Connect Timeout=90";
        private void 蓝色经典ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            styleManager1.ManagerStyle = eStyle.Office2010Blue;
        }

        private void 银色金属ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            styleManager1.ManagerStyle = eStyle.Office2010Silver;
        }

        private void 灰色怀旧ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            styleManager1.ManagerStyle = eStyle.Office2010Black;
        }

        private void 默认皮肤ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            styleManager1.ManagerStyle = eStyle.Office2007VistaGlass;
        }
        //检测是否已经创建该页面
        public bool tabControlCheckHave(System.Windows.Forms.TabControl tab, String tabName)
        {
            for (int i = 0; i < tab.TabCount; i++)
            {
                if (tab.TabPages[i].Text == tabName)
                {
                    tab.SelectedIndex = i;
                    return true;
                }
            }
            return false;
        }
        public void Deletetabpage(String tabName)
        {
            for (int i = 0; i < tabControl1.TabCount; i++)
            {
                if (tabControl1.TabPages[i].Text == tabName)
                {
                    tabControl1.TabPages.RemoveAt(i);
                }
            }
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (!tabControlCheckHave(tabControl1, "商品资料"))
            {
                TabPage tab = new TabPage();
                tab.Name = "tabgoods";
                tab.Text = "商品资料";
                Form6_goods f6 = new Form6_goods();
                f6.Owner = this;
                f6.TopLevel = false;     //设置为非顶级控件
                tab.Controls.Add(f6);
                tabControl1.TabPages.Add(tab);
                f6.Show();
                tabControl1.SelectTab("tabgoods");
            }
            else
                tabControl1.TabPages["tabgoods"].Show();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(str);
            String sql = "select count(*) from 待审核销售订单";
            conn.Open();
            SqlCommand mycom1 = new SqlCommand(sql,conn);
            SqlDataReader myread1 = mycom1.ExecuteReader();
            if(myread1.Read())
                label3.Text = myread1[0].ToString();
            conn.Close();

            sql = "select count(*) from 客户信息表 where 收款日期 > '"+DateTime.Now+"'" ;
            conn.Open();
            SqlCommand mycom2 = new SqlCommand(sql, conn);
            SqlDataReader myread2 = mycom2.ExecuteReader();
            if (myread2.Read())
                label5.Text = myread2[0].ToString();
            conn.Close();

            sql = "select count(*) from 待审核采购订单";
            conn.Open();
            SqlCommand mycom3 = new SqlCommand(sql, conn);
            SqlDataReader myread3 = mycom3.ExecuteReader();
            if (myread3.Read())
                label8.Text = myread3[0].ToString();
            conn.Close();

            sql = "select count(*) from 供应商信息表 where 收款日期 > '" + DateTime.Now + "'";
            conn.Open();
            SqlCommand mycom4 = new SqlCommand(sql, conn);
            SqlDataReader myread4 = mycom4.ExecuteReader();
            if (myread4.Read())
                label16.Text = myread4[0].ToString();
            conn.Close();

            sql = "select count(*) from 商品资料,商品库存 where 商品资料.商品编号=商品库存.商品编号 and (商品库存.库存数量< 商品资料.库存下限 or 商品库存.库存数量> 商品资料.库存上限)";
            conn.Open();
            SqlCommand mycom5 = new SqlCommand(sql, conn);
            SqlDataReader myread5 = mycom5.ExecuteReader();
            if (myread5.Read())
                label13.Text = myread5[0].ToString();
            conn.Close();
            switch (Login.User.power)
            {
                case  "管理员":
                    break;
                case "财务人员":
                    基本信息ToolStripMenuItem.Enabled = false;
                    出库管理ToolStripMenuItem.Enabled = false;
                    入库管理ToolStripMenuItem.Enabled = false;
                    库存管理ToolStripMenuItem.Enabled = false;
                    系统管理ToolStripMenuItem.Enabled = false;
                    toolStripButton1.Enabled = false;
                    toolStripButton2.Enabled = false;
                    toolStripButton3.Enabled = false;
                    toolStripButton4.Enabled = false;
                    toolStripButton5.Enabled = false;
                    break;
                case "销售员":
                    入库管理ToolStripMenuItem.Enabled = false;
                    库存管理ToolStripMenuItem.Enabled = false;
                    系统管理ToolStripMenuItem.Enabled = false;
                    toolStripButton3.Enabled = false;
                    toolStripButton4.Enabled = false;
                    toolStripButton5.Enabled = false;
                    break;
                case "采购员":
                    出库管理ToolStripMenuItem.Enabled = false;
                    库存管理ToolStripMenuItem.Enabled = false;
                    系统管理ToolStripMenuItem.Enabled = false;
                    toolStripButton3.Enabled = false;
                    toolStripButton4.Enabled = false;
                    toolStripButton5.Enabled = false;
                    break;
            }
        }
        //销售出库子窗体
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (!tabControlCheckHave(tabControl1, "销售出库"))
            {
                TabPage tab = new TabPage();
                tab.Name = "tabexport";
                tab.Text = "销售出库";
                Form2_export f2 = new Form2_export();
                f2.Owner = this;
                f2.TopLevel = false;     //设置为非顶级控件
                tab.Controls.Add(f2);
                tabControl1.TabPages.Add(tab);
                f2.Show();
                tabControl1.SelectTab("tabexport");
            }
            else
                tabControl1.TabPages["tabexport"].Show();
        }
        //采购进货子窗体
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (!tabControlCheckHave(tabControl1, "采购进货"))
            {
                TabPage tab = new TabPage();
                tab.Name = "tabimport";
                tab.Text = "采购进货";
                Form3_import f3 = new Form3_import();
                f3.Owner = this;
                f3.TopLevel = false;     //设置为非顶级控件
                tabControl1.TabPages.Add(tab);
                tab.Controls.Add(f3);
                f3.Show();
                tabControl1.SelectTab("tabimport");
            }
            else
                tabControl1.TabPages["tabimport"].Show();
        }
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (!tabControlCheckHave(tabControl1, "客户资料"))
            {
                TabPage tab = new TabPage();
                tab.Name = "tabcustomer";
                tab.Text = "客户资料";
                Form4_customer fc = new Form4_customer();
                fc.Owner = this;
                fc.TopLevel = false;     //设置为非顶级控件
                tab.Controls.Add(fc);
                tabControl1.TabPages.Add(tab);
                fc.Show();
                tabControl1.SelectTab("tabcustomer");
            }
            else
                tabControl1.TabPages["tabcustomer"].Show();

        }

        private void Form_menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (!tabControlCheckHave(tabControl1, "供应商资料"))
            {
                TabPage tab = new TabPage();
                tab.Name = "tabsupplier";
                tab.Text = "供应商资料";
                Form5_supplier fs = new Form5_supplier();
                fs.Owner = this;
                fs.TopLevel = false;     //设置为非顶级控件
                tab.Controls.Add(fs);
                tabControl1.TabPages.Add(tab);
                fs.Show();
                tabControl1.SelectTab("tabsupplier");
            }
            else
                tabControl1.TabPages["tabsupplier"].Show();
        }

        private void 销售订单审核ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!tabControlCheckHave(tabControl1, "销售订单审核"))
            {
                TabPage tab = new TabPage();
                tab.Name = "tabexauditing";
                tab.Text = "销售订单审核";
                Form_ex_auditing f = new Form_ex_auditing();
                f.Owner = this;
                f.TopLevel = false;     //设置为非顶级控件
                tab.Controls.Add(f);
                tabControl1.TabPages.Add(tab);
                f.Show();
                tabControl1.SelectTab("tabexauditing");
            }
            else
                tabControl1.TabPages["tabexauditing"].Show();
        }
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (!tabControlCheckHave(tabControl1, "商品库存"))
            {
                TabPage tab = new TabPage();
                tab.Name = "tabgoodsstore";
                tab.Text = "商品库存";
                Form_goodsstore f = new Form_goodsstore();
                f.Owner = this;
                f.TopLevel = false;     //设置为非顶级控件
                tab.Controls.Add(f);
                tabControl1.TabPages.Add(tab);
                f.Show();
                tabControl1.SelectTab("tabgoodsstore");
            }
            else
                tabControl1.TabPages["tabgoodsstore"].Show();
        }
        private void 蓝色经典ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            styleManager1.ManagerStyle = eStyle.Office2010Blue;
        }

        private void 灰色简约ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            styleManager1.ManagerStyle = eStyle.Office2010Black;
        }

        private void 银色金属ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            styleManager1.ManagerStyle = eStyle.Office2010Silver;
        }

        private void 默认皮肤ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            styleManager1.ManagerStyle = eStyle.Office2007VistaGlass;
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            Form_password fp = new Form_password();
            fp.ShowDialog();
        }

        private void 采购订单审核ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!tabControlCheckHave(tabControl1, "采购订单审核"))
            {
                TabPage tab = new TabPage();
                tab.Name = "tabimauditing";
                tab.Text = "采购订单审核";
                Form_im_auditing f = new Form_im_auditing();
                f.Owner = this;
                f.TopLevel = false;     //设置为非顶级控件
                tab.Controls.Add(f);
                tabControl1.TabPages.Add(tab);
                f.Show();
                tabControl1.SelectTab("tabimauditing");
            }
            else
                tabControl1.TabPages["tabimauditing"].Show();
        }

        private void 操作员设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!tabControlCheckHave(tabControl1, "操作员设置"))
            {
                TabPage tab = new TabPage();
                tab.Name = "tabuser";
                tab.Text = "操作员设置";
                Form7_user f = new Form7_user();
                f.Owner = this;
                f.TopLevel = false;     //设置为非顶级控件
                tab.Controls.Add(f);
                tabControl1.TabPages.Add(tab);
                f.Show();
                tabControl1.SelectTab("tabuser");
            }
            else
                tabControl1.TabPages["tabuser"].Show();
        }

        private void 采购报表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!tabControlCheckHave(tabControl1, "采购报表"))
            {
                TabPage tab = new TabPage();
                tab.Name = "tabimreport";
                tab.Text = "采购报表";
                Form_im_report f = new Form_im_report();
                f.Owner = this;
                f.TopLevel = false;     //设置为非顶级控件
                tab.Controls.Add(f);
                tabControl1.TabPages.Add(tab);
                f.Show();
                tabControl1.SelectTab("tabimreport");
                //更新系统日志
                String logsql = "insert into 系统日志 values('" + Login.User.name + "','" + "查看了采购报表" + "','" + DateTime.Now + "')";
                SqlConnection log = new SqlConnection(str);
                log.Open();
                SqlCommand logcom = new SqlCommand(logsql, log);
                logcom.ExecuteNonQuery();
            }
            else
                tabControl1.TabPages["tabexreport"].Show();
        }

        private void 销售报表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!tabControlCheckHave(tabControl1, "销售报表"))
            {
                TabPage tab = new TabPage();
                tab.Name = "tabexreport";
                tab.Text = "销售报表";
                Form_ex_report f = new Form_ex_report();
                f.Owner = this;
                f.TopLevel = false;     //设置为非顶级控件
                tab.Controls.Add(f);
                tabControl1.TabPages.Add(tab);
                f.Show();
                tabControl1.SelectTab("tabexreport");
                //更新系统日志
                String logsql = "insert into 系统日志 values('" + Login.User.name + "','" + "查看了销售报表" + "','" + DateTime.Now + "')";
                SqlConnection log = new SqlConnection(str);
                log.Open();
                SqlCommand logcom = new SqlCommand(logsql, log);
                logcom.ExecuteNonQuery();
            }
            else
                tabControl1.TabPages["tabexreport"].Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!tabControlCheckHave(tabControl1, "系统日志"))
            {
                TabPage tab = new TabPage();
                tab.Name = "tabsystemlog";
                tab.Text = "系统日志";
                Form8_systemlog f = new Form8_systemlog();
                f.Owner = this;
                f.TopLevel = false;     //设置为非顶级控件
                tab.Controls.Add(f);
                tabControl1.TabPages.Add(tab);
                f.Show();
                tabControl1.SelectTab("tabsystemlog");
                //更新系统日志
                String logsql = "insert into 系统日志 values('" + Login.User.name + "','" + "查看了系统日志" + "','" + DateTime.Now + "')";
                SqlConnection log = new SqlConnection(str);
                log.Open();
                SqlCommand logcom = new SqlCommand(logsql, log);
                logcom.ExecuteNonQuery();
            }
            else
                tabControl1.TabPages["tabsystemlog"].Show();
        }

        private void 客户类别设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_add.owner.text = "客户名称：";
            Form_add f = new Form_add();
            f.Owner = this;
            f.ShowDialog();
        }

        private void 供应商类别设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_add.owner.text = "供应商名称：";
            Form_add f = new Form_add();
            f.Owner = this;
            f.ShowDialog();
        }

        private void 商品类别设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_addgoods f = new Form_addgoods();
            f.Owner = this;
            f.ShowDialog();
        }

        private void 库存管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
