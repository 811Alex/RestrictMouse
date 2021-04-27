using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using RestrictMouse.Properties;
using static RestrictMouse.WinIF;

namespace RestrictMouse
{
    public partial class MainForm : Form
    {
        SystemEvent.WinFocusChange winFocusChange = new SystemEvent.WinFocusChange();

        public MainForm()
        {
            InitializeComponent();
        }

        private void RefreshBtn_Click(object sender, EventArgs e)
        {
            refreshProc();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (Settings.Default.mode == "window")
                RadioWindow.Checked = true;
            else
                RadioProcess.Checked = true;
            refreshProc();
            winFocusChange.SystemEventHandler += (hWinEventHook, eventType, hwnd, idObject, idChild, dwEventThread, dwmsEventTime) => onWinFocusChange(hwnd);
        }

        private void onWinFocusChange(IntPtr eventProcHandle)
        {
            if (ContainsFocus)
                clip(false);
            else
            {
                if (ProcessList.Text.Length > 0 && ((RadioWindow.Checked && getProcessByHandle(eventProcHandle).MainWindowTitle == ProcessList.Text) || (RadioProcess.Checked && getProcessByHandle(eventProcHandle).ProcessName == ProcessList.Text)))
                    clip(true);
                else if (ProcessList.Text != Settings.Default.target && ((RadioWindow.Checked && getProcessByHandle(eventProcHandle).MainWindowTitle == Settings.Default.target) || (RadioProcess.Checked && getProcessByHandle(eventProcHandle).ProcessName == Settings.Default.target)))
                {
                    refreshProc();
                    clip(true);
                }
                else
                    clip(false);
            }
        }

        private void clip(bool enable)
        {
            Cursor.Clip = enable ? getTargetRect() : new Rectangle();
        }

        private Rectangle RECT2Rectangle(RECT rect) // also converts left/top/right/bottom --> X/Y/width/height
        {
            return new Rectangle(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top);
        }

        private Rectangle getTargetRect()
        {
            int err;
            RECT targetRect = new RECT();
            IntPtr target = RadioWindow.Checked ? FindWindow(ProcessList.Text) : Process.GetProcessesByName(ProcessList.Text).First().MainWindowHandle;
            if (target != IntPtr.Zero)
            {
                if ((err = getWinBounds(target, out targetRect)) != 0)
                    MessageBox.Show("Error: can't get target window dimensions (" + err + ")", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Error: can't find selected window", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return RECT2Rectangle(targetRect);
        }

        private void refreshProc()
        {
            Cursor.Current = Cursors.WaitCursor;
            controlsEnabled(false);
            Process[] proc = Process.GetProcesses();
            Process[] titledProc = proc.Where(p => !String.IsNullOrEmpty(p.MainWindowTitle)).ToArray();
            String[] titles;
            if (RadioWindow.Checked)
                titles = titledProc.Select(p => p.MainWindowTitle).ToArray();
            else
                titles = titledProc.Select(p => p.ProcessName).ToArray();
            ProcessList.Items.Clear();
            ProcessList.Items.AddRange(titles);
            if (ProcessList.Items.Contains(Settings.Default.target))
                ProcessList.Text = Settings.Default.target;
            controlsEnabled(true);
            Cursor.Current = Cursors.Default;
        }

        private void controlsEnabled(bool enabled)
        {
            ProcessList.Enabled = enabled;
            RefreshBtn.Enabled = enabled;
            RadioWindow.Enabled = enabled;
            RadioProcess.Enabled = enabled;
        }

        private void RadioWindow_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioWindow.Checked) refreshProc();
        }

        private void RadioProcess_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioProcess.Checked) refreshProc();
        }

        private void ProcessList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Default.target = ProcessList.Text;
            Settings.Default.mode = RadioWindow.Checked ? "window" : "process";
            Settings.Default.Save();
        }
    }
}
