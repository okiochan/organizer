using organizer.Codes;
using organizer.Codes.Database;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace organizer {
    /// <summary>
    /// Interaction logic for ActivePage.xaml
    /// </summary>
    public partial class PageActive : Page {

        private List<StackPanel> spList;
        private int tasksCnt = 0;

        public PageActive() {
            InitializeComponent();
            spList = new List<StackPanel>();
        }
        
        private void Repaint() {
            foreach (var sp in spList) {
                sp.Children.Clear();
            }
            spList.Clear();
            tasksCnt = 0;
            List<TaskFolder> allFolders = DatabaseTaskFolder.ReadAll();
            foreach (var tf in allFolders) {
                if (tf.status == Status.TODO) {
                    addTaskFolder(tf);
                }
            }
        }
        
        //TaskFolder preparation
        private void addTaskFolder(TaskFolder tf) {

            PageFolderLook pageFL = new PageFolderLook(tf);
            //EVENT ADD
            pageFL.HandlerRepaint += EventButApplyClicked;

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
            DialogAddFolderTask wind = new DialogAddFolderTask(null);
            wind.HandlerButApplyClick += EventButApplyClicked;

            if (wind.ShowDialog() != true) {
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
