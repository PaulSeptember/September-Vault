using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Collections;

namespace App1.Droid
{
    class Database : IEnumerable
    {
        Field[] fields;
        private string password;

        public Database(string data)
        {
            string[] dataArray = data.Split('\n');
            password = dataArray[0];
            fields = new Field[dataArray.Length/4];
            for(var i = 1; i < dataArray.Length; i++)
            {
                switch(i%4)
                {
                    case 1:
                        fields[i / 4].name = dataArray[i];
                        break;
                    case 2:
                        fields[i / 4].login = dataArray[i];
                        break;
                    case 3:
                        fields[i / 4].password = dataArray[i];
                        break;
                    case 0:
                        fields[i / 4].url = dataArray[i];
                        break;
                    
                }
            }
        }
        public struct Field 
        {
            public string name;
            public string login;
            public string password;
            public string url;

            public override string ToString()
            {
                return name;
            }
        }

        public bool isUnlock(string trying)
        {
            if (trying.Equals(null))
                throw new ArgumentNullException();
            return string.Equals(trying, password);
        }

        public IEnumerator GetEnumerator()
        {
            return fields.GetEnumerator();
        }
    }
}