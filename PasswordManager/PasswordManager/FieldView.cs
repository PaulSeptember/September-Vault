using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;


namespace App1
{
    class FieldView 
    {
        public event PropertyChangedEventHandler PropertyChanged;

       
        public string name { get; set; }
        public string icon {
            get { return "key_icon.xml"; }
            set { }
        }
    }
}
