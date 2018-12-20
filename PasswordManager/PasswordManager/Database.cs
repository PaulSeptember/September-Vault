using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;

namespace App1
{

    public struct Field
    {
        public string name { get; set; }
        public string login;
        public string password;
        public string url;
        public string type;
        public string _icon;
        public string icon {
            get { return _icon; }
            set
            {
                this.type = value;
                if (value == "key")
                    _icon = "key_icon";
                if (value == "card")
                    _icon = "credit_card_icon";
                if (value == "secure")
                    _icon = "secure_note_icon";
                if (value == "wifi")
                    _icon = "wifi_icon";
            }   
        }

        public override string ToString()
        {
            return name.ToString();
        }
    }

    class Database : IEnumerable
    {
        public List<Field> fields = new List<Field>();
        
        private string password;
        
        public Database(string data)
        {
            char[] separator = {'&'};
            string[] dataArray = data.Split(separator);
            password = dataArray[0];
            Field temp = new Field();
            for (var i = 1; i < dataArray.Length; i++)
            {
                if(i%5==1)
                    temp = new Field();

                switch (i % 5)
                {
                    case 1:
                        temp.name = dataArray[i];
                        break;
                    case 2:
                        temp.login = dataArray[i];
                        break;
                    case 3:
                        temp.password = dataArray[i];
                        break;
                    case 4:
                        temp.url = dataArray[i];
                        break;
                    case 0:
                        temp.icon = dataArray[i];
                        break;
                }
                if (i % 5 == 0)
                {
                    fields.Add(temp);
                }
            }
        }
        
        public Field[] getForList()
        {
            Field[] ans = new Field[fields.Count];
            for (int i = 0; i < fields.Count; i++)
                ans[i] = fields[i];
            return ans;
            /*
            string[] ans = new string[fields.Count];
            for (int i = 0; i < fields.Count; i++)
            {
                ans[i] = fields[i].name;
                //ans[i].icon = "key_icon.xml";
            }
            return ans;*/
        }

        public int size()
        {
            return fields.Count;
        }

        public Field findByName(string name)
        {
            foreach (var x in fields)
                if (x.name == name)
                    return x;
            throw new Exception("pizda");    
        }

        public void Remove(string name)
        {
            for (int i = 0; i < fields.Count; i++)
                if (string.Equals(fields[i].name, name))
                {
                    fields.RemoveAt(i);
                    break;
                }
        }

        public string getForSave()
        {
            string ans = password + '&';
            foreach(var i in fields)
            {
                ans += i.name + '&';
                ans += i.login + '&';
                ans += i.password + '&';
                ans += i.url + '&';
                ans += i.type + '&';
            }
            return ans;
        }

        public bool isUnlock(string trying)
        {
            if (trying.Equals(null))
                throw new ArgumentNullException();
            return string.Equals(trying, password);
        }

        public void Add(Field toAdd)
        {
            fields.Add(toAdd);
        }

        public IEnumerator GetEnumerator()
        {
            return fields.GetEnumerator();
        }
    }
}