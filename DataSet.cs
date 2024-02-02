using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comp_test_Ameritech
{
    public class DataSet
    {
        public string CombinedData { get { return _combinedData; } }
        protected string _combinedData = "";
        protected List<string> _masterList = new List<string>();
        public object Current { get { return _masterList[position]; }
        }
        protected int position = -1;
        protected void CombineSets()
        {
            string fullSet = "";
            foreach (string set in _masterList)
            {
                fullSet += set;
            }
            _combinedData = fullSet;
        }
        public void Add(string set)
        {
            _masterList.Add(set);
            IsolateInteger();
        }
        public void Clear()
        {
            _masterList = new List<string>();
            _combinedData = "";
        }
        protected void IsolateInteger()
        {
            for (int i = 0; i < _masterList.Count; i++)
            {
                
            }
            CombineSets();
        }
        public IEnumerator GetEnumerator()
        {
            return _masterList.GetEnumerator();
        }
    }
    
}
