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
using vlapp.Control;
using vlapp.Models;

namespace vlapp
{
    /// <summary>
    /// Interaction logic for Brightness_Page.xaml
    /// </summary>
    public partial class Brightness_Page : Page
    {
        ConnectionModel connection = new ConnectionModel();
        ObservableCollection<NodeListItem> blindList;
        NodeListItem? element;
        ModuleModel? ModuleElement;
        int index, moduleIndex , blindIDHolder;
        string tempIp;
        int currentGain_1, currentGain_2, currentGain_3, currentGain_4;
        int redVal_1, redVal_2, redVal_3, redVal_4;
        int greenVal_1, greenVal_2, greenVal_3, greenVal_4;
        int blueVal_1, blueVal_2, blueVal_3, blueVal_4;
        int popCurrentGain, popRedVal, popGreenVal, popBlueVal;
        public Brightness_Page()
        {
            InitializeComponent();
            connection.sendWithResponse();
            blindList = connection.GetBlindList();
            //saveNodesToDb(new List<NodeListItem>(blindList));
            listbox_arrange_ip.ItemsSource = blindList;
            listbox_module_1.ItemsSource = displayModule();
            listbox_module_2.ItemsSource = displayModule();
            listbox_module_3.ItemsSource = displayModule();
            listbox_module_4.ItemsSource = displayModule();
            //if (blindList.Count == 0)
            //{
            //    btn_Save_Arrangement.Visibility = Visibility.Collapsed;
            //    btn_Refresh.Visibility = Visibility.Visible;
            //}
            //else
            //{
            //    btn_Save_Arrangement.Visibility = Visibility.Visible;
            //    btn_Refresh.Visibility = Visibility.Collapsed;
            //}
        }

        //private void saveNodesToDb(List<NodeListItem> nodes) {
        //    //clearing tbl_node ::dangerous//
        //    Database_Functions fDb = new Database_Functions();
        //    string tbl = "tbl_node";
        //    string msg = fDb.clearTbl(tbl);
        //    // clearing tbl_node::dangerous//

        //    foreach (var x in nodes) {
        //        string[] fields = { "ip", "blind_num", "pos" };
        //        string ip = x.BlindIp[0] + "." + x.BlindIp[1] + "." + x.BlindIp[2] + "." + x.BlindIp[3];
        //        string[] values = { ip, x.BlindNumber.ToString(), x.nodeIndex.ToString() };
        //        fDb.insertData(tbl, fields, values);
        //    }
        //}

        //blind 1
        private void sliderb1_all_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int value = Convert.ToInt32(sliderb1_all.Value);
            txtb1_all.Text = "Current gain: " + value;
            currentGain_1 = value;
        }

