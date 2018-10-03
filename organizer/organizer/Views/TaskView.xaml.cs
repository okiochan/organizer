using organizer.Codes;
using organizer.Codes.Database;
using organizer.Views.Dialogs;
using organizer.Views.Pages;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace organizer {
    /// <summary>
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    /// 

    public partial class TaskView : Window {

        private TaskFolder TaskFolder;
        private int cnt;

        public TaskView(TaskFolder tf) {
            InitializeComponent();
            this.TaskFolder = tf;
            cnt = 0;
        }
        
        //EVENT
        private void EventRepaint(object sender, EventArgs e) {
            Repaint();
        }

        private void Repaint() {

            panelMiddle.Children.Clear();
            panelLeft.Children.Clear();
            panelRight.Children.Clear();
            cnt = 0;

            DatabaseTaskFolder.ReloadTaskFolder(TaskFolder);
            TaskFolder.SortTasksByPriority();

            foreach (var t in TaskFolder.tasks) {

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

            foreach (var n in TaskFolder.notes) {
                PageNote note = new PageNote(n);

                Frame noteFrame = new Frame();
                noteFrame.Navigate(note);
                panelRight.Children.Add(noteFrame);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            TextBlock btn = (TextBlock)sender;
            DragDrop.DoDragDrop(btn, btn, DragDropEffects.Copy);
        }

        private void butAddTask_Click(object sender, RoutedEventArgs e) {
            
            Task t = new Task();
            DialogAddTask d = new DialogAddTask(t);
            
            if (d.ShowDialog() == true) {
                //repaint all
                DatabaseTask.CreateNewTask(t.text, t.prio, Status.TODO, t.startdate, t.deadline, TaskFolder);
                Repaint();
            } else {
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            Repaint();
        }

        private void butAddNote_Click(object sender, RoutedEventArgs e) {
            DialogAddNote an = new DialogAddNote(TaskFolder);
            an.ApplyClick += EventRepaint;

            if (an.ShowDialog() == true) {
                //repaint all
                Repaint();
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
            Repaint();
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
                    Repaint();
                    DatabaseTask.UpdateTask(task);

                } else {
                }
            }
        }

        private void DeleteItem(object sender, DragEventArgs e) {
            base.OnDrop(e);

            if (e.Data.GetDataPresent("Task")) { // task deletion
                Task task = (Task)e.Data.GetData("Task");
                
                // Process message box results
                string messageBoxText = "Do you want to delete task?";
                string caption = "Dialog window";
                MessageBoxButton button = MessageBoxButton.YesNo;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

                // Process message box results
                switch (result) {
                    case MessageBoxResult.Yes:
                        DatabaseTask.DeleteTask(task);
                        break;
                    case MessageBoxResult.No:
                        break;
                }
                Repaint();

            } else if (e.Data.GetDataPresent("Note")) { // note deletion
                Note note = (Note)e.Data.GetData("Note");

                // confirmation
                string messageBoxText = "Do you want to delete the note?";
                string caption = "Dialog window";
                MessageBoxButton button = MessageBoxButton.YesNo;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

                switch (result) {
                    case MessageBoxResult.Yes:
                        DatabaseNote.DeleteNote(note);
                        break;
                    case MessageBoxResult.No:
                        break;
                }
                Repaint();
            }
            e.Handled = true;
        }
    }
}
