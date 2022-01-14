using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace SongList
{

    public partial class Form1 : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        public string pathString = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SongList.txt");
        public Form1()
        {
            InitializeComponent();
            if (!File.Exists(pathString))
            {
                StreamWriter sw = new StreamWriter(pathString);
                sw.WriteLine("Ein Song in jede Zeile,");
                sw.WriteLine("die Nummer vom Namen mit einem Leerzeichen getrennt,");
                sw.WriteLine("das war's!");
                sw.WriteLine();
                sw.WriteLine("Hier ein Beispiel:");
                sw.WriteLine("25 Frau Meier hat gelbe Unterhosen an");
                sw.Close();


            }
        }
        private void txt1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (txt1.SelectionLength == 0) { txt1.SelectAll(); }
                else { txt1.SelectionLength = 0; }

                string[] songs = File.ReadLines(pathString).ToArray();
                string nummer = txt1.Text;

                foreach (string song in songs)
                {
                    if (song.StartsWith(nummer))
                    {
                        int indexOfSpace = song.IndexOf(' ') + 1;
                        string ohneZahl = song.Substring(indexOfSpace, song.Length - indexOfSpace);
                        txt1.Text = ohneZahl;
                    }
                }
            }
        }

        private void txt1_MouseEnter(object sender, EventArgs e)
        {
            txt1.SelectAll();
        }
        private void txt1_MouseLeave(object sender, EventArgs e)
        {
            txt1.SelectionLength = 0;
        }
        private void txt1_MouseClick(object sender, MouseEventArgs e)
        {
            txt1.SelectAll();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }
        private void lblExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void lblÖffnen_Click(object sender, EventArgs e)
        {
            Process.Start("explorer", pathString);
        }
        private void lblMinimized_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
        }
    }
}
