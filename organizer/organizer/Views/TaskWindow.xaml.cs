using organizer.Codes;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace organizer {
    /// <summary>
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    /// 

    public partial class TaskWindow : Window {

        private TaskFolder tf;
        private int cnt=0;

        public TaskWindow( TaskFolder tf) {
            InitializeComponent();
            this.tf = tf;
            prepData();
        }

        // Drop????
        private void prepData() {

            //add Drop!
            foreach (var t in tf.tasks) {
                Label but = new Label();
                but.Content = t.text;
                but.Name = "but" + cnt.ToString();
                but.Margin = new Thickness(10);
                but.Height = 30;
                but.Drop += Button_Drop;
                but.MouseDown += Button_MouseDown;
                but.AllowDrop = true;
                //but.Click += Button_Click;

                if (t.status == Status.DONE) {
                    panelMiddle.Children.Add(but);
                } else {
                    panelLeft.Children.Add(but);
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


    }
}
