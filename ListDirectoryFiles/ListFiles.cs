using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ListDirectoryFiles
{
    public partial class ListFiles : Form
    {
        public ListFiles()
        {
            InitializeComponent();
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
           
                    String selectedPath = this.getSelectedFolder();

                    lblSelectedFolder.Visible = true;
                    lblSelectedFolder.Text = selectedPath;
                    if (Directory.GetFiles(selectedPath).Length > 0)
                    {
                        lblSelectedFolder.Visible = true;
                        lblSelectedFolder.Text = selectedPath;
                    }
                    else
                    {
                        lblError.Visible = true;
                    }
                    DataTable fDataTable = this.getDataTable();

                // get all files from directory 
                    foreach (var fi in Directory.GetFiles(selectedPath))
                    {
                        // iterating through each files from directory to get file information and adding to row
                        FileInfo oFileInfo = new FileInfo(fi);
                        DataRow row = fDataTable.NewRow();
                        row["FileName"] = oFileInfo.Name;
                        row["CreationTime"] = oFileInfo.CreationTime.ToString();
                        row["UpdationTime"] = oFileInfo.LastWriteTime.ToString();
                        row["Extension"] = oFileInfo.Extension.ToString();
                        row["Size"] = (oFileInfo.Length/1024).ToString() + " KB";
                        row["FilePath"] = oFileInfo.Directory.ToString()+oFileInfo.DirectoryName.ToString();

                        fDataTable.Rows.Add(row);
                    }
                    dataGridViewFiles.Visible = true;
                    dataGridViewFiles.DataSource = fDataTable;
                  


                
            
        }

        //SetUp data table defination to fill out the data
        public DataTable getDataTable()
        {
            DataTable filesDataTable = new DataTable();
            filesDataTable.Columns.Add("FileName");
            filesDataTable.Columns.Add("CreationTime");
            filesDataTable.Columns.Add("UpdationTime");
            filesDataTable.Columns.Add("Extension");
            filesDataTable.Columns.Add("Size");
            filesDataTable.Columns.Add("FilePath");
            return filesDataTable;
        }


        ////Open file dialog to select the folder 
        public string getSelectedFolder()
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    return fbd.SelectedPath;
                }
                else
                {
                    return "";
                }
            }

          
                
        }

        // cleanup the gridview and reset the all label values
        private void btnReset_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;
            lblSelectedFolder.Text = "";
            dataGridViewFiles.DataSource = null;
            dataGridViewFiles.Visible = false;


        }

    }
}
