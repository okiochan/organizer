using organizer.Codes;
using organizer.Codes.Database;
using System;
using System.Windows;

namespace organizer.Views.Dialogs {
    /// <summary>
    /// Interaction logic for DialogAddFolderTask.xaml
    /// </summary>
    public partial class DialogAddFolderTask : Window {

        // if task folder is null we create a new one
        // otherwise we edit the given task folder
        public DialogAddFolderTask(TaskFolder toEdit) {
            InitializeComponent();
            this.toEdit = toEdit;
        }

        public event EventHandler ApplyClicked;
        protected virtual void OnApplyButtonClicked(EventArgs e) {
            ApplyClicked?.Invoke(this, e);
        }

        private void butApply_Click(object sender, RoutedEventArgs e) {
            string title = txtBoxTitle.Text;
            if(title == "") {
                title = "unnown title";
            }

            //Dinash add Start End date to folder

            if(toEdit == null) {
                DatabaseTaskFolder.CreateNewTaskFolder(title);
            } else {
                toEdit.text = title;
                DatabaseTaskFolder.UpdateTaskFolder(toEdit) ;
            }
            OnApplyButtonClicked(EventArgs.Empty);
            DialogResult = true;
        }

        private TaskFolder toEdit = null;
    }
}
