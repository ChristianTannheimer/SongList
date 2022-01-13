﻿using System;
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
        public Form1()
        {
            InitializeComponent();
        }



        private void txt1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (txt1.SelectionLength == 0)
                {
                    txt1.SelectAll();
                }
                else
                {
                    txt1.SelectionLength = 0;
                }

                string pathString = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SongList.txt");
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
    }
}
