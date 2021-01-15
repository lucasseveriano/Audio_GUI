using NAudio.CoreAudioApi;
using NAudio.Wave;
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

namespace Audio_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string audioFile1;
        AudioHandler audioHandler;

        public MainWindow()
        {
            InitializeComponent();
            audioFile1 = null;

            audioHandler = new AudioHandler();
            InitOutputDevices();

            //var path_of_the_file_you_want_to_play = "C:\\Users\\lucssantos\\source\\repos\\Audio_GUI\\Audio_GUI\\audio\\audio1.wav";


            // WaveStream mainOutputStream = new WaveFileReader(path_of_the_file_you_want_to_play);
            //WaveChannel32 volumeStream = new WaveChannel32(mainOutputStream);

            //WaveOutEvent player = new WaveOutEvent();
            ////player.DeviceNumber = 1;

            //for (int n = -1; n < WaveOut.DeviceCount; n++)
            //{
            //    var caps = WaveOut.GetCapabilities(n);
            //    Console.WriteLine($"{n}: {caps.ProductName}");
            //    MessageBox.Show(caps.ProductName);
            //}

            //player.Init(mainOutputStream);

            //player.Play();

            //while (player.PlaybackState == PlaybackState.Playing)
            //{
            //    Thread.Sleep(100);
            //}

        }

        public void InitOutputDevices()
        {
            deviceComboBox.Items.Clear();
            deviceComboBox.ItemsSource = audioHandler.GetDeviceList();

            deviceComboBox.SelectedIndex = 1;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            string CombinedPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "..\\abcd");
            dlg.InitialDirectory = System.IO.Path.GetFullPath(CombinedPath);

            // Set filter for file extension and default file extension 

            dlg.DefaultExt = ".wav";
            //dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                audioFile1 = dlg.FileName;                  
            }
        }
        

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            InitOutputDevices();
        }
               

        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            if (audioFile1 == null)
            {
                MessageBox.Show("Select an audio file!");
                return;
            }

            var n = audioHandler.DeviceNameToNumber(deviceComboBox.SelectedItem.ToString());
            audioHandler.Play(audioFile1, n);

        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            audioHandler.Stop();
        }
    }
}
