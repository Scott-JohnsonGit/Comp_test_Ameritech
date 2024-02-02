using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Comp_test_Ameritech
{
    
    public class LargeNumberProcess
    {
        public DataSet DataMaster { get { return _dataMaster; } }
        private DataSet _dataMaster;
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
            _dataMaster = new DataSet();
            if (sr != null)
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    _dataMaster.Add(line);
                }
                
            }            
        }
        /// <summary>
        /// Takes last 10 digits of the sum of various numbers
        /// </summary>
        /// <returns>Last 10 digits</returns>
        public string Process()
        {
            //return SumByOne(DataMaster, 10);
            DataSet d = new DataSet(); d.Add("-5"); d.Add("95"); return SumByOne(d);
        }


        /*
         * My two ideas for integer compression:
         * A: Turn each number into many 8 digit int32's
         * B: Turn each number into binary and add together in binary
         */

        private static string SumByOne(DataSet DataList)
        {
            return SumByOne(DataList, uint.MaxValue);
        }
        private static string SumByOne(DataSet DataList, uint? digits)
        {
            string biggestNum = "";
            foreach (string d in DataList)
            {
                if (d.Length > biggestNum.Length)
                {
                    biggestNum = d;
                }
            }
            string total = "";
            int remainder = 0;
            for (int i = 0; i <= biggestNum.Length - 1 && i < digits; i++)
            {
                int columnResult = 0;
                foreach (string data in DataList)
                {
                    string revData = ReverseString(data);
                    if (revData.Length > i)
                    {
                        columnResult += int.Parse(revData.Substring(i, 1));
                    }
                    else
                    {
                        continue;
                    }

                }
                columnResult += remainder;
                if (columnResult > 9)
                {
                    remainder = int.Parse(columnResult.ToString().Substring(0, columnResult.ToString().Length - 1));
                }
                else
                {
                    remainder = 0;
                }
                total += columnResult.ToString().Last();
            }
            if (remainder > 0 && (total + remainder).Length < digits)
            {
                total += remainder;
            }
            return ReverseString(total);
        }
        private static string ReverseString(string str)
        {
            char[] chars = str.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }
        /// <summary>
        /// Allow manual override of DataMaster property
        /// </summary>
        /// <param name="newData">New DataSet instance to fill DataMaster</param>
        internal void SetNewData(DataSet newData)
        {
            _dataMaster = newData;
        }
    }
}
