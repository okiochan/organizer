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

namespace organizer
{
    /// <summary>
    /// Interaction logic for DialogAddTask.xaml
    /// </summary>
    public partial class DialogAddTask : Window
    {
        private Task t;
        
        public DialogAddTask(TaskFolder tf)
        {
            InitializeComponent();

            t = new Task();
            t.text = "unnown title";
            t.prio = Priority.LOW;
        }
        
        private void butApply_Click(object sender, RoutedEventArgs e) {

            if(txtBoxTitle.Text!="") {
                t.text = txtBoxTitle.Text;
            }
            
            this.DialogResult = true;
        }

        private void butLow_Click(object sender, RoutedEventArgs e) {
            t.prio = Priority.LOW;
        }

        private void butMid_Click(object sender, RoutedEventArgs e) {
            t.prio = Priority.MID;
        }

        private void butHight_Click(object sender, RoutedEventArgs e) {
            t.prio = Priority.HIGH;
        }
    }
}
