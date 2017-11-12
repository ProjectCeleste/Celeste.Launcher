using System;
using System.Windows.Forms;

namespace PopupTest
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        private void searchBox_Search(object sender, StringEventArgs e)
        {
            webBrowser.Navigate(e.String);
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            webBrowser.Navigate(addressTextBox.Text);
        }

        private void webBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            addressTextBox.Text = e.Url.ToString();
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = false;
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                goButton_Click(null, EventArgs.Empty);
            }
        }
    }
}