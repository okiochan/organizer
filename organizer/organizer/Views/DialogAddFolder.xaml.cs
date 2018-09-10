using organizer.Codes;
using System;
using System.Windows;

namespace organizer {
    /// <summary>
    /// Interaction logic for DialogAddFolderTask.xaml
    /// </summary>
    public partial class DialogAddFolderTask : Window {
        
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

            Database.GetInstance().CreateNewTaskFolder(title);
            EventOnButtonClicked(EventArgs.Empty);
            this.DialogResult = true;
        }
        
    }
}
