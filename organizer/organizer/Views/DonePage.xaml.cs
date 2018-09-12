using organizer.Codes;
using organizer.Codes.Database;
using organizer.Views;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace organizer {
    /// <summary>
    /// Interaction logic for DonePage.xaml
    /// </summary>
    public partial class DonePage : Page {

        private List<StackPanel> spList;
        private int tasksCnt = 0;

        public DonePage() {
            InitializeComponent();
            spList = new List<StackPanel>();
        }
        
        private void repaint() {
            foreach (var sp in spList) {
                sp.Children.Clear();
            }
            spList.Clear();
            tasksCnt = 0;

            List<TaskFolder> allfolders = DatabaseTaskFolder.ReadAll();
            foreach (var tf in allfolders) {
                if (tf.status == Status.DONE) {
                    addTaskFolder(tf);
                }
            }
        }

        //TaskFolder preparation
        private void addTaskFolder(TaskFolder tf) {

            FolderLookDone pageFLD = new FolderLookDone(tf);
            pageFLD.HandlerButDeleteClicked += EventRepaint;
            
            //pageFLD.ButtonClickedHandler += EventButApplyClicked;


            Frame myFrame = new Frame();
            myFrame.Margin = new Thickness(10, 10, 10, 10);
            myFrame.Navigate(pageFLD);

            if (tasksCnt % 5 == 0) {
                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;
                spList.Add(sp);
                panelDoneTasks.Children.Add(sp);
            }
            spList[spList.Count - 1].Children.Add(myFrame);
            tasksCnt++;
        }


        private void Page_Loaded(object sender, RoutedEventArgs e) {
            repaint();
        }

        //EVENTS------------------------------

        private void EventRepaint(object sender, EventArgs e) {
            repaint();
        }

    }
}
