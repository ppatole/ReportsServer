using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;


namespace HL7TestingApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Window1 w = new Window1();
            w.Show();
        }

          

        string GotMessage(String message)
        {
                       return "";
        }

        string Log(String message)
        {
            //txtLog.Text = message + "\n\r";
            return "";
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Task t = new Task(() => HL7Listener.HL7Listener.HL7StopListen());
            t.Start();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Task t = new Task(() => HL7Listener.HL7Listener.HL7Listen());
            t.Start();
        }
    }
}
