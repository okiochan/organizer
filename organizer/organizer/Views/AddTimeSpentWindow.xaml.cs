using organizer.Codes;
using organizer.Codes.Database;
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
    /// Interaction logic for AddTimeSpentWindow.xaml
    /// </summary>
    public partial class AddTimeSpentWindow : Window {
        Task task;

        public AddTimeSpentWindow(Task task) {
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
