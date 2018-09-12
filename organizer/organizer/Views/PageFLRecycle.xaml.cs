using organizer.Codes;
using organizer.Codes.Database;
using System;
using System.Windows;
using System.Windows.Controls;

namespace organizer.Views {
    /// <summary>
    /// Interaction logic for PageFLRecycle.xaml
    /// </summary>
    public partial class PageFLRecycle : Page {

        private TaskFolder tf;
        public PageFLRecycle(TaskFolder tf) {
            InitializeComponent();
            this.tf = tf;
        }
        
        private void butRestore_Click(object sender, RoutedEventArgs e) {
            DatabaseTaskFolder.UpdateTaskFolder(tf, tf.text, Status.TODO);
            OnButtonClickedEvent(EventArgs.Empty);
        }

        private void butDelete_Click(object sender, RoutedEventArgs e) {
            DatabaseTaskFolder.DeleteTaskFolder(tf);
            OnButtonClickedEvent(EventArgs.Empty);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            txtBoxTitle.Text = tf.text;
        }

        private void butRestoreD_Click(object sender, RoutedEventArgs e) {
            DatabaseTaskFolder.UpdateTaskFolder(tf, tf.text, Status.DONE);
            OnButtonClickedEvent(EventArgs.Empty);
        }

        //EVENTS--------------------------

        public event EventHandler HandlerButClicked;
        protected virtual void OnButtonClickedEvent(EventArgs e) {
            EventHandler handler = HandlerButClicked;
            if (handler != null) {
                handler(this, e);
            }
        }

        
    }
}
