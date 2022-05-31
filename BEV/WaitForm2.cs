using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics.Eventing.Reader;
using System.Threading;


namespace BEV
{

    public partial class WaitForm2 : Form
    {
        private BackgroundWorker BGW_LocalEvt_View;
        private static BindingSource TempBinding = new BindingSource();
        private EventLogQuery eventsQuery;
        private EventLogReader logReader;
        public bool IsFilteredByDateTime = false;
        public  Thread _Thread;
        private static EventRecord CastObject;
        Master_Value.MasterValueClass MObj = new BEV.Master_Value.MasterValueClass();
        string Temps1, Temps2;
        public void _Reload_Init()
        {
            try
            {
               
                Thread.Sleep(3);
                
                BGW_LocalEvt_View = new BackgroundWorker();
                BGW_LocalEvt_View.DoWork += new DoWorkEventHandler(BGW_LocalEvt_View_DoWork);
                BGW_LocalEvt_View.ProgressChanged += new ProgressChangedEventHandler(BGW_LocalEvt_View_ProgressChanged);
                BGW_LocalEvt_View.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BGW_LocalEvt_View_RunWorkerCompleted);
                BGW_LocalEvt_View.WorkerReportsProgress = true;
                BGW_LocalEvt_View.WorkerSupportsCancellation = true;
                BGW_LocalEvt_View.RunWorkerAsync();

            }
            catch (Exception  err)
            {
                
                
            }


        }

                     
        void BGW_LocalEvt_View_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                DisplayEventAndLogInformation(BGW_LocalEvt_View, e);
            }
            catch (Exception err)
            {


            }
        }        
        
        void BGW_LocalEvt_View_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                try
                {

                    this.Update();
                    //label1.Update();
                    //label2.Update();

                    
                    TempBinding.Add((EventRecord)e.UserState);
                   
                    lock (MObj)
                    {                                               
                                   
                        CastObject = ((EventRecord)e.UserState);                                            
                                         
                        if (CastObject == null )
                        {
                            Temps1 = CastObject.RecordId.ToString();
                            Temps2 = CastObject.LevelDisplayName.ToString();
                        }
                        else
                        {
                            Master_Value.MasterValueClass.SetRows_TO_table_Local(CastObject.RecordId.ToString(), CastObject.LevelDisplayName.ToString(), CastObject.Id.ToString(), CastObject.FormatDescription(),CastObject.TimeCreated.ToString());
                        }

                    }

                }
                catch (EventLogNotFoundException err)
                {
                    Master_Value.MasterValueClass.SetRows_TO_table_Local(Temps1, Temps2," ", "Null -- Message is Null OR Messages NotFound","");
                }
            }
            catch (Exception err)
            {


            }
        }
      
        public void CopyBindingSource()
        {
            
            Master_Value.MasterValueClass.LocalBindingSource = TempBinding;
            this.Close();
           

        }

        void BGW_LocalEvt_View_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                CopyBindingSource();
                
              
            }
            catch (Exception err)
            {

                Console.WriteLine("BEV Internal Error Zero" + err.Message);
            }
        }

        public void DisplayEventAndLogInformation(BackgroundWorker bgw, DoWorkEventArgs e)
        {
            try
            {

              //  int i = 0;
                string X = "\"" + Master_Value.MasterValueClass.ActiveNode + "\"";
                string queryString = "*[System/Channel=" + X + "]";

                eventsQuery = new EventLogQuery(Master_Value.MasterValueClass.ActiveNode, PathType.LogName, queryString);
                EventLogSession Remote_session = new EventLogSession();
               
                
                // Gets Events for Local or Remote Eventlogs 
                eventsQuery.Session = Remote_session;

                logReader = new EventLogReader(eventsQuery);                               
                for (EventRecord eventInstance = logReader.ReadEvent();
                    null != eventInstance; eventInstance = logReader.ReadEvent())
                {
                    if (Form1.Isclosing) break;

                    try
                    {
                        if (!IsFilteredByDateTime)
                        {
                            bgw.ReportProgress(0, (object)eventInstance);
                        }
                        else
                        {
                            if (eventInstance.TimeCreated.Value.Date >= dateTimePicker1.Value.Date && 
                                eventInstance.TimeCreated.Value.Date <= dateTimePicker2.Value.Date)
                                bgw.ReportProgress(0, (object)eventInstance);
                        }

                       // bgw.ReportProgress(0, (object)eventInstance);
                      
                    }
                    catch (EventLogException eeee)
                    {
                        Console.WriteLine("BEV Internal Error Zero" + eeee.Message);
                    }


                }




            }
            catch (Exception err)
            {

                Console.WriteLine("BEV Internal Error Zero" + err.Message);
            }

        }

      
        public WaitForm2()
        {
            InitializeComponent();
            TempBinding.DataSource = typeof(System.Diagnostics.Eventing.Reader.EventLogRecord);
        }

        private void WaitForm2_Load(object sender, EventArgs e)
        {
            try
            {

                //label1.Text = "Connecting to Local System" ;
                //label2.Text = "Event Name: " + Master_Value.MasterValueClass.ActiveNode + " , Events Count: " 
                //    + Master_Value.MasterValueClass.ActiveNode_Count + " Records";
                //label1.Update();
                //label2.Update();
                Thread.Sleep(3);
                this.Update();
                //_Reload_Init();

            }
            catch (Exception err)
            {


            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                IsFilteredByDateTime = true;
                this.Text = "Loading , Please Wait...";
                toolStripStatusLabel1.Text = "Connecting to Local System";
                toolStripStatusLabel2.Text = "Event Name: " + Master_Value.MasterValueClass.ActiveNode + " , Events Count: "
                    + Master_Value.MasterValueClass.ActiveNode_Count + " Records";

                Master_Value.MasterValueClass.Settable_LocalTable();

                Thread.Sleep(1500);
                this.Update();
                _Reload_Init();

            }
            catch (Exception err)
            {


            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                IsFilteredByDateTime = false;
                this.Text = "Loading , Please Wait...";
                toolStripStatusLabel1.Text = "Connecting to Local System";
                toolStripStatusLabel2.Text = "Event Name: " + Master_Value.MasterValueClass.ActiveNode + " , Events Count: "
                    + Master_Value.MasterValueClass.ActiveNode_Count + " Records";

                Master_Value.MasterValueClass.Settable_LocalTable();

                statusStrip1.Update();
                Thread.Sleep(1500);
                this.Update();
                _Reload_Init();

            }
            catch (Exception err)
            {


            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

       
    }
}
