using organizer.Events;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace organizer.Views.Pages {
    /// <summary>
    /// Interaction logic for PageClock.xaml
    /// </summary>
    public partial class PageClock : Page {

        class button {
            string id;
            double X, Y;
            Button btn;
            Canvas canv;

            public button(string id, Point coord, Canvas canv, double X, double Y) {
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
                btn.Click += ButtonClicked;

                // Add to a canvas
                canv.Children.Add(btn);
                Canvas.SetLeft(btn, X - btn.Width / 2);
                Canvas.SetTop(btn, Y - btn.Height / 2);
            }


            //HANDLER
            public event EventHandler<ClockButtonEventArgs> Click;
            protected virtual void OnButtonClick(ClockButtonEventArgs e) {
                EventHandler<ClockButtonEventArgs> handler = Click;
                if (handler != null) {
                    handler(this, e);
                }
            }

            private void ButtonClicked(object sender, EventArgs e) {
                OnButtonClick(new ClockButtonEventArgs(X, Y, id ));
            }
        }
        
        //----------------classes end

        private bool drawH;
        private string hour, min;

        public PageClock() {
            InitializeComponent();
            drawH = true;
            hour = "00";
            min = "00";
        }

        public string getHours() {
            return hour;
        }
        public string getMinutes() {
            return min;
        }

        private void OnLoaded(object sender, RoutedEventArgs e) {
            Repaint();
        }

        public void setDrawH(bool H) {
            drawH = H;
            Repaint();
        }

        //HANDLER
        public event EventHandler<TimeEventArgs> TimeChanged;
        protected virtual void OnTimeChanged(TimeEventArgs e) {
            EventHandler<TimeEventArgs> handler = TimeChanged;
            if (handler != null) {
                handler(this, e);
            }
        }

        //EVENT clicked
        private void ButtonClicked(object sender, ClockButtonEventArgs e) {

            double x = e.getX - circChoosen.Width / 2;
            double y = e.getY - circChoosen.Height / 2;
            double w = circChoosen.Width;
            double h = circChoosen.Height;
            double xc = x + w / 2;
            double yc = y + h / 2;

            circChoosen.Visibility = Visibility.Visible;
            Canvas.SetLeft(circChoosen, x);
            Canvas.SetTop(circChoosen, y);

            lineChoosen.Visibility = Visibility.Visible;
            lineChoosen.X2 = xc;
            lineChoosen.Y2 = yc;

            if(drawH == true) {
                hour = e.getContent;
            } else {
                min = e.getContent;
            }

            OnTimeChanged(new TimeEventArgs(hour, min));
        }

        private void Repaint() {
            canv.Children.Clear();

            canv.Children.Add(el1);
            canv.Children.Add(el2);
            canv.Children.Add(el3);
            canv.Children.Add(el4);
            canv.Children.Add(circChoosen);
            canv.Children.Add(lineChoosen);
            circChoosen.Visibility = Visibility.Hidden;
            lineChoosen.Visibility = Visibility.Hidden;

            Point p = new Point(0, 0);
            double w = canv.Width;
            double h = canv.Height;
            double xc = p.toX(w);
            double yc = p.toY(h);

            int c = 7;
            Canvas.SetLeft(el1, xc - el1.Width / 2 + c);
            Canvas.SetTop(el1, yc - el1.Height / 2 + c);
            Canvas.SetLeft(el2, xc - el2.Width / 2 - c);
            Canvas.SetTop(el2, yc - el2.Height / 2 - c);
            Canvas.SetLeft(el3, xc - el3.Width / 2);
            Canvas.SetTop(el3, yc - el3.Height / 2);
            Canvas.SetLeft(el4, xc - el4.Width / 2);
            Canvas.SetTop(el4, yc - el4.Height / 2);
            lineChoosen.X1 = xc;
            lineChoosen.Y1 = yc;

            if(drawH == true) {

                //make inner circle
                p = new Point(0, .5);
                p = p.Rotate(-30 * Math.PI / 180);
                for (int i = 0; i < 13; i++) {
                    p = p.Rotate(30 * Math.PI / 180);
                    string id = "" + i;
                    button btn = new button(id, p, canv, p.toX(w), p.toY(h));
                    btn.Click += ButtonClicked;
                }

                //make outer circle
                p = new Point(0, .8);
                p = p.Rotate(-30 * Math.PI / 180);
                for (int i = 0; i < 13; i++) {
                    p = p.Rotate(30 * Math.PI / 180);
                    string id = "" + (i+12);
                    if(i+12 == 24) { id = "00"; }
                    button btn = new button(id, p, canv, p.toX(w), p.toY(h));
                    btn.Click += ButtonClicked;
                }
            } else {

                //make outer circle
                p = new Point(0, .8);
                p = p.Rotate(-30 * Math.PI / 180);
                for (int i = 0; i < 13; i++) {
                    p = p.Rotate(30 * Math.PI / 180);
                    string id = "" + (i * 5);
                    if (i * 5 == 60) { id = "00"; }
                    if(i * 5 == 5) { id = "05"; }
                    button btn = new button(id, p, canv, p.toX(w), p.toY(h));
                    btn.Click += ButtonClicked;
                }
            }

        }

        private class Point {
            public double x, y;
            public Point(double x, double y) {
                this.x = x;
                this.y = y;
            }

            //convert to original coord
            public double toX(double W) {
                return W / 2 * x + W / 2;
            }

            public double toY(double H) {
                return -H / 2 * y + H / 2; ;
            }

            public Point Rotate(double ang) {
                double nx = x * Math.Cos(ang) + y * Math.Sin(ang);
                double ny = -x * Math.Sin(ang) + y * Math.Cos(ang);
                return new Point(nx, ny);
            }
        }
    }
}
