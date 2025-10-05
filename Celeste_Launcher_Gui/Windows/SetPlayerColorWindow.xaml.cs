using Celeste_Launcher_Gui.Helpers;
using Celeste_Launcher_Gui.Model;
using Celeste_Public_Api.Helpers;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Xml;
using System.Xml.Serialization;

namespace Celeste_Launcher_Gui.Windows
{
    /// <summary>
    /// Interaction logic for SetPlayerColorWindow.xaml
    /// </summary>
    public partial class SetPlayerColorWindow : Window
    {
        private PlayerColors _playerColors;
        private PlayerColors _defaults;

        public SetPlayerColorWindow()
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(LegacyBootstrapper.UserConfig.GameFilesPath))
            {
                MessageBox.Show(Properties.Resources.ColorPickerGamePathNotYetSet);
                Close();
            }

            if (!File.Exists(GetPathToPlayerColors()))
            {
                File.WriteAllText(GetPathToPlayerColors(), GetDefaultXml());
            }

            _playerColors = XmlUtils.DeserializeFromFile<PlayerColors>(GetPathToPlayerColors());
            _defaults = Defaults();

            Player1ColorPicker.SelectedColor = _playerColors.Players[1].IsUserSet ? RgbToLabel(_playerColors.Players[1]) : ColorPickerColors.Default;
            Player2ColorPicker.SelectedColor = _playerColors.Players[2].IsUserSet ? RgbToLabel(_playerColors.Players[2]) : ColorPickerColors.Default;
            Player3ColorPicker.SelectedColor = _playerColors.Players[3].IsUserSet ? RgbToLabel(_playerColors.Players[3]) : ColorPickerColors.Default;
            Player4ColorPicker.SelectedColor = _playerColors.Players[4].IsUserSet ? RgbToLabel(_playerColors.Players[4]) : ColorPickerColors.Default;
            Player5ColorPicker.SelectedColor = _playerColors.Players[5].IsUserSet ? RgbToLabel(_playerColors.Players[5]) : ColorPickerColors.Default;
            Player6ColorPicker.SelectedColor = _playerColors.Players[6].IsUserSet ? RgbToLabel(_playerColors.Players[6]) : ColorPickerColors.Default;
            Player7ColorPicker.SelectedColor = _playerColors.Players[7].IsUserSet ? RgbToLabel(_playerColors.Players[7]) : ColorPickerColors.Default;
            Player8ColorPicker.SelectedColor = _playerColors.Players[8].IsUserSet ? RgbToLabel(_playerColors.Players[8]) : ColorPickerColors.Default;
            FriendorfoeselfColorPicker.SelectedColor = _playerColors.FriendOrFoeSelf.IsUserSet ? RgbToLabel(_playerColors.FriendOrFoeSelf) : ColorPickerColors.Default;
            FriendorfoeallyColorPicker.SelectedColor = _playerColors.FriendOrFoeAlly.IsUserSet ? RgbToLabel(_playerColors.FriendOrFoeAlly) : ColorPickerColors.Default;
            FriendorfoeenemyColorPicker.SelectedColor = _playerColors.FriendOrFoeEnemy.IsUserSet ? RgbToLabel(_playerColors.FriendOrFoeEnemy) : ColorPickerColors.Default;

