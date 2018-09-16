using organizer.Codes;
using organizer.Codes.Database;
using System;
using System.Collections.Generic;
using System.Windows;
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

        public DialogAddTask(TaskFolder tf)
        {
            InitializeComponent();

            prio = Priority.LOW;
            this.tf = tf;
        }

        public event EventHandler HandlerButApplyClick;
        protected virtual void EventOnButtonClicked(EventArgs e) {
            EventHandler handler = HandlerButApplyClick;
            if (handler != null) {
                handler(this, e);
            }
        }

        //init task properties
        private void butApply_Click(object sender, RoutedEventArgs e) {

            String text = "unnown title";
            if (txtBoxTitle.Text != "") {
                text = txtBoxTitle.Text;
            }

            DateTime start = DateTime.Now;
            if(calendarStart.SelectedDate.HasValue) {
                start = calendarStart.SelectedDate.Value;
            }
            DateTime end = DateTime.Now;
            if (calendarEnd.SelectedDate.HasValue) {
                end = calendarEnd.SelectedDate.Value;
            }

            DatabaseTask.CreateNewTask(text, prio, Status.TODO, start, end, tf);
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

        private void calendarEnd_SelectedDatesChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) {
            dateEnd = calendarEnd.SelectedDate;
        }
    }
}
