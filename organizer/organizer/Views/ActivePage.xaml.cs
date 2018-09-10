using organizer.Codes;
using System;
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
        Database db;

        public ActivePage() {
            InitializeComponent();

            spList = new List<StackPanel>();
            db = new Database(@"..\..\..\db\tasks.db");
        }
        
        private void Repaint() {
            foreach (var sp in spList) {
                sp.Children.Clear();
            }
            spList.Clear();
            tasksCnt = 0;

            List<TaskFolder> allFolders = db.ReadAll();
            foreach (var tf in allFolders) {
                if (tf.status == Status.TODO) {
                    addTaskFolder(tf);
                }
            }
        }
        
        //TaskFolder preparation
        private void addTaskFolder(TaskFolder tf) {

            FolderLook pageFL = new FolderLook(tasksCnt, tf.text);
            //EVENT ADD
            pageFL.ButtonClickedHandler += EventButApplyClicked;

            //delete event
            //pageFL.ButtonClickedHandler -= ButtonClickedEvent;


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

        //masha add
        private void butAddTask_Click(object sender, RoutedEventArgs e) {
            DialogAddFolderTask wind = new DialogAddFolderTask();
            if (wind.ShowDialog() == true) {
                String title = wind.getTaskTitle;
                db.CreateNewTaskFolder(title);
                Repaint();
            } else {
                MessageBox.Show("Info not saved =(");
            }
            
        }

        private void activePage_Loaded(object sender, RoutedEventArgs e) {
            Repaint();
        }

        //EVENTS------------------------------

        private void EventButApplyClicked(object sender, EventArgs e) {
            Repaint();
        }
    }
}
