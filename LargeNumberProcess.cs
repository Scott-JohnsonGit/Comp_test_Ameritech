using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Comp_test_Ameritech
{
    /// <summary>
    /// Process the use of large non-parsable numbers 
    /// </summary>
    public class LargeNumberProcess : IDisposable
    {
        /// <summary>
        /// Chunks of data, supposed to lower memory use
        /// </summary>
        public List<DataSet> Chunks { get { return _chunks; } }
        private List<DataSet> _chunks = new List<DataSet>();
        /// <summary>
        /// DataSet object holding numbers numbers
        /// </summary>
        public DataSet CurrentData { get { return _currentData; } }
        private DataSet _currentData = null;
        /// <summary>
        /// Standard initalization with externally set data
        /// </summary>
        public LargeNumberProcess()
            : this(null)
        {
            
        }
        /// <summary>
        /// Initalization creating data from selected file stream (just for more options)
        /// </summary>
        /// <param name="sr">Stream reader for selected file</param>
        public LargeNumberProcess(in StreamReader? sr)
        {
            _currentData = new DataSet();
            if (sr != null)
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    _currentData.Add(new Data(line));
                }
                _chunks = ChunkData(_currentData, _currentData.Count / 10);
            }
        }
        /// <summary>
        /// Chunk up data
        /// </summary>
        /// <param name="dataSet">Single large dataset to chunk up</param>
        /// <param name="groupSize">Ammount of data in each group</param>
        /// <returns>List of DataSets</returns>
        public static List<DataSet> ChunkData(in DataSet dataSet, int groupSize)
        {
            List<DataSet> chunks = new List<DataSet>();
            DataSet newSet = new DataSet();
            int count = 0;
            foreach (Data data in dataSet)
            {
                count++;
                newSet.Add(data);
                if (count % groupSize == 0)
                {
                    chunks.Add(newSet);
                    newSet = new DataSet();
                }
            }
            if (newSet.Count > 0)
            {
                chunks.Add(newSet);
            }
            return chunks;
        }
        /// <summary>
        /// Takes last 10 digits of the sum of various numbers
        /// </summary>
        /// <returns>Last 10 digits</returns>
        public string Process()
        {
            return Process(out _);
        }
        /// <summary>
        /// Takes last 10 digits of the sum of various numbers
        /// </summary>
        /// <param name="newData">Last 10 digits converted to Data</param>
        /// <returns>Last 10 digits</returns>
        public string Process(out Data newData)
        {
            if (CurrentData != null)
            {
                return SumByOne(CurrentData, 10, out newData);
            }
            else
            {
                throw new InvalidOperationException("Data needs to be set first");
            }
        }

        /* 
         * This code takes each data (number)
         * Reverses each piece of data
         * Adds the numbers together by the digit place (ones, tens, hundreds etc..)
         * Add previous remainder to new 
         * Adds only the final digit of the sum to the new total string
         * Takes the rest of the digits as the new remainder
         * Repeat for each piece of data
         * 
         * Does not work with negetives entirely, framework is in place but i do not have the time this week to finish it (doesnt output the right number every time... or most times)
         */
        #region Sum functions
        /// <summary>
        /// Gather the sum of large non-parsable numbers
        /// </summary>
        /// <param name="DataList">DataSet object of the numbers</param>
        /// <returns>String sum of numbers</returns>
        protected internal static string SumByOne(in DataSet DataList)
        {
            return SumByOne(DataList, (byte)(DataList.LargestNumber().Number.Length + 1), out _);
        }
        /// <summary>
        /// Gather the sum of large non-parsable numbers
        /// </summary>
        /// <param name="DataList">DataSet object of the numbers</param>
        /// <param name="newData">New string of sum converted to Data object</param>
        /// <returns>String sum of numbers</returns>
        protected internal static string SumByOne(in DataSet DataList, out Data newData)
        {
            return SumByOne(DataList, (byte)(DataList.LargestNumber().Number.Length + 1), out newData);
        }
        /// <summary>
        /// Gather the sum of large non-parsable numbers
        /// </summary>
        /// <param name="DataList">DataSet object of the numbers</param>
        /// <param name="digits">How many digits (small end) to keep</param>
        /// <returns>String sum of numbers</returns>
        protected internal static string SumByOne(in DataSet DataList, int digits)
        {
            return SumByOne(DataList, digits, out _);
        }
        /// <summary>
        /// Gather the sum of large non-parsable numbers
        /// </summary>
        /// <param name="DataList">DataSet object of the numbers</param>
        /// <param name="digits">How many digits (small end) to keep</param>
        /// <param name="newData">New string of sum converted to Data object</param>
        /// <returns>String sum of numbers</returns>
        protected internal static string SumByOne(DataSet DataList, int digits, out Data newData)
        {
            // Could've used an array instead of utilizing substrings, did not for very large array memory/crashing concerns
            // Find number with the most digits for the iteration length
            string biggestNum = DataList.LargestNumber().Number;
            // Iterate until either index runs out or digit limit is reached
            newData = new Data("", true);
            int remainder = 0;
            for (int i = 0; i < biggestNum.Length && i < digits; i++)
            {
                // The total of the numbers at the selected index
                int columnResult = 0;
                int subtract = 0;
                foreach (Data data in DataList)
                {
                    string revData;
                    if (data.Number.Length > digits)
                    {
                        revData = data.ReverseString().Substring(0, (int)digits);
                    }
                    else
                    {
                        revData = data.ReverseString();
                    }
                    if (revData.Length > i && !data.IsNegative)
                    {
                        columnResult += short.Parse(revData.Substring(i, 1));
                    }
                    else if (revData.Length > i && data.IsNegative)
                    {
                        subtract += byte.Parse(revData.Substring(i, 1));
                    }
                    // Skip over data that does not have the index length i
                    else
                    {
                        continue;
                    }
                }
                // Add on the remainder
                if ((columnResult - subtract < 0 || columnResult + remainder < 0) && i < digits - 1)
                {
                    columnResult += remainder + 10;
                    if (columnResult > 9)
                    {
                        remainder = int.Parse(columnResult.ToString().Substring(0, columnResult.ToString().Length - 1)) - 2;
                    }
                    else
                    {
                        remainder = -2;
                    }
                }
                else if (i > digits - 1)
                {
                    newData = new Data(newData.ReverseString(), true);
                    return newData.Number;
                }
                else
                {
                    columnResult += remainder;
                    if (columnResult > 9)
                    {
                        remainder = int.Parse(columnResult.ToString().Substring(0, columnResult.ToString().Length - 1));
                    }
                    else
                    {
                        remainder = 0;
                    }
                }
                columnResult -= subtract;
                // Add onto data string
                newData.Append(columnResult.ToString().Last().ToString());
            }
            // Add Remainder to final unless digit limit was reached
            if (remainder > 0 && (newData.Number + remainder).Length <= digits)
            {
                newData.Append(remainder.ToString());
            }
            newData = new Data(newData.ReverseString(), true);
            return newData.Number;
        }
        
        #endregion
        /// <summary>
        /// Allow manual override of DataMaster property
        /// </summary>
        /// <param name="newData">New DataSet instance to fill DataMaster</param>
        internal void SetNewData(DataSet newData)
        {
            _currentData = newData;
        }
        ~LargeNumberProcess()
        {
            Dispose();
        }
        public void Dispose()
        {
            _chunks.Clear();
            _currentData.Clear();
            _chunks = null;
            _currentData = null;
            GC.Collect();
            GC.SuppressFinalize(this);
        }
    }
}
