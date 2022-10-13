using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Drawing;
using System.IO;

namespace LayoutSnapshot
{
    class WindowSnapshot
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }

        [DllImport("user32.dll")]
        static extern bool GetWindowRect(IntPtr hwnd, out RECT rectangle);
        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        private static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool IsZoomed(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool ShowWindow(IntPtr hWnd, int cmdshow);

        public WindowSnapshot(Process process)
        {
            RECT rect;
            GetWindowRect(process.MainWindowHandle, out rect);
            processName = process.ProcessName;
            executable = process.MainModule.FileName;
            x = rect.Left;
            y = rect.Top;
            width = rect.Right - rect.Left;
            height = rect.Bottom - rect.Top;
            isRelevant = width > 1 && height > 1 && !IsIconic(process.MainWindowHandle);
        }

        public static bool IsWindowValid(IntPtr handle)
        {
            RECT rect;
            GetWindowRect(handle, out rect);
            return rect.Right - rect.Left > 1 && rect.Bottom - rect.Top > 1;
        }

        public WindowSnapshot()
        {
            x = 0;
            y = 0;
            width = 0;
            height = 0;
            isRelevant = false;
            processName = "";
            executable = "";
        }
        private bool isRelevant;
        private string processName;
        private string executable;
        private int x;
        private int y;
        private int width;
        private int height;

        public int X { get { return x; } }
        public int Y { get { return y; } }
        public int Width { get { return width; } }
        public int Height { get { return height; } }
        public string ProcessName { get { return processName; } }
        public Rectangle Bounds { get { return new Rectangle(x, y, width, height); } }
        public string Executable { get { return executable; } }
        public bool HasWindow { get { return width > 1 && height > 1; } }

        public string GetText()
        {
            string[] chunks = executable.Split('\\');
            return processName + " (" + chunks[chunks.Length - 1] + ")";
        }

        public bool IsRelevant { get { return isRelevant; } }
        public void Write(StreamWriter file)
        {
            string data = processName + "," + executable + "," + x.ToString() + "," + y.ToString() + "," + width + "," + height;
            file.WriteLine(data);
        }
        public bool Load(string dataLine)
        {
            string[] data = dataLine.Split(',');
            try
            {
                processName = data[0];
                executable = data[1];
                x = int.Parse(data[2]);
                y = int.Parse(data[3]);
                width = int.Parse(data[4]);
                height = int.Parse(data[5]);
                // If the window has been saved it is because it was relevant
                isRelevant = true;
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        public void Apply(Process process)
        {
            int flag = 0x4000;
            SetWindowPos(process.MainWindowHandle, 0 /*HWND_TOP*/, x, y, width, height, flag);
        }

    }
}