        private void sliderb1_red_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int value = Convert.ToInt32(sliderb1_red.Value);
            txtb1_red.Text = "Red: " + value;
            redVal_1 = value;
        }

        private void sliderb1_green_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int value = Convert.ToInt32(sliderb1_green.Value);
            txtb1_green.Text = "Green: " + value;
            greenVal_1 = value;
        }

        private void sliderb1_blue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int value = Convert.ToInt32(sliderb1_blue.Value);
            txtb1_blue.Text = "Blue: " + value;
            blueVal_1 = value;
        }
        private void btn_save_blind1_brightness_Click(object sender, RoutedEventArgs e)
        {
            Helper lHelper = new Helper();
            int y = lHelper.applyBlindConfig(tempIp, 1, redVal_1, greenVal_1, blueVal_1, currentGain_1);
            if (y == 1)
            {
                MessageBox.Show("successfuly saved");
            }
            else
            {
                MessageBox.Show("saving failed");
            }
         }

        private void btn_reset_blind1_brightness_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = MessageBox.Show("You are about to reset this blinds RGB and Current gain values and all its modules to zero.", "Are you sure?", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                Database_Functions fDb = new Database_Functions();
                long x = fDb.resetBlindConfig(tempIp, 1);
                if (x == 0)
                {
                    MessageBox.Show("resetting failed");
                }
                else
                {
                    MessageBox.Show("successfully reset");
                    sliderb1_all.Value = 0;
                    sliderb1_red.Value = 0;
                    sliderb1_green.Value = 0;
                    sliderb1_blue.Value = 0;
                }
            }
            else if (dialogResult == MessageBoxResult.No)
            {
                //do nothing
            }
        }

        //blind 2
        private void sliderb2_all_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int value = Convert.ToInt32(sliderb2_all.Value);
            txtb2_all.Text = "Current gain: " + value;
            currentGain_2 = value;
        }

        private void sliderb2_red_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int value = Convert.ToInt32(sliderb2_red.Value);
            txtb2_red.Text = "Red: " + value;
            redVal_2 = value;
        }

        private void sliderb2_green_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int value = Convert.ToInt32(sliderb2_green.Value);
            txtb2_green.Text = "Green: " + value;
            greenVal_2 = value;
        }

        private void sliderb2_blue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int value = Convert.ToInt32(sliderb2_blue.Value);
            txtb2_blue.Text = "Blue: " + value;
            blueVal_2 = value;
        }

        private void btn_save_blind2_brightness_Click(object sender, RoutedEventArgs e)
        {
            Helper lHelper = new Helper();
            int y = lHelper.applyBlindConfig(tempIp, 2, redVal_2, greenVal_2, blueVal_2, currentGain_2);
            if (y == 1)
            {
                MessageBox.Show("successfuly saved");
            }
            else
            {
                MessageBox.Show("saving failed");
            }
        }

        private void btn_reset_blind2_brightness_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = MessageBox.Show("You are about to reset this blinds RGB and Current gain values and all its modules to zero.", "Are you sure?", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                Database_Functions fDb = new Database_Functions();
                long x = fDb.resetBlindConfig(tempIp, 2);
                if (x == 0)
                {
                    MessageBox.Show("resetting failed");
                }
                else
                {
                    MessageBox.Show("successfully reset");
                    sliderb2_all.Value = 0;
                    sliderb2_red.Value = 0;
                    sliderb2_green.Value = 0;
                    sliderb2_blue.Value = 0;
                }
            }
            else if (dialogResult == MessageBoxResult.No)
            {
                //do nothing
            }
        }

        //blind 3
        private void sliderb3_all_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int value = Convert.ToInt32(sliderb3_all.Value);
            txtb3_all.Text = "Current gain: " + value;
            currentGain_3 = value;
        }

        private void sliderb3_red_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int value = Convert.ToInt32(sliderb3_red.Value);
            txtb3_red.Text = "Red: " + value;
            redVal_3 = value;
        }

        private void sliderb3_green_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int value = Convert.ToInt32(sliderb3_green.Value);
            txtb3_green.Text = "Green: " + value;
            greenVal_3 = value;
        }

        private void sliderb3_blue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int value = Convert.ToInt32(sliderb3_blue.Value);
            txtb3_blue.Text = "Blue: " + value;
            blueVal_3 = value;
        }

        private void btn_save_blind3_brightness_Click(object sender, RoutedEventArgs e)
        {
            Helper lHelper = new Helper();
            int y = lHelper.applyBlindConfig(tempIp, 3, redVal_3, greenVal_3, blueVal_3, currentGain_3);
            if (y == 1)
            {
                MessageBox.Show("successfuly saved");
            }
            else
            {
                MessageBox.Show("saving failed");
            }
        }
        private void btn_reset_blind3_brightness_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = MessageBox.Show("You are about to reset this blinds RGB and Current gain values and all its modules to zero.", "Are you sure?", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                Database_Functions fDb = new Database_Functions();
                long x = fDb.resetBlindConfig(tempIp, 3);
                if (x == 0)
                {
                    MessageBox.Show("resetting failed");
                }
                else
                {
                    MessageBox.Show("successfully reset");
                    sliderb3_all.Value = 0;
                    sliderb3_red.Value = 0;
                    sliderb3_green.Value = 0;
                    sliderb3_blue.Value = 0;
                }
            }
            else if (dialogResult == MessageBoxResult.No)
            {
                //do nothing
            }
        }

        //blind 4
        private void sliderb4_all_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int value = Convert.ToInt32(sliderb4_all.Value);
            txtb4_all.Text = "Current gain: " + value;
            currentGain_4 = value;
        }

        private void sliderb4_red_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int value = Convert.ToInt32(sliderb4_red.Value);
            txtb4_red.Text = "Red: " + value;
            redVal_4 = value;
        }

        private void sliderb4_green_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int value = Convert.ToInt32(sliderb4_green.Value);
            txtb4_green.Text = "Green: " + value;
            greenVal_4 = value;
        }

        private void sliderb4_blue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int value = Convert.ToInt32(sliderb4_blue.Value);
            txtb4_blue.Text = "Blue: " + value;
            blueVal_4 = value;
        }

        private void btn_save_blind4_brightness_Click(object sender, RoutedEventArgs e)
        {
            Helper lHelper = new Helper();
            int y = lHelper.applyBlindConfig(tempIp, 4, redVal_4, greenVal_4, blueVal_4, currentGain_4);
            if (y == 1)
            {
                MessageBox.Show("successfuly saved");
            }
            else {
                MessageBox.Show("saving failed");
            }
        }
        private void btn_reset_blind4_brightness_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = MessageBox.Show("You are about to reset this blinds RGB and Current gain values and all its modules to zero.", "Are you sure?", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                Database_Functions fDb = new Database_Functions();
                long x = fDb.resetBlindConfig(tempIp, 4);
                if (x == 0)
                {
                    MessageBox.Show("resetting failed");
                }
                else
                {
                    MessageBox.Show("successfully reset");
                    sliderb4_all.Value = 0;
                    sliderb4_red.Value = 0;
                    sliderb4_green.Value = 0;
                    sliderb4_blue.Value = 0;
                }
            }
            else if (dialogResult == MessageBoxResult.No)
            {
                //do nothing
            }
        }

        //pop up
        private void slider_popup_all_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int value = Convert.ToInt32(slider_popup_all.Value);
            txt_popup_all.Text = "Current gain: " + value;
            popCurrentGain = value;
        }

        private void slider_popup_red_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int value = Convert.ToInt32(slider_popup_red.Value);
            txt_popup_red.Text = "Red: " + value;
            popRedVal = value;
        }

        private void slider_popup_green_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int value = Convert.ToInt32(slider_popup_green.Value);
            txt_popup_green.Text = "Green: " + value;
            popGreenVal = value;
        }

        private void slider_popup_blue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int value = Convert.ToInt32(slider_popup_blue.Value);
            txt_popup_blue.Text = "Blue: " + value;
            popBlueVal = value;
        }
        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            Helper lHelper = new Helper();
            int y = lHelper.applyModuleConfig(tempIp, blindIDHolder, moduleIndex, popRedVal, popGreenVal, popBlueVal, popCurrentGain);
            if (y == 1)
            {
                MessageBox.Show("successfuly saved");
            }
            else
            {
                MessageBox.Show("saving failed");
            }
            popup_BrightnessChange.IsOpen = false;
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            popup_BrightnessChange.IsOpen = false;
        }

        private List<ModuleModel> displayModule()
        {
            List<ModuleModel> modules = new List<ModuleModel>();
            modules.Add(new ModuleModel() { ModulePosition = "Module 1"});
            modules.Add(new ModuleModel() { ModulePosition = "Module 2" });
            modules.Add(new ModuleModel() { ModulePosition = "Module 3" });
            modules.Add(new ModuleModel() { ModulePosition = "Module 4" });
            modules.Add(new ModuleModel() { ModulePosition = "Module 5" });
            modules.Add(new ModuleModel() { ModulePosition = "Module 6" });
            modules.Add(new ModuleModel() { ModulePosition = "Module 7" });
            modules.Add(new ModuleModel() { ModulePosition = "Module 8" });
            modules.Add(new ModuleModel() { ModulePosition = "Module 9" });
            modules.Add(new ModuleModel() { ModulePosition = "Module 10" });
            modules.Add(new ModuleModel() { ModulePosition = "Module 11" });

           return modules;
        }

        private void btn_esp_Click(object sender, RoutedEventArgs e)
        {
            Button? btn = sender as Button;
            
            if (btn != null)
            {
                object item = btn.DataContext;

                if (item != null)
                {
                    index = this.listbox_arrange_ip.Items.IndexOf(item);
                    element = (NodeListItem)this.listbox_arrange_ip.Items[index];

                    txt_EspNumber.Content = "Selected Esp ID: "+element.nodeIndex.ToString();
                    txt_BlindNumber.Content = "Number of blinds: "+ element.BlindNumber.ToString();
                    tempIp = element.BlindIp[0]+"."+element.BlindIp[1]+"."+element.BlindIp[2]+"."+element.BlindIp[3];
                    Database_Functions fDb = new Database_Functions();
                    var bItems = fDb.getBlindProps(tempIp);
                    if (bItems == null) {
                        MessageBox.Show("The database is not available");
                        return;
                    }
                    List<BlindItem> blindItems = new List<BlindItem>(bItems);
                    Helper lHelper = new Helper();
                    

                    switch (element.BlindNumber)
                    {
                        case 1:
                            resetSliders();
                            blind_1.IsEnabled = true;
                            if (blindItems != null) {
                                object[] sliders = { sliderb1_all, sliderb1_red, sliderb1_green, sliderb1_blue };
                                lHelper.applyBlindProps(1, blindItems, sliders);
                            }
                            blind_2.IsEnabled = false;
                            blind_3.IsEnabled = false;
                            blind_4.IsEnabled = false;
                            break;
                        case 2:
                            resetSliders();
                            blind_1.IsEnabled = true;
                            if (blindItems != null)
                            {
                                object[] sliders = { sliderb1_all, sliderb1_red, sliderb1_green, sliderb1_blue };
                                lHelper.applyBlindProps(1, blindItems, sliders);
                            }
                            blind_2.IsEnabled = true;
                            if (blindItems != null)
                            {
                                object[] sliders = { sliderb2_all, sliderb2_red, sliderb2_green, sliderb2_blue };
                                lHelper.applyBlindProps(2, blindItems, sliders);
                            }
                            blind_3.IsEnabled = false;
                            blind_4.IsEnabled = false;
                            break;
                        case 3:
                            resetSliders();
                            blind_1.IsEnabled = true;
                            if (blindItems != null)
                            {
                                object[] sliders = { sliderb1_all, sliderb1_red, sliderb1_green, sliderb1_blue };
                                lHelper.applyBlindProps(1, blindItems, sliders);
                            }
                            blind_2.IsEnabled = true;
                            if (blindItems != null)
                            {
                                object[] sliders = { sliderb2_all, sliderb2_red, sliderb2_green, sliderb2_blue };
                                lHelper.applyBlindProps(2, blindItems, sliders);
                            }
                            blind_3.IsEnabled = true;
                            if (blindItems != null)
                            {
                                object[] sliders = { sliderb3_all, sliderb3_red, sliderb3_green, sliderb3_blue };
                                lHelper.applyBlindProps(3, blindItems, sliders);
                            }
                            blind_4.IsEnabled = false;
                            break;
                        case 4:
                            resetSliders();
                            blind_1.IsEnabled = true;
                            if (blindItems != null)
                            {
                                object[] sliders = { sliderb1_all, sliderb1_red, sliderb1_green, sliderb1_blue };
                                lHelper.applyBlindProps(1, blindItems, sliders);
                            }
                            blind_2.IsEnabled = true;
                            if (blindItems != null)
                            {
                                object[] sliders = { sliderb2_all, sliderb2_red, sliderb2_green, sliderb2_blue };
                                lHelper.applyBlindProps(2, blindItems, sliders);
                            }
                            blind_3.IsEnabled = true;
                            if (blindItems != null)
                            {
                                object[] sliders = { sliderb3_all, sliderb3_red, sliderb3_green, sliderb3_blue };
                                lHelper.applyBlindProps(3, blindItems, sliders);
                            }
                            blind_4.IsEnabled = true;
                            if (blindItems != null)
                            {
                                object[] sliders = { sliderb4_all, sliderb4_red, sliderb4_green, sliderb4_blue };
                                lHelper.applyBlindProps(4, blindItems, sliders);
                            }
                            break;
                    }

                    System.Diagnostics.Trace.WriteLine(element.BlindNumber);
                    //blindList[index].BlindIndex

                }
            }
        }

        private void resetSliders()
        {
            sliderb1_all.Value= 0;
            sliderb1_blue.Value = 0;
            sliderb1_green.Value = 0;
            sliderb1_red.Value = 0;

            sliderb2_all.Value = 0;
            sliderb2_blue.Value = 0;
            sliderb2_green.Value = 0;
            sliderb2_red.Value = 0;

            sliderb3_all.Value = 0;
            sliderb3_blue.Value = 0;
            sliderb3_green.Value = 0;
            sliderb3_red.Value = 0;

            sliderb4_all.Value = 0;
            sliderb4_blue.Value = 0;
            sliderb4_green.Value = 0;
            sliderb4_red.Value = 0;
        }

        private void resetPopSliders() 
        {
            slider_popup_all.Value = 0;
            slider_popup_red.Value = 0;
            slider_popup_green.Value = 0;
            slider_popup_blue.Value = 0;
        }

        private int setPopupSliderProps() {
            Database_Functions fDb = new Database_Functions();
            var mItems = fDb.getModuleProps(tempIp, blindIDHolder);
            if (mItems == null) {
                MessageBox.Show("The database is not available");
                return 0;
            }
            List<ModuleItem> moduleItems = new List<ModuleItem>(mItems);
            Helper lHelper = new Helper();

            if (moduleItems != null)
            {
                object[] sliders = { slider_popup_all, slider_popup_red, slider_popup_green, slider_popup_blue };
                lHelper.applyModuleProps(blindIDHolder, moduleIndex, moduleItems, sliders);
            }
            return 1;
        }

        private void btn_module1_Click_1(object sender, RoutedEventArgs e)
        {
            Button? btn = sender as Button;

            if (btn != null)
            {
                object item = btn.DataContext;


                if (item != null)
                {
                    resetPopSliders();
                    moduleIndex = this.listbox_module_1.Items.IndexOf(item)+1;
                    blindIDHolder = 1;

                    txt_BlindNumber_popup.Content = "Blind ID: "+ blindIDHolder.ToString();
                    txt_ModuleNumber_popup.Content = "Module Number: "+moduleIndex.ToString();

                    int temp = setPopupSliderProps();
                    if (temp == 0) 
                    {
                        return;
                    }

                    popup_BrightnessChange.IsOpen = true;
                }
            }
        }

        private void btn_module2_Click(object sender, RoutedEventArgs e)
        {
            Button? btn = sender as Button;

            if (btn != null)
            {
                object item = btn.DataContext;


                if (item != null)
                {
                    resetPopSliders();
                    moduleIndex = this.listbox_module_2.Items.IndexOf(item) + 1;
                    blindIDHolder = 2;

                    txt_BlindNumber_popup.Content = "Blind ID: " + blindIDHolder.ToString();
                    txt_ModuleNumber_popup.Content = "Module Number: " + moduleIndex.ToString();

                    int temp = setPopupSliderProps();
                    if (temp == 0)
                    {
                        return;
                    }

                    popup_BrightnessChange.IsOpen = true;
                }
            }
        }

        private void btn_module3_Click(object sender, RoutedEventArgs e)
        {
            Button? btn = sender as Button;

            if (btn != null)
            {
                object item = btn.DataContext;


                if (item != null)
                {
                    resetPopSliders();
                    moduleIndex = this.listbox_module_3.Items.IndexOf(item) + 1;
                    blindIDHolder = 3;

                    txt_BlindNumber_popup.Content = "Blind ID: " + blindIDHolder.ToString();
                    txt_ModuleNumber_popup.Content = "Module Number: " + moduleIndex.ToString();

                    int temp = setPopupSliderProps();
                    if (temp == 0)
                    {
                        return;
                    }

                    popup_BrightnessChange.IsOpen = true;
                }
            }
        }

        private void btn_module4_Click(object sender, RoutedEventArgs e)
        {
            Button? btn = sender as Button;

            if (btn != null)
            {
                object item = btn.DataContext;


                if (item != null)
                {
                    resetPopSliders();
                    moduleIndex = this.listbox_module_4.Items.IndexOf(item) + 1;
                    blindIDHolder = 4;

                    txt_BlindNumber_popup.Content = "Blind ID: " + blindIDHolder.ToString();
                    txt_ModuleNumber_popup.Content = "Module Number: " + moduleIndex.ToString();

                    int temp = setPopupSliderProps();
                    if (temp == 0)
                    {
                        return;
                    }

                    popup_BrightnessChange.IsOpen = true;
                }
            }
        }
    }
}
