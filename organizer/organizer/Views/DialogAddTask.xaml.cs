using organizer.Codes;
using System.Windows;

namespace organizer {
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
