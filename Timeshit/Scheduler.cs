using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Timeshit
{
    public class Scheduler
    {
        private static Scheduler instance;

        private Timer timer;

        public static Scheduler Instance
        {
            get { return instance ?? (instance = new Scheduler()); }
        }

        private Scheduler()
        {
        }

        public void StopTimer()
        {
            if (this.timer != null)
                this.timer.Stop();
        }

        public void InitTimer(int interval)
        {
            this.timer = new Timer {Interval = 1000*60*interval};
            this.timer.Tick += TimerOnTick;
        }

        public void StartTimer()
        {
            if (this.timer != null)
                this.timer.Start();
        }

        private void TimerOnTick(object sender, EventArgs eventArgs)
        {
            StopTimer();
            using (InputForm form = new InputForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Storage storage = new Storage();
                    storage.AddRecord(DateTimeOffset.Now, form.UserInput);
                }
            }
            StartTimer();
        }
    }
}
