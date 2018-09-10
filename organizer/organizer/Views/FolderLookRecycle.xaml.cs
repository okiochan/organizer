using organizer.Codes;
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
    /// Interaction logic for FolderLookRecycle.xaml
    /// </summary>
    public partial class FolderLookRecycle : Window {

        private TaskFolder tf;

        public FolderLookRecycle(TaskFolder tf) {
            InitializeComponent();
            this.tf = tf;
        }

        private void butRestore_Click(object sender, RoutedEventArgs e) {
            Database.GetInstance().UpdateTaskFolder(tf, tf.text, Status.TODO);
            OnButtonClickedEvent(EventArgs.Empty);
        }

        private void butDelete_Click(object sender, RoutedEventArgs e) {
            Database.GetInstance().DeleteTaskFolder(tf);
            OnButtonClickedEvent(EventArgs.Empty);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            txtBoxTitle.Text = tf.text;
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
