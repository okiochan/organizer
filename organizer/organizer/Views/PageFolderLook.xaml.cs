using organizer.Codes;
using organizer.Codes.Database;
using organizer.Views;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace organizer {
    /// <summary>
    /// Interaction logic for FolderLook.xaml
    /// </summary>
    ///

    public partial class PageFolderLook : Page
    {
        private TaskFolder tf;

        public PageFolderLook(TaskFolder tf)
        {
            InitializeComponent();
            this.tf = tf;
        }

        private void butGoTo_Click(object sender, RoutedEventArgs e) {
            TaskWindow win2 = new TaskWindow(tf);
            win2.HandlerAddTask += EventButApplyClicked;
            win2.Show();
        }

        private void butDone_Click(object sender, RoutedEventArgs e) {
            DatabaseTaskFolder.UpdateTaskFolder(tf, tf.text, Status.DONE);
            EventRepaintPages(EventArgs.Empty);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e) {
            repaint();
        }

        private void butView_Click(object sender, RoutedEventArgs e) {
            TaskViewWindow tv = new TaskViewWindow(tf);
            tv.Show();
        }

        private void butRestore_Click(object sender, RoutedEventArgs e) {
            DatabaseTaskFolder.UpdateTaskFolder(tf, tf.text, Status.TODO);
            EventRepaintPages(EventArgs.Empty);
        }

        private void butRestoreD_Click(object sender, RoutedEventArgs e) {
            DatabaseTaskFolder.UpdateTaskFolder(tf, tf.text, Status.DONE);
            EventRepaintPages(EventArgs.Empty);
        }

        //all in
        private void repaint() {
            txtBoxTitle.Text = tf.text;

            //clear
            panel.Children.Remove(butGoTo);
            panel.Children.Remove(butView);
            panel.Children.Remove(butRestore);
            panel.Children.Remove(butRestoreD);
            panel.Children.Remove(butDone);
            panel.Children.Remove(butDelete);

            if (tf.status == Status.TODO) {
                panel.Children.Add(butDone);
                panel.Children.Add(butGoTo);
                panel.Children.Add(butDelete);
            } else if(tf.status == Status.DONE) {
                panel.Children.Add(butView);
                panel.Children.Add(butDelete);
            } else { //recycle
                panel.Children.Add(butRestore);
                panel.Children.Add(butRestoreD);
                panel.Children.Add(butDelete);
            }
        }

        //all in
        private void EventButApplyClicked(object sender, EventArgs e) {
            EventRepaintPages(EventArgs.Empty);
        }

        //all in
        private void butDelete_Click(object sender, RoutedEventArgs e) {
            if(tf.status == Status.TRASH) {
                DatabaseTaskFolder.DeleteTaskFolder(tf);
            } else {
                DatabaseTaskFolder.UpdateTaskFolder(tf, tf.text, Status.TRASH);
            }
            EventRepaintPages(EventArgs.Empty);
        }
        
        //EVENTS--------------------------

        public event EventHandler HandlerRepaint;
        protected virtual void EventRepaintPages(EventArgs e) {
            EventHandler handler = HandlerRepaint;
            if (handler != null) {
                handler(this, e);
            }
        }

        private void DoubleClickOpen(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            if (e.ClickCount == 2) {
                TaskWindow win2 = new TaskWindow(tf);
                win2.HandlerAddTask += EventButApplyClicked;
                win2.Show();
            }
        }
    }
}
