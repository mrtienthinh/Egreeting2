using Egreeting.Models.AppContext;
using Egreeting.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace EgreetingServices
{
    public partial class Service1 : ServiceBase
    {
        private System.Diagnostics.EventLog eventLogServices;
        private System.Timers.Timer scheduleServices;


        public Service1()
        {
            InitializeComponent();

            // <strong>Ghi lại hoạt động của Services bằng EventLog</strong>
            eventLogServices = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("DemoSource"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "DemoSource", "DemoLog");
            }
            eventLogServices.Source = "DemoSource";
            eventLogServices.Log = "DemoLog";

            // <strong>Xử lý nghiệp vụ mang tính tuần hoàn bằng cách sử dụng 1 Timer.</strong>
            scheduleServices = new System.Timers.Timer();
            scheduleServices.Interval = 60000;
            scheduleServices.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
            scheduleServices.Start();
        }

        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            eventLogServices.WriteEntry("Ghi log nghiệp vụ");
            // payment handle;
            using (var context = new EgreetingContext())
            {
                var currentDate = DateTime.Now;
                var nextmonthDate = DateTime.Now.AddMonths(1);
                var listUserID = context.Set<Subcriber>().Where(x => x.Draft != true).Select(x => x.EgreetingUser.EgreetingUserID).ToList();
                var listUserPaymentThisMonth = context.Set<Payment>()
                                        .Where(x => x.Draft != true
                                            && x.Month == currentDate.Month
                                            && x.Year == currentDate.Year)
                                        .Select(x => x.EgreetingUser.EgreetingUserID).ToList();
                var listUserPaymentNextMonth = context.Set<Payment>()
                                        .Where(x => x.Draft != true
                                            && x.Month == nextmonthDate.Month
                                            && x.Year == nextmonthDate.Year)
                                        .Select(x => x.EgreetingUser.EgreetingUserID).ToList();
                var listUserIDThisMonth = listUserID.Where(x => !listUserPaymentThisMonth.Contains(x)).ToList();
                var listUserIDNextMonth = listUserID.Where(x => !listUserPaymentNextMonth.Contains(x)).ToList();

                var payments = new List<Payment>();
                foreach (var itemID in listUserIDThisMonth)
                {
                    var payment = new Payment
                    {
                        Month = currentDate.Month,
                        Year = currentDate.Year,
                        PaymentStatus = false,
                        EgreetingUser = context.Set<EgreetingUser>().Find(itemID),
                        CreatedDate = DateTime.Now,
                    };
                    payments.Add(payment);
                }
                foreach (var itemID in listUserIDNextMonth)
                {
                    var payment = new Payment
                    {
                        Month = nextmonthDate.Month,
                        Year = nextmonthDate.Year,
                        PaymentStatus = false,
                        EgreetingUser = context.Set<EgreetingUser>().Find(itemID),
                        CreatedDate = DateTime.Now,
                    };
                    payments.Add(payment);
                }
                context.Set<Payment>().AddRange(payments);
                context.SaveChanges();
            }

            // sendmail handle;
            using (var context = new EgreetingContext())
            {
                var listOrderUpdate = context.Orders.Where(x => x.Draft != true && x.SendStatus == false && x.ScheduleTime <= DateTime.Now).ToList();
                foreach (var item in listOrderUpdate)
                {
                    item.SendStatus = true;
                    item.ModifiedDate = DateTime.Now;
                    foreach (var detail in item.OrderDetails)
                    {
                        detail.SendStatus = true;
                        detail.SendTime = DateTime.Now;
                        detail.ModifiedDate = DateTime.Now;
                    }
                    context.Orders.Attach(item);
                    context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }
            }
        }

        protected override void OnStart(string[] args)
        {
            scheduleServices.Stop();
            eventLogServices.WriteEntry("Started");
            scheduleServices.Start();
        }

        protected override void OnStop()
        {
            scheduleServices.Stop();
            eventLogServices.WriteEntry("Stopped");
        }


        protected override void OnPause()
        {
            scheduleServices.Stop();
            eventLogServices.WriteEntry("Paused");
        }
        protected override void OnContinue()
        {
            scheduleServices.Start(); ;
            eventLogServices.WriteEntry("Continuing");
        }

        protected override void OnShutdown()
        {
            scheduleServices.Stop();
            eventLogServices.WriteEntry("ShutDowned");
        }
    }
}