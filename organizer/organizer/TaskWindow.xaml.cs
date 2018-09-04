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
using System.Windows.Shapes;

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
                Button but = new Button();
                but.Content = t.text;
                but.Name = "but" + cnt.ToString();
                but.Margin = new Thickness(10, 0, 0, 0);
                but.Height = 30;
                // but.Drop = "Button_Drop";
                // but.AllowDrop = true;

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
            Button btn = (Button)sender;
            btn.Content = e.Data.ToString();
        }


    }
}
