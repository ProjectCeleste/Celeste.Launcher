using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Celeste_Launcher_Gui.UserControls
{
    /// <summary>
    /// Interaction logic for UserInfoWidget.xaml
    /// </summary>
    public partial class UserInfoWidget : UserControl
    {
        private static readonly string[] ProfilePicture = { "Coin-Agvald.png", "Coin-Bahram.png", "Coin-Rohham.png", "Coin-Tahmineh.png" };
        public string PlayerIcon
        {
            get { return (string)GetValue(PlayerIconProperty); }
            set { SetValue(PlayerIconProperty, value); }
        }

        public static readonly DependencyProperty PlayerIconProperty =
            DependencyProperty.Register("PlayerIcon", typeof(string), typeof(UserInfoWidget), new PropertyMetadata("pack://application:,,,/Celeste Launcher;component/Resources/ProfilePics/Coin-Bahram.png"));

        public string Username
        {
            get { return (string)GetValue(UsernameProperty); }
            set { SetValue(UsernameProperty, value); }
        }

        public static readonly DependencyProperty UsernameProperty =
            DependencyProperty.Register("Username", typeof(string), typeof(UserInfoWidget), new PropertyMetadata("martinmine"));

        public string Rank
        {
            get { return (string)GetValue(RankProperty); }
            set { SetValue(RankProperty, value); }
        }

        public static readonly DependencyProperty RankProperty =
            DependencyProperty.Register("Rank", typeof(string), typeof(UserInfoWidget), new PropertyMetadata("Pleb"));

        public UserInfoWidget()
        {
            SetRandomProfilePicture();
            InitializeComponent();
            LayoutRoot.DataContext = this;
        }

        private void SetRandomProfilePicture()
        {
            var rnd = new Random();
            var imgIndex = rnd.Next(ProfilePicture.Length);
            PlayerIcon = "pack://application:,,,/Celeste Launcher;component/Resources/ProfilePics/" + ProfilePicture[imgIndex];
        }
    }
}
