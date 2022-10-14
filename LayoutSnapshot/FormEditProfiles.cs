using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;

namespace LayoutSnapshot
{
    public partial class FormEditProfiles : Form
    {
        List<LayoutSnapshot> mLayoutSnapshots;

        delegate bool EnumThreadDelegate(IntPtr hWnd, IntPtr lParam);
        [DllImport("user32.dll")]
        static extern bool EnumThreadWindows(int dwThreadId, EnumThreadDelegate lpfn, IntPtr lParam);

        int mXmin = 0;
        int mYmin = 0;
        int mXmax = 0;
        int mYmax = 0;
        float mScale = 1.0f;
        int mX0 = 0;
        int mY0 = 0;
        bool mIsDirty = false;

        static IEnumerable<IntPtr> EnumerateProcessWindowHandles(int processId)
        {
            var handles = new List<IntPtr>();

            foreach (ProcessThread thread in Process.GetProcessById(processId).Threads)
                EnumThreadWindows(thread.Id,
                    (hWnd, lParam) => { handles.Add(hWnd); return true; }, IntPtr.Zero);

            return handles;
        }

        LayoutSnapshot mLayoutSelected = null;

        public FormEditProfiles()
        {
            InitializeComponent();
            mLayoutSnapshots = new List<LayoutSnapshot>();
            Reset();
        }

        private void Reset()
        {
            mLayoutSelected = null;
            mLayoutSnapshots.Clear();
            buttonSnapshot.Enabled = false;
            buttonSave.Enabled = false;
            buttonDeleteProfile.Enabled = false;
            mLayoutSnapshots = LayoutSnapshot.LoadData();
            if (mLayoutSnapshots.Count == 0)
            {
                CreateNewLayoutSnapshot();
            }
            UpdateFromLoadedData();
            if (mLayoutSnapshots.Count > 0)
            {
                SelectLayoutSnapshot(0);
                comboBoxProfiles.SelectedIndex = 0;
            }
            FillListOfWindowsFromData();
            CalculateDrawingScale();
            labelInfo.Text = "";
            SetDirty(false);

        }

        private void SetDirty(bool value)
        {
            mIsDirty = value;
            buttonSave.Enabled = value;
            buttonDiscardChanges.Enabled = value;
        }

        private void buttonNewProfile_Click(object sender, EventArgs e)
        {
            CreateNewLayoutSnapshot();
        }

        private void TakeSnapshot()
        {
            if (mLayoutSelected == null)
            {
                return;
            }

            Process[] processlist = Process.GetProcesses();
            mLayoutSelected.windowSnapshots.Clear();

            foreach (Process process in processlist)
            {
                if (process.Id == Process.GetCurrentProcess().Id)
                {
                    continue;
                }


                if (!String.IsNullOrEmpty(process.MainWindowTitle))
                {
                    WindowSnapshot snapshot = new WindowSnapshot(process);
                    if (snapshot.IsRelevant)
                    {
                        mLayoutSelected.windowSnapshots.Add(snapshot);
                    }
                }
            }

            mLayoutSelected.displays.Clear();
            CalculateDrawingScale();
            FillListOfWindowsFromData();
            panelLayout.Refresh();
            SetDirty(true);
        }

        void CalculateDrawingScale()
        {
            mXmin = int.MaxValue;
            mYmin = int.MaxValue;
            mXmax = int.MinValue;
            mYmax = int.MinValue;
            foreach (Screen screen in Screen.AllScreens)
            {
                mLayoutSelected.displays.Add(screen.Bounds);
                if (screen.Bounds.Left < mXmin) mXmin = screen.Bounds.Left;
                if (screen.Bounds.Top < mYmin) mYmin = screen.Bounds.Top;
                if (screen.Bounds.Right > mXmax) mXmax = screen.Bounds.Right;
                if (screen.Bounds.Bottom > mYmax) mYmax = screen.Bounds.Bottom;
            }

            const int margin = 10;
            float width = mXmax - mXmin;
            float height = mYmax - mYmin;
            float scaleX = (panelLayout.Width-2*margin) / width;
            float scaleY = (panelLayout.Height-2*margin) / height;
            mScale = scaleX < scaleY ? scaleX : scaleY;
            mX0 = (int)(0.5f * (panelLayout.Width - width * mScale));
            mY0 = (int)(0.5f * (panelLayout.Height - height * mScale));
        }

        void FillListOfWindowsFromData()
        {
            if (mLayoutSelected == null)
            {
                return;
            }

            listBoxWindows.Items.Clear();
            foreach (WindowSnapshot windowSnapshot in mLayoutSelected.windowSnapshots)
            {
                listBoxWindows.Items.Add(windowSnapshot.GetText());
            }
        }

        private void buttonSnapshot_Click(object sender, EventArgs e)
        {
            TakeSnapshot();
            SetDirty(true);
        }

        Rectangle ScaleRectangle(Rectangle rect)
        {
            Rectangle rectScaled = new Rectangle();
            rectScaled.X = (int)((rect.X - mXmin) * mScale + mX0);
            rectScaled.Y = (int)((rect.Y - mYmin) * mScale + mY0);
            rectScaled.Width = (int)(mScale * rect.Width);
            rectScaled.Height = (int)(mScale * rect.Height);
            return rectScaled;
        }

