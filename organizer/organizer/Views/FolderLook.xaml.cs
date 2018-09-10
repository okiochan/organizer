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

namespace organizer
{
    /// <summary>
    /// Interaction logic for FolderLook.xaml
    /// </summary>
    ///

    public partial class FolderLook : Page
    {
        private int tfId;
        private String text;

        public FolderLook(int tfId, String text)
        {
            InitializeComponent();
            this.tfId = tfId;
            this.text = text;
        }

        public event EventHandler ButtonClickedHandler;
        protected virtual void OnButtonClickedEvent(EventArgs e) {
            EventHandler handler = ButtonClickedHandler;
            if (handler != null) {
                handler(this, e);
            }
        }

        private void butGoTo_Click(object sender, RoutedEventArgs e) {
            TaskWindow win2 = new TaskWindow(tfId);
            win2.HandlerAddTask += EventButApplyClicked;
            win2.Show();
        }

        private void butDone_Click(object sender, RoutedEventArgs e) {
            OnButtonClickedEvent(EventArgs.Empty);
        }

        private void butDelete_Click(object sender, RoutedEventArgs e) {
            OnButtonClickedEvent(EventArgs.Empty);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e) {
            txtBoxTitle.Text = text;
        }

        //EVENTS--------------------------

        private void EventButApplyClicked(object sender, EventArgs e) {
            OnButtonClickedEvent(EventArgs.Empty);
        }

    }
}
