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
        public string? Number { get { return _number; } }
        /// <summary>
        /// Boolean descibing sign accosiated with variable
        /// </summary>
        public bool IsNegative { get; }
        protected string? _number;
        /// <summary>
        /// Data initalizer
        /// </summary>
        /// <param name="number">Number to assign Data in string form</param>
        public Data(string number)
        {
            this._number = Remove(number);
            // Check negative after excess '-' charactors were removed
            this.IsNegative = number.Contains('-');
            this._number = Remove(_number, '-');
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
            bool foundInt = false;
            // Skip iterating through chars
            if (cutChar != null)
            {
                return text.Replace(cutChar.ToString(), "");
            }
            // Find non-int chars and remove them
            foreach (char c in text)
            {
                // Try to avoid removing integers sign (not foolproof)
                if (c == '-' && !foundInt)
                {
                    continue;
                }
                if (!int.TryParse(c.ToString(), out _))
                {
                    text = text.Replace(c.ToString(), "");
                }
                else
                {
                    foundInt = true;
                }
            }
            return text;
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
                datas.Add(new Data(s));
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
