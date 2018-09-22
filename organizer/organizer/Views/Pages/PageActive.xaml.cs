using organizer.Codes;
using organizer.Codes.Database;
using organizer.Views.Dialogs;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace organizer.Views.Pages {
    /// <summary>
    /// Interaction logic for ActivePage.xaml
    /// </summary>
    public partial class PageActive : Page {

        private List<StackPanel> stackPanels;
        private int tasksCnt;

        public PageActive() {
            InitializeComponent();
            stackPanels = new List<StackPanel>();
            tasksCnt = 0;
        }
        
        private void Repaint() {
            foreach (var sp in stackPanels) {
                sp.Children.Clear();
            }
            stackPanels.Clear();
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
            pageFL.AnyChange += EventButApplyClicked;

            //delete event
            //pageFL.ButtonClickedHandler -= ButtonClickedEvent;


            Frame myFrame = new Frame();
            myFrame.Margin = new Thickness(10, 10, 10, 10);
            myFrame.Navigate(pageFL);

            if (tasksCnt % 5 == 0) {
                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;
                stackPanels.Add(sp);
                panelTasks.Children.Add(sp);
            }
            stackPanels[stackPanels.Count - 1].Children.Add(myFrame);
            tasksCnt++;
        }

        //masha add
        private void butAddTask_Click(object sender, RoutedEventArgs e) {
            DialogAddFolderTask wind = new DialogAddFolderTask(null);
            wind.ApplyClicked += EventButApplyClicked;

            if (wind.ShowDialog() != true) {
                MessageBox.Show("Info not saved =(");
            }
            
        }

        private void activePage_Loaded(object sender, RoutedEventArgs e) {
            Repaint();
        }

        private void EventButApplyClicked(object sender, EventArgs e) {
            Repaint();
        }
    }
}
