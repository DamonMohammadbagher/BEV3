using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Threading;

namespace BEV
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Basic Event Viewer v3.0 For Windows Vista and Windows Server 2008 , .NET Framework 3.5 and Updated for ver (4.5) , Published by Damon Mohammadbagher.
        /// 
        /// Note: I Created this code in 2009 & in May 2022 Updated by me for Work with Win7/Win10/11 etc (working with .NetFramework 4.5),
        /// also Some Security Features Like Working with ETWProcessMon2/ETWPM2Monitor2/Sysmon Logs will add this Source Code soon! ;)
        /// 
        /// this code still has some problem for loading Large Event log records like 400,000 records etc , time to loading is very slow for these large records
        /// so i need to work on this for better performance , with new codes and i will update source codes soon for this fix this problem also i will add
        /// new security features to the source code too.
        /// </summary>
        
        private BindingSource TempBinding_Local;
        private BindingSource TempBinding_Remote;
        private EventRecord CastObject = null;
        private bool init = false;
        private bool init2 = false;
        public static bool Isclosing = false;

        public void Refresh_Remote_TreeNodes() 
        {
            try
            {
                treeView2.Nodes.Clear();
                treeView2.Nodes.Add(Master_Value.MasterValueClass._RemoteConnection_Root_Nodes);
                treeView2.Refresh();

            }
            catch (Exception err)
            {
               
            }
        }       

        public void Refresh_Remote_DataGrid_3and4() 
        {
            try
            {

                dataGridView4.DataSource = Master_Value.MasterValueClass.RemoteBindingSource;
                //dataGridView3.DataSource = Master_Value.MasterValueClass.table_Remoting;
               
                TempBinding_Remote.DataSource = Master_Value.MasterValueClass.table_Remoting;
                dataGridView3.DataSource = TempBinding_Remote;
                
                dataGridView3.Update();
                dataGridView3.Refresh();

                dataGridView4.Update();
                dataGridView4.Refresh();

                groupBox6.Text = Master_Value.MasterValueClass.Remote_Description_Groupbox6;
                
                // for adding properties to Array fot tab 3 (Remote)
                Master_Value.MasterValueClass.PropertiesList = new string[dataGridView4.Columns.Count];
                for (int i = 0; i < dataGridView4.Columns.Count; i++)
                {
                    Master_Value.MasterValueClass.PropertiesList[i] = dataGridView4.Columns[i].Name;
                }

            }
            catch (Exception err )
            {

                MessageBox.Show(null, "Loading Error " + "\n" + err.Message, "BEV 3.0", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Refresh_Local_TreeNodes() 
        {
            try
            {
                treeView1.Nodes.Clear();
                treeView1.Nodes.Add(Master_Value.MasterValueClass._LocalConnection_Root_Nodes);
                treeView1.Refresh();

            }
            catch (Exception err)
            {

            }
        }

        public void Refresh_Local_DataGrid1and2()
        {
            try
            {
                
                dataGridView1.DataSource = Master_Value.MasterValueClass.LocalBindingSource;                 
                
                TempBinding_Local.DataSource = Master_Value.MasterValueClass.table_Local;
                dataGridView2.DataSource = TempBinding_Local.DataSource;

                dataGridView1.Update();
                dataGridView1.Refresh();

                dataGridView2.Update();
                dataGridView2.Refresh();
                dataGridView1.Visible = true;
                dataGridView1.Show();
                dataGridView2.Visible = true;
                dataGridView2.Show();

                groupBox6.Text = Master_Value.MasterValueClass.Local_Description_Groupbox6;

                // for adding properties to Array fot tab 3 (Local)
                Master_Value.MasterValueClass.PropertiesList = new string[dataGridView1.Columns.Count];
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    Master_Value.MasterValueClass.PropertiesList[i] = dataGridView1.Columns[i].Name;
                }

            }
            catch (Exception err)
            {

                MessageBox.Show(null, "Loading Error " + "\n" + err.Message, "BEV 3.0", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public Form1()
        {

            try
            {
                InitializeComponent();
                TempBinding_Remote = new BindingSource();
                TempBinding_Local = new BindingSource();

            }
            catch (Exception error)
            {
                MessageBox.Show(null, "Loading Error " + "\n" + error.Message, "BEV 3.0", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            tabControl1.Refresh();
         
        }
       
        private void ConnectRemoteBTN_Click(object sender, EventArgs e)
        {
            try
            {
                Remote_Forms.Remote_Connection_MainForm RF = new BEV.Remote_Forms.Remote_Connection_MainForm();
                RF.ShowDialog();
                Refresh_Remote_TreeNodes();

            }
            catch (Exception err)
            {
                
                
            }
        }

        private void treeView2_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (e.Node.Text != null && Master_Value.MasterValueClass.Remote_Host_Name != null)
                {

                    toolStripStatusLabel_Nodes.Text = e.Node.Text + " (" + e.Node.ToolTipText + ")";
                    Master_Value.MasterValueClass.ActiveNode_Count = e.Node.ToolTipText;
                    Master_Value.MasterValueClass.ActiveNode = e.Node.Text;
                }
            }
            catch (Exception error)
            {

                
            }
           

        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Remote_Forms.Remote_Connection_MainForm RF = new BEV.Remote_Forms.Remote_Connection_MainForm();
                RF.ShowDialog();
                Refresh_Remote_TreeNodes();
                tabControl1.SelectedIndex = 1;
            }
            catch (Exception error)
            {


            }

         
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {           
            try
            {

                tabControl1.SelectedIndex = 1;
                if (Master_Value.MasterValueClass.Remote_Host_Name != null)
                {
                    Master_Value.MasterValueClass.RemoteBindingSource.DataSource = typeof(System.Diagnostics.Eventing.Reader.EventLogRecord);
                    Master_Value.MasterValueClass.RemoteBindingSource.Clear();
                    Master_Value.MasterValueClass.Settable_RemoteTable();
                    Master_Value.MasterValueClass.table_Remoting.Clear();

                    dataGridView3.DataSource = null;
                    dataGridView4.DataSource = null;
                    WaitForm WForm = new WaitForm();
                    WForm.ShowDialog();
                    Master_Value.MasterValueClass.Remote_Description_Groupbox6 = "Event Messages for Event Name (" + Master_Value.MasterValueClass.ActiveNode + ") " + " , Remote System Name : " + Master_Value.MasterValueClass.Remote_Host_Name;
                    Refresh_Remote_DataGrid_3and4();
                }
                else if (Master_Value.MasterValueClass.Remote_Host_Name == null) 
                {

                }


            }
            catch (Exception err)
            {

                MessageBox.Show(null, "Loading Error " + "\n" + err.Message, "BEV 3.0", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        
        private void Reload_Remote_Connection_Click(object sender, EventArgs e)
        {
            try
            {

               

                Master_Value.MasterValueClass.RemoteBindingSource.DataSource = typeof(System.Diagnostics.Eventing.Reader.EventLogRecord);
                Master_Value.MasterValueClass.RemoteBindingSource.Clear();
                Master_Value.MasterValueClass.Settable_RemoteTable();
                Master_Value.MasterValueClass.table_Remoting.Clear();
                dataGridView3.DataSource = null;
                dataGridView4.DataSource = null;
                WaitForm WForm = new WaitForm();
                WForm.ShowDialog();
                Master_Value.MasterValueClass.Remote_Description_Groupbox6 = "Event Messages for Event Name (" + Master_Value.MasterValueClass.ActiveNode + ") " + " , Remote System Name : " + Master_Value.MasterValueClass.Remote_Host_Name;
                Refresh_Remote_DataGrid_3and4();
                



            }
            catch (Exception err)
            {
                
                MessageBox.Show(null, "Loading Error " + "\n" + err.Message, "BEV 3.0", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            try
            {

                Form.CheckForIllegalCrossThreadCalls = false;

                tabControl1.SelectedIndex = 0;
                // this.Icon = SystemIcons.Shield;
                GruopBox_SingleLine.Visible = false;

                dataGridView3.Hide();
                dataGridView4.Hide();
                dataGridView1.Show();
                dataGridView2.Show();
                

                dataGridView3.RowEnter+=new DataGridViewCellEventHandler(dataGridView3_RowEnter);
                dataGridView3.DataError += new DataGridViewDataErrorEventHandler(dataGridView3_DataError);
                dataGridView4.DataError += new DataGridViewDataErrorEventHandler(dataGridView4_DataError);

                tabControl1.SelectedIndex = 0;
                Local_Class _LocalClass = new Local_Class();
                string Lhost = System.Environment.MachineName;
                _LocalClass._Connect(Lhost);
                Refresh_Local_TreeNodes();
                

            }
            catch (Exception err)
            {


            }

            try
            {
                
                if (!File.Exists("BEV_Temp.tmp"))
                {
                    FileStream fs = File.Create("BEV_Temp.tmp");
                    using (fs)
                    {
                        Byte[] info = new UTF8Encoding(true).GetBytes(Master_Value.Large_String.All_Security_Events_Description2);
                        fs.Write(info, 0, info.Length);
                    }

                    
                    Master_Value.Reloading_EventsID.GetAllEvents(fs.Name,false);
                    File.Delete(fs.Name);
                }
                else
                {
                    File.Delete("BEV_Temp.tmp");
                }
               

            }
            catch (Exception err)
            {


            }
        }        

        void dataGridView3_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dataGridView3.Show();
                dataGridView4.Show();
                dataGridView3.Visible = true;
                dataGridView4.Visible = true;
                

                try
                {

                    if (Master_Value.MasterValueClass.RemoteBindingSource.Position != e.RowIndex)
                    {
                        Master_Value.MasterValueClass.RemoteBindingSource.Position = e.RowIndex;

                        /// fix error ;) with init
                        //if (init)
                        //{
                        CastObject = ((EventRecord)Master_Value.MasterValueClass.RemoteBindingSource[e.RowIndex]);
                        string msg = CastObject.FormatDescription();
                        richTextBox1.Text = msg;
                        //}

                        if (e.RowIndex == 0 && !init2) init2 = true;
                    }
                   

                }
                catch (Exception err)
                {
                   
                    if (err.Message == "Index was out of range. Must be non-negative and less than the size of the collection.\r\nParameter name: index")
                    {
                        Refresh_Remote_DataGrid_3and4();
                    }
                   

                }
            }
            catch (Exception err)
            {

              
            }
           
        }
                
        private void dataGridView2_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            
            
            try
            {
                dataGridView1.Show();
                dataGridView2.Show();
                dataGridView1.Visible = true;
                dataGridView2.Visible = true;

                try
                {

                    if (Master_Value.MasterValueClass.LocalBindingSource.Position != e.RowIndex)
                    {
                        Master_Value.MasterValueClass.LocalBindingSource.Position = e.RowIndex;

                        /// fix error ;) with init
                        //if (init)
                        //{

                        CastObject = ((EventRecord)Master_Value.MasterValueClass.LocalBindingSource[e.RowIndex]);
                        string msg = CastObject.FormatDescription();
                        richTextBox1.Text = msg;
                        //}

                        if (e.RowIndex == 0 && !init) init = true;
                    }
                    
                    
                }
                catch (Exception err)
                {
                   
                    if (err.Message == "Index was out of range. Must be non-negative and less than the size of the collection.\r\nParameter name: index")
                    {
                       //  Refresh_Local_DataGrid1and2();
                    }
                    
                }
            }
            catch (Exception err)
            {

             
            }
            
        }

        private void dataGridView2_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                if (e.Context == DataGridViewDataErrorContexts.Commit)
                {
                    MessageBox.Show("Commit error");
                }
                if (e.Context == DataGridViewDataErrorContexts.CurrentCellChange)
                {
                    MessageBox.Show("Cell change");
                }
                if (e.Context == DataGridViewDataErrorContexts.Parsing)
                {
                    MessageBox.Show("parsing error");
                }
                if (e.Context == DataGridViewDataErrorContexts.LeaveControl)
                {
                    MessageBox.Show("leave control error");
                }

                if ((e.Exception) is ConstraintException)
                {
                    DataGridView view = (DataGridView)sender;
                    view.Rows[e.RowIndex].ErrorText = "an error";
                    view.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "an error";

                    e.ThrowException = false;
                }
            }
            catch (Exception err)
            {


            }
         


            
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                if (e.Context == DataGridViewDataErrorContexts.Commit)
                {
                    MessageBox.Show("Commit error");
                }
                if (e.Context == DataGridViewDataErrorContexts.CurrentCellChange)
                {
                    MessageBox.Show("Cell change");
                }
                if (e.Context == DataGridViewDataErrorContexts.Parsing)
                {
                    MessageBox.Show("parsing error");
                }
                if (e.Context == DataGridViewDataErrorContexts.LeaveControl)
                {
                    MessageBox.Show("leave control error");
                }

                if ((e.Exception) is ConstraintException)
                {
                    DataGridView view = (DataGridView)sender;
                    view.Rows[e.RowIndex].ErrorText = "an error";
                    view.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "an error";

                    e.ThrowException = false;
                }
            }
            catch (Exception err)
            {


            }
         

            
        }

        private void dataGridView4_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                if (e.Context == DataGridViewDataErrorContexts.Commit)
                {
                    MessageBox.Show("Commit error");
                }
                if (e.Context == DataGridViewDataErrorContexts.CurrentCellChange)
                {
                    MessageBox.Show("Cell change");
                }
                if (e.Context == DataGridViewDataErrorContexts.Parsing)
                {
                    MessageBox.Show("parsing error");
                }
                if (e.Context == DataGridViewDataErrorContexts.LeaveControl)
                {
                    MessageBox.Show("leave control error");
                }

                if ((e.Exception) is ConstraintException)
                {
                    DataGridView view = (DataGridView)sender;
                    view.Rows[e.RowIndex].ErrorText = "an error";
                    view.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "an error";

                    e.ThrowException = false;
                }
            }
            catch (Exception err)
            {

                
            }
           
        }

        private void dataGridView3_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                if (e.Context == DataGridViewDataErrorContexts.Commit)
                {
                    MessageBox.Show("Commit error");
                }
                if (e.Context == DataGridViewDataErrorContexts.CurrentCellChange)
                {
                    MessageBox.Show("Cell change");
                }
                if (e.Context == DataGridViewDataErrorContexts.Parsing)
                {
                    MessageBox.Show("parsing error");
                }
                if (e.Context == DataGridViewDataErrorContexts.LeaveControl)
                {
                    MessageBox.Show("leave control error");
                }

                if ((e.Exception) is ConstraintException)
                {
                    DataGridView view = (DataGridView)sender;
                    view.Rows[e.RowIndex].ErrorText = "an error";
                    view.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "an error";

                    e.ThrowException = false;
                }
            }
            catch (Exception err)
            {

                
            }
           
        }

        private void aboutBEVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AboutBox1 ABFM = new AboutBox1();
                ABFM.ShowDialog();
            }
            catch (Exception err)
            {
                
                
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void relaodToolStripMenuItem_Click(object sender, EventArgs e)
        {

          
            try
            {
                tabControl1.SelectedIndex = 0;
               
                //Master_Value.MasterValueClass.LocalBindingSource.DataSource = typeof(System.Diagnostics.Eventing.Reader.EventLogRecord);
                
                Master_Value.MasterValueClass.LocalBindingSource.Clear();
                Master_Value.MasterValueClass.Settable_LocalTable();
                Master_Value.MasterValueClass.table_Local.Clear();
                
                dataGridView1.DataSource = null;
                dataGridView2.DataSource = null;
                dataGridView2.Show();
                dataGridView1.Show();
                WaitForm2 WForm2 = new WaitForm2();
                WForm2.ShowDialog();                
                Master_Value.MasterValueClass.Local_Description_Groupbox6 = "Event Messages for Event Name (" + Master_Value.MasterValueClass.ActiveNode + ") " + " , Local System";
                Refresh_Local_DataGrid1and2();

               

            }
            catch (Exception err)
            {


            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                tabControl1.SelectedIndex = 0;
                Local_Class LClass = new Local_Class();
                string Lhost = System.Environment.MachineName;
                LClass._Connect(Lhost);
                Refresh_Local_TreeNodes();

            }
            catch (Exception err)
            {
                
                
            }
        }

        private void remoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Remote_Class RClass = new Remote_Class();
                if (treeView2.SelectedNode.ToolTipText == null | treeView2.SelectedNode.ToolTipText == string.Empty | treeView2.SelectedNode.ToolTipText == "0")
                {
                    MessageBox.Show(null, "Warning " + "\n" + "Selected Event is Empty or Not Found any Record for this Event" + "\nSystem Error: ", "BEV 3.0", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    RClass.RemoteEventSave(treeView2.SelectedNode.Text);
                }
                else
                {
                    RClass.RemoteEventSave(treeView2.SelectedNode.Text);
                }
            }
            catch (Exception err)
            {

                MessageBox.Show(null, "Warning : Remote System " + "\n" + "Please Select Event or Relaod then Click Save" + "\nSystem Error: " + err.Message, "BEV 3.0", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void localToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Remote_Class RClass = new Remote_Class();
                Local_Class LClass = new Local_Class();
                
                if (treeView1.SelectedNode.ToolTipText == null | treeView1.SelectedNode.ToolTipText == string.Empty | treeView1.SelectedNode.ToolTipText == "0")
                {
                    MessageBox.Show(null, "Warning " + "\n" + "Selected Event is Empty or Not Found any Record for this Event" + "\nSystem Error: ", "BEV 3.0", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LClass.LocalEventSave(treeView1.SelectedNode.Text);
                }
                else
                {
                    LClass.LocalEventSave(treeView1.SelectedNode.Text);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(null, "Warning : Local System " + "\n" + "Please Select Event or Relaod then Click Save" + "\nSystem Error: " + err.Message, "BEV 3.0", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process MyProcessHelp = new System.Diagnostics.Process();
                MyProcessHelp.StartInfo.FileName = "BEV 3.CHM";
                MyProcessHelp.Start();
            }
            catch (Exception error)
            {
                MessageBox.Show(null, "Loading Error " + "\n" + "File 'BEV 3.CHM' can not loading!\n"+ error.Message, "BEV 3.0", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
            
        }

        private void Refresh_LocalBTN_Click(object sender, EventArgs e)
        {
            try
            {
                tabControl1.SelectedIndex = 0;
                Local_Class LClass = new Local_Class();
                string Lhost = System.Environment.MachineName;
                LClass._Connect(Lhost);
                Refresh_Local_TreeNodes();

            }
            catch (Exception err)
            {


            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (e.Node.Text != null)
                {

                    toolStripStatusLabel_Nodes.Text = e.Node.Text + " (" + e.Node.ToolTipText + ")";
                    Master_Value.MasterValueClass.ActiveNode_Count = e.Node.ToolTipText;
                    Master_Value.MasterValueClass.ActiveNode = e.Node.Text;
                }

            }
            catch (Exception err)
            {

                
            }
           
        }

        private void Reload_LocalBTN_Click(object sender, EventArgs e)
        {
            try
            {
                
                Master_Value.MasterValueClass.LocalBindingSource.DataSource = typeof(System.Diagnostics.Eventing.Reader.EventLogRecord);
                Master_Value.MasterValueClass.LocalBindingSource.Clear();
                Master_Value.MasterValueClass.Settable_LocalTable();
                Master_Value.MasterValueClass.table_Local.Clear();
                dataGridView1.DataSource = null;
                dataGridView2.DataSource = null;
                dataGridView2.Show();
                dataGridView1.Show();
                richTextBox1.Text = "";
                WaitForm2 WForm2 = new WaitForm2();
                WForm2.ShowDialog();
                Master_Value.MasterValueClass.Local_Description_Groupbox6 = "Event Messages for Event Name (" + Master_Value.MasterValueClass.ActiveNode + ") " + " , Local System";
                Refresh_Local_DataGrid1and2();
                
            }
            catch (Exception err)
            {
                
                
            }
           
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (tabControl1.SelectedIndex == 0)
                {
                    groupBox6.Text = Master_Value.MasterValueClass.Local_Description_Groupbox6;
                    if (groupBox6.Text == string.Empty)
                    {
                        groupBox6.Text = "Event Messages";
                    }


                    dataGridView3.Hide();
                    dataGridView4.Hide();


                    dataGridView1.Show();
                    dataGridView2.Show();
                    dataGridView1.Visible = true;
                    dataGridView2.Visible = true;

                    dataGridView1.DataSource = Master_Value.MasterValueClass.LocalBindingSource;
                    //dataGridView2.DataSource = Master_Value.MasterValueClass.table_Local;
                    dataGridView2.DataSource = TempBinding_Local;


                }
                else if (tabControl1.SelectedIndex == 1)
                {
                    groupBox6.Text = Master_Value.MasterValueClass.Remote_Description_Groupbox6;
                    if (groupBox6.Text == string.Empty)
                    {
                        groupBox6.Text = "Event Messages";
                    }



                    dataGridView1.Hide();
                    dataGridView2.Hide();

                    dataGridView3.Show();
                    dataGridView4.Show();
                    dataGridView3.Visible = true;
                    dataGridView4.Visible = true;



                    dataGridView4.DataSource = Master_Value.MasterValueClass.RemoteBindingSource;
                    //dataGridView3.DataSource = Master_Value.MasterValueClass.table_Remoting;
                    dataGridView3.DataSource = TempBinding_Remote;

                }
            }
            catch (Exception err)
            {


            }
        }

        private void reloadToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {

               
                // Master_Value.MasterValueClass.LocalBindingSource.DataSource = typeof(System.Diagnostics.Eventing.Reader.EventLogRecord);
                Master_Value.MasterValueClass.LocalBindingSource.Clear();
                Master_Value.MasterValueClass.Settable_LocalTable();
                Master_Value.MasterValueClass.table_Local.Clear();
                
                dataGridView1.DataSource = null;
                dataGridView2.DataSource = null;
                dataGridView1.Show();
                dataGridView2.Show();
                dataGridView1.Visible = true;
                dataGridView2.Visible = true;
                richTextBox1.Text = "";
                WaitForm2 WForm2 = new WaitForm2();
                WForm2.ShowDialog();
                Master_Value.MasterValueClass.Local_Description_Groupbox6 = "Event Messages for Event Name (" + Master_Value.MasterValueClass.ActiveNode + ") "
                    + " , Local System";
                Refresh_Local_DataGrid1and2();
                

            }
            catch (Exception err)
            {


            }
        }

        private void refreshToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                tabControl1.SelectedIndex = 0;
                Local_Class LClass = new Local_Class();
                string Lhost = System.Environment.MachineName;
                LClass._Connect(Lhost);
                Refresh_Local_TreeNodes();

            }
            catch (Exception err)
            {


            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            try
            {
                Remote_Forms.Remote_Connection_MainForm RF = new BEV.Remote_Forms.Remote_Connection_MainForm();
                RF.ShowDialog();
                Refresh_Remote_TreeNodes();

            }
            catch (Exception err)
            {


            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {


                if (Master_Value.MasterValueClass.Remote_Host_Name != null)
                {
                    Master_Value.MasterValueClass.RemoteBindingSource.DataSource = typeof(System.Diagnostics.Eventing.Reader.EventLogRecord);
                    Master_Value.MasterValueClass.RemoteBindingSource.Clear();
                    Master_Value.MasterValueClass.Settable_RemoteTable();
                    Master_Value.MasterValueClass.table_Remoting.Clear();

                    dataGridView3.DataSource = null;
                    dataGridView4.DataSource = null;
                    WaitForm WForm = new WaitForm();
                    WForm.ShowDialog();
                    Master_Value.MasterValueClass.Remote_Description_Groupbox6 = "Event Messages for Event Name (" + Master_Value.MasterValueClass.ActiveNode + ") " + " , Remote System Name : " + Master_Value.MasterValueClass.Remote_Host_Name;
                    Refresh_Remote_DataGrid_3and4();
                }
                else if (Master_Value.MasterValueClass.Remote_Host_Name == null) 
                {

                }



            }
            catch (Exception err)
            {

                MessageBox.Show(null, "Loading Error " + "\n" + err.Message, "BEV 3.0", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
           
            try
            {

                if (TempBinding_Local.Position != e.RowIndex)
                {
                    //TempBinding_Local.Position = e.RowIndex;
                    dataGridView1.Rows[e.RowIndex].Visible = true;
                }
            }
            catch (Exception err)
            {

                System.Diagnostics.Debug.WriteLine(err.Message);
            }
        }

        private void dataGridView4_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (TempBinding_Remote.Position != e.RowIndex)
                {
                    //TempBinding_Remote.Position = e.RowIndex;
                    dataGridView4.Rows[e.RowIndex].Visible = true;
                }
            }
            catch (Exception err)
            {

                System.Diagnostics.Debug.WriteLine(err.Message);
            }
        }

        private void saveThisEventToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Remote_Class RClass = new Remote_Class();
                Local_Class LClass = new Local_Class();

                if (treeView1.SelectedNode.ToolTipText == null | treeView1.SelectedNode.ToolTipText == string.Empty | treeView1.SelectedNode.ToolTipText == "0")
                {
                    MessageBox.Show(null, "Warning " + "\n" + "Selected Event is Empty or Not Found any Record for this Event" + "\nSystem Error: ", "BEV 3.0", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LClass.LocalEventSave(treeView1.SelectedNode.Text);
                }
                else
                {
                    LClass.LocalEventSave(treeView1.SelectedNode.Text);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(null, "Warning : Local System " + "\n" + "Please Select Event or Relaod then Click Save" + "\nSystem Error: " + err.Message, "BEV 3.0", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void saveThisEventToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                Remote_Class RClass = new Remote_Class();
                if (treeView2.SelectedNode.ToolTipText == null | treeView2.SelectedNode.ToolTipText == string.Empty | treeView2.SelectedNode.ToolTipText == "0")
                {
                    MessageBox.Show(null, "Warning " + "\n" + "Selected Event is Empty or Not Found any Record for this Event" + "\nSystem Error: ", "BEV 3.0", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    RClass.RemoteEventSave(treeView2.SelectedNode.Text);
                }
                else
                {
                    RClass.RemoteEventSave(treeView2.SelectedNode.Text);
                }
            }
            catch (Exception err)
            {

                MessageBox.Show(null, "Warning : Remote System " + "\n" + "Please Select Event or Relaod then Click Save" + "\nSystem Error: " + err.Message, "BEV 3.0", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }     

        private void filterByEventTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                if (tabControl1.SelectedIndex == 0)
                {
                    FilterForm_Form1._ZIndex = 0;
                    FilterForm_Form1 MFTF = new FilterForm_Form1();
                    MFTF.ShowDialog();
                }
                else if (tabControl1.SelectedIndex == 1)
                {
                    Filtering.Remote.FilterForm_Form2._ZIndex = 0;
                    Filtering.Remote.FilterForm_Form2 MFTF = new BEV.Filtering.Remote.FilterForm_Form2();
                    MFTF.ShowDialog();
                }


            }
            catch (Exception err)
            {
                
                
            }

        }

        private void filterByMessageTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabControl1.SelectedIndex == 0)
                {
                    FilterForm_Form1._ZIndex = 1;
                    FilterForm_Form1 MFTF = new FilterForm_Form1();
                    MFTF.ShowDialog();
                }
                else if (tabControl1.SelectedIndex == 1)
                {
                    Filtering.Remote.FilterForm_Form2._ZIndex = 1;
                    Filtering.Remote.FilterForm_Form2 MFTF = new BEV.Filtering.Remote.FilterForm_Form2();
                    MFTF.ShowDialog();
                }
            }
            catch (Exception err)
            {
                
                
            }
            
            
        }

        private void otherFiltersToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                
                if (tabControl1.SelectedIndex == 0)
                {                   
                    FilterForm_Form1._ZIndex = 2;
                    FilterForm_Form1 MFTF = new FilterForm_Form1();
                    MFTF.ShowDialog();
                }
                else if (tabControl1.SelectedIndex == 1)
                {                    
                    Filtering.Remote.FilterForm_Form2._ZIndex = 2;
                    Filtering.Remote.FilterForm_Form2 MFTF = new BEV.Filtering.Remote.FilterForm_Form2();
                    MFTF.ShowDialog();
                }

               

            }
            catch (Exception err)
            {


            }
            

        }

        private void filterByEventTypesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                FilterForm_Form1._ZIndex = 0;
                FilterForm_Form1 MFTF = new FilterForm_Form1();
                MFTF.ShowDialog();
            }
            catch (Exception err)
            {

                
            }

        }

        private void filterByMessageTextToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                FilterForm_Form1._ZIndex = 1;
                FilterForm_Form1 MFTF = new FilterForm_Form1();
                MFTF.ShowDialog();

            }
            catch (Exception err)
            {

                
            }
        }

        private void dataGridView2_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {              
                Filtering.Local.PropertiesForm_Local.Properties_Datarow_Local_1 = ((System.Data.DataRowView)TempBinding_Local.List[e.RowIndex]);
                Filtering.Local.PropertiesForm_Local.Properties_Datarow_Local_2 = ((EventLogRecord)Master_Value.MasterValueClass.LocalBindingSource.List[e.RowIndex]);
                Filtering.Local.PropertiesForm_Local PFM = new BEV.Filtering.Local.PropertiesForm_Local();
                PFM.ShowDialog();
            }
            catch (Exception err)
            {


            }

        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {

                Filtering.Local.PropertiesForm_Local.Properties_Datarow_Local_1 = ((System.Data.DataRowView)TempBinding_Local.List[e.RowIndex]);
                Filtering.Local.PropertiesForm_Local.Properties_Datarow_Local_2 = ((EventLogRecord)Master_Value.MasterValueClass.LocalBindingSource.List[e.RowIndex]);
                Filtering.Local.PropertiesForm_Local PFM = new BEV.Filtering.Local.PropertiesForm_Local();
                PFM.ShowDialog();
            }
            catch (Exception err)
            {


            }
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {

                Filtering.Local.PropertiesForm_Local.Properties_Datarow_Local_1 = ((System.Data.DataRowView)TempBinding_Local.List[dataGridView2.CurrentRow.Index]);
                Filtering.Local.PropertiesForm_Local.Properties_Datarow_Local_2 = ((EventLogRecord)Master_Value.MasterValueClass.LocalBindingSource.List[dataGridView1.CurrentRow.Index]);
                Filtering.Local.PropertiesForm_Local PFM = new BEV.Filtering.Local.PropertiesForm_Local();
                PFM.ShowDialog();
            }
            catch (Exception err)
            {


            }
        }

        private void propertiesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                Filtering.Remote.PropertiesForm_Remote.Properties_Datarow_Remote_1 = ((System.Data.DataRowView)TempBinding_Remote.List[dataGridView3.CurrentRow.Index]);
                Filtering.Remote.PropertiesForm_Remote.Properties_Datarow_Remote_2 = ((EventLogRecord)Master_Value.MasterValueClass.RemoteBindingSource.List[dataGridView4.CurrentRow.Index]);
                Filtering.Remote.PropertiesForm_Remote PFM = new BEV.Filtering.Remote.PropertiesForm_Remote();
                PFM.ShowDialog();
            }
            catch (Exception err)
            {


            }
        }

        private void dataGridView3_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
            try
            {
                Filtering.Remote.PropertiesForm_Remote.Properties_Datarow_Remote_1 = ((System.Data.DataRowView)TempBinding_Remote.List[dataGridView3.CurrentRow.Index]);
                Filtering.Remote.PropertiesForm_Remote.Properties_Datarow_Remote_2 = ((EventLogRecord)Master_Value.MasterValueClass.RemoteBindingSource.List[dataGridView3.CurrentRow.Index]);
                Filtering.Remote.PropertiesForm_Remote PFM = new BEV.Filtering.Remote.PropertiesForm_Remote();
                PFM.ShowDialog();
            }
            catch (Exception err)
            {


            }
        }

        private void dataGridView4_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                Filtering.Remote.PropertiesForm_Remote.Properties_Datarow_Remote_1 = ((System.Data.DataRowView)TempBinding_Remote.List[dataGridView4.CurrentRow.Index]);
                Filtering.Remote.PropertiesForm_Remote.Properties_Datarow_Remote_2 = ((EventLogRecord)Master_Value.MasterValueClass.RemoteBindingSource.List[dataGridView4.CurrentRow.Index]);
                Filtering.Remote.PropertiesForm_Remote PFM = new BEV.Filtering.Remote.PropertiesForm_Remote();
                PFM.ShowDialog();
            }
            catch (Exception err)
            {


            }
        }

        private void filterByTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Filtering.Remote.FilterForm_Form2._ZIndex = 0;
                Filtering.Remote.FilterForm_Form2 MFTF = new BEV.Filtering.Remote.FilterForm_Form2();
                MFTF.ShowDialog();
            }
            catch (Exception err)
            {

            }

        }

        private void filterByMessageTextToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                Filtering.Remote.FilterForm_Form2._ZIndex = 1;
                Filtering.Remote.FilterForm_Form2 MFTF = new BEV.Filtering.Remote.FilterForm_Form2();
                MFTF.ShowDialog();
            }
            catch (Exception err)
            {

            }

        }

        private void propertiesWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabControl1.SelectedIndex == 0)
                {
                    try
                    {

                        Filtering.Local.PropertiesForm_Local.Properties_Datarow_Local_1 = ((System.Data.DataRowView)TempBinding_Local.List[dataGridView2.CurrentRow.Index]);
                        Filtering.Local.PropertiesForm_Local.Properties_Datarow_Local_2 = ((EventLogRecord)Master_Value.MasterValueClass.LocalBindingSource.List[dataGridView2.CurrentRow.Index]);
                        Filtering.Local.PropertiesForm_Local PFM = new BEV.Filtering.Local.PropertiesForm_Local();
                        PFM.ShowDialog();
                    }
                    catch (Exception err)
                    {


                    }
                }
                if (tabControl1.SelectedIndex == 1)
                {
                    try
                    {
                        Filtering.Remote.PropertiesForm_Remote.Properties_Datarow_Remote_1 = ((System.Data.DataRowView)TempBinding_Remote.List[dataGridView3.CurrentRow.Index]);
                        Filtering.Remote.PropertiesForm_Remote.Properties_Datarow_Remote_2 = ((EventLogRecord)Master_Value.MasterValueClass.RemoteBindingSource.List[dataGridView3.CurrentRow.Index]);
                        Filtering.Remote.PropertiesForm_Remote PFM = new BEV.Filtering.Remote.PropertiesForm_Remote();
                        PFM.ShowDialog();
                    }
                    catch (Exception err)
                    {


                    }
                }
            }
            catch (Exception error)
            {


            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Isclosing = true;
            Thread.Sleep(1000);
            WaitForm F1 = new WaitForm();
            WaitForm2 F2 = new WaitForm2();

            if (F1._Thread.IsAlive)
                F1._Thread.Abort();

            if (F2._Thread.IsAlive)
                F2._Thread.Abort();
        }


        #region  rem for this version            
        // for this version 
        //private void vToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        dataGridView1.Visible = false;
        //        dataGridView4.Visible = false;
        //        GruopBox_SingleLine.Visible = true;
        //        GruopBox_SingleLine.Dock = DockStyle.Fill;

        //    }
        //    catch (Exception err)
        //    {


        //    }
        //}

        //private void viewMultipleLineToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        dataGridView1.Visible = true;                
        //        dataGridView4.Visible = true;
        //        Refresh_Local_DataGrid1and2();
        //        Refresh_Remote_DataGrid_3and4();
        //        GruopBox_SingleLine.Visible = false;
        //        //GruopBox_SingleLine.Dock = DockStyle.Fill;

        //    }
        //    catch (Exception err)
        //    {


        //    }
        //}

        // for this version 
        #endregion


    }
}
