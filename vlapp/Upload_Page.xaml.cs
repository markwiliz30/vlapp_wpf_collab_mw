using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
using vlapp.Control;

namespace vlapp
{
    /// <summary>
    /// Interaction logic for Upload_Page.xaml
    /// </summary>
    public partial class Upload_Page : Page
    {
        ObservableCollection<VideoListItem> vList = new ObservableCollection<VideoListItem>();
        List<TimeListItem> tempTimeList = new List<TimeListItem>();
        VideoListItem? element;
        string[] fileExtension = { ".wav", ".aac", ".wma", ".wmv", ".avi", ".mpg", ".mpeg", ".m1v", ".mp2", ".mp3", ".mpa", ".mpe", ".m3u", ".mp4", ".mov", ".3g2", ".3gp2", ".3gp", ".3gpp", ".m4a", ".cda", ".aif", ".aifc", ".aiff", ".mid", ".midi", ".rmi", ".mkv", ".WAV", ".AAC", ".WMA", ".WMV", ".AVI", ".MPG", ".MPEG", ".M1V", ".MP2", ".MP3", ".MPA", ".MPE", ".M3U", ".MP4", ".MOV", ".3G2", ".3GP2", ".3GP", ".3GPP", ".M4A", ".CDA", ".AIF", ".AIFC", ".AIFF", ".MID", ".MIDI", ".RMI", ".MKV" };
        bool dateCheckStatus = false;
        bool updateIsClicked = false;
        int index;
        public Upload_Page()
        {
            InitializeComponent();
            Database_Functions db = new Database_Functions();
            checkBoxStatus();
            DataContext = db.getSchedule();
        }




        private void btn_add_video_Click(object sender, RoutedEventArgs e)
        {
            popup_message.IsOpen = true;
        }

        private void btn_file_open_Click(object sender, RoutedEventArgs e)
        {

           OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All Media Files|*.wav;*.aac;*.wma;*.wmv;*.avi;*.mpg;*.mpeg;*.m1v;*.mp2;*.mp3;*.mpa;*.mpe;*.m3u;*.mp4;*.mov;*.3g2;*.3gp2;*.3gp;*.3gpp;*.m4a;*.cda;*.aif;*.aifc;*.aiff;*.mid;*.midi;*.rmi;*.mkv;*.WAV;*.AAC;*.WMA;*.WMV;*.AVI;*.MPG;*.MPEG;*.M1V;*.MP2;*.MP3;*.MPA;*.MPE;*.M3U;*.MP4;*.MOV;*.3G2;*.3GP2;*.3GP;*.3GPP;*.M4A;*.CDA;*.AIF;*.AIFC;*.AIFF;*.MID;*.MIDI;*.RMI;*.MKV";
            if(openFileDialog.ShowDialog() == true)
            {
                txt_filename.Text = openFileDialog.SafeFileName;
                //FOR FULL PATH USE THIS SHIT <openFileDialog.FileName>;
            }
        }

