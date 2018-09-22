using organizer.Codes;
using organizer.Codes.Database;
using organizer.Views.Dialogs;
using System;
using System.Windows;
using System.Windows.Controls;

namespace organizer.Views.Pages {
    /// <summary>
    /// Interaction logic for FolderLook.xaml
    /// </summary>
    ///
    public partial class PageFolderLook : Page
    {
        private TaskFolder taskFolder;

        public PageFolderLook(TaskFolder tf)
        {
            InitializeComponent();
            this.taskFolder = tf;
        }

        private void butGoTo_Click(object sender, RoutedEventArgs e) {
            TaskView win2 = new TaskView(taskFolder);
            win2.Show();
            Window owner = Window.GetWindow(this);
            owner.Hide();
            win2.Closing += (s, ev) => owner.Show();
        }

        private void butDone_Click(object sender, RoutedEventArgs e) {
            taskFolder.status = Status.DONE;
            DatabaseTaskFolder.UpdateTaskFolder(taskFolder);
            OnAnyChange(EventArgs.Empty);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e) {
            repaint();
        }

        private void butView_Click(object sender, RoutedEventArgs e) {
            TaskView tv = new TaskView(taskFolder);
            tv.Show();
        }

        private void butRestore_Click(object sender, RoutedEventArgs e) {
            taskFolder.status = Status.TODO;
            DatabaseTaskFolder.UpdateTaskFolder(taskFolder);
            OnAnyChange(EventArgs.Empty);
        }

        private void butRestoreD_Click(object sender, RoutedEventArgs e) {
            taskFolder.status = Status.DONE;
            DatabaseTaskFolder.UpdateTaskFolder(taskFolder);
            OnAnyChange(EventArgs.Empty);
        }

        //all in
        private void repaint() {
            txtBoxTitle.Text = taskFolder.text;

            //clear
            panel.Children.Remove(butGoTo);
            panel.Children.Remove(butView);
            panel.Children.Remove(butRestore);
            panel.Children.Remove(butRestoreD);
            panel.Children.Remove(butDone);
            panel.Children.Remove(butDelete);

            if (taskFolder.status == Status.TODO) {
                panel.Children.Add(butDone);
                panel.Children.Add(butGoTo);
                panel.Children.Add(butDelete);
            } else if(taskFolder.status == Status.DONE) {
                panel.Children.Add(butView);
                panel.Children.Add(butDelete);
            } else { //recycle
                panel.Children.Add(butRestore);
                panel.Children.Add(butRestoreD);
                panel.Children.Add(butDelete);
            }
        }
        
        //all in
        private void butDelete_Click(object sender, RoutedEventArgs e) {
            if(taskFolder.status == Status.TRASH) {
                DatabaseTaskFolder.DeleteTaskFolder(taskFolder);
            } else {
                taskFolder.status = Status.TRASH;
                DatabaseTaskFolder.UpdateTaskFolder(taskFolder);
            }
            OnAnyChange(EventArgs.Empty);
        }
        

        public event EventHandler AnyChange;
        protected virtual void OnAnyChange(EventArgs e) {
            AnyChange?.Invoke(this, e);
        }

        private void DoubleClickOpen(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            if (e.ClickCount == 2) {
                DialogAddFolderTask win2 = new DialogAddFolderTask(taskFolder);
                win2.ShowDialog();
                repaint();
            }
        }
    }
}
