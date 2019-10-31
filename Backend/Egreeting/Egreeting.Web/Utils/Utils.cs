using Egreeting.Business.Business;
using Egreeting.Domain;
using Egreeting.Models.AppContext;
using Egreeting.Models.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;

namespace Egreeting.Web.Utils
{
    public static class Utils
    {
        public static bool CheckExtentionFile(this string extention, int ecardType)
        {
            switch (ecardType)
            {
                case (int)EcardType.Picture:
                    return AcceptExtensionFile.ListAcceptPicture.Contains(extention.ToUpper());
                case (int)EcardType.Video:
                    return AcceptExtensionFile.ListAcceptVideo.Contains(extention.ToUpper());
                case (int)EcardType.GIF:
                    return AcceptExtensionFile.ListAcceptGIF.Contains(extention.ToUpper());
                default:
                    return false;
            }
        }

        public static bool CheckExtentionFile(this string extention)
        {
            return AcceptExtensionFile.ListAcceptPicture.Contains(extention.ToUpper());
        }

        public static int SendMailByOrder(int? ItemID)
        {
            using (var context = new EgreetingContext())
            {
                var order = context.Set<Order>().Find(ItemID);

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                foreach (var item in order.OrderDetails.Where(x => x.Draft != true && !x.SendStatus))
                {
                    Attachment data = new Attachment(HostingEnvironment.ApplicationPhysicalPath + "Uploads/EcardFiles/" + item.Ecard.EcardUrl);
                    mail.Attachments.Add(data);
                }

                mail.From = new MailAddress(WebConfigurationManager.AppSettings["EmailSender"]);
                mail.To.Add(order.RecipientEmail);
                mail.Subject = order.SendSubject + " - " + order.SenderName;
                mail.Body = order.SendMessage;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(WebConfigurationManager.AppSettings["EmailSender"], WebConfigurationManager.AppSettings["PasswordEmail"]);
                SmtpServer.EnableSsl = true;

                try
                {
                    SmtpServer.Send(mail);
                    order.SendStatus = true;
                    foreach (var item in order.OrderDetails.Where(x => x.Draft != true && !x.SendStatus))
                    {
                        item.SendStatus = true;
                        item.SendTime = DateTime.Now;
                    }
                    context.Set<Order>().Attach(order);
                    context.Entry(order).State = EntityState.Modified;
                    context.SaveChanges();
                }
                catch (Exception)
                {

                }
            }
            return 1;
        }

        public static int SendMailByOrderDetail(int? ItemID)
        {
            int id = 0;
            using (var context = new EgreetingContext())
            {
                var orderDetail = context.OrderDetails.Find(ItemID);
                id = orderDetail.OrderDetailID;

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                
                Attachment data = new Attachment(HostingEnvironment.ApplicationPhysicalPath + "Uploads/EcardFiles/" + orderDetail.Ecard.EcardUrl);
                mail.Attachments.Add(data);
                

                mail.From = new MailAddress(WebConfigurationManager.AppSettings["EmailSender"]);
                mail.To.Add(orderDetail.Order.RecipientEmail);
                mail.Subject = orderDetail.Order.SendSubject + " - " + orderDetail.Order.SenderName;
                mail.Body = orderDetail.Order.SendMessage;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(WebConfigurationManager.AppSettings["EmailSender"], WebConfigurationManager.AppSettings["PasswordEmail"]);
                SmtpServer.EnableSsl = true;

                try
                {
                    SmtpServer.Send(mail);
                    orderDetail.SendStatus = true;
                    orderDetail.SendTime = DateTime.Now;
                    context.OrderDetails.Attach(orderDetail);
                    context.Entry(orderDetail).State = EntityState.Modified;
                    context.SaveChanges();
                    var order = context.Set<Order>().Find(orderDetail.Order.OrderID);
                    if(!order.OrderDetails.Any(x => !x.SendStatus))
                    {
                        order.SendStatus = true;
                    }
                    context.Orders.Attach(order);
                    context.Entry(order).State = EntityState.Modified;
                    context.SaveChanges();
                }
                catch (Exception)
                {

                }
            }
            using (var db  = new EgreetingContext())
            {

                var abc = db.OrderDetails.Find(id);
            }
            return 1;

        }

        public async static Task<int> SendMailAll(List<int> ItemID)
        {
            foreach (var item in ItemID)
            {
                SendMailByOrder(item);
            }
            return 1;
            
        }
    }
}