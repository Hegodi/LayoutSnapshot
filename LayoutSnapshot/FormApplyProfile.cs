﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LayoutSnapshot
{
    public partial class FormApplyProfile : Form
    {
        List<LayoutSnapshot> mProfiles;
        FormEditProfiles mFormEditProfiles = null;
        List<WindowSnapshot> mListWindowsToOpen = null; 

        bool mOpeningWindows = false;
        Thread mWorkerThread = null;
        public FormApplyProfile()
        {
            InitializeComponent();
            ReloadData();
        }

        private void ReloadData()
        {
            mProfiles = LayoutSnapshot.LoadData();
            comboBoxProfiles.Items.Clear(); 
            foreach (LayoutSnapshot profile in mProfiles)
            {
                comboBoxProfiles.Items.Add(profile.name);
            }
            if (comboBoxProfiles.Items.Count > 0)
            {
                comboBoxProfiles.SelectedIndex = 0;
            }
            labelLog.Text = "";
        }

        private void Reset()
        {
            buttonApply.Text = "Apply";
            mOpeningWindows = false;
            buttonEditProfiles.Enabled = true;
            comboBoxProfiles.Enabled = true;
            progressBar.Visible = false;
        }

        private void buttonEditProfiles_Click(object sender, EventArgs e)
        {
            if (mFormEditProfiles == null)
            {
                mFormEditProfiles = new FormEditProfiles();
                mFormEditProfiles.FormClosed += OnEditWindowClosed;
            }
            mFormEditProfiles.Show();
            mFormEditProfiles.BringToFront();
            this.Enabled = false;

        }

        void OnEditWindowClosed(object sender, EventArgs args)
        {
            mFormEditProfiles = null;
            ReloadData();
            this.Enabled = true;
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            if (mOpeningWindows)
            {
                Reset();
                mWorkerThread.Abort();
                mWorkerThread = null;
                labelLog.Text += "Aborted\n";

            }
            else
            {
                if (comboBoxProfiles.SelectedIndex < 0 || comboBoxProfiles.SelectedIndex >= mProfiles.Count)
                {
                    return;
                }

                labelLog.Text = "Applying profile '" + mProfiles[comboBoxProfiles.SelectedIndex].name + "' ...\n";

                if (checkBoxLaunchIfNotOpen.Checked)
                {
                    buttonEditProfiles.Enabled = false;
                    comboBoxProfiles.Enabled = false;
                    mListWindowsToOpen = new List<WindowSnapshot>(mProfiles[comboBoxProfiles.SelectedIndex].windowSnapshots);
                    Process[] processlist = Process.GetProcesses();
                    foreach (Process process in processlist)
                    {
                        for (int i = 0; i < mListWindowsToOpen.Count; i++)
                        {
                            if (mListWindowsToOpen[i].ProcessName == process.ProcessName)
                            {
                                mListWindowsToOpen.RemoveAt(i);
                                break;
                            }
                        }
                    }

                    labelLog.Text += "Openning missing windows ("  + mListWindowsToOpen.Count + " windows to open)\n";
                    mWorkerThread = new Thread(WorkerThread);
                    mWorkerThread.Start();
                    progressBar.Maximum = mProfiles[comboBoxProfiles.SelectedIndex].windowSnapshots.Count;
                    progressBar.Minimum = 0;
                    progressBar.Visible = true;
                    buttonApply.Text = "Abort";
                    mOpeningWindows = true;
                    timerCheckWindowsOpened.Start();
                }
                else
                {
                    mProfiles[comboBoxProfiles.SelectedIndex].Apply();
                    labelLog.Text += "Done.\n";
                }
            }

        }


        void WorkerThread()
        {

            foreach (WindowSnapshot windowSnapshot in mListWindowsToOpen)
            {
                System.Diagnostics.Process.Start(windowSnapshot.Executable);
                Thread.Sleep(200);
            }
        }

        private void timerCheckWindowsOpened_Tick(object sender, EventArgs e)
        {
            List<string> windowsExpected = new List<string>();
            foreach (WindowSnapshot windowSnapshot in mProfiles[comboBoxProfiles.SelectedIndex].windowSnapshots)
            {
                windowsExpected.Add(windowSnapshot.ProcessName);
            }
            int numWindowsExpected = windowsExpected.Count;

            Process[] processlist = Process.GetProcesses();
            foreach (Process process in processlist)
            {
                if (windowsExpected.Contains(process.ProcessName))
                {
                    windowsExpected.Remove(process.ProcessName);
                }
            }

            int numWindowsLeft = windowsExpected.Count;
            progressBar.Value = numWindowsExpected - numWindowsLeft;
            if (numWindowsLeft == 0)
            {
                timerCheckWindowsOpened.Stop();
                mProfiles[comboBoxProfiles.SelectedIndex].Apply();
                labelLog.Text += "Done.\n";
                Reset();
            }

        }
    }
}
