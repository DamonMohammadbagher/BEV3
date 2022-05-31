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
    public partial class WaitForm : Form
    {
    
        private BackgroundWorker BGW_RemoteEvt_View;
        private static BindingSource TempBinding = new BindingSource();
        private EventLogQuery eventsQuery;
        private EventLogReader logReader;        
        public Thread _Thread;
        Master_Value.MasterValueClass MObj = new BEV.Master_Value.MasterValueClass();
        string Temps1, Temps2;
        public bool IsFilteredByDateTime = false;

        public WaitForm()
        {
           
            InitializeComponent();
            TempBinding.DataSource = typeof(System.Diagnostics.Eventing.Reader.EventLogRecord);
        }

        public void _Reload_Init()
        { 
            try
            {
                
                Thread.Sleep(3);
                BGW_RemoteEvt_View = new BackgroundWorker();
                BGW_RemoteEvt_View.DoWork += new DoWorkEventHandler(BGW_RemoteEvt_View_DoWork);
                BGW_RemoteEvt_View.ProgressChanged += new ProgressChangedEventHandler(BGW_RemoteEvt_View_ProgressChanged);
                BGW_RemoteEvt_View.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BGW_RemoteEvt_View_RunWorkerCompleted);
                BGW_RemoteEvt_View.WorkerReportsProgress = true;
                BGW_RemoteEvt_View.WorkerSupportsCancellation = true;
                BGW_RemoteEvt_View.RunWorkerAsync();
            }
            catch (Exception err)
            {
                
                
            }
            
           

        }

        public void DisplayEventAndLogInformation(BackgroundWorker bgw, DoWorkEventArgs e) 
        {
            try
            {


                try
                {

                    string X = "\"" + Master_Value.MasterValueClass.ActiveNode + "\"";
                    string queryString = "*[System/Channel=" + X + "]";

                    eventsQuery = new EventLogQuery(Master_Value.MasterValueClass.ActiveNode, PathType.LogName, queryString);
                    EventLogSession Remote_session = new EventLogSession(Master_Value.MasterValueClass.Remote_Host_Name);


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
                        catch (EventLogNotFoundException eeee)
                        {
                            Console.WriteLine("BEV Internal Error Zero A" + eeee.Message);
                        }


                    }




                }
                catch (EventLogNotFoundException err)
                {

                    Console.WriteLine("BEV Internal Error Zero B" + err.Message);
                }
            }
            catch (Exception err)
            {
                Console.WriteLine("BEV Internal Error Zero C" + err.Message);

            }
        }

        void BGW_RemoteEvt_View_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                DisplayEventAndLogInformation(BGW_RemoteEvt_View, e);
            }
            catch (Exception err)
            {
                
                
            }
           
           
        }
       
        void BGW_RemoteEvt_View_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {

                try
                {
                     
                    lock (MObj)
                    {
                        
                        
                        //Master_Value.MasterValueClass.RemoteBindingSource.Add((EventRecord)e.UserState);
                        EventRecord CastObject = ((EventRecord)e.UserState);
                        TempBinding.Add((EventRecord)e.UserState);
                       
                        string msg = CastObject.FormatDescription();
                        if (CastObject == null || msg == null)
                        {
                            Temps1 = CastObject.RecordId.ToString();
                            Temps2 = CastObject.LevelDisplayName.ToString();                            
                        }
                        else
                        {
                            Master_Value.MasterValueClass.SetRows_TO_table_Remoting(CastObject.RecordId.ToString(), CastObject.LevelDisplayName.ToString(), CastObject.Id.ToString(), msg, CastObject.TimeCreated.ToString());
                        }
                    }


                }
                catch (EventLogNotFoundException err)
                {
                    Master_Value.MasterValueClass.SetRows_TO_table_Remoting(Temps1, Temps2," ", "Null -- Message is Null OR Messages NotFound", "");
                    // Console.WriteLine("BEV Internal Error Zero BB" + err.Message + "{" + ((EventRecord)e.UserState).FormatDescription() + "}");
                }
            }
            catch (Exception err)
            {


            }
        }

        public void CopyBindingSource()
        {            
            Master_Value.MasterValueClass.RemoteBindingSource = TempBinding;
            this.Close();
            
        }

        void BGW_RemoteEvt_View_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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

        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                IsFilteredByDateTime = true;
                this.Text = "Loading , Please Wait...";
                toolStripStatusLabel1.Text = "Connecting to Remote System";
                toolStripStatusLabel2.Text = "Event Name: " + Master_Value.MasterValueClass.ActiveNode + " , Events Count: "
                    + Master_Value.MasterValueClass.ActiveNode_Count + " Records";
 
                Master_Value.MasterValueClass.Settable_RemoteTable();
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
            IsFilteredByDateTime = false;
            this.Text = "Loading , Please Wait...";
            toolStripStatusLabel1.Text = "Connecting to Remote System";
            toolStripStatusLabel2.Text = "Event Name: " + Master_Value.MasterValueClass.ActiveNode + " , Events Count: "
                + Master_Value.MasterValueClass.ActiveNode_Count + " Records";
           
            Master_Value.MasterValueClass.Settable_RemoteTable();
            Thread.Sleep(1500);
            this.Update();
            _Reload_Init();
        }

        private void WaitForm_Load(object sender, EventArgs e)
        {
            try
            {
               
                Thread.Sleep(3);
                this.Update();
                //_Reload_Init();           

            }
            catch (Exception err)
            {
                
                
            }
        }
    }
}
