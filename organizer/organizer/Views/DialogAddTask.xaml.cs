using organizer.Codes;
using System;
using System.Windows;

namespace organizer {
    /// <summary>
    /// Interaction logic for DialogAddTask.xaml
    /// </summary>
    public partial class DialogAddTask : Window
    {
        private Priority prio;
        private int tfId;
        private Database db;

        public DialogAddTask(int tfId)
        {
            InitializeComponent();

            prio = Priority.LOW;
            db = new Database(@"..\..\..\db\tasks.db");
            this.tfId = tfId;
        }

        public event EventHandler HandlerButApplyClick;
        protected virtual void EventOnButtonClicked(EventArgs e) {
            EventHandler handler = HandlerButApplyClick;
            if (handler != null) {
                handler(this, e);
            }
        }

        private void butApply_Click(object sender, RoutedEventArgs e) {

            String text = "unnown title";
            if (txtBoxTitle.Text != "") {
                text = txtBoxTitle.Text;
            }

            //DEADLINE????
            int timeM = 0;

            int n;
            if (int.TryParse(txtBoxD.Text, out n) == true) {
                timeM += Int32.Parse(txtBoxD.Text) * 24 * 60;
            }
            if (int.TryParse(txtBoxH.Text, out n) == true) {
                timeM += Int32.Parse(txtBoxH.Text) * 60;
            }
            if (int.TryParse(txtBoxM.Text, out n) == true) {
                timeM += Int32.Parse(txtBoxM.Text);
            }

            List<TaskFolder> allFolders = db.ReadAll();
            db.CreateNewTask(text, prio, Status.TODO, DateTime.Now, allFolders[tfId]);
            //repaint
            EventOnButtonClicked(EventArgs.Empty);
            this.DialogResult = true;
        }


        private void butLow_Click(object sender, RoutedEventArgs e) {
            prio = Priority.LOW;
        }

        private void butMid_Click(object sender, RoutedEventArgs e) {
            prio = Priority.MID;
        }

        private void butHight_Click(object sender, RoutedEventArgs e) {
            prio = Priority.HIGH;
        }
    }
}
