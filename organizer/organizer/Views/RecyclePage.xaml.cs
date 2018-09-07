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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
