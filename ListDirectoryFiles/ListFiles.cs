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
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    lblSelectedFolder.Visible = true;
                    lblSelectedFolder.Text = fbd.SelectedPath.ToString();
                    if (Directory.GetFiles(fbd.SelectedPath).Length > 0)
                    {
                        lblSelectedFolder.Visible = true;
                        lblSelectedFolder.Text = fbd.SelectedPath.ToString();
                    }
                    else
                    {
                        lblError.Visible = true;
                    }

                    DataTable filesDataTable = new DataTable();
                    filesDataTable.Columns.Add("FileName");
                    filesDataTable.Columns.Add("CreationTime");
                    filesDataTable.Columns.Add("UpdationTime");
                    filesDataTable.Columns.Add("Extension");
                    filesDataTable.Columns.Add("Size");
                    filesDataTable.Columns.Add("FilePath");


                    foreach (var fi in Directory.GetFiles(fbd.SelectedPath))
                    {
                        FileInfo oFileInfo = new FileInfo(fi);
                        DataRow row = filesDataTable.NewRow();
                        row["FileName"] = oFileInfo.Name;
                        row["CreationTime"] = oFileInfo.CreationTime.ToString();
                        row["UpdationTime"] = oFileInfo.LastWriteTime.ToString();
                        row["Extension"] = oFileInfo.Extension.ToString();
                        row["Size"] = (oFileInfo.Length/1024).ToString() + " KB";
                        row["FilePath"] = oFileInfo.Directory.ToString()+oFileInfo.DirectoryName.ToString();

                        filesDataTable.Rows.Add(row);
                    }
                    dataGridViewFiles.Visible = true;
                    dataGridViewFiles.DataSource = filesDataTable;
                  


                }
            }
        }



        private void btnReset_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;
            lblSelectedFolder.Text = "";
            dataGridViewFiles.DataSource = null;
            dataGridViewFiles.Visible = false;


        }

    }
}
