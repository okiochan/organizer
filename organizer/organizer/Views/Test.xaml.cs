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
    /// Interaction logic for Test.xaml
    /// </summary>
    public partial class Test : Window {
        public Test() {
            InitializeComponent();
        }

        private void repaint() {
            // Add a Line Element
            //Line myLine = new Line();
            //myLine.Stroke = System.Windows.Media.Brushes.LightSteelBlue;
            //myLine.X1 = 1;
            //myLine.X2 = 50;
            //myLine.Y1 = 1;
            //myLine.Y2 = 50;
            //myLine.HorizontalAlignment = HorizontalAlignment.Left;
            //myLine.VerticalAlignment = VerticalAlignment.Center;
            //myLine.StrokeThickness = 2;
            //canv.Children.Add(myLine);

            //Ellipse myEllipse = new Ellipse();

            //// Create a SolidColorBrush with a red color to fill the 
            //// Ellipse with.
            //SolidColorBrush mySolidColorBrush = new SolidColorBrush();

            //// Describes the brush's color using RGB values. 
            //// Each value has a range of 0-255.
            //mySolidColorBrush.Color = Color.FromArgb(255, 255, 255, 0);
            //myEllipse.Fill = mySolidColorBrush;
            //myEllipse.StrokeThickness = 2;
            //myEllipse.Stroke = Brushes.Black;

            //// Set the width and height of the Ellipse.
            //myEllipse.Width = 200;
            //myEllipse.Height = 100;

            //// Add the Ellipse to the StackPanel.
            //canv.Children.Add(myEllipse);

            int number = 5;
            int width = 30;
            int height = 30;
            int y = 210;
            int x = 240;

            for (int i = 0; i < number; i++) {
                // Create the rectangle
                Rectangle rec = new Rectangle() {
                    Width = width,
                    Height = height,
                    Fill = Brushes.LightGreen
                };

                // Add to a canvas for example
                canv.Children.Add(rec);
                Canvas.SetLeft(rec, x);
                Canvas.SetTop(rec, y);
                
                y += 50;
                x -= 50;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            repaint();
        }
    }
}