        private void listbox_card_video_Drop(object sender, DragEventArgs e)
        {
            bool hasNoError = false;
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            var path = files[0];
            var videoExtension = System.IO.Path.GetExtension(path);
            var dirName = System.IO.Path.GetFileName(path);
            if (files != null && files.Length != 0)
            {
                for(int x= 0; x < fileExtension.Length-1; x++)
                {
                    if (videoExtension == fileExtension[x])
                    {
                        hasNoError = true;
                    }
                }
                if (hasNoError)
                {
                    txt_filename.Text = dirName;
                    //txt_path.Text = files[0];
                    popup_message.IsOpen = true;
                }
                else
                {
                    MessageBox.Show("File not supported!");
                }
            }
            
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            cleanModalUncheckAllandRefresh();
            //txt_filename.Text = "";
            //btn_save.Content = "Save";
            //btn_delete.Visibility = Visibility.Collapsed;

            //updateIsClicked = false;
            //popup_message.IsOpen = false;
            //refreshModal();
            //unCheckAll();
            //checkBoxStatus();


        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {

                if (txt_filename.Text.Trim() != string.Empty)
                {
                    if (date_fromDate.SelectedDate != null && date_toDate.SelectedDate != null)
                    {

                        dateChecker((DateTime)date_fromDate.SelectedDate, (DateTime)date_toDate.SelectedDate);
                        if (dateCheckStatus)
                        {
                            MessageBox.Show("From Date Should Be Less Than To Date");
                            dateCheckStatus = false;
                        }
                        else
                        {
                            if (timeCheckResponse() == "No Days is Checked!")
                            {
                                MessageBox.Show(timeCheckResponse());
                            }
                            else if (timeCheckResponse() == "")
                            {
                                Database_Functions db = new Database_Functions();

                                if (updateIsClicked)
                                {

                                    db.scheduleUpdate(txt_filename.Text, getTimestampDate((DateTime)date_fromDate.SelectedDate), getTimestampDate((DateTime)date_toDate.SelectedDate), element.id);
                                  
                                if (check_allDays.IsChecked == true)
                                {

                                    db.updateTime(getTimestampTime((DateTime)time_fromTime_all.SelectedTime), getTimestampTime((DateTime)time_toTime_all.SelectedTime), 8, element.id);
                                }
                                else
                                {
                                    db.deleteDays(8, element.id);
                                }

                                //monday
                                if (check_monday.IsChecked == true)
                                {
                                    db.updateTime(getTimestampTime((DateTime)time_fromTime_monday.SelectedTime), getTimestampTime((DateTime)time_toTime_monday.SelectedTime), 1, element.id);
                                }
                                else
                                {
                                    db.deleteDays(1, element.id);
                                }

                                //tuesday
                                if (check_tuesday.IsChecked == true)
                                {
                                    db.updateTime(getTimestampTime((DateTime)time_fromTime_tuesday.SelectedTime), getTimestampTime((DateTime)time_toTime_tuesday.SelectedTime), 2, element.id);
                                }
                                else
                                {
                                    db.deleteDays(2, element.id);
                                }

                                //wednesday
                                if (check_wednesday.IsChecked == true)
                                {
                                    db.updateTime(getTimestampTime((DateTime)time_fromTime_wednesday.SelectedTime), getTimestampTime((DateTime)time_toTime_wednesday.SelectedTime), 3, element.id);
                                }
                                else
                                {
                                    db.deleteDays(3, element.id);
                                }

                                //thursday
                                if (check_thursday.IsChecked == true)
                                {
                                    db.updateTime(getTimestampTime((DateTime)time_fromTime_thursday.SelectedTime), getTimestampTime((DateTime)time_toTime_thursday.SelectedTime), 4, element.id);
                                }
                                else
                                {
                                    db.deleteDays(4, element.id);
                                }

                                //friday
                                if (check_friday.IsChecked == true)
                                {
                                    db.updateTime(getTimestampTime((DateTime)time_fromTime_friday.SelectedTime), getTimestampTime((DateTime)time_toTime_friday.SelectedTime), 5, element.id);
                                }
                                else
                                {
                                    db.deleteDays(5, element.id); 
                                }

                                //satruday
                                if (check_saturday.IsChecked == true)
                                {
                                    db.updateTime(getTimestampTime((DateTime)time_fromTime_saturday.SelectedTime), getTimestampTime((DateTime)time_toTime_saturday.SelectedTime), 6, element.id);
                                }
                                else
                                {
                                    db.deleteDays(6, element.id);
                                }

                                //sunday
                                if (check_sunday.IsChecked == true)
                                {
                                    db.updateTime(getTimestampTime((DateTime)time_fromTime_sunday.SelectedTime), getTimestampTime((DateTime)time_toTime_sunday.SelectedTime), 7, element.id);
                                }
                                else
                                {
                                    db.deleteDays(7, element.id);
                                }
                                //LAGE ME KENI ITANG VIDEO

                                DataContext = db.getSchedule();
                                this.listbox_card_video.Items.Refresh();
                                cleanModalUncheckAllandRefresh();
                                //btn_delete.Visibility = Visibility.Collapsed;
                                //txt_filename.Text = "";
                                //popup_message.IsOpen = false;
                                //updateIsClicked = false;
                                //refreshModal();

                            }
                                else
                                {
                                    long schedLastId = db.saveSchedule(txt_filename.Text, getTimestampDate((DateTime)date_fromDate.SelectedDate), getTimestampDate((DateTime)date_toDate.SelectedDate));
                                    List<int> days = checkBoxStatus();

                                    foreach (var item in days)
                                    {
                                        switch (item)
                                        {
                                            case (1):
                                                db.saveTime(getTimestampTime((DateTime)time_fromTime_monday.SelectedTime), getTimestampTime((DateTime)time_toTime_monday.SelectedTime), 1, (int)schedLastId);
                                                break;
                                            case (2):
                                                db.saveTime(getTimestampTime((DateTime)time_fromTime_tuesday.SelectedTime), getTimestampTime((DateTime)time_toTime_tuesday.SelectedTime), 2, (int)schedLastId);
                                                break;
                                            case (3):
                                                db.saveTime(getTimestampTime((DateTime)time_fromTime_wednesday.SelectedTime), getTimestampTime((DateTime)time_toTime_wednesday.SelectedTime), 3, (int)schedLastId);
                                                break;
                                            case (4):
                                                db.saveTime(getTimestampTime((DateTime)time_fromTime_thursday.SelectedTime), getTimestampTime((DateTime)time_toTime_thursday.SelectedTime), 4, (int)schedLastId);
                                                break;
                                            case (5):
                                                db.saveTime(getTimestampTime((DateTime)time_fromTime_friday.SelectedTime), getTimestampTime((DateTime)time_toTime_friday.SelectedTime), 5, (int)schedLastId);
                                                break;
                                            case (6):
                                                db.saveTime(getTimestampTime((DateTime)time_fromTime_saturday.SelectedTime), getTimestampTime((DateTime)time_toTime_saturday.SelectedTime), 6, (int)schedLastId);
                                                break;
                                            case (7):
                                                db.saveTime(getTimestampTime((DateTime)time_fromTime_sunday.SelectedTime), getTimestampTime((DateTime)time_toTime_sunday.SelectedTime), 7, (int)schedLastId);
                                                break;
                                            case (8):
                                                db.saveTime(getTimestampTime((DateTime)time_fromTime_all.SelectedTime), getTimestampTime((DateTime)time_toTime_all.SelectedTime), 8, (int)schedLastId);
                                                break;
                                        }
                                    }
                                //LAGE ME KENI ITANG VIDEO

                                
                                DataContext = db.getSchedule();
                                this.listbox_card_video.Items.Refresh();
                                cleanModalUncheckAllandRefresh();
                                //txt_filename.Text = "";
                                //popup_message.IsOpen = false;
                                //updateIsClicked = false;
                                //btn_delete.Visibility = Visibility.Collapsed;
                                //refreshModal();
                            }
                               

                            }
                            else
                            {
                                MessageBox.Show("Please fill in fromTime/toTime at " + timeCheckResponse() + "Time");
                            }
                        }


                    }
                    else
                    {
                        MessageBox.Show("Please fill in fromDate/toDate");
                    }
                }
                else
                {
                    MessageBox.Show("Please attached a video");
                }
            
  
        }

        private string timeCheckResponse()
        {
            string response = "";
            List<int> days = checkBoxStatus();

            if(days.Count == 0)
            {
                response = "No Days is Checked!";
            }
            else
            {
                foreach (int day in days)
                {
                    switch (day)
                    {
                        case (1):
                            if (!timeChecker(time_fromTime_monday.SelectedTime, time_toTime_monday.SelectedTime))
                            {
                                response += "Monday ";
                            }
                            break;
                        case (2):
                            if (!timeChecker(time_fromTime_tuesday.SelectedTime, time_toTime_tuesday.SelectedTime))
                            {
                                response += "Tuesday ";
                            }
                            break;
                        case (3):
                            if (!timeChecker(time_fromTime_wednesday.SelectedTime, time_toTime_wednesday.SelectedTime))
                            {
                                response += "Wednesday ";
                            }
                            break;
                        case (4):
                            if (!timeChecker(time_fromTime_thursday.SelectedTime, time_toTime_thursday.SelectedTime))
                            {
                                response += "Thursday ";
                            }
                            break;
                        case (5):
                            if (!timeChecker(time_fromTime_friday.SelectedTime, time_toTime_friday.SelectedTime))
                            {
                                response += "Friday ";
                            }
                            break;
                        case (6):
                            if (!timeChecker(time_fromTime_saturday.SelectedTime, time_toTime_saturday.SelectedTime))
                            {
                                response += "Saturday ";
                            }
                            break;
                        case (7):
                            if (!timeChecker(time_fromTime_sunday.SelectedTime, time_toTime_sunday.SelectedTime))
                            {
                                response += "Sunday ";
                            }
                            break;
                        case (8):
                            if (!timeChecker(time_fromTime_all.SelectedTime, time_toTime_all.SelectedTime))
                            {
                                response += "All days ";
                            }
                            break;
                    }
                }
            }
            

            return response;

        }

        private string getTimestampDate(DateTime date)
        {
            string formatDate = date.ToString("yyyy-MM-dd");
            //string formatTime = time.ToString("hh:mm tt");


            string combine = formatDate;
            DateTime combineDateTime = DateTime.Parse(combine);
            var unixTimeSeconds = new DateTimeOffset(combineDateTime).ToUnixTimeSeconds();
            //System.Diagnostics.Trace.WriteLine(unixTimeSeconds);


            return unixTimeSeconds.ToString();

        }


        private string getTimestampTime(DateTime time)
        {
            //string formatDate = date.ToString("yyyy-MM-dd");
            string formatTime = time.ToString("hh:mm tt");


            string combine = formatTime;
            DateTime combineDateTime = DateTime.Parse(combine);
            var unixTimeSeconds = new DateTimeOffset(combineDateTime).ToUnixTimeSeconds();
            //System.Diagnostics.Trace.WriteLine(unixTimeSeconds);


            return unixTimeSeconds.ToString();

        }

        private DateTime convertUTCtoDT(long utc)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(utc).ToLocalTime();
            return dateTime;
        }


