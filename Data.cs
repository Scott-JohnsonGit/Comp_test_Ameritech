using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comp_test_Ameritech
{
    /// <summary>
    /// Large int data
    /// </summary>
    public class Data
    {
        /// <summary>
        /// Data displayed in string form
        /// </summary>
        public string Number { get { return _number; } }
        /// <summary>
        /// Boolean descibing sign accosiated with variable
        /// </summary>
        public bool IsNegative { get; } = false;
        protected string _number;
        /// <summary>
        /// Data initalizer
        /// </summary>
        /// <param name="number">Number to assign Data in string form</param>
        public Data(string number)
            : this(number, false) 
        {
            
        }
        /// <summary>
        /// Data initalizer
        /// </summary>
        /// <param name="number"Number to assign Data in string form></param>
        /// <param name="skipIntCheck">Tell the system to skip automatic non-Integer removal</param>
        public Data(string number, bool skipIntCheck)
        {
            if (!skipIntCheck)
            {
                this._number = Remove(number);
                // Check negative after excess '-' charactors were removed
                if (number.Contains('-'))
                {
                    this.IsNegative = true;
                    this._number = Remove(_number, '-');
                }
            }
            else
            {
                this._number = number;
            }
        }
        #region Data adjusting
        /// <summary>
        /// Add values to the end of the data
        /// </summary>
        /// <param name="number">Number data to add to the end</param>
        public void Append(string number)
        {
            _number += Remove(number);
        }
        /// <summary>
        /// Remove non-int chars from the string
        /// </summary>
        /// <param name="text">String to remove from</param>
        /// <returns>Revised string with only integers</returns>
        protected static string Remove(string text)
        {
            return Remove(text, null);
        }
        /// <summary>
        /// Remove specific chars from the string
        /// </summary>
        /// <param name="text">String to remove from</param>
        /// <param name="cutChar">Chars to be targeted</param>
        /// <returns>Revised string without specified chars</returns>
        protected static string Remove(string text, char? cutChar)
        {
            if (cutChar != null)
            {
                return text.Replace(cutChar.ToString(), "");
            }
            string newString = new string(text.Where(c => char.IsDigit(c)).ToArray());
            return newString;
        }
        #endregion
        /// <summary>
        /// Finds reversed value of instance
        /// </summary>
        /// <returns>Reversed string of instance value</returns>
        public string ReverseString()
        {
            char[] chars = this._number.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }
        /// <summary>
        /// Seperate text by commas ',' reccomended before creating or appending data
        /// </summary>
        /// <param name="text">Text to be sperated</param>
        /// <returns>DataSet of seperated Data</returns>
        public static DataSet Seperate(string text)
        {
            string[] seperatedStrings = text.Split(',');
            DataSet datas = new DataSet();
            foreach (string s in seperatedStrings)
            {
                Data data = new Data(s);
                if (data.Number.Length > 0)
                {
                    datas.Add(data);
                }
            }
            return datas;
        }
        /// <summary>
        /// 
        /// </summary>
        public void FixFormat()
        {
            if (this._number.StartsWith('0'))
            {
                this._number = this._number.Substring(1);
            }
            // Add more own pleasure
        }
    }
}
