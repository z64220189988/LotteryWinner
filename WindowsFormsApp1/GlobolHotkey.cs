using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;
using System.Diagnostics;

namespace WindowsFormsApp1
{
    public class GlobalHotkey : IDisposable
    {
        private int modifier;
        private int key;
        private IntPtr hWnd;
        private int id;

        public GlobalHotkey(Keys key, int modifier, Form form)
        {
            this.key = (int)key;
            this.modifier = modifier;
            this.hWnd = form.Handle;
            this.id = this.GetHashCode();
            RegisterHotKey();
            Application.AddMessageFilter(new MessageFilter(this));
        }

        public void RegisterHotKey()
        {
            UnregisterHotKey(hWnd, id);
            RegisterHotKey(hWnd, id, modifier, key);
        }

        public void Dispose()
        {
            Application.RemoveMessageFilter(new MessageFilter(this));
            UnregisterHotKey(hWnd, id);
        }

        private class MessageFilter : IMessageFilter
        {
            private GlobalHotkey hotkey;

            public MessageFilter(GlobalHotkey hotkey)
            {
                this.hotkey = hotkey;
            }

            public bool PreFilterMessage(ref Message m)
            {
                if (m.Msg == 0x0312 && m.WParam.ToInt32() == hotkey.id)
                {
                    // 快捷键按下，执行相应操作，例如清空ListBox
                    MessageBox.Show("已触发全局快捷键操作！");
                    return true;
                }
                return false;
            }
        }

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
    }
   
}