        private void date_fromDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            //string formatDate = date_fromDate.SelectedDate.Value.ToString("yyyy-MM-dd");

            //System.Diagnostics.Trace.WriteLine(formatDate);
        }

        private void dateChecker( DateTime fromDate, DateTime toDate)
        {
            dateCheckStatus = false;

            if(fromDate > toDate)
            {
                dateCheckStatus = true;
            }
        }

        private bool timeChecker(DateTime? fromTime , DateTime? toTime)
        {
            bool status = false;
            if(fromTime != null && toTime != null)
            {
                status = true;
            }

            return status;
        }


        //Days Clicked
        private void check_allDays_Click(object sender, RoutedEventArgs e)
        {
            checkBoxStatus();
        }

        private void check_monday_Click(object sender, RoutedEventArgs e)
        {
            checkBoxStatus();
        }

        private void check_tuesday_Click(object sender, RoutedEventArgs e)
        {
            checkBoxStatus();
        }

        private void check_wednesday_Click(object sender, RoutedEventArgs e)
        {
            checkBoxStatus();
        }

        private void check_thursday_Click(object sender, RoutedEventArgs e)
        {
            checkBoxStatus();
        }

        private void check_friday_Click(object sender, RoutedEventArgs e)
        {
            checkBoxStatus();
        }

