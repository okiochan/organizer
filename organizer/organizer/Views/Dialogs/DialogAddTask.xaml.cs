using organizer.Codes;
using System.Windows;
using System.Windows.Media;

namespace organizer.Views.Dialogs {
    /// <summary>
    /// Interaction logic for DialogAddTask.xaml
    /// </summary>
    public partial class DialogAddTask : Window
    {
        private Task task;
        public DialogAddTask(Task task)
        {
            InitializeComponent();
            this.task = task;
        }
       
        //init task properties
        private void butApply_Click(object sender, RoutedEventArgs e) {

            task.text = txtBoxTitle.Text;
            if(calendarStart.SelectedDate.HasValue) {
                task.startdate = calendarStart.SelectedDate.Value;
            }
            if (calendarEnd.SelectedDate.HasValue) {
                task.deadline = calendarEnd.SelectedDate.Value;
            }
            
            //repaint
            this.DialogResult = true;
        }


        private void butLow_Click(object sender, RoutedEventArgs e) {
            setPrioL();
        }

        private void butMid_Click(object sender, RoutedEventArgs e) {
            setPrioM();
        }

        private void butHight_Click(object sender, RoutedEventArgs e) {
            setPrioH();
        }
        
        private void butAddNotification_Click(object sender, RoutedEventArgs e) {
            DialogAddNotification d = new DialogAddNotification();
            if (d.ShowDialog() == true) {
            } else {
            }
        }

        private void setPrioL() {
            task.prio = Priority.LOW;
            butLow.Background = Brushes.Yellow;
            butMid.Background = Brushes.Gray;
            butHight.Background = Brushes.Gray;
        }
        private void setPrioM() {
            task.prio = Priority.MID;
            butLow.Background = Brushes.Gray;
            butMid.Background = Brushes.Orange;
            butHight.Background = Brushes.Gray;
        }
        private void setPrioH() {
            task.prio = Priority.HIGH;
            butLow.Background = Brushes.Gray;
            butMid.Background = Brushes.Gray;
            butHight.Background = Brushes.Red;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            txtBoxTitle.Text = task.text;

            if (task.prio == Priority.HIGH) {
                setPrioH();
            } else if (task.prio == Priority.MID) {
                setPrioM();
            } else {
                setPrioL();
            }
        }
    }
}
