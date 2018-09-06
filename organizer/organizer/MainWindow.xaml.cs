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
            ap = new ActivePage();
            dp = new DonePage();
            rp = new RecyclePage();
        }
        
        private void butActive_Click(object sender, RoutedEventArgs e) {
            ap.ShowsNavigationUI = false;
            dp.ShowsNavigationUI = false;
            rp.ShowsNavigationUI = false;
            mainFrame.Navigate(ap);
        }

        private void butDone_Click(object sender, RoutedEventArgs e) {
            ap.ShowsNavigationUI = false;
            rp.ShowsNavigationUI = false;
            dp.ShowsNavigationUI = false;
            mainFrame.Navigate(dp);
        }

        private void butRecycle_Click(object sender, RoutedEventArgs e) {
            ap.ShowsNavigationUI = false;
            rp.ShowsNavigationUI = false;
            dp.ShowsNavigationUI = false;
            mainFrame.Navigate(rp);

        }
    }
}
