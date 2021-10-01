using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using KControl.Data;
using KControl.KTabControl;

namespace KNotpad
{
    public partial class Form1 : Form
    {
        public bool Saved = false;
        private KTabcontrol Tabcon = new KTabcontrol();
        public Form1(string path)
        {
            InitializeComponent();
            if (path != string.Empty)
            {
                try
                {
                    if (path.Contains(".SData"))
                    {
                        TextEditor.Text = KData.LoadData(path);
                    }
                    else
                    {
                        TextEditor.Text = KData.LoadDataWithotBinary(path);
                    }
                }
                catch (Exception Error)
                {
                    MessageBox.Show(Error.ToString());
                }
            }
            Tabcon.Add(this, panel1);
            foreach (Control item in panel1.Controls)
            {
                if (item.GetType() != new Button().GetType())
                {
                    Tabcon.Add(this, item);
                }
            }
        }

        private void New_Click(object sender, EventArgs e)
        {
            TextEditor.Text = string.Empty;
            TextEditor.ForeColor = Color.White;
            if (!Saved)
            {
                if (Savembox("Your file is not save yet.\nDo you want to save it?", "Warning") == true)
                {
                    Save_Click(new object(), new EventArgs());
                }
                Saved = true;
            }
        }

        private void ChangeColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ColorDialog C = new ColorDialog())
            {
                if (C.ShowDialog() == DialogResult.OK)
                {
                    TextEditor.SelectionColor = C.Color;
                }
            }
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TextEditor.SelectedText = string.Empty;
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FontDialog Fdg = new FontDialog())
            {
                if (Fdg.ShowDialog() == DialogResult.OK)
                {
                    TextEditor.SelectionFont = Fdg.Font;
                }
            }
        }

        private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(TextEditor.SelectedText);
            TextEditor.SelectedText = string.Empty;
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(TextEditor.SelectedText);
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TextEditor.Paste();
        }

        private void Open_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog Ofd = new OpenFileDialog())
            {
                Ofd.Filter = "Text File(.txt)|*.txt|Text File(.Ktxt)|*.Ktxt|All file|*.*";
                Ofd.Title = "Open TXT File";
                if (Ofd.ShowDialog() == DialogResult.OK)
                {
                    StreamReader Txtfile = new StreamReader(Ofd.FileName);
                    TextEditor.Text = Txtfile.ReadToEnd();
                    Txtfile.Close();
                }
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog Ofd = new SaveFileDialog())
            {
                Ofd.Filter = "Text File(.txt)|*.txt|Text File(.Ktxt)|*.Ktxt|All file|*.*";
                Ofd.Title = "Save TXT File";
                if (Ofd.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter Txtfile = new StreamWriter(Ofd.FileName);
                    Txtfile.WriteLine(TextEditor.Text);
                    Txtfile.Close();
                }
                Saved = true;
            }
        }

        private void fontToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (FontDialog Fdg = new FontDialog())
            {
                if (Fdg.ShowDialog() == DialogResult.OK)
                {
                    TextEditor.Font = Fdg.Font;
                }
            }
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            TextEditor.Text = string.Empty;
        }

        private void colorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (ColorDialog C = new ColorDialog())
            {
                if (C.ShowDialog() == DialogResult.OK)
                {
                    TextEditor.ForeColor = C.Color;
                }
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextEditor.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextEditor.Redo();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.S))
            {
                Save_Click(new object(), new EventArgs());
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_NCCALCSIZE = 0x0083;
            if ((m.Msg == WM_NCCALCSIZE) && (m.WParam.ToInt32() == 1))
            {
                return;
            }
            base.WndProc(ref m);
        }

        public bool Savembox(string Text,string Caption)
        {
            if (MessageBox.Show(Text, Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification) == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void TextEditor_TextChanged(object sender, EventArgs e)
        {
            if (Saved)
                Saved = false;
            return;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!Saved)
            {
                if (Savembox("Your file is not save yet.\nDo you want to save it?", "Warning") == true)
                {
                    Save_Click(new object(), new EventArgs());
                }
            }
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
                this.WindowState = FormWindowState.Maximized;
            else
                this.WindowState = FormWindowState.Normal;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
