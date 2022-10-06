using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using vlapp.Models;
using Dragablz;
using System.Data;

namespace vlapp
{
    /// <summary>
    /// Interaction logic for Arrangement_Page.xaml
    /// </summary>
    public partial class Arrangement_Page : Page
    {
        ConnectionModel connection = new ConnectionModel();
        ObservableCollection<NodeListItem> blindList;
        NodeListItem? element;
        int index;

        public Arrangement_Page()
        {
        InitializeComponent();

                connection.sendWithResponse();
                blindList = connection.GetBlindList();

            DataContext = blindList;

            if(blindList.Count == 0)
            {
                btn_Save_Arrangement.Visibility = Visibility.Collapsed;
                btn_Refresh.Visibility = Visibility.Visible;
            }
            else
            {
                btn_Save_Arrangement.Visibility = Visibility.Visible;
                btn_Refresh.Visibility = Visibility.Collapsed;
            }
        }
        //public ObservableCollection<BlindListItem> GetBlindList()
        //{
        //    foreach(BlindListItem element in global.BlindList)
        //    {
        //        bList.Add(element);
        //    }
 
        //    return bList;
        //}
        

   

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Button? btn = sender as Button;
            if (btn != null)
            {
                object item = btn.DataContext;


                if (item != null)
                {
                    index = this.listbox_arrange_ip.Items.IndexOf(item);
                    element = (NodeListItem)this.listbox_arrange_ip.Items[index];

                    txt_currentPosition.Text = "Current: " + element.nodeIndex.ToString();
                    popup_changeIndex.IsOpen = true;



                    System.Diagnostics.Trace.WriteLine(index);
                    //blindList[index].BlindIndex
                   
                }
            }
           
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            popup_changeIndex.IsOpen = false;
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            if(txt_toPosition.Text != "")
            {
                if(Convert.ToInt32(txt_toPosition.Text) <= blindList.Count())
                {
                    var tempItem = blindList.First(a => a.nodeIndex == Convert.ToByte(txt_toPosition.Text));

                    byte tempIndex = (byte)blindList.IndexOf(tempItem);

                    blindList[tempIndex].nodeIndex = Convert.ToByte(element.nodeIndex);
                    blindList[index].nodeIndex = Convert.ToByte(txt_toPosition.Text);


                    this.listbox_arrange_ip.Items.Refresh();
                    txt_toPosition.Text = "";

                    popup_changeIndex.IsOpen = false;
                }
            }
            else
            {
             
            }
        }

        private void btn_Save_Arrangement_Click(object sender, RoutedEventArgs e)
        {
            connection.sendArrange(blindList);
            blindList = new ObservableCollection<NodeListItem>(blindList.OrderBy(i => i.nodeIndex));
            //DataContext = blindList;
            this.listbox_arrange_ip.Items.Refresh();

        }
    }
}
