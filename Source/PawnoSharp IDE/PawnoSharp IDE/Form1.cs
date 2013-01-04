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
using ScintillaNET;

namespace PawnoSharp_IDE
{
    public partial class mainForm : Form
    {
        //Initialize form2 for use with the New button.
        newFileForm form2 = new newFileForm();
        string openFile = null;
        string settingsFile = System.IO.Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);

        public mainForm()
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
            string sampWeedURL = "http://weedarr.wikidot.com/";
            System.Diagnostics.Process.Start(sampWeedURL);
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
                SetFont(font);
                fontDialog1.Font = font;
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
                    scintilla1.Text = File.ReadAllText(@"newGM.pwn");
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
                    scintilla1.Text = File.ReadAllText(@"newFS.pwn");
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
            int FormMaximizedI = 0;
            Size formSize = new Size();
            Point formPos = new Point();
            string fontSize = fontDialog1.Font.Size.ToString();
            string fontName = fontDialog1.Font.Name;
            
            //Prepare and convert variables to strings for writing.
            formSize.Height = this.Size.Height;
            formSize.Width = this.Size.Width;

            formPos.X = this.Location.X;
            formPos.Y = this.Location.Y;

            IniFile fsINI = new IniFile();

            Uri uri = new Uri(settingsFile);


            //System.IO.Path.GetDirectoryName(
                //System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)

            switch(this.WindowState)
            {
                case FormWindowState.Maximized:
                    FormMaximizedI = 1;
                    break;
                case FormWindowState.Normal:
                    FormMaximizedI = 0;
                    break;
                default: break;
            }

            //Write the variables to Settings.ini
            fsINI.SetKeyValue("User", "FontSize", fontSize);
            fsINI.SetKeyValue("User", "FontName", fontName);

            fsINI.SetKeyValue("Application", "FormH", formSize.Height.ToString());
            fsINI.SetKeyValue("Application", "FormW", formSize.Width.ToString());

            fsINI.SetKeyValue("Application", "FormX", formPos.X.ToString());
            fsINI.SetKeyValue("Application", "FormY", formPos.Y.ToString());

            fsINI.SetKeyValue("Application", "Maximized", FormMaximizedI.ToString());

            fsINI.AddSection("User").AddKey("FileInTitle").Value = "1";

            fsINI.Save(uri.LocalPath + "\\Settings.ini");
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
            IniFile fsINI = new IniFile();
            
            Uri uri = new Uri(settingsFile);
            fsINI.Load(uri.LocalPath + "\\Settings.ini");

            string fontName = fsINI.GetKeyValue("User", "FontName");
            string fontSize = fsINI.GetKeyValue("User", "FontSize");
            float fontSizeF = 0F;

            string FormW = fsINI.GetKeyValue("Application", "FormW");
            string FormH = fsINI.GetKeyValue("Application", "FormH");
            int FormWI = Convert.ToInt32(FormW);
            int FormHI = Convert.ToInt32(FormH);

            string FormX = fsINI.GetKeyValue("Application", "FormX");
            string FormY = fsINI.GetKeyValue("Application", "FormY");
            int FormXI = Convert.ToInt32(FormX);
            int FormYI = Convert.ToInt32(FormY);

            string FormMaximized = fsINI.GetKeyValue("Application", "Maximized");
            int FormMaximizedI = Convert.ToInt16(FormMaximized);

            string FormFileTitle = fsINI.GetKeyValue("User", "FileInTitle");
            int FormFileTitleI = Convert.ToInt16(FormFileTitle);

            float.TryParse(fontSize, out fontSizeF);

            Point FormPos = new Point();
            FormPos.X = FormXI;
            FormPos.Y = FormYI;

            Size FormSize = new Size();
            FormSize.Width = FormWI;
            FormSize.Height = FormHI;


            //Apply the application settings!
            Font font = new Font(fontName, fontSizeF);
            SetFont(font);
            fontDialog1.Font = font;

            this.Location = FormPos;
            this.Size = FormSize;

            if (FormMaximizedI == 1)
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }
        public void SetFont(Font xFont)
        {
            scintilla1.Font = xFont;

            scintilla1.Styles.Default.Font = xFont;

            scintilla1.Styles[0].Font = xFont;
            scintilla1.Styles[1].Font = xFont;
            scintilla1.Styles[2].Font = xFont;
            scintilla1.Styles[3].Font = xFont;
            scintilla1.Styles[4].Font = xFont;
            scintilla1.Styles[5].Font = xFont;
            scintilla1.Styles[6].Font = xFont;
            scintilla1.Styles[7].Font = xFont;
            scintilla1.Styles[8].Font = xFont;
            scintilla1.Styles[9].Font = xFont;
            scintilla1.Styles[10].Font = xFont;
            scintilla1.Styles[11].Font = xFont;
            scintilla1.Styles[12].Font = xFont;
            scintilla1.Styles[14].Font = xFont;
            scintilla1.Styles[15].Font = xFont;
            scintilla1.Styles[16].Font = xFont;
            scintilla1.Styles[19].Font = xFont;
            scintilla1.Styles[32].Font = xFont;

            scintilla1.Styles[ScintillaNET.StylesCommon.LineNumber].Font = xFont;
            scintilla1.Styles[ScintillaNET.StylesCommon.BraceBad].Font = xFont;
            scintilla1.Styles[ScintillaNET.StylesCommon.BraceLight].Font = xFont;
            scintilla1.Styles[ScintillaNET.StylesCommon.CallTip].Font = xFont;
            scintilla1.Styles[ScintillaNET.StylesCommon.ControlChar].Font = xFont;
            scintilla1.Styles[ScintillaNET.StylesCommon.Default].Font = xFont;
            scintilla1.Styles[ScintillaNET.StylesCommon.IndentGuide].Font = xFont;
            scintilla1.Styles[ScintillaNET.StylesCommon.LastPredefined].Font = xFont;
            scintilla1.Styles[ScintillaNET.StylesCommon.Max].Font = xFont;

        }

    }
}
