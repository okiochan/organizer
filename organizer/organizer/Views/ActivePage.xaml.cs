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

namespace organizer {
    /// <summary>
    /// Interaction logic for ActivePage.xaml
    /// </summary>
    public partial class ActivePage : Page {

        private List<StackPanel> spList;
        private int tasksCnt = 0;
        private Database db;

        public ActivePage() {
            InitializeComponent();

            //read tasks
            db = new Database(@"..\..\..\db\tasks.db");
            spList = new List<StackPanel>();
        }

        //can I keep it public????????????????
        public void Repaint() {
            foreach (var sp in spList) {
                sp.Children.Clear();
            }
            spList.Clear();
            tasksCnt = 0;

            List<TaskFolder> allFolders = db.ReadAll();
            foreach (var t in allFolders) {
                if (t.status == Status.TODO) {
                    addTaskFolder(t);
                }
            }
        }

        //TaskFolder preparation
        private void addTaskFolder(TaskFolder tf) {

            FolderLook pageFL = new FolderLook(tf,this);

            Frame myFrame = new Frame();
            myFrame.Margin = new Thickness(10, 10, 10, 10);
            myFrame.Navigate(pageFL);

            if (tasksCnt % 5 == 0) {
                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;
                spList.Add(sp);
                panelTasks.Children.Add(sp);
            }
            spList[spList.Count - 1].Children.Add(myFrame);
            tasksCnt++;
        }
        
        private void addFolderToDB(String tfName) {
            db.CreateNewTaskFolder(tfName);
            Repaint();
        }

        //masha add
        private void butAddTask_Click(object sender, RoutedEventArgs e) {
            DialogAddFolderTask wind = new DialogAddFolderTask();
            if (wind.ShowDialog() == true) {
                addFolderToDB(wind.getTaskTitle);
            } else {
                MessageBox.Show("Info not saved =(");
            }
            
        }

        private void activePage_Loaded(object sender, RoutedEventArgs e) {
            Repaint();
        }
    }
}
