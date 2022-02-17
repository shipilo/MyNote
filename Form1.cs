using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyNote
{
    public partial class Form1 : Form
    {
        private bool saved;
        private bool opend;
        private string path;
        private bool ctrl;

        public Form1()
        {
            InitializeComponent();
            saved = true;
            opend = false;
            ctrl = false;
            path = "";
            rtbField.MouseWheel += RtbField_MouseWheel;
        }

        private void RtbField_MouseWheel(object sender, MouseEventArgs e)
        {
            if (ctrl)
            {
                if (e.Delta > 0)
                {
                    rtbField.ZoomFactor++;
                }
                else
                {
                    rtbField.ZoomFactor--;
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Control)
            {
                ctrl = true;
            }
            else
            {
                ctrl = false;
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void новоеОкноToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    StreamReader sr = new StreamReader(fileDialog.FileName);
                    rtbField.Text = sr.ReadToEnd();
                    sr.Close();
                    path = fileDialog.FileName;
                    opend = true;
                    saved = true;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "MyNote", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (opend)
            {
                if (File.Exists(path))
                {
                    StreamWriter sw = new StreamWriter(path);
                    sw.Write(rtbField.Text);
                    sw.Close();
                    saved = true;
                }
                else
                {
                    MessageBox.Show("Данный файл не может быть сохранён", "MyNote", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                    SaveFileDialog fileDialog = new SaveFileDialog();
                    fileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
                    if (fileDialog.ShowDialog() == DialogResult.OK)
                    {
                        StreamWriter sw = new StreamWriter(fileDialog.FileName);
                        sw.Write(rtbField.Text);
                        sw.Close();
                        path = fileDialog.FileName;
                        saved = true;
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message, "MyNote", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!saved)
            {
                string message = "Вы хотите сохранить изменения в файле " + path + " ?";
                if (path == "") message = "Вы хотите сохранить изменения в новом файле?";
                DialogResult dialogResult = MessageBox.Show(message, "MyNote", MessageBoxButtons.YesNoCancel);
                if (dialogResult == DialogResult.Yes)
                {
                    сохранитьToolStripMenuItem_Click(null, null);
                }
                else if (dialogResult == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void rtbField_TextChanged(object sender, EventArgs e)
        {
            saved = false;
        }

        private void выделитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cg = new ColorDialog();
            if (cg.ShowDialog() == DialogResult.OK)
            {
                rtbField.SelectionColor = cg.Color;
            }
        }

        private void rtbField_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (rtbField.SelectedText == "")
                {
                    cmsRightClick.Items[0].Enabled = false;
                }
                else
                {
                    cmsRightClick.Items[0].Enabled = true;
                }
                cmsRightClick.Show(new Point(Cursor.Position.X, Cursor.Position.Y));
            }
        }

        private void шрифтToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                rtbField.SelectionFont = fd.Font;
            }
        }

        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbField.Cut();
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbField.Copy();
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbField.Paste();
        }

        private void текстToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                rtbField.Font = fd.Font;
            }
        }

        private void фонToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cg = new ColorDialog();
            if (cg.ShowDialog() == DialogResult.OK)
            {
                rtbField.BackColor = cg.Color;
                BackColor = cg.Color;
            }
        }

        private void цветТекстаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cg = new ColorDialog();
            if (cg.ShowDialog() == DialogResult.OK)
            {
                rtbField.ForeColor = cg.Color;
            }
        }

        private void горизонтальнаяПрокруткаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            горизонтальнаяПрокруткаToolStripMenuItem.Checked = !Convert.ToBoolean(горизонтальнаяПрокруткаToolStripMenuItem.CheckState);
            if (горизонтальнаяПрокруткаToolStripMenuItem.CheckState == CheckState.Checked)
            {
                rtbField.WordWrap = false;
            }
            else
            {
                rtbField.WordWrap = true;
            }
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Location = this.Location;
            form2.Show();
        }

        private void увеличитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbField.ZoomFactor++;
        }

        private void уменьшитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbField.ZoomFactor--;
        }

        private void жирныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!rtbField.SelectionFont.Bold)
            {
                rtbField.SelectionFont = new Font(rtbField.SelectionFont, FontStyle.Bold);
            }
            else
            {
                rtbField.SelectionFont = new Font(rtbField.SelectionFont, FontStyle.Regular);
            }
        }
        private void курсивToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!rtbField.SelectionFont.Italic)
            {
                rtbField.SelectionFont = new Font(rtbField.SelectionFont, FontStyle.Italic);
            }
            else
            {
                rtbField.SelectionFont = new Font(rtbField.SelectionFont, FontStyle.Regular);
            }
        }

        private void подчеркнутыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!rtbField.SelectionFont.Underline)
            {
                rtbField.SelectionFont = new Font(rtbField.SelectionFont, FontStyle.Underline);
            }
            else
            {
                rtbField.SelectionFont = new Font(rtbField.SelectionFont, FontStyle.Regular);
            }
        }        
    }
}
