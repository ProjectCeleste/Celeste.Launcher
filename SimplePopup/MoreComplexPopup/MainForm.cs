using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PopupControl;

namespace MoreComplexPopup
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            gridToolTip = new Popup(gridCustomToolTip = new GridToolTip());
            gridToolTip.AutoClose = false;
            gridToolTip.FocusOnOpen = false;
            gridToolTip.ShowingAnimation = gridToolTip.HidingAnimation = PopupAnimations.LeftToRight | PopupAnimations.Slide;

            toolTip = new Popup(customToolTip = new CustomToolTip());
            toolTip.AutoClose = false;
            toolTip.FocusOnOpen = false;
            toolTip.ShowingAnimation = toolTip.HidingAnimation = PopupAnimations.Blend;

            complex = new Popup(complexPopup = new ComplexPopup());
            complex.Resizable = true;
            complexPopup.ButtonMore.Click += (_sender, _e) =>
            {
                ComplexPopup cp = new ComplexPopup();
                cp.ButtonMore.Click +=
                    (__sender, __e) => new Popup(new ComplexPopup()).Show(__sender as Button);
                new Popup(cp).Show(_sender as Button);
            };

            dataGridView.Columns.Add("col1", "col1");
            dataGridView.Columns.Add("col2", "col2");
            dataGridView.Columns.Add("col3", "col3");

            dataGridView.Rows.Add("1", "A", "!");
            dataGridView.Rows.Add("2", "B", "@");
            dataGridView.Rows.Add("3", "C", "#");
            dataGridView.CellMouseEnter += new DataGridViewCellEventHandler(dataGridView_CellMouseEnter);
            dataGridView.CellMouseLeave += new DataGridViewCellEventHandler(dataGridView_CellMouseLeave);
            dataGridView.CellMouseMove += new DataGridViewCellMouseEventHandler(dataGridView_CellMouseMove);

            // Simple treeview attached to the PopupComboBox
            treeView = new TreeView()
            {
                FullRowSelect = true,
                HotTracking = true,
                ShowLines = false,
                ShowNodeToolTips = false,
                ShowPlusMinus = false,
                ShowRootLines = false
            };
            treeView.Nodes.AddRange(new[] { new TreeNode("Node 1"), new TreeNode("Node 2"), new TreeNode("Node 3") });
            treeView.Nodes[1].Nodes.Add("Node 2.5");
            treeView.ExpandAll();
            treeView.BeforeCollapse += treeView_BeforeCollapse;
            treeView.NodeMouseClick += treeView_NodeMouseClick;
            popupComboBox.DropDownControl = treeView;

            // Fill the PopupComboBox with nodes from the tree view
            Action<TreeNodeCollection> fillCombo = null;
            fillCombo = delegate(TreeNodeCollection nodes)
            {
                if (nodes == null) return;
                foreach (TreeNode node in nodes)
                {
                    popupComboBox.Items.Add(node.Text);
                    fillCombo(node.Nodes);
                }
            };
            fillCombo(treeView.Nodes);
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        delegate TResult Func<TArg, TResult>(TArg arg);

        Popup toolTip;
        CustomToolTip customToolTip;
        Popup gridToolTip;
        GridToolTip gridCustomToolTip;
        Popup complex;
        ComplexPopup complexPopup;
        TreeView treeView;

        void dataGridView_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            gridCustomToolTip.Controls[0].Text = "Mouse pointer location: " + e.Location.ToString();
        }

        void dataGridView_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            gridToolTip.Close();
        }

        void dataGridView_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            gridCustomToolTip.Controls[1].Text = string.Format("Column: {0}, Row: {1}", e.ColumnIndex, e.RowIndex);
            if (!gridToolTip.Visible)
            {
                Rectangle rect = dataGridView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                gridToolTip.Show(dataGridView, rect);
            }
        }

        private void buttonDropDown_Click(object sender, EventArgs e)
        {
            complex.Show(sender as Button);
        }

        private void label1_MouseHover(object sender, EventArgs e)
        {
            toolTip.Show(label1);
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Close();
        }

        private static void treeView_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        { // we don't allow to collapse the node in the treeview
            e.Cancel = true;
        }

        private void popupComboBox_DropDown(object sender, EventArgs e)
        { // select the proper node in the treeview
            if (popupComboBox.SelectedItem == null)
            {
                treeView.SelectedNode = null;
                return;
            }
            Func<TreeNodeCollection, TreeNode> findNode = null;
            findNode = delegate(TreeNodeCollection nodes)
            {
                if (nodes == null)
                    return null;
                foreach (TreeNode node in nodes)
                {
                    if (node.Text == popupComboBox.SelectedItem.ToString())
                        return node;
                    TreeNode n = findNode(node.Nodes);
                    if (n != null)
                        return n;
                }
                return null;
            };
            treeView.SelectedNode = findNode(treeView.Nodes);
        }

        private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        { // Select the proper item in the PopupComboBox
            popupComboBox.SelectedItem = e.Node.Text;
            popupComboBox.HideDropDown();
        }
    }
}