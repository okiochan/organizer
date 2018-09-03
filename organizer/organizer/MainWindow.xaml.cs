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
        private List<System.Windows.Controls.StackPanel> spList;
        private int tasksCnt=0;
        private List<TaskFolder> allFolders;
        private Database db;

        public MainWindow()
        {
            InitializeComponent();
            prepeareData();
        }

        //TaskFolder preparation
        private void addTaskFolder(String tfName) {
            
            FolderLook pageFL = new FolderLook();
            pageFL.txtBoxTitle.Text = tfName;

            System.Windows.Controls.Frame myFrame = new System.Windows.Controls.Frame();
            myFrame.Margin = new Thickness(10, 10, 10, 10);
            myFrame.Navigate(pageFL);

            if (tasksCnt % 5 == 0) {
                System.Windows.Controls.StackPanel sp = new StackPanel();
                sp.Orientation = System.Windows.Controls.Orientation.Horizontal;
                spList.Add(sp);
                panelTasks.Children.Add(sp);
            }
            spList[spList.Count - 1].Children.Add(myFrame);
            tasksCnt++;
        }

        private void addFolderToDB(String tfName) {
            TaskFolder tf = new TaskFolder();
            tf.text = tfName;
            tf.status = Status.TODO;
            allFolders.Add(tf);
        }

        //masha add
        private void prepeareData() {
            spList = new List<System.Windows.Controls.StackPanel>();

            //read tasks
            db = new Database(@"..\..\..\db\tasks.db");
            allFolders = db.ReadAll();
            
            foreach(var t in allFolders) {
                addTaskFolder(t.text);
            }
        }
        
        //masha add
        private void butAddTask_Click(object sender, RoutedEventArgs e) {
            DialogAddFolderTask wind = new DialogAddFolderTask();
            if (wind.ShowDialog() == true) {
                String name = wind.getTaskTitle;
                addFolderToDB(name);
                addTaskFolder(name);
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
