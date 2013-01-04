using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace PawnoSharp_IDE
{
    public partial class compileForm : Form
    {
        private string _path;
        public compileForm(string path)
        {
            InitializeComponent();

            this._path = path;
            string pathAesthetic = Path.GetFileName(path);
            this.Text = "Compiling " + pathAesthetic + "...";

            listView1.Items.Clear();
            string rawOut = null;
            System.Diagnostics.Process stCompile = new System.Diagnostics.Process();
            stCompile.StartInfo.UseShellExecute = false;
            stCompile.StartInfo.RedirectStandardOutput = true;
            stCompile.StartInfo.RedirectStandardError = true;
            stCompile.StartInfo.FileName = "pawncc.exe";
            stCompile.StartInfo.Arguments = "\"" + path + "\"" + " -r -;+ -(+";
            stCompile.StartInfo.CreateNoWindow = true;
            stCompile.Start();
            while ((stCompile.HasExited == false))
            {
                rawOut = stCompile.StandardError.ReadToEnd();
                rawOut += stCompile.StandardOutput.ReadToEnd();
                if ((!string.IsNullOrEmpty(rawOut)))
                {
                }

                Application.DoEvents();
            }

            richTextBox1.Text = rawOut;

            if (rawOut == String.Empty || rawOut.Contains("warning") == true || rawOut.Contains("error") == true)
            {
                dynamic indivLine = rawOut.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
                for (int i = 0; i <= indivLine.Length; i++)
                {
                    if (indivLine[i] == String.Empty) break;
                    if (indivLine[i].Contains("Pawn compiler 3.2.3664")) continue;
                    indivLine[i] = indivLine[i].Replace(path, "");
                    string line = indivLine[i].Substring(indivLine[i].IndexOf('(') + 1, indivLine[i].IndexOf(')') - indivLine[i].IndexOf('(') - 1);
                    if (indivLine[i].Contains("warning"))
                    {
                        ListViewItem newListItem = new ListViewItem("", 0);
                        newListItem.SubItems.Add(indivLine[i].Replace("(" + line + ") :", "").Trim());
                        newListItem.SubItems.Add(line);
                        listView1.Items.Add(newListItem);
                    }
                    else if (indivLine[i].Contains("error"))
                    {
                        ListViewItem newListItem = new ListViewItem("", 1);
                        newListItem.SubItems.Add(indivLine[i].Replace("(" + line + ") :", "").Trim());
                        newListItem.SubItems.Add(line);
                        listView1.Items.Add(newListItem);
                    }
                }
            }
            this.Text = "Attempted to compile " + pathAesthetic + ".";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(richTextBox1.Text);
            MessageBox.Show("Copied to clipboard!");
        }


    }
}
