
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
        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_TRANSPARENT = 0x00000020;

        RECT rect;
        public const string WINDOW_NAME = "GunBound";
        IntPtr handle = FindWindow(null, WINDOW_NAME);

        public struct RECT
        {
            public int left, top, right, bottom;
        }



        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        [DllImport("kernel32.dll")]
        static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        int mockWidth = 1200;
        int mockHeight = 900;
        int read, write;

        private Point squareLocation;
        private Point squareLocation2;

        public Form1()
        {
            var hotkey = new Hotkey(Keys.F9, () => {
                // Set the form click-through
                int exStyle = GetWindowLong(this.Handle, GWL_EXSTYLE);
                SetWindowLong(this.Handle, GWL_EXSTYLE, exStyle | WS_EX_TRANSPARENT);
            });

            var hotkey1 = new Hotkey(Keys.F10, () => {
                // Set the form click-through
                int exStyle = GetWindowLong(this.Handle, GWL_EXSTYLE);
                SetWindowLong(this.Handle, GWL_EXSTYLE, exStyle & ~WS_EX_TRANSPARENT);
            });

            InitializeComponent();
        }

        private double calc(double x1,double x2)
        {
            double distance = 0;
            if (x1 > x2)
            {
                distance = x1 - x2;
            } else
            {
                distance = x2 - x1;

            }
            double GRAVITY = 94; // Aceleração gravitacional em m/s²
            double DISTANCIA = distance / 1.5; // Distância em metros
            double SPEED = 240; // Velocidade em m/s
            double ANGULO = 50; // Ângulo em graus

            double radian = ANGULO * Math.PI / 180.0; // Converter o ângulo para radianos
            double result = 4 * Math.Sqrt((GRAVITY * DISTANCIA * 1.00) / (4 * Math.Pow(SPEED, 2) * Math.Sin(2 * radian)));

            return result;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.Wheat;
            this.TransparencyKey = Color.Wheat;
            this.TopMost = true;


            this.FormBorderStyle = FormBorderStyle.None;

            GetWindowRect(handle, out rect);
            this.Size = new Size(rect.right - rect.left, rect.bottom - rect.top);
            //this.Size = new Size(mockWidth, mockHeight);

            this.Top = rect.top;
            this.Left = rect.left;

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                squareLocation = e.Location;

            }
            if (e.Button == MouseButtons.Right)
            {
                squareLocation2 = e.Location;

            }
            if (squareLocation2.X > squareLocation.X)
            {
                Debug.WriteLine(squareLocation2.X - squareLocation.X);

            } else
            {
                Debug.WriteLine(squareLocation.X - squareLocation2.X);
            }
            this.Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            e.Graphics.DrawRectangle(Pens.Blue, new Rectangle(squareLocation.X - 5, squareLocation.Y - 5, 10, 10));
            e.Graphics.DrawRectangle(Pens.Red, new Rectangle(squareLocation2.X - 5, squareLocation2.Y - 5, 10, 10));


            Pen pen = new Pen(Color.Red);

            // Define a fonte para os números
            Font font = new Font("Arial", 16);

            double power = calc(squareLocation.X, squareLocation2.X) ;

            e.Graphics.DrawLine(pen, (int)((power * 150.25) + 364), 903, (int)((power * 150.25) + 364), 870);
            e.Graphics.DrawLine(pen, (int)((power * 150.25) + 364), 903, (int)((power * 150.25) + 364), 870);
        }
    }
}