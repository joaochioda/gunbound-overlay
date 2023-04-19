using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace WinFormsApp1
{
    public partial class Hotkey : Form
    {
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private const int WM_HOTKEY = 0x0312;

        private int keyId;
        private Action callback;


        public Hotkey(Keys key, Action callback)
        {
            this.callback = callback;
            this.keyId = this.GetHashCode();

            uint modifiers = 0;
            if ((key & Keys.Alt) == Keys.Alt)
                modifiers |= 0x0001; // MOD_ALT
            if ((key & Keys.Control) == Keys.Control)
                modifiers |= 0x0002; // MOD_CONTROL
            if ((key & Keys.Shift) == Keys.Shift)
                modifiers |= 0x0004; // MOD_SHIFT

            if (!RegisterHotKey(this.Handle, this.keyId, modifiers, (uint)(key & Keys.KeyCode)))
                throw new InvalidOperationException("Couldn't register the hotkey.");
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_HOTKEY && m.WParam.ToInt32() == this.keyId)
                callback?.Invoke();
        }

        protected override void Dispose(bool disposing)
        {
            UnregisterHotKey(this.Handle, this.keyId);

            base.Dispose(disposing);
        }

    }
}