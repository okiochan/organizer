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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<TaskFolder> tasks;

        public MainWindow()
        {
            InitializeComponent();
        }

        //masha add
        private void butAddTask_Click(object sender, RoutedEventArgs e) {

            TaskFolder tf = new TaskFolder();
            DialogAddFolderTask wind = new DialogAddFolderTask();
            if (wind.ShowDialog() == true) {
                tf.name = wind.getTaskTitle;
            } else {
                MessageBox.Show("Folder dialog not opened =(");
            }

        }

        private void butActive_Click(object sender, RoutedEventArgs e) {

        }

        private void butDone_Click(object sender, RoutedEventArgs e) {

        }

        private void butRecycle_Click(object sender, RoutedEventArgs e) {

        }

        
    }
}
