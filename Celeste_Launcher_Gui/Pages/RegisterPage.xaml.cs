using Celeste_Launcher_Gui.Windows;
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
using System.Windows.Shapes;

namespace Celeste_Launcher_Gui.Pages
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void OnResendVerificationKey(object sender, RoutedEventArgs args)
        {
            OnVerifyEmail(sender, args);
        }

        private async void OnVerifyEmail(object sender, RoutedEventArgs args)
        {
            VerifyEmailBtn.IsEnabled = false;
            ResentKeyBtn.IsEnabled = false;

            try
            {
                var response = await LegacyBootstrapper.WebSocketApi.DoValidMail(EmailField.LabelContent);

                if (response.Result)
                {
                    GenericMessageDialog.Show($@"{response.Message}", DialogIcon.Warning);
                }
                else
                {
                    GenericMessageDialog.Show($@"Error: {response.Message}", DialogIcon.Error);
                }
            }
            catch (Exception ex)
            {
                GenericMessageDialog.Show($"Error: {ex.Message}", DialogIcon.Error);
            }

            VerifyEmailBtn.IsEnabled = true;
            ResentKeyBtn.IsEnabled = true;
        }

        private async void OnRegister(object sender, RoutedEventArgs args)
        {
            if (ConfirmPasswordField.PasswordInputBox.Password != PasswordField.PasswordInputBox.Password)
            {
                GenericMessageDialog.Show(@"The confirmed password must match the first password", DialogIcon.Error);
                return;
            }

            RegisterBtn.IsEnabled = false;

            try
            {
                var response = await LegacyBootstrapper.WebSocketApi.DoRegister(EmailField.LabelContent, VerifyKeyField.LabelContent, 
                    UsernameField.LabelContent, PasswordField.PasswordInputBox.Password);

                if (response.Result)
                {
                    GenericMessageDialog.Show($@"{response.Message}", DialogIcon.Error);

                    NavigationService.Navigate(new Uri("Pages/MainMenuPage.xaml", UriKind.Relative));
                }
                else
                {
                    GenericMessageDialog.Show($@"Error: {response.Message}", DialogIcon.Error);
                }
            }
            catch (Exception ex)
            {
                GenericMessageDialog.Show($"Error: {ex.Message}", DialogIcon.Error);
            }

            RegisterBtn.IsEnabled = true;
        }

        private void OnAbort(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
