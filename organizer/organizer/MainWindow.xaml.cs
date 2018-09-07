using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace organizer {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ActivePage ap;
        private DonePage dp;
        private RecyclePage rp;

        public MainWindow() {
            InitializeComponent();
            mainFrame.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;
            ap = new ActivePage();
            dp = new DonePage();
            rp = new RecyclePage();
        }
        
        private void butActive_Click(object sender, RoutedEventArgs e) {
            mainFrame.Navigate(ap);
        }

        private void butDone_Click(object sender, RoutedEventArgs e) {
            mainFrame.Navigate(dp);
        }

        private void butRecycle_Click(object sender, RoutedEventArgs e) {
            mainFrame.Navigate(rp);

        }
    }
}
