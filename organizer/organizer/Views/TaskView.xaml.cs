using organizer.Codes;
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

namespace organizer.Views {
    /// <summary>
    /// Interaction logic for TaskView.xaml
    /// </summary>
    public partial class TaskView : Window {

        private TaskFolder tf;
        private int cnt = 0;

        public TaskView(TaskFolder tf) {
            InitializeComponent();
            this.tf = tf;
        }

        private void repaint()
        {

            panelLeft.Children.Clear();
            panelLeft.Children.Clear();
            cnt = 0;
            
            foreach (var t in tf.tasks) {
                Label labe = new Label();
                labe.Content = t.text;
                labe.Name = "labe" + cnt.ToString();
                labe.Background = Brushes.PapayaWhip;
                labe.BorderBrush = Brushes.DarkSalmon;
                labe.BorderThickness = new Thickness(2);
                labe.Margin = new Thickness(10);
                labe.Height = 30;
                
                if (t.status == Status.DONE) {
                    labe.Background = Brushes.AliceBlue;
                    labe.BorderBrush = Brushes.DarkGray;
                } else {
                    labe.Background = Brushes.LightPink;
                    labe.BorderBrush = Brushes.DarkRed;
                }

                panelLeft.Children.Add(labe);
                cnt++;
            }

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

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            repaint();
        }
    }
}
