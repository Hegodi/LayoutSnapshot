using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace LayoutSnapshot
{
    class LayoutSnapshot
    {
        private static readonly string FileNameProfiles = ".profiles.txt";
        public string name;
        public List<WindowSnapshot> windowSnapshots;
        public List<Rectangle> displays;
        public LayoutSnapshot()
        {
            name = "New Layout";
            windowSnapshots = new List<WindowSnapshot>();
            displays = new List<Rectangle>();
        }
        public void Apply()
        {
            Process[] processlist = Process.GetProcesses();
            List<WindowSnapshot> windowsSnapshotUnused = new List<WindowSnapshot>(windowSnapshots);

            foreach (Process process in processlist)
            {
                for (int i=0; i< windowsSnapshotUnused.Count; i++)
                {
                    if (windowsSnapshotUnused[i].ProcessName == process.ProcessName)
                    {
                        WindowSnapshot procWin = new WindowSnapshot(process);
                        if (procWin.HasWindow)
                        {
                            windowsSnapshotUnused[i].Apply(process);
                            windowsSnapshotUnused.RemoveAt(i);
                            break;
                        }
                    }
                }
            }

        }

        public void Write(StreamWriter file)
        {
            file.WriteLine(name);
            file.WriteLine(windowSnapshots.Count.ToString());
            foreach (WindowSnapshot window in windowSnapshots)
            {
                window.Write(file);
            }
            file.WriteLine(displays.Count.ToString());
            foreach(Rectangle display in displays)
            {
                file.WriteLine(display.X.ToString() + "," + display.Y.ToString() + "," + display.Width.ToString() + "," + display.Height.ToString());
            }
        }

        public void Read(StreamReader file)
        {
            windowSnapshots.Clear();
            displays.Clear();

            name = file.ReadLine();
            int numberWindows = int.Parse(file.ReadLine());
            for (int i=0; i<numberWindows; i++)
            {
                WindowSnapshot window = new WindowSnapshot();
                window.Load(file.ReadLine());
                windowSnapshots.Add(window);
            }
            int numberDisplays = int.Parse(file.ReadLine());
            for (int i = 0; i < numberDisplays; i++)
            {
                string line = file.ReadLine();
                string[] data = line.Split(',');
                Rectangle display = new Rectangle(int.Parse(data[0]), int.Parse(data[1]), int.Parse(data[2]), int.Parse(data[3]));
                displays.Add(display);
            }
        }
        public static bool SaveData(List<LayoutSnapshot> listLayoutSnapshots)
        {
            try
            {
                StreamWriter file = new StreamWriter(FileNameProfiles);
                file.WriteLine(listLayoutSnapshots.Count.ToString());
                foreach (LayoutSnapshot layoutSnapshot in listLayoutSnapshots)
                {
                    layoutSnapshot.Write(file);
                }
                file.Close();

            }
            catch(Exception e)
            {
                MessageBox.Show("Error: cannot write the profile file (" + FileNameProfiles + ")", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        public static List<LayoutSnapshot> LoadData()
        {
            List<LayoutSnapshot> listLayoutSnapshots = new List<LayoutSnapshot>();
            try
            {
                StreamReader file = new StreamReader(FileNameProfiles);
                int numberProfiles = int.Parse(file.ReadLine());
                for(int i=0; i<numberProfiles; i++)
                {
                    LayoutSnapshot layoutSnapshot = new LayoutSnapshot();
                    layoutSnapshot.Read(file);
                    listLayoutSnapshots.Add(layoutSnapshot);
                }
                file.Close();
            }
            catch(Exception e)
            {
                MessageBox.Show("Error: cannot read the profile file (" + FileNameProfiles + ")", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return listLayoutSnapshots;
        }

    }

}
