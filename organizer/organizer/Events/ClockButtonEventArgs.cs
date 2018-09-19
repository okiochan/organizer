using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace organizer.Events {
    //EVENT ARGS click position
    public class ClockButtonEventArgs : EventArgs {
        private double X, Y;
        private string content;

        public ClockButtonEventArgs(double X, double Y, string content) {
            this.X = X;
            this.Y = Y;
            this.content = content;
        }
        public double getX {
            get { return X; }
            set { X = value; }
        }
        public double getY {
            get { return Y; }
            set { Y = value; }
        }
        public string getContent {
            get { return content; }
            set { content = value; }
        }
    }
}
