using organizer.Codes;
using organizer.Codes.Database;
using System;
using System.Collections.Generic;
using System.Windows;

namespace organizer {
    /// <summary>
    /// Interaction logic for DialogAddTask.xaml
    /// </summary>
    public partial class DialogAddTask : Window
    {
        private Priority prio;
        private TaskFolder tf;

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

        private void butApply_Click(object sender, RoutedEventArgs e) {

            String text = "unnown title";
            if (txtBoxTitle.Text != "") {
                text = txtBoxTitle.Text;
            }

            //DEADLINE????
            int timeM = 0;

            int n;
            if (int.TryParse(txtBoxD.Text, out n) == true) {
                timeM += Int32.Parse(txtBoxD.Text) * 24 * 60;
            }
            if (int.TryParse(txtBoxH.Text, out n) == true) {
                timeM += Int32.Parse(txtBoxH.Text) * 60;
            }
            if (int.TryParse(txtBoxM.Text, out n) == true) {
                timeM += Int32.Parse(txtBoxM.Text);
            }

            DatabaseTask.CreateNewTask(text, prio, Status.TODO, DateTime.Now, tf);
            //repaint
            EventOnButtonClicked(EventArgs.Empty);
            this.DialogResult = true;
        }


        private void butLow_Click(object sender, RoutedEventArgs e) {
            prio = Priority.LOW;
        }

        private void butMid_Click(object sender, RoutedEventArgs e) {
            prio = Priority.MID;
        }

        private void butHight_Click(object sender, RoutedEventArgs e) {
            prio = Priority.HIGH;
        }
    }
}
