using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using FarmN_2010;



namespace WindowsFormsApplication1
{
    public partial class FarmNReprogramming : Form
    {
        public FarmNReprogramming()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strSelect = "SELECT * FROM SkemaData;";
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='C:/FarmNTestdatabase/SkemaData.accdb'";
            OleDbConnection myConn = new OleDbConnection(connectionString);
            OleDbDataAdapter myDataAdapter = new OleDbDataAdapter();
            myDataAdapter.SelectCommand = new OleDbCommand(strSelect, myConn);
            myConn.Open();
            DataSet ds = new DataSet();
            myDataAdapter.Fill(ds, "SkemaData");
            DataRow dr = ds.Tables[0].Rows[0];
            int inputSkemaId = Convert.ToInt16(dr[0].ToString());
            string inputPostnummer = dr[1].ToString();
            bool inputOption1 = Convert.ToBoolean(dr[2].ToString());
            bool inputOption2 = Convert.ToBoolean(dr[3].ToString());
            strSelect = "SELECT * FROM AreaData WHERE (Skema=" + dr[0].ToString() + ");";
            OleDbDataAdapter myDataAdapter1 = new OleDbDataAdapter();
            myDataAdapter1.SelectCommand = new OleDbCommand(strSelect, myConn);
            myDataAdapter1.Fill(ds, "AreaData");
            DataRow dr1 = ds.Tables[1].Rows[0];
            strSelect = "SELECT * FROM ManureData WHERE (Skema=" + dr[0].ToString() + ");";
            OleDbDataAdapter myDataAdapter2 = new OleDbDataAdapter();
            myDataAdapter2.SelectCommand = new OleDbCommand(strSelect, myConn);
            //ds.Clear();
            myDataAdapter2.Fill(ds, "ManureData");
            DataRow dr2 = ds.Tables[2].Rows[0];
            //LocalFarmNWebService.Areal[] arealer1 = new LocalFarmNWebService.Areal[ds.Tables[1].Rows.Count];//new List<LocalFarmNWebService.Areal>();
            //LocalFarmNWebService.ArealTyper arealtype1 = new LocalFarmNWebService.ArealTyper();
            ProductionFarmNWebService1.Areal[] arealer1 = new ProductionFarmNWebService1.Areal[ds.Tables[1].Rows.Count];//new List<LocalFarmNWebService.Areal>();
            ProductionFarmNWebService1.ArealTyper arealtype1 = new ProductionFarmNWebService1.ArealTyper();
            for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
            {
                dr1 = ds.Tables[1].Rows[j];
                //LocalFarmNWebService.Areal areal1 = new LocalFarmNWebService.Areal();
                ProductionFarmNWebService1.Areal areal1 = new ProductionFarmNWebService1.Areal();
                areal1.DbId = Convert.ToInt16(ds.Tables[1].Rows[j][1].ToString());
                arealtype1 = 0;
                areal1.ArealType = arealtype1;
                areal1.Referencesaedskifte = dr1[3].ToString();
                areal1.Saedskifte = dr1[4].ToString();
                areal1.Jordbundstype = dr1[5].ToString();
                areal1.Ha = Convert.ToDecimal(dr1[2].ToString());
                arealer1[j] = areal1;
            }

            //LocalFarmNWebService.Goedningsmaengde[] goedningsmaengder1 = new LocalFarmNWebService.Goedningsmaengde[ds.Tables[1].Rows.Count];
            ProductionFarmNWebService1.Goedningsmaengde[] goedningsmaengder1 = new ProductionFarmNWebService1.Goedningsmaengde[ds.Tables[1].Rows.Count];
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                dr2 = ds.Tables[2].Rows[i];
                //LocalFarmNWebService.Goedningsmaengde goedningsmaengde1 = new LocalFarmNWebService.Goedningsmaengde();
                ProductionFarmNWebService1.Goedningsmaengde goedningsmaengde1 = new ProductionFarmNWebService1.Goedningsmaengde();
                goedningsmaengde1.DbId = Convert.ToInt16(dr2[1].ToString());
                goedningsmaengde1.KgN = Convert.ToDecimal(dr2[2].ToString());
                goedningsmaengde1.GoedningstypeNUdnyttelsesProcent = Convert.ToInt16(dr2[3].ToString());
                //LocalFarmNWebService.Goedningstype goedningstype1 = new LocalFarmNWebService.Goedningstype();
                ProductionFarmNWebService1.Goedningstype goedningstype1 = new ProductionFarmNWebService1.Goedningstype();
                goedningstype1.Navn = dr2[4].ToString();
                goedningsmaengde1.Goedningstype = goedningstype1;
                goedningsmaengder1[i] = goedningsmaengde1;
            }

