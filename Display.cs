using System;
using System.Diagnostics;
using System.Runtime;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Comp_test_Ameritech
{
    public partial class CSVFileCollectionForm : Form
    {
        public CSVFileCollectionForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Opens file explorer and retrieves selected CSV file data
        /// </summary>
        #if !DEBUG
        private void OpenFileSelector(object? sender, EventArgs? e)
        {

            // Open file explorer with filter
            OpenFileDialog fileDialog = new OpenFileDialog()
            {
                Title = "Open file",
                FileName = "Select CSV file",
                Filter = "CSV files (.CSV)|*.CSV"
            };
            // User selects file
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                // Open the CSV file data in notepad
                Process.Start("notepad.exe", fileDialog.FileName);
                // Read data by line
                using (StreamReader sr = new StreamReader(fileDialog.FileName))
                {
                    DataSet dataSet = new DataSet();
                    while (!sr.EndOfStream)
                    {

                        string s = sr.ReadLine();
                        // Make sure data is seperated (CSV files only)
                        if (s.Contains(','))
                        {
                            DataSet seperatedData = Data.Seperate(Text);
                            foreach (Data data in seperatedData)
                            {
                                dataSet.Add(data);
                            }
                            seperatedData.Clear();
                            continue;
                        }
                        dataSet.Add(new Data(s));
                        
                    }
                    // Begin proccessing and display data
                    FinishData(LargeNumberProcess.ChunkData(dataSet, 10));
                    dataSet.Clear();
                    dataSet = null;
                }
            }
        }
        #else
        // TESING OUT MEMORY SAVING FEATURE WITH LARGE NUMBERS 
        // Takes a very long time, save lotta memory though
        private void OpenFileSelector(object? sender, EventArgs? e)
        { 
            
            DataSet ds = new DataSet();
            Random random = new Random();
            for (int i = 0; i < 6000000; i++)
            {
                ds.Add(new Data(random.Next(int.MaxValue).ToString() + random.Next(int.MaxValue).ToString()));
            }
            List<DataSet> datas = LargeNumberProcess.ChunkData(ds, 500000);
            ds.Clear();
            ds = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            FinishData(datas);
        }
        #endif
        /// <summary>
        /// Cycle through adding all data
        /// </summary>
        /// <param name="datas"></param>
        private void FinishData(in List<DataSet> datas)
        {
            LargeNumberProcess process = new LargeNumberProcess();
            DataSet ds = new DataSet();
            for (int i = 0; i < datas.Count; i++)
            {
                process.SetNewData(datas[i]);
                process.Process(out Data d);
                ds.Add(d);
            }
            process.SetNewData(ds);
            DataBox.Text = process.Process();
        }
    }
}
