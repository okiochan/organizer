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

namespace organizer {
    /// <summary>
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window {
        public TaskWindow() {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            TextBlock btn = (TextBlock)sender;
            DragDrop.DoDragDrop(btn, btn, DragDropEffects.Copy);
        }

        private void Button_Drop(object sender, DragEventArgs e) {
            Button btn = (Button)sender;
            btn.Content = e.Data.ToString();
        }
    }
}
