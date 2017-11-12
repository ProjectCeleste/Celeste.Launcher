using System;
using System.Collections.Generic;
using System.Text;

namespace PopupTest
{
    public class StringEventArgs : EventArgs
    {
        public StringEventArgs(string str)
        {
            _string = str;
        }

        string _string;

        public string String
        {
            get { return _string; }
        }
    }
}