        private void check_saturday_Click(object sender, RoutedEventArgs e)
        {
            checkBoxStatus();
        }

        private void check_sunday_Click(object sender, RoutedEventArgs e)
        {
            checkBoxStatus();
        }

        private List<int> checkBoxStatus()
        {
            List<int> checkedDays = new List<int>();
            //all
            if (check_allDays.IsChecked == true)
            {
                checkedDays.Add(8);
                disableAllDays();
                time_fromTime_all.IsEnabled = true;
                time_toTime_all.IsEnabled = true;
            }
            else
            {
                enableAllDays();
                time_fromTime_all.IsEnabled = false;
                time_toTime_all.IsEnabled = false;
            }

            //monday
            if(check_monday.IsChecked == true)
            {
                checkedDays.Add(1);
                time_fromTime_monday.IsEnabled = true;
                time_toTime_monday.IsEnabled= true;
            }
            else
            {
                time_fromTime_monday.IsEnabled = false;
                time_toTime_monday.IsEnabled = false;
            }

            //tuesday
            if (check_tuesday.IsChecked == true)
            {
                checkedDays.Add(2);
                time_fromTime_tuesday.IsEnabled = true;
                time_toTime_tuesday.IsEnabled = true;
            }
            else
            {
                time_fromTime_tuesday.IsEnabled = false;
                time_toTime_tuesday.IsEnabled = false;
            }

            //wednesday
            if (check_wednesday.IsChecked == true)
            {
                checkedDays.Add(3);
                time_fromTime_wednesday.IsEnabled = true;
                time_toTime_wednesday.IsEnabled = true;
            }
            else
            {
                time_fromTime_wednesday.IsEnabled = false;
                time_toTime_wednesday.IsEnabled = false;
            }

            //thursday
            if (check_thursday.IsChecked == true)
            {
                checkedDays.Add(4);
                time_fromTime_thursday.IsEnabled = true;
                time_toTime_thursday.IsEnabled = true;
            }
            else
            {
                time_fromTime_thursday.IsEnabled = false;
                time_toTime_thursday.IsEnabled = false;
            }

            //friday
            if (check_friday.IsChecked == true)
            {
                checkedDays.Add(5);
                time_fromTime_friday.IsEnabled = true;
                time_toTime_friday.IsEnabled = true;
            }
            else
            {
                time_fromTime_friday.IsEnabled = false;
                time_toTime_friday.IsEnabled = false;
            }

            //satruday
            if (check_saturday.IsChecked == true)
            {
                checkedDays.Add(6);
                time_fromTime_saturday.IsEnabled = true;
                time_toTime_saturday.IsEnabled = true;
            }
            else
            {
                time_fromTime_saturday.IsEnabled = false;
                time_toTime_saturday.IsEnabled = false;
            }

            //sunday
            if (check_sunday.IsChecked == true)
            {
                checkedDays.Add(7);
                time_fromTime_sunday.IsEnabled = true;
                time_toTime_sunday.IsEnabled = true;
            }
            else
            {
                time_fromTime_sunday.IsEnabled = false;
                time_toTime_sunday.IsEnabled = false;
            }

            return checkedDays;
        }

