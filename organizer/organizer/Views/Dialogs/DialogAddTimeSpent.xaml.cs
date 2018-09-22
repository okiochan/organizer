using organizer.Codes;
using organizer.Codes.Database;
using System;
using System.Windows;

namespace organizer.Views.Dialogs {
    /// <summary>
    /// Interaction logic for AddTimeSpentWindow.xaml
    /// </summary>
    public partial class DialogAddTimeSpent : Window {
        Task task;

        public DialogAddTimeSpent(Task task) {
            InitializeComponent();
            this.task = task;
        }

        private void button_Click(object sender, RoutedEventArgs e) {
            string hoursText = hoursBox.Text;
            string minutesText = minutesBox.Text;

            int minutes;
            int hours;
            if (!Int32.TryParse(hoursText, out hours)) {
                MessageBox.Show("Cannot process hours. Front format.");
                return;
            }
            if (!Int32.TryParse(minutesText, out minutes)) {
                MessageBox.Show("Cannot process minutes. Front format.");
                return;
            }

            int addTimeSpent = hours * 60 + minutes;
            task.timeSpent += addTimeSpent;
            DatabaseTask.UpdateTask(task);
            Close();
        }
    }
}
