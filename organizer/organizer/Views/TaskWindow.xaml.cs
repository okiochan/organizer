using organizer.Codes;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace organizer {
    /// <summary>
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    /// 

    public partial class TaskWindow : Window {

        private int tfId;
        private int cnt=0;
        private Database db;

        public TaskWindow( int tfId) {
            InitializeComponent();

            db = new Database(@"..\..\..\db\tasks.db");
            this.tfId = tfId;
        }

        public event EventHandler HandlerAddTask;
        protected virtual void EventTaskAdded(EventArgs e) {
            EventHandler handler = HandlerAddTask;
            if (handler != null) {
                handler(this, e);
            }
        }

        private void repaint() {

            panelMiddle.Children.Clear();
            panelLeft.Children.Clear();
            List<TaskFolder> allFolders = db.ReadAll();
            TaskFolder tf = allFolders[tfId];

            foreach (var t in tf.tasks) {
                Label labe = new Label();
                labe.Content = t.text;
                labe.Name = "labe" + cnt.ToString();
                labe.Margin = new Thickness(10);
                labe.Height = 30;
                labe.Drop += Button_Drop;
                labe.MouseDown += Button_MouseDown;
                labe.AllowDrop = true;

                if (t.status == Status.DONE) {
                    panelMiddle.Children.Add(labe);
                } else {
                    panelLeft.Children.Add(labe);
                }
                cnt++;
            }

            foreach (var n in tf.notes) {
                TextBox tb = new TextBox();
                tb.Name = "txtBox" + cnt.ToString();
                tb.Text = n.text;
                tb.Margin = new Thickness(10, 10, 10, 10);
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
            
            DialogAddTask d = new DialogAddTask(tfId);
            d.HandlerButApplyClick += EventButClicked;
            
            if (d.ShowDialog() == true) {
                //repaint all
                EventTaskAdded(EventArgs.Empty);
                repaint();
            } else {
                MessageBox.Show("Info not saved =(");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            repaint();
        }

        private void EventButClicked(object sender, EventArgs e) {
            repaint();
        }

    }
}
