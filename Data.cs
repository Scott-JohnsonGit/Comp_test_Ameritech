using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comp_test_Ameritech
{
    public class Data
    {
        public string Text { get { return _text; } }
        protected string _text;
        public Data(string text)
        {
            this._text = RemoveNonInt(text);
        }

        protected string RemoveNonInt(string text)
        {
            foreach (char c in text)
            {
                if (c != '-' && !int.TryParse(c.ToString(), out _))
                {
                    text = text.Replace(c.ToString(), "");
                }
            }
            return text;
        }
    }
}
