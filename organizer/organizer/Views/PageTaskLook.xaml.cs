﻿using organizer.Codes;
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

            if (t.status == Status.DONE) { //DONE

                labe.Background = Brushes.AliceBlue;
                labe.BorderBrush = Brushes.DarkGray;
                butR.Content = "<--";
                panel.Children.Remove(butTime);

                int time = t.timeSpent;
                int mins = time % 60;
                time /= 60;
                int hours = time % 60;
                time /= 60;
                int days = time;
                txtTime.Text = String.Format("Spent: D:{0} H:{1} M:{2}", days, hours, mins);

            } else { //TODO

                panel.Children.Remove(txtTime);

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
            DatabaseTask.UpdateTask(t);
            EventRepaint(EventArgs.Empty);
        }

        private void butTime_Click(object sender, RoutedEventArgs e) {
            AddTimeSpentWindow wnd = new AddTimeSpentWindow(t);
            wnd.Show();
        }
    }
}