        private void panelLayout_Paint(object sender, PaintEventArgs e)
        {
            if (mLayoutSelected == null)
            {
                return;
            }

            Graphics g = e.Graphics;
            Pen penDisplay = new Pen(Color.Black, 4);
            Brush brushDisplay = new SolidBrush(Color.Gray);
            Brush brushWindowSelected = new SolidBrush(Color.LightBlue);

            foreach (Rectangle rect in mLayoutSelected.displays)
            {
                Rectangle rectScaled = ScaleRectangle(rect);
                g.FillRectangle(brushDisplay, rectScaled);
                g.DrawRectangle(penDisplay, rectScaled);
            }

            Pen penWindow = new Pen(Color.Blue, 2);
            // Draw first background and then frame, so windows in the background are visible
            for (int i = 0; i < listBoxWindows.Items.Count; i++)
            {
                Rectangle rect = ScaleRectangle(mLayoutSelected.windowSnapshots[i].Bounds);
                g.FillRectangle(brushWindowSelected, rect);
            }

            for (int i=0; i<listBoxWindows.Items.Count; i++)
            {
                Rectangle rect = ScaleRectangle(mLayoutSelected.windowSnapshots[i].Bounds);
                g.DrawRectangle(penWindow, rect);
            }

            Pen penWindowSelected = new Pen(Color.Red, 3);
            if (listBoxWindows.SelectedIndex >= 0 && listBoxWindows.SelectedIndex < listBoxWindows.Items.Count)
            {
                Rectangle rect = ScaleRectangle(mLayoutSelected.windowSnapshots[listBoxWindows.SelectedIndex].Bounds);
                g.DrawRectangle(penWindowSelected, rect);
            }
        }

        private void buttonRemoveWindow_Click(object sender, EventArgs e)
        {
            if (mLayoutSelected == null)
            {
                return;
            }

            if (listBoxWindows.SelectedIndex < 0 || listBoxWindows.SelectedIndex >= mLayoutSelected.windowSnapshots.Count)
            {
                return;
            }

            int indSel = listBoxWindows.SelectedIndex;
            mLayoutSelected.windowSnapshots.RemoveAt(indSel);

            FillListOfWindowsFromData();
            indSel--;
            if (indSel >= 0 && indSel < listBoxWindows.Items.Count)
            {
                listBoxWindows.SelectedIndex = indSel;
            }
            panelLayout.Refresh();
            SetDirty(true);
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            if (mLayoutSelected == null)
            {
                return;
            }

            mLayoutSelected.Apply();
        }

        private void buttonDeleteProfile_Click(object sender, EventArgs e)
        {
            if (mLayoutSelected == null)
            {
                return;
            }

            mLayoutSnapshots.RemoveAt(comboBoxProfiles.SelectedIndex);
            comboBoxProfiles.Items.RemoveAt(comboBoxProfiles.SelectedIndex);
            SelectLayoutSnapshot(comboBoxProfiles.SelectedIndex);
            SetDirty(true);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (mLayoutSelected != null)
            {
                if (!string.IsNullOrWhiteSpace(textBoxRename.Text))
                {
                    mLayoutSelected.name = textBoxRename.Text;
                }
            }

            int indSel = comboBoxProfiles.SelectedIndex;
            UpdateFromLoadedData();
            comboBoxProfiles.SelectedIndex = indSel;
            LayoutSnapshot.SaveData(mLayoutSnapshots);
            SetDirty(false);
        }
        private void CreateNewLayoutSnapshot()
        {
            LayoutSnapshot layoutSnapshot = new LayoutSnapshot();
            mLayoutSnapshots.Add(layoutSnapshot);
            SelectLayoutSnapshot(mLayoutSnapshots.Count - 1);
            comboBoxProfiles.Items.Add(layoutSnapshot.name);
            comboBoxProfiles.SelectedIndex = mLayoutSnapshots.Count-1;
            buttonSnapshot.Enabled = true;
            SetDirty(true);
            TakeSnapshot();
        }

        private void SelectLayoutSnapshot(int ind)
        {
            listBoxWindows.Items.Clear();
            if (ind >= 0 && ind < mLayoutSnapshots.Count)
            {
                mLayoutSelected = mLayoutSnapshots[ind];
                textBoxRename.Text = mLayoutSelected.name;
                textBoxRename.Enabled = true;
                buttonDeleteProfile.Enabled = true;
                buttonSnapshot.Enabled = true;
                FillListOfWindowsFromData();
            }
            else
            {
                mLayoutSelected = null;
                textBoxRename.Text = "";
                textBoxRename.Enabled = false;
                buttonDeleteProfile.Enabled = false;
                buttonSnapshot.Enabled = false;
            }
            panelLayout.Refresh();
        }

        private void UpdateFromLoadedData()
        {
            comboBoxProfiles.Items.Clear();
            foreach (LayoutSnapshot snapshot in mLayoutSnapshots)
            {
                comboBoxProfiles.Items.Add(snapshot.name);
            }
        }

        private void comboBoxProfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectLayoutSnapshot(comboBoxProfiles.SelectedIndex);
        }

        private void FormEditProfiles_SizeChanged(object sender, EventArgs e)
        {
            CalculateDrawingScale();
            panelLayout.Refresh();
        }

        private void listBoxWindows_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelInfo.Text = "";
            if (mLayoutSelected != null)
            {
                if (listBoxWindows.SelectedIndex >= 0 && listBoxWindows.SelectedIndex < mLayoutSelected.windowSnapshots.Count)
                {
                    WindowSnapshot wss = mLayoutSelected.windowSnapshots[listBoxWindows.SelectedIndex];
                    labelInfo.Text = wss.Bounds + "\n" + wss.Executable;
                }
            }
            panelLayout.Refresh();
        }
        private void textBoxRename_TextChanged(object sender, EventArgs e)
        {
            if (textBoxRename.Text.Contains(","))
            {
                textBoxRename.Text = textBoxRename.Text.Replace(",", "");
            }

            if (mLayoutSelected != null)
            {
                if (textBoxRename.Text != mLayoutSelected.name)
                {
                    SetDirty(true);
                }
            }
        }

        private void buttonDiscardChanges_Click(object sender, EventArgs e)
        {
            Reset();
        }
    }
}
