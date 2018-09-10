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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace organizer.Views
{
    /// <summary>
    /// Interaction logic for FolderLookDone.xaml
    /// </summary>
    public partial class FolderLookDone : Page
    {
        private TaskFolder tf;
        public FolderLookDone(TaskFolder tf)
        {
            InitializeComponent();
            this.tf = tf;
        }
        
        private void butDelete_Click(object sender, RoutedEventArgs e) {
            Database.GetInstance().UpdateTaskFolder(tf, tf.text, Status.TRASH);
            OnButtonClickedEvent(EventArgs.Empty);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e) {
            txtBoxTitle.Text = tf.text;
        }

        //EVENTS--------------------------

        public event EventHandler HandlerButDeleteClicked;
        protected virtual void OnButtonClickedEvent(EventArgs e) {
            EventHandler handler = HandlerButDeleteClicked;
            if (handler != null) {
                handler(this, e);
            }
        }

        private void EventButApplyClicked(object sender, EventArgs e) {
            OnButtonClickedEvent(EventArgs.Empty);
        }

        private void butView_Click(object sender, RoutedEventArgs e) {

        }
    }
}
