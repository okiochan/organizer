using organizer.Codes;
using organizer.Codes.Database;
using organizer.Views.Dialogs;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace organizer.Views.Pages {
    /// <summary>
    /// Interaction logic for PageTaskLook.xaml
    /// </summary>
    public partial class PageTaskLook : Page {

        private Task t;

        public PageTaskLook(Task t) {
            InitializeComponent();
            this.t = t;
        }

        //Handler
        public event EventHandler HandlerRepaint;
        protected virtual void EventRepaint(EventArgs e) {
            HandlerRepaint?.Invoke(this, e);
        }

        private void repaint() {
            labe.Content = t.text;

            //clear
            panel.Children.Remove(txtTime);
            panel.Children.Remove(butTime);
            panel.Children.Remove(btnNotifList);
            panel.Children.Remove(btnChange);

            if (t.status == Status.DONE) { //DONE

                labe.Background = Brushes.AliceBlue;
                labe.BorderBrush = Brushes.DarkGray;
                panel.Children.Remove(butTime);

                int time = t.timeSpent;
                int mins = time % 60;
                time /= 60;
                int hours = time % 60;
                time /= 60;
                int days = time;
                txtTime.Text = String.Format("Spent: D:{0} H:{1} M:{2}", days, hours, mins);
                panel.Children.Add(txtTime);

            } else { //TODO

                panel.Children.Add(butTime);
                panel.Children.Add(btnNotifList);
                panel.Children.Add(btnChange);

                TimeSpan deadlineDiff = t.deadline - DateTime.Now;
                TimeSpan startdateDiff = t.startdate - DateTime.Now;
                TimeSpan twoDays = new TimeSpan(2, 0, 0, 0);

                if (!DateTime.MinValue.Equals(t.startdate) && startdateDiff > TimeSpan.Zero) {        // not started yet
                    labe.Background = Brushes.LightGray;
                } else if (!DateTime.MinValue.Equals(t.deadline) && deadlineDiff < TimeSpan.Zero) {  // already ended
                    labe.Background = Brushes.Red;
                } else if (!DateTime.MinValue.Equals(t.deadline) && deadlineDiff < twoDays) {        // due to end in 2 days
                    labe.Background = Brushes.Salmon;
                } else {                                    // default    
                    labe.Background = Brushes.SandyBrown;

                    if (t.prio == Priority.MID) {
                        labe.Background = Brushes.PeachPuff;
                    } else if (t.prio == Priority.HIGH) {
                        labe.Background = Brushes.LightSalmon;
                    } else {
                        labe.Background = Brushes.PapayaWhip;
                    }
                }

            }
        }

            private void Page_Loaded(object sender, RoutedEventArgs e) {
            repaint();
        }

        private void butTime_Click(object sender, RoutedEventArgs e) {
            DialogAddTimeSpent wnd = new DialogAddTimeSpent(t);
            wnd.Show();
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);
            if (e.LeftButton == MouseButtonState.Pressed) {
                // Package the data.
                DataObject data = new DataObject();
                data.SetData("Task", this.t);

                // Inititate the drag-and-drop operation.
                DragDrop.DoDragDrop(this, data, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }
        protected override void OnGiveFeedback(GiveFeedbackEventArgs e) {
            base.OnGiveFeedback(e);
            // These Effects values are set in the drop target's
            // DragOver event handler.
            if (e.Effects.HasFlag(DragDropEffects.Copy)) {
                Mouse.SetCursor(Cursors.Cross);
            } else if (e.Effects.HasFlag(DragDropEffects.Move)) {
                Mouse.SetCursor(Cursors.Pen);
            } else {
                Mouse.SetCursor(Cursors.No);
            }
            e.Handled = true;
        }

        private void btnNotifList_Click(object sender, RoutedEventArgs e) {
            TaskNotificationListWindow d = new TaskNotificationListWindow();
            if (d.ShowDialog() == true) {
            } else {
            }
        }

        private void btnChange_Click(object sender, RoutedEventArgs e) {
            DialogAddTask d = new DialogAddTask(t);
            if (d.ShowDialog() == true) {
                DatabaseTask.UpdateTask(t);
                //repaint taskwindow
                EventRepaint(EventArgs.Empty);
            } else {
            }
        }
    }
}
