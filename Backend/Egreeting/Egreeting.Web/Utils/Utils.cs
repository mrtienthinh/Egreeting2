using Egreeting.Domain;
using Egreeting.Models.AppContext;
using Egreeting.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
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

        public static void SendMailByOrder(int? ItemID)
        {
            using (var context = new EgreetingContext())
            {
                var order = context.Set<Order>().Find(ItemID);

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                foreach (var item in order.OrderDetails)
                {
                    Attachment data = new Attachment(HostingEnvironment.ApplicationPhysicalPath + "Uploads/EcardFiles/" + item.Ecard.EcardUrl);
                    mail.Attachments.Add(data);
                }

                mail.From = new MailAddress("lethanh.hlht1993@gmail.com");
                mail.To.Add(order.RecipientEmail);
                mail.Subject = order.SendSubject + " - " + order.SenderName;
                mail.Body = order.SendMessage;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("lethanh.hlht1993@gmail.com", "*******");
                SmtpServer.EnableSsl = true;

                try
                {
                    //SmtpServer.Send(mail);
                    order.SendStatus = true;
                    foreach (var item in order.OrderDetails)
                    {
                        item.SendStatus = true;
                        item.SendTime = DateTime.Now;
                    }
                    context.Set<Order>().Attach(order);
                    context.SaveChanges();
                }
                catch (Exception)
                {

                }
            }



        }
        public static void SendMailByOrderDetail(int? ItemID)
        {
            using (var context = new EgreetingContext())
            {
                var orderDetail = context.Set<OrderDetail>().Find(ItemID);

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                
                Attachment data = new Attachment(HostingEnvironment.ApplicationPhysicalPath + "Uploads/EcardFiles/" + orderDetail.Ecard.EcardUrl);
                mail.Attachments.Add(data);
                

                mail.From = new MailAddress("lethanh.hlht1993@gmail.com");
                mail.To.Add(orderDetail.Order.RecipientEmail);
                mail.Subject = orderDetail.Order.SendSubject + " - " + orderDetail.Order.SenderName;
                mail.Body = orderDetail.Order.SendMessage;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("lethanh.hlht1993@gmail.com", "*******");
                SmtpServer.EnableSsl = true;

                try
                {
                    //SmtpServer.Send(mail);
                    orderDetail.SendStatus = true;
                    orderDetail.SendTime = DateTime.Now;
                    context.Set<OrderDetail>().Attach(orderDetail);
                    context.SaveChanges();
                }
                catch (Exception)
                {

                }
            }
        }

        public async static void SendMailAll(List<int> ItemID)
        {
            foreach (var item in ItemID)
            {
                SendMailByOrder(item);
            }
            
        }
    }
}