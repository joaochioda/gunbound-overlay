using System.Diagnostics;

namespace WinFormsApp1
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            var hotkey = new Hotkey(Keys.Control, () => {
                Debug.WriteLine("ctrl");
            });
            Application.Run(new Form1());
            Application.Run(hotkey);
            
        }
    }
}