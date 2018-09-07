using organizer.Codes;
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
            prepData();
        }

        private void prepData() {
            txtBoxTitle.Text = tf.text;
        }

        private void butGoTo_Click(object sender, RoutedEventArgs e) {
            TaskWindow win2 = new TaskWindow(tf);
            win2.Show();
        }
    }
}
