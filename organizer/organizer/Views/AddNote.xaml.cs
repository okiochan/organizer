using organizer.Codes;
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
using System.Windows.Shapes;

namespace organizer.Views {
    /// <summary>
    /// Interaction logic for AddNote.xaml
    /// </summary>
    public partial class AddNote : Window {

        private TaskFolder tf;

        public AddNote(TaskFolder tf) {
            InitializeComponent();
            this.tf = tf;
        }

        //Handler
        public event EventHandler HandlerButApplyClick;
        protected virtual void EventOnButtonClicked(EventArgs e) {
            EventHandler handler = HandlerButApplyClick;
            if (handler != null) {
                handler(this, e);
            }
        }

        private void butApply_Click(object sender, RoutedEventArgs e) {
            DatabaseNote.CreateNewNote(txtNote.Text, tf);

            EventOnButtonClicked(EventArgs.Empty);
            this.DialogResult = true;
        }
    }
}
