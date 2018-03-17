using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Timeshit
{
    class ShitApplicationContext : System.Windows.Forms.ApplicationContext
    {
        private NotifyIcon trayIcon;
        private ContextMenuStrip trayIconContextMenu;
        private ToolStripMenuItem closeMenuItem;

        public ShitApplicationContext()
        {
            Application.ApplicationExit += OnApplicationExit;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            trayIcon = new NotifyIcon();
            trayIcon.BalloonTipIcon = ToolTipIcon.Info;
            trayIcon.BalloonTipText = "Timeshit is running...";
            trayIcon.BalloonTipTitle = "...";
            trayIcon.Text = "Timeshit logger.";
            trayIcon.Icon = Resources.Icon1;
            trayIcon.Visible = true;

            trayIcon.DoubleClick += trayIcon_DoubleClick;

            trayIconContextMenu = new ContextMenuStrip();
            closeMenuItem = new ToolStripMenuItem();
            trayIconContextMenu.SuspendLayout();

            trayIconContextMenu.Items.Add(this.closeMenuItem);
            trayIconContextMenu.Name = "TrayIconContextMenu";
            trayIconContextMenu.Size = new Size(153, 70);

            closeMenuItem.Name = "CloseMenuItem";
            closeMenuItem.Size = new Size(152, 22);
            closeMenuItem.Text = "Exit";
            closeMenuItem.Click += CloseMenuItemOnClick;

            trayIconContextMenu.ResumeLayout(false);
            trayIcon.ContextMenuStrip = trayIconContextMenu;

            Scheduler.Instance.InitTimer(Settings.Instance.Interval);
            Scheduler.Instance.StartTimer();
        }

        private void CloseMenuItemOnClick(object sender, EventArgs eventArgs)
        {
            if (MessageBox.Show("Do you really want to exit?",
                    "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        void trayIcon_DoubleClick(object sender, EventArgs e)
        {
            Scheduler.Instance.StopTimer();

            MainForm settings = new MainForm();
            if (settings.ShowDialog() == DialogResult.OK)
            {
                Settings.Instance.Interval = settings.Interval;
                Settings.Instance.LogFile = settings.LogFile;
                Settings.Instance.Update();
            }

            Scheduler.Instance.InitTimer(Settings.Instance.Interval);
            Scheduler.Instance.StartTimer();
        }

        private void OnApplicationExit(object sender, EventArgs eventArgs)
        {
            
        }
    }
}
