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

namespace PawnoSharp_IDE
{
    public partial class mainForm : Form
    {
        //Initialize form2 for use with the New button.
        newFileForm form2 = new newFileForm();
        string openFile = null;
        public mainForm(string path)
        {
            InitializeComponent();
        }


        /*
         * ----------------------
         * BEGIN DIALOG CALLBACKS
         * ----------------------
         */

        /// <summary>
        /// Calls when the user has finished selecting a file to save as.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Arguments</param>
        private void saveFileDialog1_FileOk_1(object sender, CancelEventArgs e)
        {
            string name = saveFileDialog1.FileName;
            if (!File.Exists(name))
            {
                statusLabel.Text = "File doesn't exist. Creating and saving...";

                StreamWriter sw = new StreamWriter(name);
                sw.Write(scintilla1.Text);
                sw.Flush();

                statusLabel.Text = "Saved new file.";
                openFile = name;
                this.Text = Path.GetFileName(openFile) + " - PawnoSharp";
                return;
            }
            else
            {
                StreamWriter sw = new StreamWriter(name);
                sw.Write(scintilla1.Text);
                sw.Flush();
                statusLabel.Text = "File saved and overwritten.";
                openFile = name;
                this.Text = Path.GetFileName(openFile) + " - PawnoSharp";
                return;
            }
        }

        /*
         * -------------------------
         * BEGIN TOOLSTRIP CALLBACKS
         * -------------------------
         */

        /// <summary>
        /// Called when the user clicks "Cut" in the ToolStrip.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Arguments</param>
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scintilla1.Clipboard.Cut();
        }


