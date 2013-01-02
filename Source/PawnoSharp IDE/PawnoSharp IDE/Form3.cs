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
        private static System.Text.StringBuilder m_sbText;

        public compileForm(string path)
        {
            InitializeComponent();

            this._path = path;
            string pathAesthetic = Path.GetFileName(path);
            this.Text = "Compiling " + pathAesthetic + "...";

            Process myProcess = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo("pawncc.exe");
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.Arguments = path + " -r";
            myProcess.StartInfo = startInfo;
            myProcess.Start();

            m_sbText = new System.Text.StringBuilder(1000);

            myProcess.OutputDataReceived += ProcessDataHandler;
            myProcess.ErrorDataReceived += ProcessDataHandler;

            myProcess.Start();

            myProcess.BeginOutputReadLine();
            myProcess.BeginErrorReadLine();

            while (!myProcess.HasExited)
            {
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Application.DoEvents();
            }
            richTextBox1.Text = m_sbText.ToString();
            this.Text = "Compiled " + pathAesthetic + "!";
        }

        private static void ProcessDataHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            if (!String.IsNullOrEmpty(outLine.Data))
            {
                m_sbText.AppendLine(outLine.Data);
            }
        }
    }
}