            ProductionFarmNWebService1.FarmNWebServiceSoapClient PService = new ProductionFarmNWebService1.FarmNWebServiceSoapClient();
            ProductionFarmNWebService1.FarmNData[] farmNList = PService.BeregnNudriftNy(arealer1, goedningsmaengder1, inputSkemaId, inputPostnummer, inputOption1, inputOption2);

            //LocalFarmNWebService.FarmNWebServiceSoapClient localService = new LocalFarmNWebService.FarmNWebServiceSoapClient();
            //LocalFarmNWebService.FarmNData[] farmNList = localService.BeregnNudriftNy(arealer1, goedningsmaengder1, inputSkemaId, inputPostnummer, inputOption1, inputOption2);

            //NyFarmNWebService.FarmNWebService2010SoapClient nyService = new NyFarmNWebService.FarmNWebService2010SoapClient();
            //NyFarmNWebService.FarmNData[] farmNList = nyService.CalculateApplication(arealer1, goedningsmaengder1, inputSkemaId, inputPostnummer, inputafterCropPercent, inputNormPercent, inputOption1, inputOption2);


            //label1.Text = "ZipCode: " + farmNList[0].Jordbundstype + "";
            label1.Text = "BeregnNudrift, Skema: " + inputSkemaId + ", Postnummer: " + dr[1].ToString() + "-rowcount: " + ds.Tables[1].Rows.Count + "";
            farmNDataBindingSource.DataSource = farmNList;
            dataGridView2.AutoGenerateColumns = true;
            dataGridView2.DataSource = goedningsmaengder1;
            dataGridView3.AutoGenerateColumns = true;
            dataGridView3.DataSource = arealer1;

        }

        private void farmNDataBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string strSelect = "SELECT * FROM SkemaData;";
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='C:/FarmNTestdatabase/SkemaData.accdb'";
            OleDbConnection myConn = new OleDbConnection(connectionString);
            OleDbDataAdapter myDataAdapter = new OleDbDataAdapter();
            myDataAdapter.SelectCommand = new OleDbCommand(strSelect, myConn);
            myConn.Open();
            DataSet ds = new DataSet();
            myDataAdapter.Fill(ds, "SkemaData");
            DataRow dr = ds.Tables[0].Rows[0];
            int inputSkemaId = Convert.ToInt16(dr[0].ToString());
            string inputPostnummer = dr[1].ToString();
            bool inputOption1 = Convert.ToBoolean(dr[2].ToString());
            bool inputOption2 = Convert.ToBoolean(dr[3].ToString());
            strSelect = "SELECT * FROM AreaData WHERE (Skema=" + dr[0].ToString() + ");";
            OleDbDataAdapter myDataAdapter1 = new OleDbDataAdapter();
            myDataAdapter1.SelectCommand = new OleDbCommand(strSelect, myConn);
            myDataAdapter1.Fill(ds, "AreaData");
            DataRow dr1 = ds.Tables[1].Rows[0];
            strSelect = "SELECT * FROM ManureData WHERE (Skema=" + dr[0].ToString() + ");";
            OleDbDataAdapter myDataAdapter2 = new OleDbDataAdapter();
            myDataAdapter2.SelectCommand = new OleDbCommand(strSelect, myConn);
            //ds.Clear();
            myDataAdapter2.Fill(ds, "ManureData");
            DataRow dr2 = ds.Tables[2].Rows[0];
            ProductionFarmNWebService1.Areal[] arealer1 = new ProductionFarmNWebService1.Areal[ds.Tables[1].Rows.Count];//new List<LocalFarmNWebService.Areal>();
            ProductionFarmNWebService1.ArealTyper arealtype1 = new ProductionFarmNWebService1.ArealTyper();
            for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
            {
                dr1 = ds.Tables[1].Rows[j];
                ProductionFarmNWebService1.Areal areal1 = new ProductionFarmNWebService1.Areal();
                areal1.DbId = Convert.ToInt16(ds.Tables[1].Rows[j][1].ToString());
                arealtype1 = 0;
                areal1.ArealType = arealtype1;
                areal1.Referencesaedskifte = dr1[3].ToString();
                areal1.Saedskifte = dr1[4].ToString();
                areal1.Jordbundstype = dr1[5].ToString();
                areal1.Ha = Convert.ToDecimal(dr1[2].ToString());
                arealer1[j] = areal1;
            }

            ProductionFarmNWebService1.Goedningsmaengde[] goedningsmaengder1 = new ProductionFarmNWebService1.Goedningsmaengde[ds.Tables[1].Rows.Count];
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                dr2 = ds.Tables[2].Rows[i];
                ProductionFarmNWebService1.Goedningsmaengde goedningsmaengde1 = new ProductionFarmNWebService1.Goedningsmaengde();
                goedningsmaengde1.DbId = Convert.ToInt16(dr2[1].ToString());
                goedningsmaengde1.KgN = Convert.ToDecimal(dr2[2].ToString());
                goedningsmaengde1.GoedningstypeNUdnyttelsesProcent = Convert.ToInt16(dr2[3].ToString());
                ProductionFarmNWebService1.Goedningstype goedningstype1 = new ProductionFarmNWebService1.Goedningstype();
                goedningstype1.Navn = dr2[4].ToString();
                goedningsmaengde1.Goedningstype = goedningstype1;
                goedningsmaengder1[i] = goedningsmaengde1;
            }
            decimal inputafterCropPercent = Convert.ToDecimal(0);
            decimal inputNormPercent = Convert.ToDecimal(50);
            ProductionFarmNWebService1.FarmNWebServiceSoapClient PService = new ProductionFarmNWebService1.FarmNWebServiceSoapClient();
            ProductionFarmNWebService1.FarmNData[] farmNList = PService.BeregnAnsoegtNy(arealer1, goedningsmaengder1, inputSkemaId, inputPostnummer, inputafterCropPercent, inputNormPercent, inputOption1, inputOption2);


            label1.Text = "BeregnAnsoegt, Skema: " + inputSkemaId + ", Postnummer: " + dr[1].ToString() + "";
