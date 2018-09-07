using organizer.Codes;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace organizer {
    /// <summary>
    /// Interaction logic for RecyclePage.xaml
    /// </summary>
    public partial class RecyclePage : Page {

        private List<StackPanel> spList;
        private int tasksCnt = 0;
        private Database db;


        public RecyclePage() {
            InitializeComponent();
            prepeareData();
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
                panelRecycle.Children.Add(sp);
            }
            spList[spList.Count - 1].Children.Add(myFrame);
            tasksCnt++;
        }

        //gilfoyle add
        private void prepeareData() {
            spList = new List<StackPanel>();

            //read tasks
            db = new Database(@"..\..\..\db\tasks.db");
            List<TaskFolder> allFolders = db.ReadAll();

            foreach (var t in allFolders) {
                if(t.status == Status.TRASH) {
                    addTaskFolder(t);
                }
            }
        }
    }
}
