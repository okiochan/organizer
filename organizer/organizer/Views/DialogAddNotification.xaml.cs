using organizer.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace organizer.Views {
    /// <summary>
    /// Interaction logic for DialogAddNotification.xaml
    /// </summary>
    public partial class DialogAddNotification : Window {

        private PageClock pageClock;

        public DialogAddNotification() {
            InitializeComponent();

            pageClock = new PageClock();
            pageClock.HandlerSetTime += EventSetClockTime;
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

        private void btnClockClick(object sender, RoutedEventArgs e) {

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
            this.DialogResult = true;
        }
    }
}