        /// <summary>
        /// Called when the user clicks "Copy" in the ToolStrip.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Arguments</param>
        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            scintilla1.Clipboard.Copy();
        }

        /// <summary>
        /// Called when the user clicks "Paste" in the ToolStrip.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Arguments</param>
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scintilla1.Clipboard.Paste();
        }

        /// <summary>
        /// Called when the user clicks "Save As" in the ToolStrip.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Arguments</param>
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusLabel.Text = "Saving file...";
            saveFileDialog1.ShowDialog();
        }

        /// <summary>
        /// Called when the user clicks "Exit" in the ToolStrip.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Arguments</param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Called when the user clicks "SA-MP Wiki" in the ToolStrip Menu.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Arguments</param>
        private void sAMPWikiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sampWikiURL = "http://wiki.sa-mp.com/";
            System.Diagnostics.Process.Start(sampWikiURL);
        }

        /// <summary>
        /// Called when the user clicks "Weedarr" in the ToolStrip Menu.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Arguments</param>
        private void weedarrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sampWikiURL = "http://weedarr.wikidot.com/";
            System.Diagnostics.Process.Start(sampWikiURL);
        }

        /// <summary>
        /// Called when the user clicks "About PawnoSharp" in the ToolStrip Menu.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Arguments</param>
        private void aboutPawnoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            aboutForm form4 = new aboutForm();
            form4.Show();
        }

        /// <summary>
        /// Called when the user clicks "Settings" in the ToolStrip Menu.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Arguments</param>
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            optionForm form5 = new optionForm();
            form5.Show();
        }

        /// <summary>
        /// Called when the user clicks "Font" in the ToolStrip Menu.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Arguments</param>
        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = fontDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                Font font = fontDialog1.Font;
                scintilla1.Font = font;
                fontDialog1.Font = font;
                Properties.Settings.Default.FontName = font.Name;
                Properties.Settings.Default.FontSize = font.Size;
            }
        }

        /// <summary>
        /// Called when the user clicks "Close" in the ToolStrip Menu.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Arguments</param>
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scintilla1.Text = "";
            openFile = null;
            this.Text = "PawnoSharp";
        }

        /*
         * ----------------------
         * BEGIN BUTTON CALLBACKS
         * ----------------------
         */

        /// <summary>
        /// Called when the user clicks the New File button.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Arguments</param>
        private void newFileButton_Click(object sender, EventArgs e)
        {
            newFileForm form2 = new newFileForm();
            DialogResult res = form2.ShowDialog();
            if (res == DialogResult.Yes)
            {
                try
                {
                    statusLabel.Text = "Creating new gamemode...";
                    scintilla1.Text = File.ReadAllText(@"../../newGM.pwn");
                    openFile = null;
                    this.Text = "PawnoSharp";
                    statusLabel.Text = "Created new gamemode.";
                }

                catch (IOException)
                {
                }
                return;
            }
            else if(res == DialogResult.No)
            {
                try
                {
                    statusLabel.Text = "Creating new filterscript...";
                    scintilla1.Text = File.ReadAllText(@"../../newFS.pwn");
                    openFile = null;
                    this.Text = "PawnoSharp";
                    statusLabel.Text = "Created new filterscript.";
                }
                
                catch (IOException)
                {
                }
                return;
            }
            else if (res == DialogResult.Cancel)
            {
                return;
            }
        }

        /// <summary>
        /// Called when the user clicks the "Open File" button.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Arguments</param>
        private void openFileButton_Click(object sender, EventArgs e)
        {
            statusLabel.Text = "Opening file...";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string file = openFileDialog1.FileName;
                try
                {
                    scintilla1.Text = File.ReadAllText(file);
                    openFile = file;
                    statusLabel.Text = "Opened file.";
                    this.Text = Path.GetFileName(openFile) + " - PawnoSharp";
                }
                catch (IOException)
                {
                }
            }
        }

        /// <summary>
        /// Called when the user clicks the "Save File" button.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Arguments</param>
        private void saveFileButton_Click(object sender, EventArgs e)
        {
            if (scintilla1.Text.Length <= 1) return;
            statusLabel.Text = "Saving file...";
            if (openFile == null)
            {
                saveFileDialog1.ShowDialog();
            }
            else
            {
                File.WriteAllText(openFile, scintilla1.Text);
                statusLabel.Text = "Saved file.";
            }
        }

        /// <summary>
        /// Called when the user clicks the "Compile" button.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Arguments</param>
        private void compileFileButton_Click(object sender, EventArgs e)
        {
            if (scintilla1.Text.Length <= 1) return;
            if (openFile == null)
            {
                statusLabel.Text = "Cannot compile. File must be saved.";
                saveFileDialog1.ShowDialog();
            }
            else
            {
                statusLabel.Text = "Compiling file...";
                compileForm form3 = new compileForm(openFile);
                form3.Show();
                form3.FormClosed += FormClosedEventHandler;
            }
        }

        /*
         * --------------------
         * BEGIN FORM CALLBACKS
         * --------------------
         */

        /// <summary>
        /// Called when the user closes Form3.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Arguments</param>
        void FormClosedEventHandler(object sender, FormClosedEventArgs e)
        {
            statusLabel.Text = "Compiling finished.";
        }


        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.FormSize = this.Size;
            Properties.Settings.Default.FormPos = this.Location;

            if (this.WindowState == FormWindowState.Maximized)
            {
                Properties.Settings.Default.FormMaximized = true;
            }
            else if (this.WindowState == FormWindowState.Normal)
            {
                Properties.Settings.Default.FormMaximized = false;
            }
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            this.Text = "PawnoSharp";
            //Create the ToolTip component not available in the designer.
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            //Create the tooltips for the bar buttons.
            toolTip1.SetToolTip(this.newFileButton, "New File...");
            toolTip1.SetToolTip(this.openFileButton, "Open File...");
            toolTip1.SetToolTip(this.saveFileButton, "Save File");
            toolTip1.SetToolTip(this.compileFileButton, "Compile");

            //Set the scintilla1 RichTextBox margin width to 50 to allow for large line numbers
            //to show properly. (Up to 100,000?)
            scintilla1.Margins[0].Width = 50;

            //Load the application settings!
            string fontName = Properties.Settings.Default.FontName;
            float fontSize = Properties.Settings.Default.FontSize;
            Size FormSize = Properties.Settings.Default.FormSize;
            Point FormPos = Properties.Settings.Default.FormPos;
            bool FormMaximized = Properties.Settings.Default.FormMaximized;
            bool FormFileTitle = Properties.Settings.Default.FileInTitle;

            //Apply the application settings!
            Font font = new Font(fontName, fontSize);
            scintilla1.Font = font;
            fontDialog1.Font = font;

            this.Location = FormPos;
            this.Size = FormSize;

            if (FormMaximized == true)
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

    }
}
