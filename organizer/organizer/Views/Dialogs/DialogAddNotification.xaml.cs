using organizer.Events;
using organizer.Views.Pages;
using System;
using System.Windows;
using System.Windows.Controls;

namespace organizer.Views.Dialogs {
    /// <summary>
    /// Interaction logic for DialogAddNotification.xaml
    /// </summary>
    public partial class DialogAddNotification : Window {

        private PageClock pageClock;

        public DialogAddNotification() {
            InitializeComponent();

            pageClock = new PageClock();
            pageClock.TimeChanged += EventSetClockTime;
            frameClock.Navigate(pageClock);
        }

        //EVENT set time
        private void EventSetClockTime(object sender, TimeEventArgs e) {
            string H = e.getH;
            string M = e.getM;
            if (H.Length < 2) {
                H = "0" + H;
            }
            btnH.Content = "H: " + H;
            btnM.Content = "M: " + M;

            //DINASH add time to DB
        }

        private void btnChangeClockType_Click(object sender, RoutedEventArgs e) {

            Button btn = (Button)sender;
            switch (btn.Name) {
                case "btnH":
                    pageClock.setDrawH(true);
                    break;
                case "btnM":
                    pageClock.setDrawH(false);
                    break;
                default:
                    break;
            }
        }

        private void butApply_Click(object sender, RoutedEventArgs e) {

            DateTime start = DateTime.Now;
            if (calendar.SelectedDate.HasValue) {
                start = calendar.SelectedDate.Value;
            }
            DateTime end = DateTime.Now;
            if (calendar.SelectedDate.HasValue) {
                end = calendar.SelectedDate.Value;
            }

           //add here to DB
            DialogResult = true;
        }
    }
}
