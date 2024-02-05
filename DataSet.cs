using Microsoft.Win32.SafeHandles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Comp_test_Ameritech
{
    /// <summary>
    /// Listed set of Data objects
    /// </summary>
    public class DataSet : IDisposable
    {
        public int Count { get { return _masterList.Count; } }
        // Stolen HashSet for storing data with minimal memory usage
        protected HashSet<Data> _masterList = new HashSet<Data>();
        private bool _disposedValue;
        /// <summary>
        /// Combines all stored data into one string (For ease of displaying)
        /// </summary>
        /// <returns>Combined string of instance values</returns>
        public string CombineSets()
        {
            return CombineSets("");
        }
        /// <summary>
        /// Combines all stored data into one string (For ease of displaying)
        /// </summary>
        /// <param name="seperator">String that seperates values</param>
        /// <returns>Combined string of instance values</returns>
        /// <exception cref="ArgumentNullException">Seperator parameter is null</exception>
        public string CombineSets(string seperator)
        {
            if (seperator == null)
            {
                throw new ArgumentNullException("seperator");
            }
            string fullSet = "";
            foreach (Data set in _masterList)
            {
                fullSet += set.Number + seperator;
            }
            return fullSet;
        }
        /// <summary>
        /// Add Data object to collection
        /// </summary>
        /// <param name="set">New Data object coming in</param>
        /// <exception cref="ArgumentNullException">Data is null</exception>
        public void Add(Data set)
        {
            if (set != null && set.Number != null)
            {
                _masterList.Add(set);
            }
            else
            {
                throw new ArgumentNullException("set");
            }
        }
        /// <summary>
        /// Clear collection
        /// </summary>
        public void Clear()
        {
            _masterList.Clear();
        }
        /// <summary>
        /// Find The Largest number in the instance collection
        /// </summary>
        /// <returns>Largest number by digits</returns>
        public Data LargestNumber()
        {
            Data biggestNum = new Data("");
            foreach (Data d in this._masterList)
            {
                if (d.Number.Length > biggestNum.Number.Length)
                {
                    biggestNum = d;
                }
            }
            return biggestNum;
        }
        ~DataSet()
        {
            Dispose(false);
        }
        // Basically cheating iteration via stealing _masterList's Enumerator (Leaving public is typically bad practice)
        public IEnumerator GetEnumerator()
        {
            return _masterList.GetEnumerator();
        }
        #region Garbage collection
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {

                }
                this._masterList.Clear();
                
                _disposedValue = true;
            }
        }
        #endregion
    }

}
