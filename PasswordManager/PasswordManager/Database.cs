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

        public override string ToString()
        {
            return name.ToString();
        }
    }

    class Database : IEnumerable
    {
        List<Field> fields = new List<Field>();
        
        private string password;
        
        public Database(string data)
        {
            string[] dataArray = data.Split('\n');
            password = dataArray[0];
            Field temp = new Field();
            for (var i = 1; i < dataArray.Length; i++)
            {
                if(i%4==1)
                    temp = new Field();

                switch (i % 4)
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
                    case 0:
                        temp.url = dataArray[i];
                        break;
                }
                if (i % 4 == 0)
                    fields.Add(temp);
            }
        }

        public string[] getForList()
        {
            string[] ans = new string[fields.Count];
            for (int i = 0; i < fields.Count; i++)
                ans[i] = fields[i].name;
            return ans;
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
            string ans = password + '\n';
            foreach(var i in fields)
            {
                ans += i.name + '\n';
                ans += i.login + '\n';
                ans += i.password + '\n';
                ans += i.url + '\n';
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