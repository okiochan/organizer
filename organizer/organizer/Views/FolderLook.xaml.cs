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
        private TaskFolder tf;
        private ActivePage ap;

        public FolderLook(TaskFolder tf, ActivePage ap)
        {
            InitializeComponent();
            this.tf = tf;
            this.ap = ap;
        }

        private void butGoTo_Click(object sender, RoutedEventArgs e) {
            TaskWindow win2 = new TaskWindow(tf);
            win2.Show();
        }

        private void butDone_Click(object sender, RoutedEventArgs e) {
            ap.Repaint();
        }

        private void butDelete_Click(object sender, RoutedEventArgs e) {
            ap.Repaint();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e) {
            txtBoxTitle.Text = tf.text;
        }
    }
}
