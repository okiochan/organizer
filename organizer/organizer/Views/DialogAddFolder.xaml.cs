using organizer.Codes;
using organizer.Codes.Database;
using System;
using System.Windows;

namespace organizer {
    /// <summary>
    /// Interaction logic for DialogAddFolderTask.xaml
    /// </summary>
    public partial class DialogAddFolderTask : Window {

        private DateTime? dateStart, dateEnd;

        public DialogAddFolderTask() {
            InitializeComponent();
        }

        public event EventHandler HandlerButApplyClick;
        protected virtual void EventOnButtonClicked(EventArgs e) {
            EventHandler handler = HandlerButApplyClick;
            if (handler != null) {
                handler(this, e);
            }
        }

        private void butApply_Click(object sender, RoutedEventArgs e) {
            String title = txtBoxTitle.Text;
            if(title == "") {
                title = "unnown title";
            }

            //Dinash add Start End date to folder

            DatabaseTaskFolder.CreateNewTaskFolder(title);
            EventOnButtonClicked(EventArgs.Empty);
            this.DialogResult = true;
        }

        private void calendarStart_SelectedDatesChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) {
            dateStart = calendarStart.SelectedDate;
        }

        private void calendarEnd_SelectedDatesChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) {
            dateEnd = calendarEnd.SelectedDate;
        }
    }
}
