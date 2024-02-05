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
                            DataSet seperatedData = Data.Seperate(s);
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
                }
            }
        }
#else
        
        // TESING OUT MEMORY SAVING FEATURE WITH LARGE NUMBERS 
        // Spoiler its bad
        private DataSet TEST_dataSet()
        {
            DataSet ds = new DataSet();
            Random random = new Random();
            for (int i = 0; i < 60000000; i++)
            {
                ds.Add(new Data(random.Next(int.MaxValue).ToString() + random.Next(int.MaxValue).ToString() + random.Next(int.MaxValue).ToString() + random.Next(int.MaxValue).ToString()));
            }
            return ds;
        }
        // FOR TESTING ONLY
        #region Preset testing fields
        // Should take integers from the jumble
        private string _TestString1 = "Name,HEX,RGB\r\nWhite,#FFFFFF,\"rgb(100,100,100)\"\r\nSilver,#C0C0C0,\"rgb(75,75,75)\"\r\nGray,#808080,\"rgb(50,50,50)\"\r\nBlack,#000000," +
            "\"rgb(0,0,0)\"\r\nRed,#FF0000,\"rgb(100,0,0)\"\r\nMaroon,#800000,\"rgb(50,0,0)\"\r\nYellow,#FFFF00,\"rgb(100,100,0)\"\r\nOlive,#808000,\"rgb(50,50,0)\"\r\nLime," +
            "#00FF00,\"rgb(0,100,0)\"\r\nGreen,#008000,\"rgb(0,50,0)\"\r\nAqua,#00FFFF,\"rgb(0,100,100)\"\r\nTeal,#008080,\"rgb(0,50,50)\"\r\nBlue,#0000FF,\"rgb(0,0,100)\"\r\n" +
            "Navy,#000080,\"rgb(0,0,50)\"\r\nFuchsia,#FF00FF,\"rgb(100,0,100)\"\r\nPurple,#800080,\"rgb(50,0,50)\"";
        // Should return nothing if working right
        private string _TestString2 = "Code,Symbol,Name\r\nAED,د.إ,United Arab Emirates d\r\nAFN,؋,Afghan afghani\r\nALL,L,Albanian lek\r\nAMD,AMD,Armenian dram\r\nANG,ƒ," +
            "Netherlands Antillean gu\r\nAOA,Kz,Angolan kwanza\r\nARS,$,Argentine peso\r\nAUD,$,Australian dollar\r\nAWG,Afl.,Aruban florin\r\nAZN,AZN,Azerbaijani manat\r\nBAM," +
            "KM,Bosnia and Herzegovina \r\nBBD,$,Barbadian dollar\r\nBDT,৳ ,Bangladeshi taka\r\nBGN,лв.,Bulgarian lev\r\nBHD,.د.ب,Bahraini dinar\r\nBIF,Fr,Burundian franc\r\nBMD," +
            "$,Bermudian dollar\r\nBND,$,Brunei dollar\r\nBOB,Bs.,Bolivian boliviano\r\nBRL,R$,Brazilian real\r\nBSD,$,Bahamian dollar\r\nBTC,฿,Bitcoin\r\nBTN,Nu.," +
            "Bhutanese ngultrum\r\nBWP,P,Botswana pula\r\nBYR,Br,Belarusian ruble (old)'\r\nBYN,Br,Belarusian ruble\r\nBZD,$,Belize dollar\r\nCAD,$,Canadian dollar\r\nCDF,Fr," +
            "Congolese franc\r\nCHF,CHF,Swiss franc\r\nCLP,$,Chilean peso\r\nCNY,¥,Chinese yuan\r\nCOP,$,Colombian peso\r\nCRC,₡,Costa Rican colón\r\nCUC,$,Cuban convertible" +
            " peso')\r\nCUP,$,Cuban peso\r\nCVE,$,Cape Verdean escudo\r\nCZK,Kč,Czech koruna\r\nDJF,Fr,Djiboutian franc\r\nDKK,DKK,Danish krone\r\nDOP,RD$,Dominican peso\r\nDZD," +
            "د.ج,Algerian dinar\r\nEGP,EGP,Egyptian pound\r\nERN,Nfk,Eritrean nakfa\r\nETB,Br,Ethiopian birr\r\nEUR,€,Euro\r\nFJD,$,Fijian dollar\r\nFKP,£,Falkland Islands pound')" +
            "\r\nGBP,£,Pound sterling\r\nGEL,₾,Georgian lari\r\nGGP,£,Guernsey pound\r\nGHS,₵,Ghana cedi\r\nGIP,£,Gibraltar pound\r\nGMD,D,Gambian dalasi\r\nGNF,Fr,Guinean franc" +
            "\r\nGTQ,Q,Guatemalan quetzal\r\nGYD,$,Guyanese dollar\r\nHKD,$,Hong Kong dollar\r\nHNL,L,Honduran lempira\r\nHRK,kn,Croatian kuna\r\nHTG,G,Haitian gourde\r\nHUF,Ft," +
            "Hungarian forint\r\nIDR,Rp,Indonesian rupiah\r\nILS,₪,Israeli new shekel\r\nIMP,£,Manx pound\r\nINR,₹,Indian rupee\r\nIQD,ع.د,Iraqi dinar\r\nIRR,﷼,Iranian rial" +
            "\r\nIRT,تومان,Iranian toman\r\nISK,kr.,Icelandic króna\r\nJEP,£,Jersey pound\r\nJMD,$,Jamaican dollar\r\nJOD,د.ا,Jordanian dinar\r\nJPY,¥,Japanese yen\r\nKES,KSh," +
            "Kenyan shilling\r\nKGS,сом,Kyrgyzstani som\r\nKHR,៛,Cambodian riel\r\nKMF,Fr,Comorian franc\r\nKPW,₩,North Korean won\r\nKRW,₩,South Korean won\r\nKWD,د.ك," +
            "Kuwaiti dinar\r\nKYD,$,Cayman Islands dollar\r\nKZT,₸,Kazakhstani tenge\r\nLAK,₭,Lao kip\r\nLBP,ل.ل,Lebanese pound\r\nLKR,රු,Sri Lankan rupee\r\nLRD,$,Liberian dollar" +
            "\r\nLSL,L,Lesotho loti\r\nLYD,ل.د,Libyan dinar\r\nMAD,د.م.,Moroccan dirham\r\nMDL,MDL,Moldovan leu\r\nMGA,Ar,Malagasy ariary\r\nMKD,ден,Macedonian denar\r\nMMK,Ks," +
            "Burmese kyat\r\nMNT,₮,Mongolian tögrög\r\nMOP,P,Macanese pataca\r\nMRU,UM,Mauritanian ouguiya\r\nMUR,₨,Mauritian rupee\r\nMVR,.ރ,Maldivian rufiyaa\r\nMWK,MK,Malawian " +
            "kwacha\r\nMXN,$,Mexican peso\r\nMYR,RM,Malaysian ringgit\r\nMZN,MT,Mozambican metical\r\nNAD,N$,Namibian dollar\r\nNGN,₦,Nigerian naira\r\nNIO,C$,Nicaraguan córdoba" +
            "\r\nNOK,kr,Norwegian krone\r\nNPR,₨,Nepalese rupee\r\nNZD,$,New Zealand dollar\r\nOMR,ر.ع.,Omani rial\r\nPAB,B/.,Panamanian balboa\r\nPEN,S/,Sol\r\nPGK,K,Papua " +
            "New Guinean kina')\r\nPHP,₱,Philippine peso\r\nPKR,₨,Pakistani rupee\r\nPLN,zł,Polish złoty\r\nPRB,р.,Transnistrian ruble\r\nPYG,₲,Paraguayan guaraní\r\nQAR,ر.ق," +
            "Qatari riyal\r\nRON,lei,Romanian leu\r\nRSD,рсд,Serbian dinar\r\nRUB,₽,Russian ruble\r\nRWF,Fr,Rwandan franc\r\nSAR,ر.س,Saudi riyal\r\nSBD,$,Solomon Islands dollar')" +
            "\r\nSCR,₨,Seychellois rupee\r\nSDG,ج.س.,Sudanese pound\r\nSEK,kr,Swedish krona\r\nSGD,$,Singapore dollar\r\nSHP,£,Saint Helena pound\r\nSLL,Le,Sierra Leonean leone" +
            "\r\nSOS,Sh,Somali shilling\r\nSRD,$,Surinamese dollar\r\nSSP,£,South Sudanese pound\r\nSTN,Db,São Tomé and Príncipe d\r\nSYP,ل.س,Syrian pound\r\nSZL,L,Swazi lilangeni" +
            "\r\nTHB,฿,Thai baht\r\nTJS,ЅМ,Tajikistani somoni\r\nTMT,m,Turkmenistan manat\r\nTND,د.ت,Tunisian dinar\r\nTOP,T$,Tongan paʻanga\r\nTRY,₺,Turkish lira\r\nTTD,$," +
            "Trinidad and Tobago doll\r\nTWD,NT$,New Taiwan dollar\r\nTZS,Sh,Tanzanian shilling\r\nUAH,₴,Ukrainian hryvnia\r\nUGX,UGX,Ugandan shilling\r\nUSD,$,United States (US)" +
            " dolla\r\nUYU,$,Uruguayan peso\r\nUZS,UZS,Uzbekistani som\r\nVEF,Bs F,Venezuelan bolívar\r\nVES,Bs.S,Bolívar soberano\r\nVND,₫,Vietnamese đồng\r\nVUV,Vt,Vanuatu " +
            "vatu\r\nWST,T,Samoan tālā\r\nXAF,CFA,Central African CFA fr\r\nXCD,$,East Caribbean dollar\r\nXOF,CFA,West African CFA franc\r\nXPF,Fr,CFP franc\r\nYER,﷼,Yemeni rial" +
            "\r\nZAR,R,South African rand\r\nZMW,ZK,Zambian kwacha";
        #endregion
        // I made this to figure out what my program was returning from this (stolen string from a sample CSV file)
        // Thats how i found out i calulated the wromg number and my program actually worked perfectly
        private DataSet TESTSET2(string text)
        {
            DataSet ds = Data.Seperate(text);
            string lString = ds.CombineSets(", ");
            MessageBox.Show(lString);
            return ds;
        }
        
        private void OpenFileSelector(object? sender, EventArgs? e)
        {
            DataSet ds = TESTSET2(_TestString2);
            List<DataSet> datas = LargeNumberProcess.ChunkData(ds, 10);
            FinishData(datas);
        }
        
#endif
        /// <summary>
        /// Cycle through adding all data
        /// </summary>
        /// <param name="datas"></param>
        private void FinishData(List<DataSet> datas)
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
            ds.Dispose();
        }
    }
}
