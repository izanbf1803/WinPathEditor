using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinPathEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string GetPathStr()
        {
            return (string)Registry.GetValue(@"HKEY_CURRENT_USER\Environment", "Path", null);
        }

        private void SetPathStr(string str)
        {
            System.Environment.SetEnvironmentVariable("PATH", str, EnvironmentVariableTarget.User);
        }

        private void UploadPathEntriesList()
        {
            string pathStr = GetPathStr();

            if (pathStr != null) {
                List<string> path = GeneralUse.Split(pathStr, ';');

                listView1.Items.Clear();

                foreach (string s in path) {
                    ListViewItem list = new ListViewItem(s);
                    listView1.Items.Add(list);
                }
            }
            else {
                MessageBox.Show("Error loading path, please restart the app.");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Lock form resize
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;

            UploadPathEntriesList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This can't be reversed. Are you sure?", "Confirm insertion",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Question,
               MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes) 
            {
                string newPathStr = GetPathStr() + textBox1.Text + ';';
                SetPathStr(newPathStr);
            }

            UploadPathEntriesList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This can be reversed. Are you sure?", "Confirm deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                string pathStr = GetPathStr();

                if (pathStr != null) {
                    List<string> path = GeneralUse.Split(pathStr, ';');
                    int offset = 0;
                    foreach (ListViewItem item in listView1.SelectedItems) {
                        path.RemoveAt(item.Index - offset);
                        ++offset;
                    }

                    string newPathStr = GeneralUse.Join(path, ';');

                    SetPathStr(newPathStr);
                }
                else {
                    MessageBox.Show("Error loading path, please restart the app.");
                }
            }

            UploadPathEntriesList();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/izanbf1803");
        }
    }
}
