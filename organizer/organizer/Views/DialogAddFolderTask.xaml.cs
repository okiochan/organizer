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
    /// Interaction logic for DialogAddFolderTask.xaml
    /// </summary>
    public partial class DialogAddFolderTask : Window {
        public DialogAddFolderTask() {
            InitializeComponent();
        }

        private void butApply_Click(object sender, RoutedEventArgs e) {
            this.DialogResult = true;
        }

        public string getTaskTitle {
            get { return txtBoxTitle.Text; }
        }
    }
}