        private void disableAllDays()
        {
            check_monday.IsChecked = false;
            day_monday.IsEnabled = false;
            day_monday.Visibility = Visibility.Collapsed;

            check_tuesday.IsChecked = false;
            day_tuesday.IsEnabled = false;
            day_tuesday.Visibility = Visibility.Collapsed;

            check_wednesday.IsChecked = false;
            day_wednesday.IsEnabled = false;
            day_wednesday.Visibility = Visibility.Collapsed;

            check_thursday.IsChecked = false;
            day_thrusday.IsEnabled = false;
            day_thrusday.Visibility = Visibility.Collapsed;

            check_friday.IsChecked = false;
            day_friday.IsEnabled = false;
            day_friday.Visibility = Visibility.Collapsed;

            check_saturday.IsChecked = false;
            day_saturday.IsEnabled = false;
            day_saturday.Visibility = Visibility.Collapsed;

            check_sunday.IsChecked = false;
            day_sunday.IsEnabled = false;
            day_sunday.Visibility = Visibility.Collapsed;

        }

        private void enableAllDays()
        {
            visibilityAll();
            day_monday.IsEnabled = true;

            day_tuesday.IsEnabled = true;

            day_wednesday.IsEnabled = true;

            day_thrusday.IsEnabled = true;

            day_friday.IsEnabled = true;

            day_saturday.IsEnabled = true;

            day_sunday.IsEnabled = true;

        }

