using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Stardust
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnClickFilePickerButton(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            var isSelected = dialog.ShowDialog();

            if (isSelected == true)
            {
                PathTextBox.Text = dialog.FileName;
            }
            else
            {
                PathTextBox.Text =
                    Environment.GetFolderPath(Environment.SpecialFolder.StartMenu);
            }
        }

        private void OnClickRegisterButton(object sender, RoutedEventArgs e)
        {
            try
            {
                var startMenuRegister = new StartMenuRegister(PathTextBox.Text);
                startMenuRegister.Register();
            }
            catch (FileNotFoundException ex)
            {
                ShowErrorDialog(ex);
            }
            catch (ShortcutAlreadyExistException ex)
            {
                ShowErrorDialog(ex);
            }
        }

        private void ShowErrorDialog(Exception e)
            => MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }
}
