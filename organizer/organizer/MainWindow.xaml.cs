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
        private List<TaskFolder> tasks;
        private List<System.Windows.Controls.StackPanel> spList;
        private int butCnt=0;
        
        public MainWindow()
        {
            InitializeComponent();
            prepeareData();
        }

        //masha add
        private void prepSpList() {
            if (butCnt % 5 == 0) {
                System.Windows.Controls.StackPanel sp = new StackPanel();
                sp.Orientation = System.Windows.Controls.Orientation.Horizontal;
                spList.Add(sp);
                panelTasks.Children.Add(sp);
            }
        }

        //masha add
        private void prepeareData() {
            tasks = new List<TaskFolder>();
            spList = new List<System.Windows.Controls.StackPanel>();

            TaskFolder tf = new TaskFolder();
            tf.name = "fsdfsdf";
            tf.status = Status.TODO;
            tasks.Add(tf); tasks.Add(tf); tasks.Add(tf); tasks.Add(tf); tasks.Add(tf); tasks.Add(tf);
            //read tasks
            
            //display tasks
            foreach(var t in tasks) {
                prepSpList();
                System.Windows.Controls.Button butt = new Button();
                butt.Content = t.name;
                butt.Name = "but" + butCnt.ToString();
                butCnt++;
                spList[spList.Count - 1].Children.Add(butt);
            }
        }
        
        //masha add
        private void butAddTask_Click(object sender, RoutedEventArgs e) {

            TaskFolder tf = new TaskFolder();
            DialogAddFolderTask wind = new DialogAddFolderTask();
            if (wind.ShowDialog() == true) {
                tf.name = wind.getTaskTitle;
                tf.status = Status.TODO;
                tasks.Add(tf);

                prepSpList();
                System.Windows.Controls.Button butt = new Button();
                butt.Content = tf.name;
                butt.Name = "but" + butCnt.ToString();
                butCnt++;
                spList[spList.Count - 1].Children.Add(butt);

            } else {
                MessageBox.Show("Folder dialog not opened =(");
            }

            mainWindow.UpdateLayout();

        }

        private void butActive_Click(object sender, RoutedEventArgs e) {

        }

        private void butDone_Click(object sender, RoutedEventArgs e) {

        }

        private void butRecycle_Click(object sender, RoutedEventArgs e) {

        }

        
    }
}
