using organizer.Codes;
using organizer.Codes.Database;
using organizer.Events;
using organizer.Views;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace organizer {
    /// <summary>
    /// Interaction logic for DialogAddTask.xaml
    /// </summary>
    public partial class DialogAddTask : Window
    {
        private Priority prio;
        private TaskFolder tf;
        private DateTime? dateStart, dateEnd;
        private PageClock pageClock;

        public DialogAddTask(TaskFolder tf)
        {
            InitializeComponent();

            prio = Priority.LOW;
            this.tf = tf;

            pageClock = new PageClock();
            pageClock.HandlerSetTime += EventSetClockTime;
            frameClock.Navigate(pageClock);
        }

        //HANDLER
        public event EventHandler HandlerButApplyClick;
        protected virtual void EventOnButtonClicked(EventArgs e) {
            EventHandler handler = HandlerButApplyClick;
            if (handler != null) {
                handler(this, e);
            }
        }

        //EVENT set time
        private void EventSetClockTime(object sender, TimeEventArgs e) {
            string H = e.getH;
            string M = e.getM;
            if(H.Length < 2) {
                H = "0" + H;
            }
            btnH.Content = "H: "+H;
            btnM.Content = "M: "+M;

            //DINASH add time to DB
        }

        //init task properties
        private void butApply_Click(object sender, RoutedEventArgs e) {

            String text = "unnown title";
            if (txtBoxTitle.Text != "") {
                text = txtBoxTitle.Text;
            }

            DatabaseTask.CreateNewTask(text, prio, Status.TODO, DateTime.Now, DateTime.Now, tf);
            //repaint
            EventOnButtonClicked(EventArgs.Empty);
            this.DialogResult = true;
        }


        private void butLow_Click(object sender, RoutedEventArgs e) {
            prio = Priority.LOW;
            butLow.Background = Brushes.Yellow;
            butMid.Background = Brushes.Gray;
            butHight.Background = Brushes.Gray;
        }

        private void butMid_Click(object sender, RoutedEventArgs e) {
            prio = Priority.MID;
            butLow.Background = Brushes.Gray;
            butMid.Background = Brushes.Orange;
            butHight.Background = Brushes.Gray;
        }

        private void butHight_Click(object sender, RoutedEventArgs e) {
            prio = Priority.HIGH;
            butLow.Background = Brushes.Gray;
            butMid.Background = Brushes.Gray;
            butHight.Background = Brushes.Red;
        }

        //calendar
        private void calendarStart_SelectedDatesChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) {
            dateStart = calendarStart.SelectedDate;
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

        private void calendarEnd_SelectedDatesChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) {
            dateEnd = calendarEnd.SelectedDate;
        }

        //clock

    }
}
