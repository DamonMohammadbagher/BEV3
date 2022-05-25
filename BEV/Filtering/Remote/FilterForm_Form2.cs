using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics.Eventing.Reader;

namespace BEV.Filtering.Remote
{
    public partial class FilterForm_Form2 : Form
    {
       
        

        public static int _ZIndex;
        //private BindingSource Filtering_TempBinding_Local_1;
        //private BindingSource Filtering_TempBinding_Local_2;
        //private BindingSource Filtering_TempBinding_Local_3;

        private BindingSource Filtering_TempBinding_Remote_1;
        private BindingSource Filtering_TempBinding_Remote_2;
        private BindingSource Filtering_TempBinding_Remote_3;
        private BindingSource Filtering_TempBinding_Remote_3_1;
        private EventRecord CastObject = null;


        public FilterForm_Form2()
        {
            InitializeComponent();
        }  
       
        private void FilterForm_Form2_Load(object sender, EventArgs e)
        {
             try
            {
               
                tabControl1.SelectedIndex = _ZIndex;
               //////// Filtering_TempBinding_Local_1 = new BindingSource();
               //////// Filtering_TempBinding_Local_2 = new BindingSource();
                //remote
                Filtering_TempBinding_Remote_1 = new BindingSource();
                Filtering_TempBinding_Remote_2 = new BindingSource();
                Filtering_TempBinding_Remote_3 = new BindingSource();
                Filtering_TempBinding_Remote_3_1 = new BindingSource();

                DataTable TempTable_For_Tab1 = Master_Value.MasterValueClass.table_Remoting.Copy();
                DataTable TempTable_For_Tab2 = Master_Value.MasterValueClass.table_Remoting.Copy();
                DataTable TempTable_For_Tab3 = Master_Value.MasterValueClass.table_Remoting.Copy();

                //remote
               
 

                ////////DataTable TempTable_For_Tab1 = Master_Value.MasterValueClass.table_Local.Copy();
                ////////DataTable TempTable_For_Tab2 = Master_Value.MasterValueClass.table_Local.Copy();
                ////////DataTable TempTable_For_Tab3 = Master_Value.MasterValueClass.table_Local.Copy();

                //remote
                Filtering_TempBinding_Remote_1.DataSource = TempTable_For_Tab1;
                Filtering_TempBinding_Remote_2.DataSource = TempTable_For_Tab2;
                Filtering_TempBinding_Remote_3.DataSource = TempTable_For_Tab3;
                Filtering_TempBinding_Remote_3_1.DataSource = Master_Value.Reloading_EventsID.table_;

                //dataGridView2.DataSource = Filtering_TempBinding_Remote_2;

                dataGridView5.DataSource = Filtering_TempBinding_Remote_3_1;
                dataGridView5.Refresh();
                //remote


            
            }
            catch (Exception err)
            {
                
                
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            
            //DataView view = (DataView)dataGrid1.DataSource;

            //// Set the filter to display only those rows that were modified.
            //view.RowStateFilter = DataViewRowState.ModifiedCurrent;

            //// Change the value of the CompanyName column for each modified row.
            //foreach (DataRowView rowView in view)
            //{
            //    Console.WriteLine(rowView.Row[2]);
            //}



            //try
            //{
            //    int _i = Filtering_TempBinding_Local_1.Count;
            //    //Array_to_Save_1 = new object[_i];
            //    System.Data.DataRowView g;
            //    for (int i = 0; i <= _i; i++)
            //    {
            //        //                    Filtering_TempBinding_Local_1.CopyTo(Array_to_Save_1, i);
            //        //      g = ((System.Data.DataRowView)Filtering_TempBinding_Local_1.List[i]);
            //        g = ((System.Data.DataRowView)Master_Value.MasterValueClass.LocalBindingSource.List[i]);
            //        MessageBox.Show(g.Row[2].ToString());


            //    }
            //}
            //catch (Exception err)
            //{


            //}


            //  BindingSource f = new BindingSource();

            //  System.Collections.ArrayList fruits = new System.Collections.ArrayList();
            //fruits.Add("apple");
            //fruits.Add("mango");

            //          IEnumerable<EventLogRecord> query =
            //                fruits.Cast<EventLogRecord>().Select(fruit => fruit);
            //IEnumerable<EventLogRecord> query =
            //                Filtering_TempBinding_Local_1.List.Cast<EventLogRecord>().Select(Filtering_TempBinding_Local_1.List[ => fruit);

            //foreach (EventLogRecord fruit in query)
            //    Console.WriteLine(fruit.RecordId);

            // This code produces the following output:
            //
            // apple
            // mango




            //// System.IO.FileInfo f = new System.IO.FileInfo("test.txt");
            ////// System.IO.StreamWriter ff = new System.IO.StreamWriter(
            //// foreach (string item in Array_to_Save_1)
            //// {

            //// }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string temp1 = string.Empty;
                string temp2 = string.Empty;

                if (checkBox1.Checked) { temp1 = "Type = 'Information'"; temp2 = "LevelDisplayName = 'Information'"; }
                else
                    if (checkBox2.Checked) { temp1 = "Type = 'Warning'"; temp2 = "LevelDisplayName = 'Warning'"; }
                else
                        if (checkBox3.Checked) { temp1 = "Type = 'Error'"; temp2 = "LevelDisplayName = 'Error'"; }
                else
                            if (checkBox4.Checked)
                {
                    temp1 = "(Type <> 'Information') AND (Type <> 'Error') AND (Type <> 'Warning') ";


                }



                Master_Value.MasterValueClass.Filtering_Types_Remote_Filter_Query = temp1;
                Filtering_TempBinding_Remote_1.Filter = temp1;


                dataGridView1.DataSource = Filtering_TempBinding_Remote_1;
                groupBox2.Text = "Events , Results:( " + dataGridView1.RowCount.ToString() + " )";
                if (dataGridView1.RowCount == 0 || dataGridView1.Rows == null) { MessageBox.Show("Record Not found ", "BEV Filters: ", MessageBoxButtons.OK, MessageBoxIcon.Question); richTextBox_TypeMsg.Clear(); }




            }
            catch (Exception err)
            {
                try
                {
                    if (dataGridView1.RowCount == 0 || dataGridView1.Rows == null)
                    {
                        MessageBox.Show("Record Not found ", "BEV Filters: ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        richTextBox_TypeMsg.Clear();

                    }

                }
                catch (Exception eror1)
                {


                }

            }

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {

                checkBox1.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {

                checkBox3.Checked = false;
                checkBox2.Checked = false;
                checkBox4.Checked = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {

                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox4.Checked = false;
            }
           
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {

                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
            }

        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                //for filters binding source need create _Index Value type of Int
                Filtering.Remote.PropertiesForm_Remote.Properties_Datarow_Remote_1 = ((System.Data.DataRowView)Filtering_TempBinding_Remote_1.List[e.RowIndex]);                
                int _Index = Convert.ToInt32(((System.Data.DataRowView)Filtering_TempBinding_Remote_1.List[e.RowIndex]).Row[0]);
                Filtering.Remote.PropertiesForm_Remote.Properties_Datarow_Remote_2 = ((EventLogRecord)Master_Value.MasterValueClass.RemoteBindingSource.List[_Index - 1]);                
                Filtering.Remote.PropertiesForm_Remote PFM = new PropertiesForm_Remote();                
                PFM.ShowDialog();
            }
            catch (Exception err)
            {


            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string text = textBox1.Text;
                Filtering_TempBinding_Remote_2.Filter = "Message LIKE '*" + text + "*'";
                dataGridView2.DataSource = Filtering_TempBinding_Remote_2;
                groupBox4.Text = "Events , Results:( " + dataGridView2.RowCount.ToString() + " )";

            }
            catch (Exception err)
            {


            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView2_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                //for filters binding source need create _Index Value type of Int
                Filtering.Remote.PropertiesForm_Remote.Properties_Datarow_Remote_1 = ((System.Data.DataRowView)Filtering_TempBinding_Remote_2.List[e.RowIndex]);               
                int _Index = Convert.ToInt32(((System.Data.DataRowView)Filtering_TempBinding_Remote_2.List[e.RowIndex]).Row[0]);
                Filtering.Remote.PropertiesForm_Remote.Properties_Datarow_Remote_2 = ((EventLogRecord)Master_Value.MasterValueClass.RemoteBindingSource.List[_Index - 1]);               
                Filtering.Remote.PropertiesForm_Remote PFM = new PropertiesForm_Remote();
               
                PFM.ShowDialog();
            }
            catch (Exception err)
            {


            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView5_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            string _Get_Set_EventID_Tooltip = Convert.ToString(((System.Data.DataRowView)Filtering_TempBinding_Remote_3_1.List[e.RowIndex]).Row[3]);
            //textBox4.Text = _Get_Set_EventID_Tooltip;
            richTextBox1.Text = _Get_Set_EventID_Tooltip;

        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView5_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string Current_Logname = ((EventLogRecord)Master_Value.MasterValueClass.RemoteBindingSource.List[0]).LogName;
                if (Current_Logname.ToUpper() == "SECURITY")
                {
                    int _Get_Set_EventID = Convert.ToInt32(((System.Data.DataRowView)Filtering_TempBinding_Remote_3_1.List[e.RowIndex]).Row[0]);
                    // int _Get_Set_LevelID = Convert.ToInt32(((System.Data.DataRowView)Filtering_TempBinding_Remote_3_1.List[e.RowIndex]).Row[1]);
                    // string Temps = "(EventID = " + _Get_Set_EventID.ToString() + " )" + " AND " + "( Level = " + _Get_Set_LevelID + " )";
                    string Temps = "EventID = " + _Get_Set_EventID.ToString();
                    Filtering_TempBinding_Remote_3.Filter = Temps;
                    dataGridView4.DataSource = Filtering_TempBinding_Remote_3;
                    dataGridView4.Refresh();
                    groupBox7.Text = "Events , Results:( " + dataGridView4.RowCount.ToString() + " )";
                    if (dataGridView4.RowCount == 0 || dataGridView4.Rows == null) { MessageBox.Show("Record Not found ", "BEV Filters: ", MessageBoxButtons.OK, MessageBoxIcon.Question); }
                }
                else 
                {
                    MessageBox.Show("Record Not found \nPlease first ReLoading \"Security\" log!\nCurrent LogName is \"" + Current_Logname + "\"", "BEV Filters: ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
            }
            catch (Exception err)
            {
               
                if (dataGridView4.RowCount == 0 || dataGridView4.Rows == null) { MessageBox.Show("Record Not found ", "BEV Filters: ", MessageBoxButtons.OK, MessageBoxIcon.Question); }

            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            
            
            try
            {

                
                string Current_Logname = ((EventLogRecord)Master_Value.MasterValueClass.RemoteBindingSource.List[0]).LogName;
                if (Current_Logname.ToUpper() == "SECURITY")
                {
                    int _Get_Set_EventID = Convert.ToInt32(((System.Data.DataRowView)Filtering_TempBinding_Remote_3_1.List[dataGridView5.CurrentRow.Index]).Row[0]);
                    // int _Get_Set_LevelID = Convert.ToInt32(((System.Data.DataRowView)Filtering_TempBinding_Remote_3_1.List[dataGridView5.CurrentRow.Index]).Row[1]);
                    string Temps = "EventID = " + _Get_Set_EventID.ToString();
                    // string Temps = "(EventID = " + _Get_Set_EventID.ToString() + " )" + " AND " + "( Level = " + _Get_Set_LevelID + " )";
                    Filtering_TempBinding_Remote_3.Filter = Temps;
                    dataGridView4.DataSource = Filtering_TempBinding_Remote_3;
                    dataGridView4.Refresh();
                    groupBox7.Text = "Events , Results:( " + dataGridView4.RowCount.ToString() + " )";
                    if (dataGridView4.RowCount == 0 || dataGridView4.Rows == null) { MessageBox.Show("Record Not found ", "BEV Filters: ", MessageBoxButtons.OK, MessageBoxIcon.Question); }
                }
                else
                {
                    MessageBox.Show("Record Not found \nPlease first ReLoading \"Security\" log!\nCurrent LogName is \"" + Current_Logname + "\"", "BEV Filters: ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }

            }
            catch (Exception err)
            {
               
                if (dataGridView4.RowCount == 0 || dataGridView4.Rows == null) { MessageBox.Show("Record Not found ", "BEV Filters: ", MessageBoxButtons.OK, MessageBoxIcon.Question); }

            }
        }

        private void dataGridView4_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {            
            
            try
            {
                //for filters binding source need create _Index Value type of Int
                Filtering.Remote.PropertiesForm_Remote.Properties_Datarow_Remote_1 = ((System.Data.DataRowView)Filtering_TempBinding_Remote_3.List[e.RowIndex]);
                int _Index = Convert.ToInt32(((System.Data.DataRowView)Filtering_TempBinding_Remote_3.List[e.RowIndex]).Row[0]);
                Filtering.Remote.PropertiesForm_Remote.Properties_Datarow_Remote_2 = ((EventLogRecord)Master_Value.MasterValueClass.RemoteBindingSource.List[_Index - 1]);
                Filtering.Remote.PropertiesForm_Remote PFM = new BEV.Filtering.Remote.PropertiesForm_Remote();
                PFM.ShowDialog();
            }
            catch (Exception err)
            {


            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                try
                {

                    //int _Get_Set_EventIndex = Convert.ToInt32(((System.Data.DataRowView)Filtering_TempBinding_Remote_1.List[e.RowIndex]).Row[0]);
                    //CastObject = (EventRecord)Master_Value.MasterValueClass.RemoteBindingSource[_Get_Set_EventIndex - 1];
                    //string msg = CastObject.FormatDescription();
                    //richTextBox_TypeMsg.Text = msg;
                    richTextBox_TypeMsg.Text = "";
                    richTextBox_TypeMsg.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                }
                catch (Exception err)
                {

                    if (err.Message == "Index was out of range. Must be non-negative and less than the size of the collection.\r\nParameter name: index")
                    {

                    }

                }
            }
            catch (Exception err)
            {

                //  System.Diagnostics.Debug.WriteLine(err.Message);
            }
        }

        private void dataGridView4_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                try
                {

                    int i = e.RowIndex;
                    richTextBox_SecurityEVT.Text = dataGridView4.Rows[e.RowIndex].Cells[3].Value.ToString();
                }
                catch (Exception err)
                {

                    if (err.Message == "Index was out of range. Must be non-negative and less than the size of the collection.\r\nParameter name: index")
                    {

                    }

                }
            }
            catch (Exception err)
            {

                //  System.Diagnostics.Debug.WriteLine(err.Message);
            }
        }

        private void DataGridView2_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            richTextBox2.Text = "";
            richTextBox2.Text = dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
        }
    }
}
