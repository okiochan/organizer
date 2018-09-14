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

        //HANDLER
        public event EventHandler HandlerAddTask;
        protected virtual void EventRepaintAP(EventArgs e) {
            EventHandler handler = HandlerAddTask;
            if (handler != null) {
                handler(this, e);
            }
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
                taskLook.HandlerButClicked += EventRepaint;
                
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


            //old version
            //foreach (var t in tf.tasks) {
            //    Label labe = new Label();
            //    labe.Content = t.text;
            //    labe.Name = "labe" + cnt.ToString();

            //    if(t.prio == Priority.MID) {
            //        labe.Background = Brushes.SandyBrown;
            //    } else if(t.prio == Priority.HIGH) {
            //        labe.Background = Brushes.Salmon;
            //    } else {
            //        labe.Background = Brushes.PapayaWhip;
            //    }

            //    labe.BorderBrush = Brushes.DarkSalmon;
            //    labe.BorderThickness = new Thickness(2);
            //    labe.Margin = new Thickness(10);
            //    labe.Height = 30;

            //    //labe.Drop += Button_Drop;
            //    //labe.MouseDown += Button_MouseDown;
            //    //labe.AllowDrop = true;

            //    if (t.status == Status.DONE) {
            //        labe.Background = Brushes.AliceBlue;
            //        labe.BorderBrush = Brushes.DarkGray;
            //        panelMiddle.Children.Add(labe);
            //    } else {
            //        panelLeft.Children.Add(labe);
            //    }
            //    cnt++;
            //}

            foreach (var n in tf.notes) {
                TextBox tb = new TextBox();
                tb.Name = "txtBox" + cnt.ToString();
                tb.Text = n.text;
                tb.Background = Brushes.SeaShell;
                tb.BorderThickness = new Thickness(2);
                tb.BorderBrush = Brushes.DarkKhaki;
                tb.Margin = new Thickness(10, 10, 10, 10);
                tb.Height = 50;
                panelRight.Children.Add(tb);
                cnt++;
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
            
            DialogAddTask d = new DialogAddTask(tf);
            d.HandlerButApplyClick += EventRepaint;
            
            if (d.ShowDialog() == true) {
                //repaint all
                EventRepaintAP(EventArgs.Empty);
                repaint();
            } else {
                MessageBox.Show("Info not saved =(");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            repaint();
        }

        private void butAddNote_Click(object sender, RoutedEventArgs e) {
            AddNote an = new AddNote(tf);
            an.HandlerButApplyClick += EventRepaint;

            if (an.ShowDialog() == true) {
                //repaint all
                EventRepaintAP(EventArgs.Empty);
                repaint();
            } else {
                MessageBox.Show("Info not saved =(");
            }
        }
    }
}
