using organizer.Codes;
using organizer.Views;
using System;
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

        public RecyclePage() {
            InitializeComponent();
            spList = new List<StackPanel>();
        }
        
        private void repaint() {
            foreach (var sp in spList) {
                sp.Children.Clear();
            }
            spList.Clear();
            tasksCnt = 0;

            List<TaskFolder> allfolders = Database.GetInstance().ReadAll();
            foreach (var tf in allfolders) {
                if (tf.status == Status.TRASH) {
                    addTaskFolder(tf);
                }
            }
        }
        
        //TaskFolder preparation
        private void addTaskFolder(TaskFolder tf) {

            PageFLRecycle pageFL = new PageFLRecycle(tf);
            pageFL.HandlerButClicked += EventRepaint;

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

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e) {
            repaint();
        }

        //EVENTS------------------------------

        private void EventRepaint(object sender, EventArgs e) {
            repaint();
        }
        
    }
}