//            farmNDataBindingSource.DataSource = farmNList;
            dataGridView2.AutoGenerateColumns = true;
            dataGridView2.DataSource = goedningsmaengder1;
            dataGridView3.AutoGenerateColumns = true;
            dataGridView3.DataSource = arealer1;
            dataGridView1.DataSource = farmNList;
        }

        private void button3_Click(object sender, EventArgs e)
        {
           NyFarmNWebService2.FarmNWebService2010SoapClient nyService = new NyFarmNWebService2.FarmNWebService2010SoapClient();

//           nyService.();
            //long newnumber;
        }


        private void buttonCalculateApplication_Click(object sender, EventArgs e)
        {
            string strSelect = "SELECT * FROM SkemaData;";
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='C:/FarmNTestdatabase/SkemaData.accdb'";
            OleDbConnection myConn = new OleDbConnection(connectionString);
            OleDbDataAdapter myDataAdapter = new OleDbDataAdapter();
            myDataAdapter.SelectCommand = new OleDbCommand(strSelect, myConn);
            myConn.Open();
            DataSet ds = new DataSet();
            myDataAdapter.Fill(ds, "SkemaData");
            DataRow dr = ds.Tables[0].Rows[0];
            int inputSkemaId = Convert.ToInt16(dr[0].ToString());
            string inputPostnummer = dr[1].ToString();
            bool inputOption1 = Convert.ToBoolean(dr[2].ToString());
            bool inputOption2 = Convert.ToBoolean(dr[3].ToString());
            strSelect = "SELECT * FROM AreaData WHERE (Skema=" + dr[0].ToString() + ");";
            OleDbDataAdapter myDataAdapter1 = new OleDbDataAdapter();
            myDataAdapter1.SelectCommand = new OleDbCommand(strSelect, myConn);
            myDataAdapter1.Fill(ds, "AreaData");
            DataRow dr1 = ds.Tables[1].Rows[0];
            strSelect = "SELECT * FROM ManureData WHERE (Skema=" + dr[0].ToString() + ");";
            OleDbDataAdapter myDataAdapter2 = new OleDbDataAdapter();
            myDataAdapter2.SelectCommand = new OleDbCommand(strSelect, myConn);
            //ds.Clear();
            myDataAdapter2.Fill(ds, "ManureData");
            DataRow dr2 = ds.Tables[2].Rows[0];
            NyFarmNWebService2.Areal[] arealer1 = new NyFarmNWebService2.Areal[ds.Tables[1].Rows.Count];//new List<LocalFarmNWebService.Areal>();
            NyFarmNWebService2.ArealTyper arealtype1 = new NyFarmNWebService2.ArealTyper();
            for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
            {
                dr1 = ds.Tables[1].Rows[j];
                NyFarmNWebService2.Areal areal1 = new NyFarmNWebService2.Areal();
                areal1.DbId = Convert.ToInt16(ds.Tables[1].Rows[j][1].ToString());
                arealtype1 = 0;
                areal1.ArealType = arealtype1;
                areal1.Referencesaedskifte = dr1[3].ToString();
                areal1.Saedskifte = dr1[4].ToString();
                areal1.Jordbundstype = dr1[5].ToString();
                areal1.Ha = Convert.ToDecimal(dr1[2].ToString());
                arealer1[j] = areal1;
            }

            NyFarmNWebService2.Goedningsmaengde[] goedningsmaengder1 = new NyFarmNWebService2.Goedningsmaengde[ds.Tables[1].Rows.Count];
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                dr2 = ds.Tables[2].Rows[i];
                NyFarmNWebService2.Goedningsmaengde goedningsmaengde1 = new NyFarmNWebService2.Goedningsmaengde();
                goedningsmaengde1.DbId = Convert.ToInt16(dr2[1].ToString());
                goedningsmaengde1.KgN = Convert.ToDecimal(dr2[2].ToString());
                goedningsmaengde1.GoedningstypeNUdnyttelsesProcent = Convert.ToInt16(dr2[3].ToString());
                NyFarmNWebService2.Goedningstype goedningstype1 = new NyFarmNWebService2.Goedningstype();
                goedningstype1.Navn = dr2[4].ToString();
                goedningsmaengde1.Goedningstype = goedningstype1;
                goedningsmaengder1[i] = goedningsmaengde1;
            }
            decimal inputafterCropPercent = Convert.ToDecimal(0);
            decimal inputNormPercent = Convert.ToDecimal(0);
            NyFarmNWebService2.FarmNWebService2010SoapClient nyService = new NyFarmNWebService2.FarmNWebService2010SoapClient();
            //NyFarmNWebService2.FarmNData[] farmNList = nyService.CalculateApplication(arealer1, goedningsmaengder1, inputSkemaId, inputPostnummer, inputafterCropPercent, inputNormPercent, inputOption1, inputOption2,1);
            nyService.CalculateApplication(arealer1, goedningsmaengder1, inputSkemaId, inputPostnummer, inputafterCropPercent, inputNormPercent, inputOption1, inputOption2, 1);


            label1.Text = "CalculateApplication, Skema: " + inputSkemaId + ", Postnummer: " + dr[1].ToString() + "";
            //farmNDataBindingSource.DataSource = farmNList;
            dataGridView2.AutoGenerateColumns = true;
            dataGridView2.DataSource = goedningsmaengder1;
            dataGridView3.AutoGenerateColumns = true;
            dataGridView3.DataSource = arealer1;

        }

        private void buttonCalculateExisting_Click(object sender, EventArgs e)
        {
            string strSelect = "SELECT * FROM SkemaData;";
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='C:/FarmNTestdatabase/SkemaData.accdb'";
            OleDbConnection myConn = new OleDbConnection(connectionString);
            OleDbDataAdapter myDataAdapter = new OleDbDataAdapter();
            myDataAdapter.SelectCommand = new OleDbCommand(strSelect, myConn);
            myConn.Open();
            DataSet ds = new DataSet();
            myDataAdapter.Fill(ds, "SkemaData");
            DataRow dr = ds.Tables[0].Rows[0];
            int inputSkemaId = Convert.ToInt16(dr[0].ToString());
            string inputPostnummer = dr[1].ToString();
            bool inputOption1 = Convert.ToBoolean(dr[2].ToString());
            bool inputOption2 = Convert.ToBoolean(dr[3].ToString());
            strSelect = "SELECT * FROM AreaData WHERE (Skema=" + dr[0].ToString() + ");";
            OleDbDataAdapter myDataAdapter1 = new OleDbDataAdapter();
            myDataAdapter1.SelectCommand = new OleDbCommand(strSelect, myConn);
            myDataAdapter1.Fill(ds, "AreaData");
            DataRow dr1 = ds.Tables[1].Rows[0];
            strSelect = "SELECT * FROM ManureData WHERE (Skema=" + dr[0].ToString() + ");";
            OleDbDataAdapter myDataAdapter2 = new OleDbDataAdapter();
            myDataAdapter2.SelectCommand = new OleDbCommand(strSelect, myConn);
            //ds.Clear();
            myDataAdapter2.Fill(ds, "ManureData");
            DataRow dr2 = ds.Tables[2].Rows[0];
            NyFarmNWebService2.Areal[] arealer1 = new NyFarmNWebService2.Areal[ds.Tables[1].Rows.Count];//new List<LocalFarmNWebService.Areal>();
            NyFarmNWebService2.ArealTyper arealtype1 = new NyFarmNWebService2.ArealTyper();
            for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
            {
                dr1 = ds.Tables[1].Rows[j];
                NyFarmNWebService2.Areal areal1 = new NyFarmNWebService2.Areal();
                areal1.DbId = Convert.ToInt16(ds.Tables[1].Rows[j][1].ToString());
                arealtype1 = 0;
                areal1.ArealType = arealtype1;
                areal1.Referencesaedskifte = dr1[3].ToString();
                areal1.Saedskifte = dr1[4].ToString();
                areal1.Jordbundstype = dr1[5].ToString();
                areal1.Ha = Convert.ToDecimal(dr1[2].ToString());
                arealer1[j] = areal1;
            }

            NyFarmNWebService2.Goedningsmaengde[] goedningsmaengder1 = new NyFarmNWebService2.Goedningsmaengde[ds.Tables[1].Rows.Count];
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                dr2 = ds.Tables[2].Rows[i];
                NyFarmNWebService2.Goedningsmaengde goedningsmaengde1 = new NyFarmNWebService2.Goedningsmaengde();
                goedningsmaengde1.DbId = Convert.ToInt16(dr2[1].ToString());
                goedningsmaengde1.KgN = Convert.ToDecimal(dr2[2].ToString());
                goedningsmaengde1.GoedningstypeNUdnyttelsesProcent = Convert.ToInt16(dr2[3].ToString());
                NyFarmNWebService2.Goedningstype goedningstype1 = new NyFarmNWebService2.Goedningstype();
                goedningstype1.Navn = dr2[4].ToString();
                goedningsmaengde1.Goedningstype = goedningstype1;
                goedningsmaengder1[i] = goedningsmaengde1;
            }
            decimal inputafterCropPercent = Convert.ToDecimal(0);
            decimal inputNormPercent = Convert.ToDecimal(0);
            NyFarmNWebService2.FarmNWebService2010SoapClient nyService = new NyFarmNWebService2.FarmNWebService2010SoapClient();
