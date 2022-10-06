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
using System.Net.Sockets;
using System.IO;
using Microsoft.Win32;
using System.Threading;
using vlapp.Models;
using vlapp.Control;

namespace vlapp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ConnectionModel connection = new ConnectionModel();
        bool firstBoot = true;
        public MainWindow()
        {
            InitializeComponent();
            Database_Functions.connectDb();
            frame.Navigate(new Uri("Upload_Page.xaml", UriKind.RelativeOrAbsolute));

        }

        private void SideMenuControl_SelectionChanged(object sender, EventArgs e)
        {
            //System.Diagnostics.Trace.WriteLine(sender);
            switch (MenuList.SelectedIndex)
            {
                case 0:
                    frame.Navigate(new Uri("Upload_Page.xaml", UriKind.RelativeOrAbsolute));
                    break;
                //case 1:
                //    frame.Navigate(new Uri("Collection_Page.xaml", UriKind.RelativeOrAbsolute));
                //    break;
                case 1:
                    frame.Navigate(new Uri("Arrangement_Page.xaml", UriKind.RelativeOrAbsolute));
                    break;
                case 2:
                    frame.Navigate(new Uri("Brightness_Page.xaml", UriKind.RelativeOrAbsolute));
                    break;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            //loading_popup.IsOpen = true;
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += connectionStatus;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 2);
            dispatcherTimer.Start();
          
        }

        private void connectionStatus(object sender, EventArgs e)
        {
            
            //connection.Connect();
            //connection.getConnection();

            //bool check = connection.receiveData;

            //if (check) 
            //{
            //    loading_popup.IsOpen = false;
            //}
            //else
            //{
            //    loading_popup.IsOpen = true;  
            //}

        }
    }
}
