using organizer.Codes;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace organizer {
    /// <summary>
    /// Interaction logic for RecyclePage.xaml
    /// </summary>
    public partial class RecyclePage : Page {

        private List<StackPanel> spList;
        private int tasksCnt = 0;
        private Database db;


        public RecyclePage() {
            InitializeComponent();
        }
    }
}