//            NyFarmNWebService2.FarmNData[] farmNList = nyService.CalculateExisting(arealer1, goedningsmaengder1, inputSkemaId, inputPostnummer, inputOption1, inputOption2);


            //label1.Text = "CalculateExisting, Skema: " + inputSkemaId + ", Postnummer: " + dr[1].ToString() + "";
            //farmNDataBindingSource.DataSource = farmNList;
            //dataGridView2.AutoGenerateColumns = true;
            //dataGridView2.DataSource = goedningsmaengder1;
            //dataGridView3.AutoGenerateColumns = true;
            //dataGridView3.DataSource = arealer1;
            //dataGridView1.DataSource = farmNList;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //string strSelect = "SELECT * FROM SkemaData;";
            //string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='C:/FarmNTestdatabase/SkemaData.accdb'";
            //OleDbConnection myConn = new OleDbConnection(connectionString);
            //OleDbDataAdapter myDataAdapter = new OleDbDataAdapter();
            //myDataAdapter.SelectCommand = new OleDbCommand(strSelect, myConn);
            //myConn.Open();
            //DataSet ds = new DataSet();
            //myDataAdapter.Fill(ds, "SkemaData");
            //DataRow dr = ds.Tables[0].Rows[0];
            //int inputSkemaId = Convert.ToInt16(dr[0].ToString());
            //string inputPostnummer = dr[1].ToString();
            //bool inputOption1 = Convert.ToBoolean(dr[2].ToString());
            //bool inputOption2 = Convert.ToBoolean(dr[3].ToString());
            //strSelect = "SELECT * FROM AreaData WHERE (Skema=" + dr[0].ToString() + ");";
            //OleDbDataAdapter myDataAdapter1 = new OleDbDataAdapter();
            //myDataAdapter1.SelectCommand = new OleDbCommand(strSelect, myConn);
            //myDataAdapter1.Fill(ds, "AreaData");
            //DataRow dr1 = ds.Tables[1].Rows[0];
            //strSelect = "SELECT * FROM ManureData WHERE (Skema=" + dr[0].ToString() + ");";
            //OleDbDataAdapter myDataAdapter2 = new OleDbDataAdapter();
            //myDataAdapter2.SelectCommand = new OleDbCommand(strSelect, myConn);
            ////ds.Clear();
            //myDataAdapter2.Fill(ds, "ManureData");
            //DataRow dr2 = ds.Tables[2].Rows[0];
            ////LocalFarmNWebService.Areal[] arealer1 = new LocalFarmNWebService.Areal[ds.Tables[1].Rows.Count];//new List<LocalFarmNWebService.Areal>();
            ////LocalFarmNWebService.ArealTyper arealtype1 = new LocalFarmNWebService.ArealTyper();
            ////ProductionFarmNWebService.Areal[] arealer1 = new ProductionFarmNWebService.Areal[ds.Tables[1].Rows.Count];//new List<LocalFarmNWebService.Areal>();
            ////ProductionFarmNWebService.ArealTyper arealtype1 = new ProductionFarmNWebService.ArealTyper();
            //for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
            //{
            //    dr1 = ds.Tables[1].Rows[j];
            //    //LocalFarmNWebService.Areal areal1 = new LocalFarmNWebService.Areal();
            //    //ProductionFarmNWebService.Areal areal1 = new ProductionFarmNWebService.Areal();
            //    areal1.DbId = Convert.ToInt16(ds.Tables[1].Rows[j][1].ToString());
            //    arealtype1 = 0;
            //    areal1.ArealType = arealtype1;
            //    areal1.Referencesaedskifte = dr1[3].ToString();
            //    areal1.Saedskifte = dr1[4].ToString();
            //    areal1.Jordbundstype = dr1[5].ToString();
            //    areal1.Ha = Convert.ToDecimal(dr1[2].ToString());
            //    arealer1[j] = areal1;
            //}

            ////LocalFarmNWebService.Goedningsmaengde[] goedningsmaengder1 = new LocalFarmNWebService.Goedningsmaengde[ds.Tables[1].Rows.Count];
            ////ProductionFarmNWebService.Goedningsmaengde[] goedningsmaengder1 = new ProductionFarmNWebService.Goedningsmaengde[ds.Tables[1].Rows.Count];
            //for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            //{
            //    dr2 = ds.Tables[2].Rows[i];
            //    //LocalFarmNWebService.Goedningsmaengde goedningsmaengde1 = new LocalFarmNWebService.Goedningsmaengde();
            //    //ProductionFarmNWebService.Goedningsmaengde goedningsmaengde1 = new ProductionFarmNWebService.Goedningsmaengde();
            //    goedningsmaengde1.DbId = Convert.ToInt16(dr2[1].ToString());
            //    goedningsmaengde1.KgN = Convert.ToDecimal(dr2[2].ToString());
            //    goedningsmaengde1.GoedningstypeNUdnyttelsesProcent = Convert.ToInt16(dr2[3].ToString());
            //    //LocalFarmNWebService.Goedningstype goedningstype1 = new LocalFarmNWebService.Goedningstype();
            //    //ProductionFarmNWebService.Goedningstype goedningstype1 = new ProductionFarmNWebService.Goedningstype();
            //    goedningstype1.Navn = dr2[4].ToString();
            //    goedningsmaengde1.Goedningstype = goedningstype1;
            //    goedningsmaengder1[i] = goedningsmaengde1;
            //}

            ////ProductionFarmNWebService.FarmNWebServiceSoapClient PService = new ProductionFarmNWebService.FarmNWebServiceSoapClient();
            ////ProductionFarmNWebService.FarmNData[] farmNList = PService.BeregnNudriftNy(arealer1, goedningsmaengder1, inputSkemaId, inputPostnummer, inputOption1, inputOption2);

            ////LocalFarmNWebService.FarmNWebServiceSoapClient localService = new LocalFarmNWebService.FarmNWebServiceSoapClient("FarmNWebServiceSoap");
            ////LocalFarmNWebService.FarmNData[] farmNList = localService.BeregnNudriftNy(arealer1, goedningsmaengder1, inputSkemaId, inputPostnummer, inputOption1, inputOption2);

            ////NyFarmNWebService.FarmNWebService2010SoapClient nyService = new NyFarmNWebService.FarmNWebService2010SoapClient();
            ////NyFarmNWebService.FarmNData[] farmNList = nyService.CalculateApplication(arealer1, goedningsmaengder1, inputSkemaId, inputPostnummer, inputafterCropPercent, inputNormPercent, inputOption1, inputOption2);


            ////label1.Text = "ZipCode: " + farmNList[0].Jordbundstype + "";
            //label1.Text = "BeregnNudriftLocal, Skema: " + inputSkemaId + ", Postnummer: " + dr[1].ToString() + "-rowcount: " + ds.Tables[1].Rows.Count + "";
            //farmNDataBindingSource.DataSource = farmNList;
            //dataGridView2.AutoGenerateColumns = true;
            //dataGridView2.DataSource = goedningsmaengder1;
            //dataGridView3.AutoGenerateColumns = true;
            //dataGridView3.DataSource = arealer1;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //string strSelect = "SELECT * FROM SkemaData;";
            //string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='C:/FarmNTestdatabase/SkemaData.accdb'";
            //OleDbConnection myConn = new OleDbConnection(connectionString);
            //OleDbDataAdapter myDataAdapter = new OleDbDataAdapter();
            //myDataAdapter.SelectCommand = new OleDbCommand(strSelect, myConn);
            //myConn.Open();
            //DataSet ds = new DataSet();
            //myDataAdapter.Fill(ds, "SkemaData");
            //DataRow dr = ds.Tables[0].Rows[0];
            //int inputSkemaId = Convert.ToInt16(dr[0].ToString());
            //string inputPostnummer = dr[1].ToString();
            //bool inputOption1 = Convert.ToBoolean(dr[2].ToString());
            //bool inputOption2 = Convert.ToBoolean(dr[3].ToString());
            //strSelect = "SELECT * FROM AreaData WHERE (Skema=" + dr[0].ToString() + ");";
            //OleDbDataAdapter myDataAdapter1 = new OleDbDataAdapter();
            //myDataAdapter1.SelectCommand = new OleDbCommand(strSelect, myConn);
            //myDataAdapter1.Fill(ds, "AreaData");
            //DataRow dr1 = ds.Tables[1].Rows[0];
            //strSelect = "SELECT * FROM ManureData WHERE (Skema=" + dr[0].ToString() + ");";
            //OleDbDataAdapter myDataAdapter2 = new OleDbDataAdapter();
            //myDataAdapter2.SelectCommand = new OleDbCommand(strSelect, myConn);
            ////ds.Clear();
            //myDataAdapter2.Fill(ds, "ManureData");
            //DataRow dr2 = ds.Tables[2].Rows[0];
            //LocalFarmNWebService.Areal[] arealer1 = new LocalFarmNWebService.Areal[ds.Tables[1].Rows.Count];//new List<LocalFarmNWebService.Areal>();
            //LocalFarmNWebService.ArealTyper arealtype1 = new LocalFarmNWebService.ArealTyper();
            ////ProductionFarmNWebService.Areal[] arealer1 = new ProductionFarmNWebService.Areal[ds.Tables[1].Rows.Count];//new List<LocalFarmNWebService.Areal>();
            ////ProductionFarmNWebService.ArealTyper arealtype1 = new ProductionFarmNWebService.ArealTyper();
            //for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
            //{
            //    dr1 = ds.Tables[1].Rows[j];
            //    LocalFarmNWebService.Areal areal1 = new LocalFarmNWebService.Areal();
            //    //ProductionFarmNWebService.Areal areal1 = new ProductionFarmNWebService.Areal();
            //    areal1.DbId = Convert.ToInt16(ds.Tables[1].Rows[j][1].ToString());
            //    arealtype1 = 0;
            //    areal1.ArealType = arealtype1;
            //    areal1.Referencesaedskifte = dr1[3].ToString();
            //    areal1.Saedskifte = dr1[4].ToString();
            //    areal1.Jordbundstype = dr1[5].ToString();
            //    areal1.Ha = Convert.ToDecimal(dr1[2].ToString());
            //    arealer1[j] = areal1;
            //}

            //LocalFarmNWebService.Goedningsmaengde[] goedningsmaengder1 = new LocalFarmNWebService.Goedningsmaengde[ds.Tables[1].Rows.Count];
            ////ProductionFarmNWebService.Goedningsmaengde[] goedningsmaengder1 = new ProductionFarmNWebService.Goedningsmaengde[ds.Tables[1].Rows.Count];
            //for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            //{
            //    dr2 = ds.Tables[2].Rows[i];
            //    LocalFarmNWebService.Goedningsmaengde goedningsmaengde1 = new LocalFarmNWebService.Goedningsmaengde();
            //    //ProductionFarmNWebService.Goedningsmaengde goedningsmaengde1 = new ProductionFarmNWebService.Goedningsmaengde();
            //    goedningsmaengde1.DbId = Convert.ToInt16(dr2[1].ToString());
            //    goedningsmaengde1.KgN = Convert.ToDecimal(dr2[2].ToString());
            //    goedningsmaengde1.GoedningstypeNUdnyttelsesProcent = Convert.ToInt16(dr2[3].ToString());
            //    LocalFarmNWebService.Goedningstype goedningstype1 = new LocalFarmNWebService.Goedningstype();
            //    //ProductionFarmNWebService.Goedningstype goedningstype1 = new ProductionFarmNWebService.Goedningstype();
            //    goedningstype1.Navn = dr2[4].ToString();
            //    goedningsmaengde1.Goedningstype = goedningstype1;
            //    goedningsmaengder1[i] = goedningsmaengde1;
            //}

            ////ProductionFarmNWebService.FarmNWebServiceSoapClient PService = new ProductionFarmNWebService.FarmNWebServiceSoapClient();
            ////ProductionFarmNWebService.FarmNData[] farmNList = PService.BeregnNudriftNy(arealer1, goedningsmaengder1, inputSkemaId, inputPostnummer, inputOption1, inputOption2);

            //decimal inputafterCropPercent = Convert.ToDecimal(0);
            //decimal inputNormPercent = Convert.ToDecimal(50);
            //LocalFarmNWebService.FarmNWebServiceSoapClient localService = new LocalFarmNWebService.FarmNWebServiceSoapClient("FarmNWebServiceSoap");
            //LocalFarmNWebService.FarmNData[] farmNList = localService.BeregnAnsoegtNy(arealer1, goedningsmaengder1, inputSkemaId, inputPostnummer, inputafterCropPercent, inputNormPercent, inputOption1, inputOption2);

            ////NyFarmNWebService.FarmNWebService2010SoapClient nyService = new NyFarmNWebService.FarmNWebService2010SoapClient();
            ////NyFarmNWebService.FarmNData[] farmNList = nyService.CalculateApplication(arealer1, goedningsmaengder1, inputSkemaId, inputPostnummer, inputafterCropPercent, inputNormPercent, inputOption1, inputOption2);


            ////label1.Text = "ZipCode: " + farmNList[0].Jordbundstype + "";
            //label1.Text = "BeregnAnsoegtLocal, Skema: " + inputSkemaId + ", Postnummer: " + dr[1].ToString() + "-rowcount: " + ds.Tables[1].Rows.Count + "";
            //farmNDataBindingSource.DataSource = farmNList;
            //dataGridView2.AutoGenerateColumns = true;
            //dataGridView2.DataSource = goedningsmaengder1;
            //dataGridView3.AutoGenerateColumns = true;
            //dataGridView3.DataSource = arealer1;
            //dataGridView1.DataSource = farmNList;
        }


    }

}