        //Time Changed All days
        private void time_fromTime_all_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            if(time_fromTime_all.SelectedTime == null)
            {
                return;
            }
            else
            {
                time_fromTime_all.SelectedTime = timeValidation(time_fromTime_all.SelectedTime, time_toTime_all.SelectedTime, 1);
            }
            

        }

        private void time_toTime_all_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            if (time_toTime_all.SelectedTime == null)
            {
                return;
            }
            else
            {
                time_toTime_all.SelectedTime = timeValidation(time_fromTime_all.SelectedTime, time_toTime_all.SelectedTime, 2);
            }
            
        }

        //Time Changed Monday
        private void time_fromTime_monday_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            if (time_fromTime_monday.SelectedTime == null)
            {
                return;
            }
            else
            {
                time_fromTime_monday.SelectedTime = timeValidation(time_fromTime_monday.SelectedTime, time_toTime_monday.SelectedTime, 1);
            }
        }

        private void time_toTime_monday_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            if (time_toTime_monday.SelectedTime == null)
            {
                return;
            }
            else
            {
                time_toTime_monday.SelectedTime = timeValidation(time_fromTime_monday.SelectedTime, time_toTime_monday.SelectedTime, 2);
            }
            
        }

        //Time Changed Tuesday

        private void time_fromTime_tuesday_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            if (time_fromTime_tuesday.SelectedTime == null)
            {
                return;
            }
            else
            {
                time_fromTime_tuesday.SelectedTime = timeValidation(time_fromTime_tuesday.SelectedTime, time_toTime_tuesday.SelectedTime, 1);
            }
            
        }

        private void time_toTime_tuesday_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            if (time_toTime_tuesday.SelectedTime == null)
            {
                return;
            }
            else
            {
                time_toTime_tuesday.SelectedTime = timeValidation(time_fromTime_tuesday.SelectedTime, time_toTime_tuesday.SelectedTime, 2);
            }
            
        }

        //Time Changed Wednesday

        private void time_fromTime_wednesday_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            if (time_fromTime_wednesday.SelectedTime == null)
            {
                return;
            }
            else
            {
                time_fromTime_wednesday.SelectedTime = timeValidation(time_fromTime_wednesday.SelectedTime, time_toTime_wednesday.SelectedTime, 1);
            }
            
        }

        private void time_toTime_wednesday_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            if (time_toTime_wednesday.SelectedTime == null)
            {
                return;
            }
            else
            {
                time_toTime_wednesday.SelectedTime = timeValidation(time_fromTime_wednesday.SelectedTime, time_toTime_wednesday.SelectedTime, 2);
            }
            
        }

        //Time Changed Thursday

        private void time_fromTime_thursday_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            if (time_fromTime_thursday.SelectedTime == null)
            {
                return;
            }
            else
            {
                time_fromTime_thursday.SelectedTime = timeValidation(time_fromTime_thursday.SelectedTime, time_toTime_thursday.SelectedTime, 1);
            }
            
        }

        private void time_toTime_thursday_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            if (time_toTime_thursday.SelectedTime == null)
            {
                return;
            }
            else
            {
                time_toTime_thursday.SelectedTime = timeValidation(time_fromTime_thursday.SelectedTime, time_toTime_thursday.SelectedTime, 2);
            }
            
        }

        //Time Changed Friday

        private void time_fromTime_friday_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            if (time_fromTime_friday.SelectedTime == null)
            {
                return;
            }
            else
            {
                time_fromTime_friday.SelectedTime = timeValidation(time_fromTime_friday.SelectedTime, time_toTime_friday.SelectedTime, 1);
            }
            
        }

        private void time_toTime_friday_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            if (time_toTime_friday.SelectedTime == null)
            {
                return;
            }
            else
            {
                time_toTime_friday.SelectedTime = timeValidation(time_fromTime_friday.SelectedTime, time_toTime_friday.SelectedTime, 2);
            }
            
        }

        //Time Changed Saturday

        private void time_fromTime_saturday_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            if (time_fromTime_saturday.SelectedTime == null)
            {
                return;
            }
            else
            {
                time_fromTime_saturday.SelectedTime = timeValidation(time_fromTime_saturday.SelectedTime, time_toTime_saturday.SelectedTime, 1);
            }
            
        }

        private void time_toTime_saturday_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            if (time_toTime_saturday.SelectedTime == null)
            {
                return;
            }
            else
            {
                time_toTime_saturday.SelectedTime = timeValidation(time_fromTime_saturday.SelectedTime, time_toTime_saturday.SelectedTime, 2);
            }
            
        }

        //Time Changed Sunday

        private void time_fromTime_sunday_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            if (time_fromTime_sunday.SelectedTime == null)
            {
                return;
            }
            else
            {
                time_fromTime_sunday.SelectedTime = timeValidation(time_fromTime_sunday.SelectedTime, time_toTime_sunday.SelectedTime, 1);
            }
            
        }

        private void time_toTime_sunday_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            if (time_toTime_sunday.SelectedTime == null)
            {
                return;
            }
            else
            {
                time_toTime_sunday.SelectedTime = timeValidation(time_fromTime_sunday.SelectedTime, time_toTime_sunday.SelectedTime, 2);
            }
            
        }

        private DateTime timeValidation(DateTime? from, DateTime? to , int cat)
        {
            DateTime holder = DateTime.Now;

            if (from != null && to != null)
            {
                switch (cat)
                {
                    case (1):
                        if (from > to)
                        {
                            MessageBox.Show("From Time Should Be Less Than To Time");
                            holder = to.Value.AddMinutes(-1);

                        }
                        else
                        {
                            holder = (DateTime)from;

                        }
                        break;
                    case (2):
                        if (from > to)
                        {
                            MessageBox.Show("From Time Should Be Less Than To Time");
                            holder = from.Value.AddMinutes(1);
                        }
                        else
                        {
                            holder = (DateTime)to;
                        }
                        break;

                }
                return holder;
            }
            else
            {
                switch (cat)
                {
                    case (0):
                        break;
                    case(1):
                        holder =  (DateTime)from;
                        break;
                    case(2):
                        holder = (DateTime)to;
                        break;
                }
            }
            return holder;
        
        }

        private void btn_videos_Click(object sender, RoutedEventArgs e)
        {
            updateIsClicked = true;
            btn_save.Content = "Update";
            btn_delete.Visibility = Visibility.Visible;

            Button? btn = sender as Button;


            if (btn != null)
            {
                object item = btn.DataContext;


                if (item != null)
                {
                    index = this.listbox_card_video.Items.IndexOf(item);
                    element = (VideoListItem)this.listbox_card_video.Items[index];

                    DateTime fromDate = convertUTCtoDT((long)Convert.ToInt32(element.tfrom));
                    DateTime endDate = convertUTCtoDT((long)Convert.ToInt32(element.tto));

                    date_fromDate.SelectedDate = fromDate;
                    date_toDate.SelectedDate = endDate;

                    txt_filename.Text = element.title;

                    Database_Functions db = new Database_Functions();
                    
                    
                    tempTimeList = db.getTimeListOf(element.id);



                    foreach (TimeListItem itemtime in tempTimeList)
                    {
                        DateTime from =  convertUTCtoDT((long)Convert.ToInt32(itemtime.from_time));
                        DateTime end = convertUTCtoDT((long)Convert.ToInt32(itemtime.to_time));

                        setCheckBox(from, end, itemtime.day);


                    }

                    popup_message.IsOpen = true;
                    System.Diagnostics.Trace.WriteLine(element.title);

                }
            }
        }


        private void setCheckBox(DateTime fromTimeDT,DateTime endTimeDT ,int day)
        {

        
            if(day == 8)
            {
                check_allDays.IsChecked = true;
                time_fromTime_all.IsEnabled = true;
                time_toTime_all.IsEnabled = true;
                time_fromTime_all.SelectedTime = fromTimeDT;
                time_toTime_all.SelectedTime = endTimeDT;
                checkBoxStatus();
            }
            else
            {
                check_allDays.IsChecked = false;
                time_fromTime_all.IsEnabled = false;
                time_toTime_all.IsEnabled = false;
                visibilityAll();

                switch (day)
                {
                    case (1):
                        check_monday.IsChecked = true;
                        time_fromTime_monday.IsEnabled = true;
                        time_toTime_monday.IsEnabled = true;
                        time_fromTime_monday.SelectedTime = fromTimeDT;
                        time_toTime_monday.SelectedTime = endTimeDT;
                        break;
                    case (2):
                        check_tuesday.IsChecked = true;
                        time_fromTime_tuesday.IsEnabled = true;
                        time_toTime_tuesday.IsEnabled = true;
                        time_fromTime_tuesday.SelectedTime = fromTimeDT;
                        time_toTime_tuesday.SelectedTime = endTimeDT;
                        break;
                    case (3):
                        check_wednesday.IsChecked = true;
                        time_fromTime_wednesday.IsEnabled = true;
                        time_toTime_wednesday.IsEnabled = true;
                        time_fromTime_wednesday.SelectedTime = fromTimeDT;
                        time_toTime_wednesday.SelectedTime = endTimeDT;
                        break;
                    case (4):
                        check_thursday.IsChecked = true;
                        time_fromTime_thursday.IsEnabled = true;
                        time_toTime_thursday.IsEnabled = true;
                        time_fromTime_thursday.SelectedTime = fromTimeDT;
                        time_toTime_thursday.SelectedTime = endTimeDT;
                        break;
                    case (5):
                        check_friday.IsChecked = true;
                        time_fromTime_friday.IsEnabled = true;
                        time_toTime_friday.IsEnabled = true;
                        time_fromTime_friday.SelectedTime = fromTimeDT;
                        time_toTime_friday.SelectedTime = endTimeDT;
                        break;
                    case (6):
                        check_saturday.IsChecked = true;
                        time_fromTime_saturday.IsEnabled = true;
                        time_toTime_saturday.IsEnabled = true;
                        time_fromTime_saturday.SelectedTime = fromTimeDT;
                        time_toTime_saturday.SelectedTime = endTimeDT;
                        break;
                    case (7):
                        check_sunday.IsChecked = true;
                        time_fromTime_sunday.IsEnabled = true;
                        time_toTime_sunday.IsEnabled = true;
                        time_fromTime_sunday.SelectedTime = fromTimeDT;
                        time_toTime_sunday.SelectedTime = endTimeDT;
                        break;
                }

                checkBoxStatus();
            }

          
        }

        private void unCheckAll()
        {
            check_allDays.IsChecked = false;

            check_monday.IsChecked = false;

            check_tuesday.IsChecked = false;

            check_wednesday.IsChecked = false;

            check_thursday.IsChecked = false;

            check_friday.IsChecked = false;

            check_saturday.IsChecked = false;

            check_sunday.IsChecked = false;

        }

        private void visibilityAll()
        {

            day_monday.Visibility = Visibility.Visible;

            day_tuesday.Visibility = Visibility.Visible;


            day_wednesday.Visibility = Visibility.Visible;

            day_thrusday.Visibility = Visibility.Visible;


            day_friday.Visibility = Visibility.Visible;


            day_saturday.Visibility = Visibility.Visible;

            day_sunday.Visibility = Visibility.Visible;
        }

        private void refreshModal()
        {
            date_fromDate.SelectedDate = null;
            date_toDate.SelectedDate = null;

            time_fromTime_all.SelectedTime = null;
            time_toTime_all.SelectedTime = null;

            time_fromTime_monday.SelectedTime = null;
            time_toTime_monday.SelectedTime = null;

            time_fromTime_tuesday.SelectedTime = null;
            time_toTime_tuesday.SelectedTime = null;

            time_fromTime_wednesday.SelectedTime = null;
            time_toTime_wednesday.SelectedTime = null;

            time_fromTime_thursday.SelectedTime = null;
            time_toTime_thursday.SelectedTime = null;

            time_fromTime_friday.SelectedTime = null;
            time_toTime_friday.SelectedTime = null;

            time_fromTime_saturday.SelectedTime = null;
            time_toTime_saturday.SelectedTime = null;

            time_fromTime_sunday.SelectedTime = null;
            time_toTime_sunday.SelectedTime = null;

        }

        private void cleanModalUncheckAllandRefresh()
        {
            txt_filename.Text = "";
            btn_save.Content = "Save";
            btn_delete.Visibility = Visibility.Collapsed;

            updateIsClicked = false;
            popup_message.IsOpen = false;
            refreshModal();
            unCheckAll();
            checkBoxStatus();
        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = MessageBox.Show("You are about to delete " +element.title+".", "Are you sure?", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                Database_Functions db = new Database_Functions();
                db.deleteSched(element.id);
                db.deleteDays(element.id);


                DataContext = db.getSchedule();
                this.listbox_card_video.Items.Refresh();
                cleanModalUncheckAllandRefresh();

            }
            else if (dialogResult == MessageBoxResult.No)
            {
                //do nothing
            }
        }
    }
}
