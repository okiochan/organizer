using organizer.Views.Pages;
using System.Windows;

namespace organizer.Views {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class TaskFolderView : Window
    {
        private PageActive ap;
        private PageDone dp;
        private PageRecycle rp;

        public TaskFolderView() {
            InitializeComponent();
            mainFrame.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;
            ap = new PageActive();
            dp = new PageDone();
            rp = new PageRecycle();
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
