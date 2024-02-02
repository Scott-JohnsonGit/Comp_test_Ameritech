using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Comp_test_Ameritech
{
    public partial class CSVFileCollectionForm : Form
    {
        public CSVFileCollectionForm()
        {
            InitializeComponent();
        }
        private void OpenFileSelector(object? sender, EventArgs? e)
        {
            // Open file explorer
            OpenFileDialog fileDialog = new OpenFileDialog()
            {
                Title = "Open file",
                FileName = "Select CSV file",
                Filter = "CSV files (.CSV)|*.CSV"
            };
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                Process.Start("notepad.exe", fileDialog.FileName);
                DataSet data = new DataSet();
                using (StreamReader sr = new StreamReader(fileDialog.FileName))
                {
                    while (!sr.EndOfStream)
                    {
                        data.Add(sr.ReadLine());
                    }
                    LargeNumberProcess process = new LargeNumberProcess();
                    process.SetNewData(data);
                    DataBox.Text = process.Process();
                }
                
            }
            
        }
        
    }
}
