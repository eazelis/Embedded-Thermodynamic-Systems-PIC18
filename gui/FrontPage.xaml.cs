using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace _6040CEM_GUI_Windows.View
{
    public partial class FrontPage : Page
    {
        private SerialPort serialPort;

        public FrontPage()
        {
            InitializeComponent();
        }

        private void ConnectSerialCom(object sender, RoutedEventArgs e)
        {
            SerialComRx.Text = "Waiting for the data...";
            SerialComRx.BorderBrush = Brushes.Transparent;

            if (SerialComConfigs.isSerialConfigsSelected)
            {
                SetupSerialPort();

                try
                {
                    serialPort.Open();
                    Console.WriteLine("Serial port connection established successfully.");
                }
                catch (Exception ex)
                {
                    SerialComRx.Text = "Failed to establish Serial Com connection: " + ex.Message;
                }
            }
            else
            {
                SerialComRx.Text = "Error, please go to the Settings page!";
            }
        }

        private void SetupSerialPort() // Uploading Serial Com Configs
        {
            ConnectBtn.Visibility = Visibility.Hidden;
            DisconnectBtn.Visibility = Visibility.Visible;

            serialPort = new SerialPort
            {
                PortName = SerialComConfigs.selectedComPort,
                BaudRate = SerialComConfigs.selectedBaudRate,
                Parity = (Parity)Enum.Parse(typeof(Parity), SerialComConfigs.selectedParity),
                DataBits = SerialComConfigs.selectedDataBits,
                StopBits = StopBits.One,
                ReadTimeout = 500
            };

            serialPort.DataReceived += SerialPort_DataReceived;
        }

        private async void HighlightReceivedData() // Showing to the user that the data is still being received
        {
            SerialComRx.BorderBrush = Brushes.Green;
            await Task.Delay(350);
            SerialComRx.BorderBrush = Brushes.Transparent;
        }

        private void UpdateSensorData(JObject sensorData) // Displaying filtered data to UI
        {
            // Update sensor labels
            SensorNameLabel1.Content = sensorData.ContainsKey("AN0") ? $"AN0: Temperature (Thermistor)" : "N/A";
            SensorNameLabel2.Content = sensorData.ContainsKey("AN1") ? $"AN1: Temperature (Thermocouple)" : "N/A";
            SensorNameLabel3.Content = sensorData.ContainsKey("AN4") ? $"AN4: Pressure (MPX4250)" : "N/A";

            // Update sensor values and store them in SerialComReceivedData
            if (sensorData.ContainsKey("AN0"))
            {
                string value = sensorData["AN0"].ToString();
                SensorValueLabel1.Content = value;
                SerialComReceivedData.sensorDataAN0 = value;
            }
            else
            {
                SensorValueLabel1.Content = "N/A";
            }

            if (sensorData.ContainsKey("AN1"))
            {
                string value = sensorData["AN1"].ToString();
                SensorValueLabel2.Content = value;
                SerialComReceivedData.sensorDataAN1 = value;
            }
            else
            {
                SensorValueLabel2.Content = "N/A";
            }

            if (sensorData.ContainsKey("AN4"))
            {
                string value = sensorData["AN4"].ToString();
                SensorValueLabel3.Content = value;
                SerialComReceivedData.sensorDataAN4 = value;
            }
            else
            {
                SensorValueLabel3.Content = "N/A";
            }
        }

        private JObject ParseJsonData(string jsonData) // Parsing the JSON data
        {
            try
            {
                return JsonConvert.DeserializeObject<JObject>(jsonData);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to parse JSON data: {ex.Message}");
                return null;
            }
        }

        private string lastReceivedData = "", receivedDataBuffer = "";

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e) // Filtering the packages message
        {
            SerialPort sp = (SerialPort)sender;
            try
            {
                receivedDataBuffer += sp.ReadExisting();
                int startIndex = receivedDataBuffer.IndexOf('{');
                int endIndex = receivedDataBuffer.IndexOf('}', startIndex);

                while (startIndex >= 0 && endIndex >= 0)
                {
                    if (startIndex <= endIndex)
                    {
                        string completeData = receivedDataBuffer.Substring(startIndex, endIndex - startIndex + 1);
                        receivedDataBuffer = receivedDataBuffer.Substring(endIndex + 1);

                        Debug.WriteLine(completeData);
                        Dispatcher.Invoke(() =>
                        {
                            JObject sensorData = ParseJsonData(completeData);

                            if (sensorData != null)
                            {
                                SerialComRx.Text = completeData;
                                HighlightReceivedData();
                                UpdateSensorData(sensorData);
                            }
                            else
                            {
                                SerialComRx.Text = "Failed to parse JSON data.";
                            }
                        });
                    }

                    startIndex = receivedDataBuffer.IndexOf('{', endIndex);
                    endIndex = receivedDataBuffer.IndexOf('}', startIndex);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to process received data: " + ex.Message);
            }
        }

        private void DisconnectSerialCom(object sender, RoutedEventArgs e) // Disconnecting the Serial Com link
        {
            try
            {
                serialPort.Close();
                ConnectBtn.Visibility = Visibility.Visible;
                DisconnectBtn.Visibility = Visibility.Hidden;
                SerialComRx.Text = "Disconnected";
            }
            catch (Exception ex)
            {
                SerialComRx.Text = "Failed to close Serial Com connection: " + ex.Message;
            }
        }
    }

    public static class SerialComReceivedData // Global variables
    {
        public static string sensorDataAN0, sensorDataAN1, sensorDataAN4;
    }
}

