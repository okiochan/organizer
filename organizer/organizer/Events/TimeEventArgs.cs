using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace organizer.Events {
    //EVENT ARGS time
    public class TimeEventArgs : EventArgs {
        private string H, M;

        public TimeEventArgs(string H, string M) {
            this.H = H;
            this.M = M;
        }
        public string getH {
            get { return H; }
            set { H = value; }
        }
        public string getM {
            get { return M; }
            set { M = value; }
        }
    }
}
