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
    /// Interaction logic for TaskNotificationListWindow.xaml
    /// </summary>
    public partial class TaskNotificationListWindow : Window {
        public TaskNotificationListWindow() {
            InitializeComponent();
        }

        private void butApply_Click(object sender, RoutedEventArgs e) {
            this.DialogResult = true;
        }

        private void btnAddNotif_Click(object sender, RoutedEventArgs e) {
            DialogAddNotification d = new DialogAddNotification();
            if (d.ShowDialog() == true) {
            } else {
            }
        }
    }
}
