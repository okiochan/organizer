using organizer.Codes;
using organizer.Codes.Database;
using System;
using System.Windows;

namespace organizer.Views.Dialogs {
    /// <summary>
    /// Interaction logic for AddNote.xaml
    /// </summary>
    public partial class DialogAddNote : Window {

        private TaskFolder taskFolder;

        public DialogAddNote(TaskFolder taskFolder) {
            InitializeComponent();
            this.taskFolder = taskFolder;
        }

        //Handler
        public event EventHandler ApplyClick;
        protected virtual void OnApplyClick(EventArgs e) {
            ApplyClick?.Invoke(this, e);
        }

        private void butApply_Click(object sender, RoutedEventArgs e) {
            DatabaseNote.CreateNewNote(txtNote.Text, taskFolder);
            OnApplyClick(EventArgs.Empty);
            DialogResult = true;
        }
    }
}
