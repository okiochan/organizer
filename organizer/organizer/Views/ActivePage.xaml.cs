using organizer.Codes;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

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
            prepeareData();

            // usage example
            // db = new Database(@"..\..\..\db\tasks.db");
            // db.CreateNewTaskFolder("a new task");
            // var allFolders = db.ReadAll();
            // var lastTaskFolder = allFolders[allFolders.Count - 1];
            // db.CreateNewNote("a new note", lastTaskFolder);
            // db.CreateNewTask("a new task", Priority.LOW, Status.DONE, DateTime.Now,lastTaskFolder);
            // allFolders = db.ReadAll(); // update folders
        }

        //TaskFolder preparation
        private void addTaskFolder(TaskFolder tf) {

            FolderLook pageFL = new FolderLook(tf);

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
        
        //do what says Dinash
        //private void addFolderToDB(String tfName) {
        //    TaskFolder tf = new TaskFolder();
        //    tf.text = tfName;
        //    tf.status = Status.TODO;
        //    allFolders.Add(tf);
        //}

        //masha add
        private void prepeareData() {
            spList = new List<StackPanel>();

            //read tasks
            db = new Database(@"..\..\..\db\tasks.db");
            List<TaskFolder> allFolders = db.ReadAll();

            foreach (var t in allFolders) {
                if (t.status == Status.TODO) {
                    addTaskFolder(t);
                }
            }
        }

        //masha add
        private void butAddTask_Click(object sender, RoutedEventArgs e) {
            DialogAddFolderTask wind = new DialogAddFolderTask();
            if (wind.ShowDialog() == true) {

                TaskFolder tf = new TaskFolder();
                tf.text = wind.getTaskTitle;
                //addFolderToDB(name);
                addTaskFolder(tf);
            } else {
                MessageBox.Show("Info not saved =(");
            }
            
        }

    }
}
