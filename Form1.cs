using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using System.Net;

namespace dirsearch
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ErrorProvider provider = new ErrorProvider();  

        bool resized = false;
        void resize(int height)
        {
            for (int i = 300; i < height; i++)
            {
                this.Size = new Size(i, 300);

            }

        }


        
        string read(string filename,int indexnumber)
        { // To read a text file line by line

            File.Exists(filename);
            // Store each line in array of strings
            string[] lines = File.ReadAllLines(filepath);
            //length = lines.Length;

            return lines[indexnumber];
            
        }
        
        HttpStatusCode statusCode = new HttpStatusCode();
        
        string url;
        string get(string site ,string dizinadi )
        {
            string directoryName = dizinadi;
            url = site + dizinadi + "/";
            try
            {
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create("https://google.com/");
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                //myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                //statusCode = myHttpWebResponse.StatusCode;


            }
            catch (Exception we)
            {
                MessageBox.Show(we.ToString());

            }
            return statusCode.ToString();
        }



        OleDbConnection connection = new OleDbConnection("Provider=Microsoft.jet.oledb.4.0; Data Source=ogrenciler.mdb");
        string filepath;
        void connect()
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
        }
        void disconnect()
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
        //void add()
        //{
        //    string sorgu = "insert into ogrtablo(ogr_ad,ogr_no,ogr_tc,ogr_ort) values(@siteName,@dirName,@statusCode,@length)";
        //    OleDbCommand komut = new OleDbCommand(sorgu, connection);
        //    komut.Parameters.AddWithValue("@siteName", domain );
        //    komut.Parameters.AddWithValue("@dirName", ln );
        //    komut.Parameters.AddWithValue("@statusCode", );
        //    komut.Parameters.AddWithValue("@length", );
            

        //    OleDbDataAdapter adp = new OleDbDataAdapter(sorgu, connection);

        //    DataTable tablo = new DataTable();
        //    adp.Fill(tablo);
        //    list();
        //}
        void list()
        {
            string query = "select * from "+ domain;
            OleDbDataAdapter adp = new OleDbDataAdapter(query, connection);
            DataTable tablo = new DataTable();
            adp.Fill(tablo);
            dgResults.DataSource = tablo;
        }
        void create()
        {
            string sorgu = "CREATE TABLE "+domain+" AS SELECT * FROM mainTable;";
            OleDbCommand komut = new OleDbCommand(sorgu, connection);
            

            OleDbDataAdapter adp = new OleDbDataAdapter(sorgu, connection);

            DataTable tablo = new DataTable();
            adp.Fill(tablo);
            list();
        }
        string domain;
        private void btScan_Click(object sender, EventArgs e)
        {
            
            int charPos = tbUrl.Text.LastIndexOf("\\");

            string asd = tbUrl.Text.Substring(charPos + 1);
            domain = asd;

            if (tbUrl.Text.StartsWith("http"))
            {
                if (resized == false )
                {
                    resize(750);
                    resized = true;
                    

                }
                //else
                //    //scan();
            }
            else
            {

                provider.SetError(tbUrl, "url formatı 'http://****.***' veya 'https://****.***' şeklinde olmalıdır");
            }

           

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Size = new Size(300, 300);
            get("https://stackoverflow.com/", "");
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"D:\",
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "txt",
                Filter = "txt files (*.txt)|*.txt",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };
            
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                 filepath = openFileDialog1.FileName;
                //int charPos = filepath.LastIndexOf("\\");
               
                //file = filepath.Substring(charPos+1);
                //button1.Text = file;
            }  
           
        }
        //void scan()
        ////{
        ////    string bisey ;
        ////    for (int i = 0; i < 100; i++)
        ////    {
        ////        bisey = read(file,i);
        ////        listBox1.Items.Add( get(tbUrl.Text,bisey));
                

 
        ////    }
        //    //MessageBox.Show();
            
        //}
    }
}