            UpdatePlayerColorPreview();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BorderMoved(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        public void ConfirmBtnClick(object sender, RoutedEventArgs e)
        {
            _playerColors.Players[1] = Player1ColorPicker.SelectedColor == ColorPickerColors.Default ? _defaults.Players[1] : (Player)LabelToColor(_playerColors.Players[1], Player1ColorPicker.SelectedColor);
            _playerColors.Players[2] = Player2ColorPicker.SelectedColor == ColorPickerColors.Default ? _defaults.Players[2] : (Player)LabelToColor(_playerColors.Players[2], Player2ColorPicker.SelectedColor);
            _playerColors.Players[3] = Player3ColorPicker.SelectedColor == ColorPickerColors.Default ? _defaults.Players[3] : (Player)LabelToColor(_playerColors.Players[3], Player3ColorPicker.SelectedColor);
            _playerColors.Players[4] = Player4ColorPicker.SelectedColor == ColorPickerColors.Default ? _defaults.Players[4] : (Player)LabelToColor(_playerColors.Players[4], Player4ColorPicker.SelectedColor);
            _playerColors.Players[5] = Player5ColorPicker.SelectedColor == ColorPickerColors.Default ? _defaults.Players[5] : (Player)LabelToColor(_playerColors.Players[5], Player5ColorPicker.SelectedColor);
            _playerColors.Players[6] = Player6ColorPicker.SelectedColor == ColorPickerColors.Default ? _defaults.Players[6] : (Player)LabelToColor(_playerColors.Players[6], Player6ColorPicker.SelectedColor);
            _playerColors.Players[7] = Player7ColorPicker.SelectedColor == ColorPickerColors.Default ? _defaults.Players[7] : (Player)LabelToColor(_playerColors.Players[7], Player7ColorPicker.SelectedColor);
            _playerColors.Players[8] = Player8ColorPicker.SelectedColor == ColorPickerColors.Default ? _defaults.Players[8] : (Player)LabelToColor(_playerColors.Players[8], Player8ColorPicker.SelectedColor);

            _playerColors.FriendOrFoeSelf = FriendorfoeselfColorPicker.SelectedColor == ColorPickerColors.Default ? _defaults.FriendOrFoeSelf : LabelToColor(_playerColors.FriendOrFoeSelf, FriendorfoeselfColorPicker.SelectedColor);
            _playerColors.FriendOrFoeAlly = FriendorfoeallyColorPicker.SelectedColor == ColorPickerColors.Default ? _defaults.FriendOrFoeAlly : LabelToColor(_playerColors.FriendOrFoeAlly, FriendorfoeallyColorPicker.SelectedColor);
            _playerColors.FriendOrFoeEnemy = FriendorfoeenemyColorPicker.SelectedColor == ColorPickerColors.Default ? _defaults.FriendOrFoeEnemy : LabelToColor(_playerColors.FriendOrFoeEnemy, FriendorfoeenemyColorPicker.SelectedColor);

            var pathToPlayerColors = GetPathToPlayerColors();
            SerializeToXmlFile(_playerColors, pathToPlayerColors);
            Close();
        }

        private string GetPathToPlayerColors()
        => Path.Combine(LegacyBootstrapper.UserConfig.GameFilesPath, "Data", "playercolors.xml");

        private PlayerColors Defaults()
        {
            var xmlContent = GetDefaultXml();
            return XmlUtils.DeserializeFromString<PlayerColors>(xmlContent);
        }

        private string GetDefaultXml()
        {
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream("Celeste_Launcher_Gui.Resources.playercolors.xml");
            using var reader = new StreamReader(stream);
            var xmlContent = reader.ReadToEnd();
            return xmlContent;
        }

        private ColorSetting LabelToColor(ColorSetting selectedColor, string color)
        {
            if (color == ColorPickerColors.Blue)
            {
                selectedColor.Color1 = "2 146 239";
                selectedColor.Color2 = "55 55 255";
                selectedColor.Color3 = "111 127 178";
                selectedColor.Minimap = "0 0 255";
            }
            else if (color == ColorPickerColors.Red)
            {
                selectedColor.Color1 = "210 40 40";
                selectedColor.Color2 = "240 50 50";
                selectedColor.Color3 = "154 77 77";
                selectedColor.Minimap = "255 50 50";
            }
            else if (color == ColorPickerColors.Yellow)
            {
                selectedColor.Color1 = "255 255 0";
                selectedColor.Color2 = "225 225 40";
                selectedColor.Color3 = "181 169 90";
                selectedColor.Minimap = "255 255 0";
            }
            else if (color == ColorPickerColors.Purple)
            {
                selectedColor.Color1 = "150 15 250";
                selectedColor.Color2 = "160 25 255";
                selectedColor.Color3 = "116 86 126";
                selectedColor.Minimap = "148 0 197";
            }
            else if (color == ColorPickerColors.Green)
            {
                selectedColor.Color1 = "15 210 80";
                selectedColor.Color2 = "25 220 90";
                selectedColor.Color3 = "82 122 82";
                selectedColor.Minimap = "0 255 0";
            }
            else if (color == ColorPickerColors.Orange)
            {
                selectedColor.Color1 = "255 150 5";
                selectedColor.Color2 = "255 160 15";
                selectedColor.Color3 = "192 142 77";
                selectedColor.Minimap = "255 100 0";
            }
            else if (color == ColorPickerColors.Cyan)
            {
                selectedColor.Color1 = "150 255 240";
                selectedColor.Color2 = "160 255 250";
                selectedColor.Color3 = "99 163 159";
                selectedColor.Minimap = "6 190 179";
            }
            else if (color == ColorPickerColors.Pink)
            {
                selectedColor.Color1 = "255 190 255";
                selectedColor.Color2 = "255 200 255";
                selectedColor.Color3 = "186 117 185";
                selectedColor.Minimap = "255 0 253";
            }
            else if (color == ColorPickerColors.BlueSelf)
            {
                selectedColor.Color1 = "0 0 255";
                selectedColor.Color2 = "75 75 230";
                selectedColor.Color3 = "75 75 230";
                selectedColor.Minimap = "0 0 255";
            }
            else if (color == ColorPickerColors.RedFoe)
            {
                selectedColor.Color1 = "255 0 0";
                selectedColor.Color2 = "230 40 40";
                selectedColor.Color3 = "230 40 40";
                selectedColor.Minimap = "255 0 0";
            }
            else if (color == ColorPickerColors.YellowAlly)
            {
                selectedColor.Color1 = "255 255 0";
                selectedColor.Color2 = "215 215 30";
                selectedColor.Color3 = "215 215 30";
                selectedColor.Minimap = "255 255 0";
            }

            selectedColor.IsUserSet = true;
            return selectedColor;
        }

        private string RgbToLabel(ColorSetting rgb)
        {
            if (rgb.Color1 == "255 255 0" && rgb.Color2 == "215 215 30")
            {
                return ColorPickerColors.YellowAlly;
            }

            return rgb.Color1 switch
            {
                "2 146 239" => ColorPickerColors.Blue,
                "210 40 40" => ColorPickerColors.Red,
                "255 255 0" => ColorPickerColors.Yellow,
                "150 15 250" => ColorPickerColors.Purple,
                "15 210 80" => ColorPickerColors.Green,
                "255 150 5" => ColorPickerColors.Orange,
                "150 255 240" => ColorPickerColors.Cyan,
                "255 190 255" => ColorPickerColors.Pink,
                "0 0 255" => ColorPickerColors.BlueSelf,
                "255 0 0" => ColorPickerColors.RedFoe,
                _ => ColorPickerColors.Default,
            };
        }

        private static void SerializeToXmlFile<T>(T obj, string filePath)
        {
            var serializer = new XmlSerializer(typeof(T));

            var settings = new XmlWriterSettings
            {
                Indent = true,
                Encoding = Encoding.UTF8,
                OmitXmlDeclaration = false
            };

            var emptyNamespaces = new XmlSerializerNamespaces();
            emptyNamespaces.Add("", "");
            using var writer = XmlWriter.Create(filePath, settings);
            serializer.Serialize(writer, obj, emptyNamespaces);
        }

        private void ColorPicker_ColorChanged(object sender, RoutedEventArgs e)
        {
            UpdatePlayerColorPreview();
        }

        private void UpdatePlayerColorPreview()
        {
            ColorP1Preview.Source = ColorPreviewImage(Player1ColorPicker.SelectedColor, "P1");
            ColorP2Preview.Source = ColorPreviewImage(Player2ColorPicker.SelectedColor, "P2");
            ColorP3Preview.Source = ColorPreviewImage(Player3ColorPicker.SelectedColor, "P3");
            ColorP4Preview.Source = ColorPreviewImage(Player4ColorPicker.SelectedColor, "P4");
            ColorP5Preview.Source = ColorPreviewImage(Player5ColorPicker.SelectedColor, "P5");
            ColorP6Preview.Source = ColorPreviewImage(Player6ColorPicker.SelectedColor, "P6");
            ColorP7Preview.Source = ColorPreviewImage(Player7ColorPicker.SelectedColor, "P7");
            ColorP8Preview.Source = ColorPreviewImage(Player8ColorPicker.SelectedColor, "P8");
            ColorAPreview.Source = ColorPreviewImage(FriendorfoeallyColorPicker.SelectedColor, "Ally");
            ColorSPreview.Source = ColorPreviewImage(FriendorfoeselfColorPicker.SelectedColor, "Self");
            ColorFPreview.Source = ColorPreviewImage(FriendorfoeenemyColorPicker.SelectedColor, "Foe");
        }

        private BitmapImage ColorPreviewImage(string colorName, string playerNum)
        {
            var filePrefix = ColorNameToFilePrefix(colorName);

            if (colorName == ColorPickerColors.Default)
            {
                filePrefix = GetDefaultPreview(playerNum);
            }

            return new BitmapImage(new Uri($"pack://application:,,,/Celeste Launcher;component/Resources/ColorPicker/Preview/{playerNum}{filePrefix}.png", UriKind.Absolute));
        }

        private string GetDefaultPreview(string playerNum)
        {
            if (playerNum == "Ally")
            {
                return "C9";
            }
            else if (playerNum == "Self")
            {
                return "C10";
            }
            else if (playerNum == "Foe")
            {
                return "C11";
            }
            else
            {
                return "C" + playerNum.Substring(1);
            }
        }

        private string ColorNameToFilePrefix(string colorName)
        {
            if (colorName == ColorPickerColors.Blue)
                return "C1";
            else if (colorName == ColorPickerColors.Red)
                return "C2";
            else if (colorName == ColorPickerColors.Yellow)
                return "C3";
            else if (colorName == ColorPickerColors.Purple)
                return "C4";
            else if (colorName == ColorPickerColors.Green)
                return "C5";
            else if (colorName == ColorPickerColors.Orange)
                return "C6";
            else if (colorName == ColorPickerColors.Cyan)
                return "C7";
            else if (colorName == ColorPickerColors.Pink)
                return "C8";
            else if (colorName == ColorPickerColors.BlueSelf)
                return "C9";
            else if (colorName == ColorPickerColors.RedFoe)
                return "C10";
            else if (colorName == ColorPickerColors.YellowAlly)
                return "C11";
            else
                return "C1";
        }
    }
}
