using organizer.Codes;
using organizer.Codes.Database;
using organizer.Views;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace organizer {
    /// <summary>
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    /// 

    public partial class TaskWindow : Window {

        private TaskFolder tf;
        private int cnt=0;

        public TaskWindow(TaskFolder tf) {
            InitializeComponent();
            this.tf = tf;
        }
        
        //EVENT
        private void EventRepaint(object sender, EventArgs e) {
            repaint();
        }

        private void repaint() {

            panelMiddle.Children.Clear();
            panelLeft.Children.Clear();
            panelRight.Children.Clear();
            cnt = 0;

            DatabaseTaskFolder.ReloadTaskFolder(tf);
            tf.SortTasksByPriority();

            foreach (var t in tf.tasks) {

                PageTaskLook taskLook = new PageTaskLook(t);
                //EVENT ADD
                taskLook.HandlerRepaint += EventRepaint;
                
                Frame myFrame = new Frame();
                myFrame.Margin = new Thickness(10, 10, 10, 10);
                myFrame.Navigate(taskLook);
                
                if (t.status == Status.DONE) {
                    panelMiddle.Children.Add(myFrame);
                } else {
                    panelLeft.Children.Add(myFrame);
                }
                cnt++;
            }

            foreach (var n in tf.notes) {
                Border border = new Border();
                border.BorderThickness = new Thickness(2);
                border.BorderBrush = Brushes.DarkKhaki;
                border.Margin = new Thickness(10, 10, 10, 10);

                TextBlock tb = new TextBlock();
                tb.TextWrapping = TextWrapping.Wrap;
                tb.Name = "txtBox" + cnt.ToString();
                tb.Text = n.text;
                tb.Background = Brushes.SeaShell;
                tb.Height = 50;
                cnt++;

                border.Child = tb;
                panelRight.Children.Add(border);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            TextBlock btn = (TextBlock)sender;
            DragDrop.DoDragDrop(btn, btn, DragDropEffects.Copy);
        }

        private void Button_Drop(object sender, DragEventArgs e) {
            Label lbl = (Label)sender;
            String text = (String)e.Data.GetData(DataFormats.Text);
            lbl.Content = text;
        }

        private void Button_MouseDown(object sender, MouseButtonEventArgs e) {
            Label src = (Label)sender;
            DragDrop.DoDragDrop(src, src.Content, DragDropEffects.Copy);
        }

        private void butAddTask_Click(object sender, RoutedEventArgs e) {
            
            Task t = new Task();
            DialogAddTask d = new DialogAddTask(t);
            
            if (d.ShowDialog() == true) {
                //repaint all
                DatabaseTask.CreateNewTask(t.text, t.prio, Status.TODO, t.startdate, t.deadline, tf);
                repaint();
            } else {
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            repaint();
        }

        private void butAddNote_Click(object sender, RoutedEventArgs e) {
            DialogAddNote an = new DialogAddNote(tf);
            an.HandlerButApplyClick += EventRepaint;

            if (an.ShowDialog() == true) {
                //repaint all
                repaint();
            } else {
            }
        }

        private void panelMiddle_Drop(object sender, DragEventArgs e) {
            base.OnDrop(e);

            // If the DataObject contains string data, extract it.
            if (e.Data.GetDataPresent("Task")) {
                Task task = (Task)e.Data.GetData("Task");
                task.status = Status.DONE;
                DatabaseTask.UpdateTask(task);
            }
            e.Handled = true;
            repaint();
        }

        private void panelLeft_Drop(object sender, DragEventArgs e) {
            base.OnDrop(e);

            // If the DataObject contains string data, extract it.
            if (e.Data.GetDataPresent("Task")) {
                Task task = (Task)e.Data.GetData("Task");
                task.status = Status.TODO;

                DialogAddTask an = new DialogAddTask(task);
                if (an.ShowDialog() == true) {
                    //repaint all
                    e.Handled = true;
                    repaint();
                    DatabaseTask.UpdateTask(task);

                } else {
                }
            }
        }

        private void DeleteItem(object sender, DragEventArgs e) {

            base.OnDrop(e);

            // If the DataObject contains string data, extract it.
            if (e.Data.GetDataPresent("Task")) {
                Task task = (Task)e.Data.GetData("Task");


                // Configure the message box to be displayed
                string messageBoxText = "Do you want to delete task?";
                string caption = "Dialog window";
                MessageBoxButton button = MessageBoxButton.YesNo;
                MessageBoxImage icon = MessageBoxImage.Warning;
                // Display message box
                MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

                // Process message box results
                switch (result) {
                    case MessageBoxResult.Yes:
                        DatabaseTask.DeleteTask(task);
                        break;
                    case MessageBoxResult.No:
                        break;
                }
                repaint();

            }
            e.Handled = true;
        }
    }
}
