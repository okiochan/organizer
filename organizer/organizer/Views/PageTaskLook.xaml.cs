using organizer.Codes;
using organizer.Codes.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace organizer.Views {
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
        public event EventHandler HandlerButClicked;
        protected virtual void EventRepaint(EventArgs e) {
            HandlerButClicked?.Invoke(this, e);
        }

        private void repaint() {
            labe.Content = t.text;

            //clear
            panel.Children.Remove(txtTime);
            panel.Children.Remove(butTime);
            panel.Children.Remove(btnNotifList);

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

                TimeSpan deadlineDiff = t.deadline - DateTime.Now;
                TimeSpan startdateDiff = t.startdate - DateTime.Now;
                TimeSpan twoDays = new TimeSpan(2, 0, 0, 0);

                if (startdateDiff > TimeSpan.Zero) {        // not started yet
                    labe.Background = Brushes.LightGray;
                } else if (deadlineDiff < TimeSpan.Zero) {  // already ended
                    labe.Background = Brushes.Red;
                } else if (deadlineDiff < twoDays) {        // due to end in 2 days
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
            AddTimeSpentWindow wnd = new AddTimeSpentWindow(t);
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
    }
}
