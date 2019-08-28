#region Using directives

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Celeste_AOEO_Controls.Helpers;
using Celeste_AOEO_Controls.MsgBox;
using Celeste_Public_Api.WebSocket_Api.WebSocket.CommandInfo.Member;

#endregion

namespace Celeste_Launcher_Gui.Forms
{
    public partial class FriendsForm : Form
    {
        public FriendsForm()
        {
            InitializeComponent();

            SkinHelperFonts.SetFont(Controls);
        }

        private async void RefreshBtn_Click(object sender, EventArgs e)
        {
            await LoadFriendlist();
        }

        private async Task LoadFriendlist()
        {
            listView1.Enabled = false;
            customBtn1.Enabled = false;

            try
            {
                var response = await LegacyBootstrapper.WebSocketApi.DoGetFriends();

                if (!response.Result)
                    throw new Exception(response.Message);

                var friends = response.Friends.Friends;

                //
                listView1.Items.Clear();
                listView1.Items.AddRange(friends.Where(friend => friend.IsConnected).Select(friend =>
                    {
                        var ret = new ListViewItem(friend.ProfileName)
                        {
                            Group = friend.IsConnected ? listView1.Groups[0] : listView1.Groups[1],
                            Tag = friend
                        };
                        if (string.IsNullOrWhiteSpace(friend.RichPresence))
                            return ret;

                        foreach (var str in friend.RichPresence.Split(
                            new[] {"\r\n", "\r", "\n"},
                            StringSplitOptions.RemoveEmptyEntries
                        ))
                            ret.SubItems.Add(str);
                        return ret;
                    }
                ).ToArray());
                listView1.Groups[0].Header = $@"Online ({listView1.Groups[0].Items.Count} / {friends.Length})";
                listView1.Groups[1].Header = $@"Offline ({listView1.Groups[1].Items.Count} / {friends.Length})";

                //
                listView3.Items.Clear();
                listView3.Items.AddRange(friends.Where(friend => !friend.IsConnected).Select(friend =>
                    {
                        var ret = new ListViewItem(friend.ProfileName)
                        {
                            Group = friend.IsConnected ? listView3.Groups[0] : listView3.Groups[1],
                            Tag = friend
                        };
                        if (string.IsNullOrWhiteSpace(friend.RichPresence))
                            return ret;

                        foreach (var str in friend.RichPresence.Split(
                            new[] {"\r\n", "\r", "\n"},
                            StringSplitOptions.RemoveEmptyEntries
                        ))
                            ret.SubItems.Add(str);
                        return ret;
                    }
                ).ToArray());
                listView3.Groups[0].Header = $@"Online ({listView1.Groups[0].Items.Count} / {friends.Length})";
                listView3.Groups[1].Header = $@"Offline ({listView1.Groups[1].Items.Count} / {friends.Length})";
            }
            catch (Exception ex)
            {
                MsgBox.ShowMessage($@"Error: {ex.Message}", @"Celeste Fan Project",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            listView1.Enabled = true;
            customBtn1.Enabled = true;
        }

        private void PictureBoxButtonCustom1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FriendsForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (DwmApi.DwmIsCompositionEnabled())
                    DwmApi.DwmExtendFrameIntoClientArea(Handle, new DwmApi.MARGINS(12, 12, 12, 12));
            }
            catch (Exception)
            {
                //
            }
        }

        private async void FriendsForm_Shown(object sender, EventArgs e)
        {
            await LoadFriendlist();
            await LoadFriendRequests();
        }

        private async void RemoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count < 1)
                return;

            await RemFriends(((FriendJson) listView1.SelectedItems[0].Tag).Xuid);

