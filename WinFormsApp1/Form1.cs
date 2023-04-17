
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System;
using System.Threading;
using System.Numerics;
using System.Drawing;

namespace WinFormsApp1
{
    public partial class Form1 : Form


    {
        RECT rect;
        public const string WINDOW_NAME = "GunBound";
        IntPtr handle = FindWindow(null, WINDOW_NAME);

        public struct RECT
        {
            public int left, top, right, bottom;
        }


        [DllImport("user32.dll")]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        [DllImport("kernel32.dll")]
        static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

      

        int mockWidth = 1200;
        int mockHeight = 900;
        int read,write;

        public Form1()
        {
            InitializeComponent();
        }

       

        private void Form1_Load(object sender, EventArgs e)
        {
         
            this.BackColor = Color.Wheat;
            this.TransparencyKey = Color.Wheat;
            this.TopMost = true;

           
            this.FormBorderStyle = FormBorderStyle.None;

            int initialStyle = GetWindowLong(this.Handle, -20);
            SetWindowLong(this.Handle, -20, initialStyle | 0x80000 | 0x20);

            GetWindowRect(handle, out rect);
            this.Size = new Size(rect.right - rect.left, rect.bottom - rect.top);
            //this.Size = new Size(mockWidth, mockHeight);

            this.Top = rect.top;
            this.Left = rect.left;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //g = e.Graphics;
            //g.DrawRectangle(myPen, 100, 100, 200,200);

            Pen pen = new Pen(Color.Red);

            // Define a fonte para os números
            Font font = new Font("Arial", 12);

            // Define a distância entre as marcas da régua
            int step = mockWidth/8;

            // Define o comprimento da régua
            int length = mockWidth;

            // Desenha as marcas da régua
     

            for (int i = 0; i <= length; i += step)
            {
                e.Graphics.DrawLine(pen, i, mockHeight -200, i, mockHeight + 10 -200);
                e.Graphics.DrawLine(pen, i - (step/2), mockHeight - 200, i - (step/2), mockHeight + 10 - 200);

                e.Graphics.DrawString((i/step).ToString(), font, Brushes.Red, i - 8, mockHeight - 190);


            }

            // Desenha

        }
    }
}