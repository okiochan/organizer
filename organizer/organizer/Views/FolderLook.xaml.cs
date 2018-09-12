using organizer.Codes;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace organizer {
    /// <summary>
    /// Interaction logic for FolderLook.xaml
    /// </summary>
    ///

    public partial class FolderLook : Page
    {
        private TaskFolder tf;

        public FolderLook(TaskFolder tf)
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
            Database.GetInstance().UpdateTaskFolder(tf, tf.text, Status.DONE);
            OnButtonClickedEvent(EventArgs.Empty);
        }

        private void butDelete_Click(object sender, RoutedEventArgs e) {
            Database.GetInstance().UpdateTaskFolder(tf, tf.text, Status.TRASH);
            OnButtonClickedEvent(EventArgs.Empty);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e) {
            txtBoxTitle.Text = tf.text;
        }

        //EVENTS--------------------------

        public event EventHandler ButtonClickedHandler;
        protected virtual void OnButtonClickedEvent(EventArgs e) {
            EventHandler handler = ButtonClickedHandler;
            if (handler != null) {
                handler(this, e);
            }
        }

        private void EventButApplyClicked(object sender, EventArgs e) {
            OnButtonClickedEvent(EventArgs.Empty);
        }

    }
}
