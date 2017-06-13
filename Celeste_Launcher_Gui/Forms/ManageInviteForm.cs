#region Using directives

using System;
using System.Windows.Forms;

#endregion

namespace Celeste_Launcher_Gui.Forms
{
    public partial class ManageInviteForm : Form
    {
        public ManageInviteForm()
        {
            InitializeComponent();
            foreach (var invite in Program.RemoteUser.Invites)
                if (!invite.Used)
                {
                    var lvi = new ListViewItem
                    {
                        Tag = invite.Code,
                        Text = invite.Id.ToString()
                    };

                    lv_AvInvite.Items.Add(lvi);
                }
                else
                {
                    var lvi = new ListViewItem
                    {
                        Text = invite.Id.ToString()
                    };
                    lvi.SubItems.Add(invite.UsedByUser);

                    lv_UsedInvite.Items.Add(lvi);
                }
        }

        private void lv_AvInvite_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lv_AvInvite.SelectedItems.Count == 0)
                return;

            textBox1.Text = (string) lv_AvInvite.SelectedItems[0].Tag;
        }
    }
}