using organizer.Codes;
using organizer.Codes.Database;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace organizer.Views.Pages {
    /// <summary>
    /// Interaction logic for DonePage.xaml
    /// </summary>
    public partial class PageDone : Page {

        private List<StackPanel> stackPanels;
        private int tasksCnt;

        public PageDone() {
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

            List<TaskFolder> allfolders = DatabaseTaskFolder.ReadAll();
            foreach (var tf in allfolders) {
                if (tf.status == Status.DONE) {
                    addTaskFolder(tf);
                }
            }
        }

        //TaskFolder preparation
        private void addTaskFolder(TaskFolder tf) {

            PageFolderLook pageFL = new PageFolderLook(tf);
            pageFL.AnyChange += EventRepaint;
            
            //pageFLD.ButtonClickedHandler += EventButApplyClicked;


            Frame myFrame = new Frame();
            myFrame.Margin = new Thickness(10, 10, 10, 10);
            myFrame.Navigate(pageFL);

            if (tasksCnt % 5 == 0) {
                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;
                stackPanels.Add(sp);
                panelDoneTasks.Children.Add(sp);
            }
            stackPanels[stackPanels.Count - 1].Children.Add(myFrame);
            tasksCnt++;
        }


        private void Page_Loaded(object sender, RoutedEventArgs e) {
            Repaint();
        }

        private void EventRepaint(object sender, EventArgs e) {
            Repaint();
        }

    }
}
