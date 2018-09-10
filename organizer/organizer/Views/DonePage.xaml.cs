using organizer.Codes;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace organizer {
    /// <summary>
    /// Interaction logic for DonePage.xaml
    /// </summary>
    public partial class DonePage : Page {

        private List<StackPanel> spList;
        private int tasksCnt = 0;
        private Database db;

        public DonePage() {
            InitializeComponent();
        }

    }
}