            await LoadFriendlist();
        }

        private async void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (listView3.SelectedItems.Count < 1)
                return;

            await RemFriends(((FriendJson) listView3.SelectedItems[0].Tag).Xuid);

            await LoadFriendlist();
        }

        private static async Task RemFriends(long xuid)
        {
            using (var form =
                new MsgBoxYesNo(
                    @"Are you sure you want to remove this friends ? Click ""Yes"" to continue, or ""No"" to cancel.")
            )
            {
                var dr = form.ShowDialog();
                if (dr != DialogResult.OK)
                    return;
            }

            try
            {
                var response =
                    await LegacyBootstrapper.WebSocketApi.DoRemoveFriend(xuid);

                if (!response.Result)
                    throw new Exception(response.Message);

                MsgBox.ShowMessage(@"Friend removed with success", @"Celeste Fan Project",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MsgBox.ShowMessage($@"Error: {ex.Message}", @"Celeste Fan Project",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void CustomBtn1_Click(object sender, EventArgs e)
        {
            customBtn1.Enabled = false;
            tb_AddFriend.Enabled = false;
            try
            {
                var response =
                    await LegacyBootstrapper.WebSocketApi.DoAddFriend(tb_AddFriend.Text);

                if (!response.Result)
                    throw new Exception(response.Message);

                await LoadFriendRequests();

                MsgBox.ShowMessage(@"Friend request send with success", @"Celeste Fan Project",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MsgBox.ShowMessage($@"Error: {ex.Message}", @"Celeste Fan Project",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            customBtn1.Enabled = true;
            tb_AddFriend.Enabled = true;
        }

        private async void RemoveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count < 1)
                return;

            await RemFriends(((FriendJson) listView2.SelectedItems[0].Tag).Xuid);
            await LoadFriendRequests();
        }

        private async void ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count < 1)
                return;

            try
            {
                var response =
                    await LegacyBootstrapper.WebSocketApi.DoConfirmFriend(((FriendJson) listView2.SelectedItems[0].Tag).Xuid);

                if (!response.Result)
                    throw new Exception(response.Message);

                await LoadFriendlist();

                await LoadFriendRequests();

                MsgBox.ShowMessage(@"Friend add with success", @"Celeste Fan Project",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MsgBox.ShowMessage($@"Error: {ex.Message}", @"Celeste Fan Project",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (listView4.SelectedItems.Count < 1)
                return;

            await RemFriends(((FriendJson) listView4.SelectedItems[0].Tag).Xuid);
            await LoadFriendRequests();
        }

        private async void PictureBoxButtonCustom3_Click(object sender, EventArgs e)
        {
            await LoadFriendRequests();
        }

        private async Task LoadFriendRequests()
        {
            listView2.Enabled = false;
            listView4.Enabled = false;
            pictureBoxButtonCustom3.Enabled = false;

            try
            {
                var responsePendingFriends = await LegacyBootstrapper.WebSocketApi.DoGetPendingFriends();

                if (!responsePendingFriends.Result)
                    throw new Exception(responsePendingFriends.Message);

                //
                listView2.Items.Clear();
                listView2.Items.AddRange(responsePendingFriends.PendingFriendsInvite.Friends
                    .Select(friend => new ListViewItem(friend.ProfileName)
                    {
                        Group = listView2.Groups[0],
                        Tag = friend
                    }).ToArray());

                listView2.Groups[0].Header = $@"Received Invite ({listView1.Groups[0].Items.Count})";
                listView2.Groups[1].Header = $@"Invite Sended ({listView1.Groups[1].Items.Count})";

                //
                listView4.Items.Clear();
                listView4.Items.AddRange(responsePendingFriends.PendingFriendsRequest.Friends
                    .Select(friend => new ListViewItem(friend.ProfileName)
                    {
                        Group = listView2.Groups[1],
                        Tag = friend
                    }).ToArray());

                listView4.Groups[0].Header = $@"Received Invite ({listView1.Groups[0].Items.Count})";
                listView4.Groups[1].Header = $@"Invite Sended ({listView1.Groups[1].Items.Count})";
            }
            catch (Exception ex)
            {
                MsgBox.ShowMessage($@"Error: {ex.Message}", @"Celeste Fan Project",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            listView2.Enabled = true;
            listView4.Enabled = true;
            pictureBoxButtonCustom3.Enabled = true;
        }
    }
}