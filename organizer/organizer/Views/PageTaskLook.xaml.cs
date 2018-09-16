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
            EventHandler handler = HandlerButClicked;
            if (handler != null) {
                handler(this, e);
            }
        }

        private void repaint() {
            labe.Content = t.text;

            //clear
            panel.Children.Remove(txtTime);
            panel.Children.Remove(butTime);
            panel.Children.Remove(butDelete);

            if (t.status == Status.DONE) { //DONE

                labe.Background = Brushes.AliceBlue;
                labe.BorderBrush = Brushes.DarkGray;
                butR.Content = "<--";
                panel.Children.Add(txtTime);
                //DINASH set txtTime.Text = "Spent: D:... H:... M:... "

            } else { //TODO

                panel.Children.Add(butTime);
                panel.Children.Add(butDelete);
                if (t.prio == Priority.MID) {
                    labe.Background = Brushes.SandyBrown;
                } else if (t.prio == Priority.HIGH) {
                    labe.Background = Brushes.Salmon;
                } else {
                    labe.Background = Brushes.PapayaWhip;
                }
            }
            
        }

            private void Page_Loaded(object sender, RoutedEventArgs e) {
            repaint();
        }
        
        private void butR_Click(object sender, RoutedEventArgs e) {
            if(t.status == Status.TODO) {
                t.status = Status.DONE;
            } else {
                t.status = Status.TODO;
            }
            DatabaseTask.UpdateTask(t, t.text, t.prio, t.status, t.deadline);
            EventRepaint(EventArgs.Empty);
        }

        private void butTime_Click(object sender, RoutedEventArgs e) {
            //DINASH create window for user that sums time spent on task

        }

        private void butDelete_Click(object sender, RoutedEventArgs e) {
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
                    DatabaseTask.DeleteTask(t);
                    break;
                case MessageBoxResult.No:
                    break;
            }
            EventRepaint(EventArgs.Empty);
        }
    }
}
