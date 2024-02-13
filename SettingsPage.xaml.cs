using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
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

namespace _6040CEM_GUI_Windows.View
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private void SerialCommApply(object sender, RoutedEventArgs e)
        {
            if (AreSerialConfigsValid())
            {
                SerialComConfigs.selectedComPort = ComPort.Text;
                SerialComConfigs.selectedBaudRate = int.Parse(BaudRate.Text);
                SerialComConfigs.selectedParity = Parity.Text;
                SerialComConfigs.selectedDataBits = int.Parse(DataBits.Text);
                SerialComConfigs.isSerialConfigsSelected = true;
                ConfigMsg.Content = "Configs has been saved!";
                ConfigMsg.Foreground = new SolidColorBrush(Colors.Green);
            }
        }
        private bool AreSerialConfigsValid()
        {
            ConfigMsg.Visibility = Visibility.Visible;
            if (string.IsNullOrEmpty(ComPort.Text))
            {
                ConfigMsg.Content = "Please select a COM port.";
                return false;
            }

            if (string.IsNullOrEmpty(BaudRate.Text))
            {
                ConfigMsg.Content = "Please select a Baud Rate.";
                return false;
            }

            if (string.IsNullOrEmpty(Parity.Text))
            {
                ConfigMsg.Content = "Please select a Parity.";
                return false;
            }

            if (string.IsNullOrEmpty(DataBits.Text))
            {
                ConfigMsg.Content = "Please select a Data Bits value.";
                return false;
            }

            return true;
        }
    }
    public static class SerialComConfigs
    {
        public static bool isSerialConfigsSelected = false;
        public static string selectedComPort, selectedParity;
        public static int selectedBaudRate, selectedDataBits;
    }
}
