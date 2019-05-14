using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
                ToggleProcessing(true);
                var startMenuRegister = new StartMenuRegister(PathTextBox.Text);
                startMenuRegister.Register();
                ShowSuccessDialog("Register complete");
            }
            catch (FileNotFoundException ex)
            {
                ShowErrorDialog(ex);
            }
            catch (ShortcutAlreadyExistException ex)
            {
                ShowErrorDialog(ex);
            }
            finally
            {
                ToggleProcessing(false);
            }
        }

        private void ShowSuccessDialog(string msg)
            => MessageBox.Show(msg, "Success", MessageBoxButton.OK, MessageBoxImage.Information);

        private void ShowErrorDialog(Exception e)
            => MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

        private void ToggleProcessing(bool isProcessing)
        {
            if (isProcessing)
            {
                Mouse.OverrideCursor = Cursors.Wait;
                PathTextBox.IsEnabled = false;
                FilePickerButton.IsEnabled = false;
                RegisterButton.IsEnabled = false;
            }
            else
            {
                Mouse.OverrideCursor = null;
                PathTextBox.IsEnabled = true;
                FilePickerButton.IsEnabled = true;
                RegisterButton.IsEnabled = true;
            }
        }
    }
}
