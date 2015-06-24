using System;
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
using LightInject;
using Quartz;
using Quartz.Impl;

namespace Chrono
{
    public partial class frmMain : Form, ITraceWriter
    {
        public frmMain()
        {
            InitializeComponent();
        }


        private void frmMain_Load(object sender, EventArgs e)
        {
            scheduler = StdSchedulerFactory.GetDefaultScheduler();
            var customTraceListener = new CustomTraceListener();
            customTraceListener.TraceWriter = this;
            GlobalContainer.Instance.ServiceContainer.Register<ITraceWriter, frmMain>(new PerContainerLifetime());
            GlobalContainer.Instance.ServiceContainer.Initialize(reg => reg.ServiceType == typeof(ITraceWriter), (factory, instance) => instance = this);
            Trace.Listeners.Add(customTraceListener);
        }

        private IScheduler scheduler;

        private void btnTest_Click(object sender, EventArgs e)
        {
            // and start it off
            scheduler.Start();

            // define the job and tie it to our HelloJob class
            var job1 = JobBuilder.Create<HelloJob1>()
                .WithIdentity("job1", "group1")
                .RequestRecovery(true)
                .Build();
            var job2 = JobBuilder.Create<HelloJob2>()
                .RequestRecovery(true)
                .WithIdentity("job2", "group1")
                .Build();
            var job3 = JobBuilder.Create<HelloJob3>()
                .RequestRecovery(true)
                .WithIdentity("job3", "group1")
                .Build();

            // Trigger the job to run now, and then repeat every 10 seconds
            var trigger1 = TriggerBuilder.Create()
                .StartNow()
                .WithIdentity("trigger1", "group1")
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(10)
                    .RepeatForever())
                .Build();
            var trigger2 = TriggerBuilder.Create()
                .StartNow()
                .WithIdentity("trigger2", "group1")
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(10)
                    .RepeatForever())
                .Build();
            var trigger3 = TriggerBuilder.Create()
                .StartNow()
                .WithIdentity("trigger3", "group1")
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(10)
                    .RepeatForever())
                .Build();

            // Tell quartz to schedule the job using our trigger
            try
            {
                scheduler.ScheduleJob(job1, trigger1);
            }
            catch (Exception ex)
            {
                WriteTrace(ex.Message);
            }

            try
            {
                scheduler.ScheduleJob(job2, trigger2);
            }
            catch (Exception ex)
            {
                WriteTrace(ex.Message);
            }

            try
            {
                scheduler.ScheduleJob(job3, trigger3);
            }
            catch (Exception ex)
            {
                WriteTrace(ex.Message);
            }

            // some sleep to show what's happening
            //Thread.Sleep(TimeSpan.FromSeconds(60));

            // and last shut down the scheduler when you are ready to close your program
            //scheduler.Shutdown();
        }

        public void WriteTrace(string trace)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => tbTraceLog.AppendText(string.Format("\r\n {0}", trace))));
            }
            else
            {
                tbTraceLog.AppendText(string.Format("\r\n {0}", trace));
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (scheduler != null)
                scheduler.Shutdown();
        }
    }

    public interface ITraceWriter
    {
        void WriteTrace(string trace);
    }

    public class StandardTraceWriter : ITraceWriter
    {
        public void WriteTrace(string trace)
        {
            Trace.WriteLine(trace);
        }
    }

    public class HelloJob1: IJob
    {
        public HelloJob1()
        {
            traceWriter = new StandardTraceWriter();
        }

        private ITraceWriter traceWriter;

        public void Execute(IJobExecutionContext context)
        {
                traceWriter.WriteTrace(string.Format("Job1 Executed At: {0}", DateTime.Now.ToString("u")));
        }
    }

    public class HelloJob2 : IJob
    {
        public HelloJob2()
        {
            traceWriter = new StandardTraceWriter();
        }

        private ITraceWriter traceWriter;

        public void Execute(IJobExecutionContext context)
        {
            traceWriter.WriteTrace(string.Format("Job2 Executed At: {0}", DateTime.Now.ToString("u")));
        }
    }

    public class HelloJob3 : IJob
    {
        public HelloJob3()
        {
            traceWriter = new StandardTraceWriter();
        }

        private ITraceWriter traceWriter;

        public void Execute(IJobExecutionContext context)
        {
            traceWriter.WriteTrace(string.Format("Job3 Executed At: {0}", DateTime.Now.ToString("u")));
        }
    }
}
