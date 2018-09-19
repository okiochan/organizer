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

        class pt {
            public double x, y;
            public pt(double x, double y) {
                this.x = x; this.y = y;
            }

            //convert to original coord
            public double toX(double W) {
                return W / 2 * x + W / 2;
            }
            public double toY(double H) {
                return -H / 2 * y + H / 2; ;
            }

            public void rotateAng(double ang) {
                double nx = x * Math.Cos(ang) + y * Math.Sin(ang);
                double ny = -x * Math.Sin(ang) + y * Math.Cos(ang);
                x = nx; y = ny;
            }
        };

        class button {
            Button btn;
            int id;
            double X, Y;
            Canvas canv;

            public button(int id, pt coord, Canvas canv, double X, double Y) {
                this.id = id;
                this.canv = canv;
                this.X = X;
                this.Y = Y;

                btn = new Button() {
                    Width = 20,
                    Height = 20,
                    Content = (id).ToString(),
                    Name = "btn" + (id).ToString(),
                    Background = Brushes.GhostWhite
                };
                btn.Click += btn_Click;
                
                // Add to a canvas
                canv.Children.Add(btn);
                Canvas.SetLeft(btn, X-btn.Width/2);
                Canvas.SetTop(btn, Y-btn.Height/2);
            }


            //EVENTS--------------------------
            public event EventHandler<CustomEventArgs> HandlerEventRepaintCirc;
            protected virtual void EventRepaintCirc(CustomEventArgs e) {
                EventHandler<CustomEventArgs> handler = HandlerEventRepaintCirc;
                if (handler != null) {
                    handler(this, e);
                }
            }

            private void btn_Click(object sender, EventArgs e) {
                EventRepaintCirc(new CustomEventArgs(X, Y));
            }
        }

        //EVENT ARGS
        public class CustomEventArgs : EventArgs {
            private double X, Y;

            public CustomEventArgs(double X, double Y) {
                this.X = X;
                this.Y = Y;
            }
            public double getX {
                get { return X; }
                set { X = value; }
            }
            public double getY {
                get { return Y; }
                set { Y = value; }
            }
        }

        //EVENT
        private void EventBtnClicked(object sender, CustomEventArgs e) {

            double x = e.getX - circChoosen.Width / 2;
            double y = e.getY - circChoosen.Height / 2;
            double w = circChoosen.Width;
            double h = circChoosen.Height;
            double xc = x + w / 2;
            double yc = y + h / 2;

            circChoosen.Visibility = Visibility.Visible;
            Canvas.SetLeft(circChoosen,x);
            Canvas.SetTop(circChoosen, y);

            lineChoosen.Visibility = Visibility.Visible;
            lineChoosen.X2 = xc;
            lineChoosen.Y2 = yc;
        }

        private void repaint() {

            pt p = new pt(0, 0);
            double w = canv.Width;
            double h = canv.Height;
            double xc = p.toX(w);
            double yc = p.toY(h);

            int c = 7;
            Canvas.SetLeft(el1, xc - el1.Width / 2 +c);
            Canvas.SetTop(el1, yc - el1.Height / 2 +c);
            Canvas.SetLeft(el2, xc - el2.Width / 2 -c);
            Canvas.SetTop(el2, yc - el2.Height / 2 -c);
            Canvas.SetLeft(el3, xc - el3.Width / 2);
            Canvas.SetTop(el3, yc - el3.Height / 2);
            Canvas.SetLeft(el4, xc - el4.Width / 2);
            Canvas.SetTop(el4, yc - el4.Height / 2);

            //make inner circle
            p = new pt(0, .6);
            p.rotateAng(-30 * Math.PI / 180);
            for (int i = 0; i < 13; i++) {
                p.rotateAng(30 * Math.PI / 180);
                button btn = new button(i, p, canv, p.toX(w), p.toY(h));
                btn.HandlerEventRepaintCirc += EventBtnClicked;
            }
            
            //make outer circle
            p = new pt(0, .8);
            p.rotateAng(-30 * Math.PI / 180);
            for (int i = 0; i < 13; i++) {
                p.rotateAng(30 * Math.PI / 180);
                button btn = new button(i, p, canv, p.toX(w), p.toY(h));
                btn.HandlerEventRepaintCirc += EventBtnClicked;
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            repaint();
        }


    }
}
